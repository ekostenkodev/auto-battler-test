using Zenject;

namespace Scorewarrior.Test.Models
{
    public class CharacterAimingState : State
    {
        private readonly CharacterProvider _self;
        private readonly CharacterProvider _target;
        
        private IGameTime _gameTime;
        private float _aimTime;

        [Inject]
        private void Construct(IGameTime gameTime)
        {
            _gameTime = gameTime;
        }

        public CharacterAimingState(CharacterProvider self, CharacterProvider target)
        {
            _self = self;
            _target = target;
        }

        public override void OnEnter()
        {
            _aimTime = _self.Stats.GetStats().AimTime;
            
            _self.Animator.SetAiming(true);
            _self.Animator.SetReloading(false);
            _self.View.Root.LookAt(_target.View.Root.position);
        }

        public override void OnUpdate()
        {
            if (_target == null || false == _target.Health.IsAlive)
            {
                _self.StateMachine.ChangeTo(new CharacterIdleState(_self));
                
                return;
            }
            
            _aimTime -= _gameTime.DeltaTime;
            
            if (_aimTime <= 0)
            {
                _self.StateMachine.ChangeTo(new CharacterShootingState(_self, _target));
            }
        }
    }
}