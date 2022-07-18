using UnityEngine;

namespace Scorewarrior.Test.Models.Animation
{
    public class CharacterAnimator
    {
        private static readonly int AimingKey = Animator.StringToHash("aiming");
        private static readonly int ReloadingKey = Animator.StringToHash("reloading");
        private static readonly int ShootKey = Animator.StringToHash("shoot");
        private static readonly int DieKey = Animator.StringToHash("die");
        private static readonly int ReloadTimeKey = Animator.StringToHash("reload_time");
        
        private readonly Animator _animator;

        public CharacterAnimator(Animator animator)
        {
            _animator = animator;
        }

        public void SetAiming(bool active)
        {
            _animator.SetBool(AimingKey, active);
        }

        public void SetReloading(bool active, float reloadTime = 0)
        {
            _animator.SetBool(ReloadingKey, active);
            _animator.SetFloat(ReloadTimeKey, reloadTime);
        }

        public void Shoot()
        {
            _animator.SetTrigger(ShootKey);
        }

        public void SetDied()
        {
            _animator.SetTrigger(DieKey);
        }
    }
}