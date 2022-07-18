using System;

namespace Scorewarrior.Test.Models
{
    public interface IWeaponFactory
    {
        event Action<WeaponProvider> Created;
        
        WeaponProvider Create(WeaponConfiguration view);
    }
}