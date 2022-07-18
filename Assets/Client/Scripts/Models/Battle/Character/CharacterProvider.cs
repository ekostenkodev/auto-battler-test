using Scorewarrior.Test.Data;
using Scorewarrior.Test.Models.Animation;
using Scorewarrior.Test.Views;
using UnityEngine;

namespace Scorewarrior.Test.Models
{
    public class CharacterProvider
    {
        public HealthComponent Health { get; }
        public IStatsProvider<CharacterStats> Stats { get; }
        public IStateMachine StateMachine { get; }
        public CharacterView View { get; }
        public CharacterAnimator Animator { get; }
        public WeaponProvider Weapon { get; private set; }

        public CharacterProvider(
            HealthComponent health,
            IStatsProvider<CharacterStats> stats,
            CharacterView view,
            CharacterAnimator animator,
            IStateMachine stateMachine
        )
        {
            Health = health;
            Stats = stats;
            StateMachine = stateMachine;
            View = view;
            Animator = animator;
        }

        public void SetWeapon(WeaponProvider weapon)
        {
            Weapon = weapon;

            weapon.View.Root.SetParent(View.RightPalm);
            weapon.View.Root.localPosition = Vector3.zero;
            weapon.View.Root.localRotation = Quaternion.identity;
        }
    }
}