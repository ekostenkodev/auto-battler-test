using System;
using System.Collections.Generic;
using System.Linq;
using Scorewarrior.Test.Views;
using Zenject;

namespace Scorewarrior.Test.Models
{
	public interface IBattlefieldLifecycle
	{
		event Action Ended;
		
		bool IsActive { get; }

		void Start();
	}
	
	public class BattlefieldLifecycle : IBattlefieldLifecycle, ITickable
	{
		private readonly IBattleSpawner _battleSpawner;
		private readonly ICharacterLifecycle _characterLifecycle;
		private readonly IWeaponLifecycle _weaponLifecycle;
		private readonly IBulletLifecycle _bulletLifecycle;

		public bool IsActive { get; private set; }
		public event Action Ended;

		[Inject]
		public BattlefieldLifecycle(
			IBattleSpawner battleSpawner, 
			ICharacterFactory characterFactory, 
			ICharacterLifecycle characterLifecycle, 
			IWeaponLifecycle weaponLifecycle, 
			IBulletLifecycle bulletLifecycle)
		{
			_battleSpawner = battleSpawner;
			_characterLifecycle = characterLifecycle;
			_weaponLifecycle = weaponLifecycle;
			_bulletLifecycle = bulletLifecycle;

			characterFactory.Created += OnCharacterCreated;
		}

		public void Start()
		{
			_battleSpawner.Spawn();

			IsActive = true;
		}

		public void Tick()
		{
			if (IsActive)
			{
				_weaponLifecycle.Update();
				_bulletLifecycle.Update();
				_characterLifecycle.Update();
			}
		}
		
		private void OnCharacterCreated(CharacterProvider character, uint team)
		{
			character.Health.Changed += CheckForBattleEnd;
		}
		
		private void CheckForBattleEnd()
		{
			IReadOnlyDictionary<uint, List<CharacterProvider>> characters = _characterLifecycle.GetTeams();
			bool isTeamDead = characters.Any(CheckForDeadTeam);
			
			if (isTeamDead)
			{
				IsActive = false;
				Ended?.Invoke();
			}
		}

		private bool CheckForDeadTeam(KeyValuePair<uint, List<CharacterProvider>> team)
		{
			return team.Value.All(character => false == character.Health.IsAlive);
		}
	}
}