using UnityEngine;
using System.Collections;
//using System.Collections.Generic.Dictionary<GameObject, bool>;

public class ConstraintController : MonoBehaviour {

	//string is the constraint type?
	private System.Collections.Generic.Dictionary<GameObject, bool> constraintsMap;

	// Use this for initialization
	void Start () {
		constraintsMap = new System.Collections.Generic.Dictionary<GameObject, bool> ();
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
		Debug.Log ("Success");
		constraintsMap[gameObject] = true;
	}

	void ConstraintFailure(GameObject gameObject) {
		Debug.Log ("Failure");
		constraintsMap[gameObject] = false;
	}

	int NumberOfActiveConstraints() {
		return constraintsMap.Count;
	}

	int NumberOfFailedConstraints() {
		int failures = 0;
		foreach (var value in constraintsMap.Values) {
			if (value == false) {
				failures = failures + 1;
			}
		}
		return failures;
	}
}
