using System;

namespace Scorewarrior.Test.Models
{
    public interface ICharacterFactory
    {
        event Action<CharacterProvider, uint> Created;
        
        CharacterProvider Create(CharacterConfiguration prefab, uint team);
    }
}