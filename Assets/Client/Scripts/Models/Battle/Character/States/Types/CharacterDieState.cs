namespace Scorewarrior.Test.Models
{
    public class CharacterDieState : State
    {
        private readonly CharacterProvider _character;

        public CharacterDieState(CharacterProvider character)
        {
            _character = character;
        }

        public override void OnEnter()
        {
            _character.Animator.SetDied();
        }
    }
}