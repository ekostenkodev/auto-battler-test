using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Scorewarrior.Test.Models
{
    public class CharacterIdleState : State
    {
        private readonly CharacterProvider _self;

        private ICharacterLifecycle _characterLifecycle;

        public CharacterIdleState(CharacterProvider self)
        {
            _self = self;
        }

        [Inject]
        private void Construct(IGameTime gameTime, ICharacterLifecycle lifecycle)
        {
            _characterLifecycle = lifecycle;
        }

        public override void OnEnter()
        {
            _self.Animator.SetAiming(false);
            _self.Animator.SetReloading(false);
        }

        public override void OnUpdate()
        {
            if (TryGetNearestAliveEnemy(out CharacterProvider target))
            {
                _self.StateMachine.ChangeTo(new CharacterAimingState(_self, target));
            }
        }

        private bool TryGetNearestAliveEnemy(out CharacterProvider target)
        {
            IEnumerable<CharacterProvider> enemies = GetAllEnemies();
            float nearestDistance = float.MaxValue;

            target = null;

            foreach (CharacterProvider enemy in enemies)
            {
                if (enemy.Health.IsAlive)
                {
                    float distance = Vector3.SqrMagnitude(_self.View.Root.position - enemy.View.Root.position);
                    if (distance < nearestDistance)
                    {
                        nearestDistance = distance;
                        target = enemy;
                    }
                }
            }

            return target != null;
        }

        private IEnumerable<CharacterProvider> GetAllEnemies()
        {
            IReadOnlyDictionary<uint, List<CharacterProvider>> characters = _characterLifecycle.GetTeams();

            if (TryGetTeam(_self, out uint team))
            {
                return characters
                    .Where(x => x.Key != team)
                    .SelectMany(x => x.Value)
                    .ToList();
            }
            
            return Array.Empty<CharacterProvider>();
        }

        private bool TryGetTeam(CharacterProvider target, out uint team)
        {
            foreach (KeyValuePair<uint, List<CharacterProvider>> charactersPair in _characterLifecycle.GetTeams())
            {
                List<CharacterProvider> characters = charactersPair.Value;
                foreach (CharacterProvider character in characters)
                {
                    if (character == target)
                    {
                        team = charactersPair.Key;
                        return true;
                    }
                }
            }

            team = default;
            return false;
        }
    }
}