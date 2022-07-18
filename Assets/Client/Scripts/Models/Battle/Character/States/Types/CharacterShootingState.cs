using Zenject;

namespace Scorewarrior.Test.Models
{
    public class CharacterShootingState : State
    {
        private readonly CharacterProvider _self;
        private readonly CharacterProvider _target;

        private IBulletFactory _bulletFactory;

        public CharacterShootingState(CharacterProvider self, CharacterProvider target)
        {
            _self = self;
            _target = target;
        }
        
        [Inject]
        private void Construct(IBulletFactory bulletFactory)
        {
            _bulletFactory = bulletFactory;
        }

        public override void OnEnter()
        {
            _self.Animator.SetAiming(true);
            _self.Animator.SetReloading(false);
        }

        public override void OnUpdate()
        {
            if (false == _self.Weapon.Model.HasAmmo)
            {
                _self.StateMachine.ChangeTo(new CharacterReloadingState(_self, _target));
                
                return;
            }

            if (_target == null || false == _target.Health.IsAlive)
            {
                _self.StateMachine.ChangeTo(new CharacterIdleState(_self));
                
                return;
            }
            
            if (_self.Weapon.Model.TryShoot())
            {
                _bulletFactory.Create(_self, _target);
                _self.Animator.Shoot();
            }
        }
    }
}