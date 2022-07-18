using Zenject;

namespace Scorewarrior.Test.Models
{
    public class CharacterReloadingState : State
    {
        private readonly CharacterProvider _self;
        private readonly CharacterProvider _target;

        private float _reloadLeftTime;
        private float _reloadTotalTime;
        private IGameTime _gameTime;

        public CharacterReloadingState(CharacterProvider self, CharacterProvider target)
        {
            _self = self;
            _target = target;
        }
        
        [Inject]
        private void Construct(IGameTime gameTime)
        {
            _gameTime = gameTime;
        }
        
        public override void OnEnter()
        {
            _reloadTotalTime = _reloadLeftTime = _self.Weapon.Model.Stats.GetStats().ReloadTime;
            
            _self.Animator.SetAiming(true);
        }

        public override void OnUpdate()
        {
            _reloadLeftTime -= _gameTime.DeltaTime;
            
            _self.Animator.SetReloading(true, 1 - _reloadLeftTime / _reloadTotalTime);

            if (_reloadLeftTime <= 0)
            {
                _self.Weapon.Model.Reload();
                _self.StateMachine.ChangeTo(new CharacterShootingState(_self, _target));
            }
        }
    }
}