using System;
using Scorewarrior.Test.Data;
using Scorewarrior.Test.Models.Animation;
using Scorewarrior.Test.Views;
using Zenject;
using Object = UnityEngine.Object;

namespace Scorewarrior.Test.Models
{
    public class CharacterFactory : ICharacterFactory
    {
        private DiContainer _container;
        private IWeaponFactory _weaponFactory;
        private ModificationFactory<CharacterStats> _modificationFactory;
        
        public event Action<CharacterProvider, uint> Created;

        [Inject]
        private void Construct(
            DiContainer container, 
            IWeaponFactory weaponFactory, 
            ModificationFactory<CharacterStats> modificationFactory)
        {
            _container = container;
            _weaponFactory = weaponFactory;
            _modificationFactory = modificationFactory;
        }

        public CharacterProvider Create(CharacterConfiguration configuration, uint team)
        {
            IStatsProvider<CharacterStats> stats = _modificationFactory.GedModifiedStats(configuration.Stats);
            IStateMachine stateMachine = new StateMachine(_container);
            WeaponProvider weapon = _weaponFactory.Create(configuration.Weapon);
            CharacterView view = Object.Instantiate(configuration.View);
            CharacterAnimator animator = new CharacterAnimator(view.Animator);
            HealthComponent health = new HealthComponent(stats.GetStats().MaxHealth, stats.GetStats().MaxArmor);

            CharacterProvider characterProvider = new CharacterProvider(health, stats, view, animator, stateMachine);

            characterProvider.SetWeapon(weapon);
            stateMachine.ChangeTo(new CharacterIdleState(characterProvider));

            Created?.Invoke(characterProvider, team);

            return characterProvider;
        }
    }
}