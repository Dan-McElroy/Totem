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
			for (var i=0; i < level-1; i++) {
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
			this.SendMessage ("ConstraintSuccessSound");
		}
	}

	void ConstraintFailure(GameObject gameObject) {
		if (currentConstraintsMap.ContainsKey (gameObject)) {
			Debug.Log ("Failure");
			currentConstraintsMap [gameObject] = false;
			this.SendMessage ("ConstraintFailureSound");
		}
	}

	void StartConstraintOpportunity() {
		//propagate for sound
	}

	public int NumberOfActiveConstraints() {
		return currentConstraintsMap.Count;
	}

	void Restart() {
		Debug.Log ("level end in constraint controller");
		if (NumberOfFailedConstraints() == 0) {
			IncrementLevel ();
		}
	}

	public int NumberOfFailedConstraints() {
		int failures = 0;
		foreach (var value in currentConstraintsMap.Values) {
			if (value == false) {
				failures = failures + 1;
			}
		}
		return failures;
	}

	private void IncrementLevel() {
		level = level + 1;
		currentConstraintsMap.Add (globalConstraints[level], false);
	}
}
