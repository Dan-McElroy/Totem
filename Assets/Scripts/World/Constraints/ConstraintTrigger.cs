﻿using UnityEngine;
using Scripts.Player;

namespace Scripts.World.Constraints {

	[RequireComponent(typeof(Collider2D))]
	public class ConstraintTrigger : MonoBehaviour {

//		private PlatformerCharacter2D Player;
		private GameObject circle;

		[SerializeField] private Vector3 watchRadius; //how far to watch for trigger action

		// Use this for initialization
		void Start () {
			Debug.Log ("I started");
//			Player = FindObjectOfType<PlatformerCharacter2D>();
			circle = (GameObject) Instantiate(Resources.Load("WatchCircle"));
			//make it a child of this object
			circle.transform.parent = transform;
			circle.transform.localScale = watchRadius;
		}

//		void OnTriggerEnter2D (Collider2D otherCollider)
//		{
//			if (otherCollider.name == "Player")
//			{
////				Player.isCrouching
//				Destroy(this,.5f);
//				Debug.Log ("I was triggered");
//			}
//		}

		public void ActivityDetected() {
			this.BroadcastMessage("SelfDestruct");
			Debug.Log("Bazinga!");
			this.SendMessageUpwards ("ConstraintSuccess", this.gameObject);
		}
	}
}