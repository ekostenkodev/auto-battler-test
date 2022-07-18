using UnityEngine;

namespace Scorewarrior.Test
{
	public class SpawnPointView : MonoBehaviour
	{
		[field: SerializeField]
		public uint Team { get; private set; }
	}
}