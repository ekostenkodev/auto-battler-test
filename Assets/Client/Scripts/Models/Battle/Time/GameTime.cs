using UnityEngine;

namespace Scorewarrior.Test.Models
{
    public class GameTime : IGameTime
    {
        public float DeltaTime => Time.deltaTime;
    }
}