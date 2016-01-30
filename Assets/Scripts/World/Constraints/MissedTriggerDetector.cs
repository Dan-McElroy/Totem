using UnityEngine;
using System.Collections;

namespace Scripts.World.Constraints {

	[RequireComponent(typeof(Collider2D))]
	public class MissedTriggerDetector : MonoBehaviour {

		// Use this for initialization
		void Start () {
		
		}
		
		void OnTriggerEnter2D (Collider2D otherCollider)
		{
			if (otherCollider.name == "Player")
			{
				this.SendMessageUpwards("StartConstraintOpportunity");
				Debug.Log ("I am waiting");
			}
		}

		void OnTriggerExit2D (Collider2D otherCollider)
		{
			if (otherCollider.name == "Player")
			{
				this.SendMessageUpwards("ConstraintFailed");
			}
		}
	}
}
