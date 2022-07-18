using UnityEngine;

namespace Scorewarrior.Test.Views
{
	public class CharacterView : MonoBehaviour
	{
		[field: SerializeField]
		public  Transform Root { get; private set; }
		
		[field: SerializeField]
		public Animator Animator { get; private set; }

		[field: SerializeField]
		public Transform RightPalm { get; private set; }
	}
}
