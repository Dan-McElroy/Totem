using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConstraintController : MonoBehaviour {

	//string is the constraint type?
	private Dictionary<GameObject, bool> currentConstraintsMap;

	[SerializeField] List<GameObject> globalConstraints;

	[SerializeField] int level = 0;

	// Use this for initialization
	void Start () {
		currentConstraintsMap = new Dictionary<GameObject, bool> ();
		if (level > 0) {
			Debug.Log ("level: " + level);
			for (var i=0; i < level; i++) {
				currentConstraintsMap.Add (globalConstraints[i], false);
			}
		}
	}

	// Update is called once per frame
	void Update () {
//		Debug.Log ("Constraint map:");
//		foreach (var key in constraintsMap.Keys) {
//			Debug.Log (key);
//			Debug.Log (constraintsMap [key]);
//		}
	}

	void ConstraintSuccess(GameObject gameObject) {
		if (currentConstraintsMap.ContainsKey (gameObject)) {
			Debug.Log ("Success");
			currentConstraintsMap [gameObject] = true;
		}
	}

	void ConstraintFailure(GameObject gameObject) {
		if (currentConstraintsMap.ContainsKey (gameObject)) {
			Debug.Log ("Failure");
			currentConstraintsMap [gameObject] = false;
		}
	}

	int NumberOfActiveConstraints() {
		return currentConstraintsMap.Count;
	}

	void IncrementLevel() {
		currentConstraintsMap.Add (globalConstraints[level], false);
		level = level + 1;
	}

	int NumberOfFailedConstraints() {
		int failures = 0;
		foreach (var value in currentConstraintsMap.Values) {
			if (value == false) {
				failures = failures + 1;
			}
		}
		return failures;
	}
}
