using UnityEngine;

namespace Scorewarrior.Test.Views
{
	public class WeaponView : MonoBehaviour
	{
		[field:SerializeField]
		public Transform Root { get; private set; }

		[field:SerializeField]
		public Transform BarrelRoot { get; private set; }

		[field:SerializeField]
		public BulletView BulletView { get; private set; }
	}
}