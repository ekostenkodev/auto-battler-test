using Scorewarrior.Test.Data;

namespace Scorewarrior.Test.Models
{
	public class WeaponModel
	{
		private uint _ammo;
		private float _cooldown;

		public WeaponModel(IStatsProvider<WeaponStats> stats)
		{
			Stats = stats;

			Reload();
		}

		public bool HasAmmo => _ammo > 0;
		public IStatsProvider<WeaponStats> Stats { get; }
		
		public void Update(float deltaTime)
		{
			_cooldown -= deltaTime;
		}

		public void Reload()
		{
			_ammo = Stats.GetStats().ClipSize;
		}

		public bool TryShoot()
		{
			if (_cooldown <= 0 && HasAmmo)
			{
				_ammo -= 1;
				_cooldown = 1.0f / Stats.GetStats().FireRate;

				return true;
			}

			return false;
		}
	}
}