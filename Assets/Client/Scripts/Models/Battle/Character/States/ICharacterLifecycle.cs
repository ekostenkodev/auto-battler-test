using System;
using System.Collections.Generic;

namespace Scorewarrior.Test.Models
{
    public interface ICharacterLifecycle
    {
        event Action<CharacterProvider> Diad;
        void Update();
        IReadOnlyDictionary<uint, List<CharacterProvider>> GetTeams();
    }
}