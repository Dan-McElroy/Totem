using UnityEngine;
using System.Collections;

namespace Scripts.World.Constraints {

	[RequireComponent(typeof(Collider2D))]
	public class MissedTriggerDetector : MonoBehaviour {

		private bool enabled = true;
		
		void OnTriggerEnter2D (Collider2D otherCollider)
		{
			if (otherCollider.name == "Player" && enabled)
			{
				this.SendMessageUpwards("StartConstraintOpportunity");
			}
		}

		void OnTriggerExit2D (Collider2D otherCollider)
		{
			if (otherCollider.name == "Player" && enabled)
			{
				this.SendMessageUpwards("ConstraintFailed");
			}
		}

		void Activate() {
			enabled = true;
		}

		void DeActivate() {
			enabled = false;
		}
	}
}
