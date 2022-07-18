using System;
using Scorewarrior.Test.Views;

namespace Scorewarrior.Test.Models
{
    public interface IBulletFactory
    {
        event Action<BulletProvider> Created;
        
        BulletProvider Create(CharacterProvider from, CharacterProvider to);
        void Destroy(BulletProvider bullet);
    }
}