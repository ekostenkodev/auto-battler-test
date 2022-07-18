using System;
using System.Collections.Generic;
using Zenject;

namespace Scorewarrior.Test.Models
{
    public class CharacterLifecycle : ICharacterLifecycle, IDisposable
    {
        private readonly Dictionary<uint, List<CharacterProvider>> _teamToCharactersMap = new();
        private readonly List<CharacterProvider> _aliveCharacters = new();
        private readonly List<CharacterProvider> _deadCharacters = new();

        private DiContainer _container;
        private ICharacterFactory _factory;
        
        public event Action<CharacterProvider> Diad;

        [Inject]
        private void Construct(ICharacterFactory factory)
        {
            _factory = factory;
            
            _factory.Created += OnCharacterCreate;
        }

        public void Dispose()
        {
            _factory.Created -= OnCharacterCreate;
        }

        private void OnCharacterCreate(CharacterProvider character, uint team)
        {
            if (false == _teamToCharactersMap.ContainsKey(team))
            {
                _teamToCharactersMap[team] = new List<CharacterProvider>();
            }
            
            _teamToCharactersMap[team].Add(character);
            _aliveCharacters.Add(character);
            
            character.Health.Changed += CheckForDeath;
        }
        
        public void Update()
        {
            foreach (List<CharacterProvider> characters in _teamToCharactersMap.Values)
            {
                foreach (CharacterProvider character in characters)
                {
                    character.StateMachine.Update();
                }
            }
        }

        public IReadOnlyDictionary<uint, List<CharacterProvider>> GetTeams()
        {
            return _teamToCharactersMap;
        }

        private void CheckForDeath()
        {
            foreach (CharacterProvider character in _aliveCharacters)
            {
                if (false == character.Health.IsAlive)
                {
                    character.StateMachine.ChangeTo(new CharacterDieState(character));
                    _deadCharacters.Add(character);
                }
            }

            ClearDeadCharacters();
        }

        private void ClearDeadCharacters()
        {
            foreach (CharacterProvider dead in _deadCharacters)
            {
                _aliveCharacters.Remove(dead);
                dead.Health.Changed -= CheckForDeath;
                Diad?.Invoke(dead);
            }
            
            _deadCharacters.Clear();
        }
    }
}