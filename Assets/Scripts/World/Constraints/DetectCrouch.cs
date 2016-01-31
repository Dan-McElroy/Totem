using UnityEngine;
using Scripts.Player;

namespace Scripts.World.Constraints {

	[RequireComponent(typeof(Collider2D))]
	public class DetectCrouch : MonoBehaviour {

		// Use this for initialization
		void Start () {
		
		}

		void OnTriggerEnter2D (Collider2D otherCollider)
		{
			if (otherCollider.name == "Player")
			{
				this.SendMessageUpwards("ActivityDetected");
			}
		}

		public void SelfDestruct() {
			Destroy (this, .5f);
		}
	}
}