﻿using UnityEngine;
using Scripts.Player;

namespace Scripts.World.Constraints {
    
	public class ConstraintTrigger : MonoBehaviour {

//		private PlatformerCharacter2D Player;
		private GameObject circle;

		[SerializeField] private Vector3 watchRadius; //how far to watch for trigger action
		[SerializeField] private Vector3 offset = new Vector3(0,0,0);
		[SerializeField] private int numberOfRepetitionsNeeded = 1;
		private int currentNumberOfRepetitions;
		private bool haveSucceeded = false;

		// Use this for initialization
		void Start () {
			currentNumberOfRepetitions = 0;
//			Player = FindObjectOfType<PlatformerCharacter2D>();
			circle = (GameObject) Instantiate(Resources.Load("WatchCircle"));
			//make it a child of this object
			circle.transform.parent = transform;
			circle.transform.localPosition = offset;
			circle.transform.localScale = watchRadius;
		}

		public void ActivityDetected() {
			currentNumberOfRepetitions++;
			if (currentNumberOfRepetitions >= numberOfRepetitionsNeeded) {
				this.BroadcastMessage("SelfDestruct");
				this.SendMessageUpwards ("ConstraintSuccess", this.gameObject);
				haveSucceeded = true;
			}
		}

		public void ConstraintFailed() {
			if (!haveSucceeded) {
				this.SendMessageUpwards ("ConstraintFailure", this.gameObject);
			}
		}

		public void Reset() {
			currentNumberOfRepetitions = 0;
		}
	}
}