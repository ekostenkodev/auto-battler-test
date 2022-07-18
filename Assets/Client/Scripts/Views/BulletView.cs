using UnityEngine;

namespace Scorewarrior.Test.Views
{
	public class BulletView : MonoBehaviour
	{
		[field: SerializeField]
		public Transform Root { get; private set; }
		
		[field: SerializeField]
		public TrailRenderer Trail { get; private set; }
	}
}