using UnityEngine;
using Scripts.Player;

namespace Scripts.World.Constraints {

	[RequireComponent(typeof(Collider2D))]
	public class DetectCrouch : MonoBehaviour {

		new private bool enabled = true;

		void OnTriggerEnter2D (Collider2D otherCollider)
		{
			if (otherCollider.name == "Player" && enabled)
			{
				this.SendMessageUpwards("ActivityDetected");
			}
		}

		public void SelfDestruct() {
//			Destroy (this, .5f);
			enabled = false;
		}

		void Reset() {
			enabled = true;
		}
	}
}