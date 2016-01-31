using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ConstraintController : MonoBehaviour {
    
	//string is the constraint type?
	private Dictionary<GameObject, bool> currentConstraintsMap;

	[SerializeField] List<GameObject> globalConstraints;
    [SerializeField] GameObject maw;
    [SerializeField] GameObject endLevel;

	[SerializeField] int level = 0;

	// Use this for initialization
	void Start () {
        currentConstraintsMap = new Dictionary<GameObject, bool> ();
		Debug.Log ("level: " + level);
		for (var i=0; i < globalConstraints.Count; i++) {
			if (level > i) {
				currentConstraintsMap.Add (globalConstraints[i], false);
			}
			BroadcastMessage ("Hide", i);
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
		BroadcastMessage ("Reset");
		if (level == 0 || NumberOfFailedConstraints () == 0) {
			IncrementLevel ();
			Debug.Log ("we're in level: " + level);
		} else {
			Debug.Log ("failed the level");
		}
	}

	public int NumberOfFailedConstraints() {
		int failures = 0;
		foreach (var key in currentConstraintsMap.Keys) {
			if (currentConstraintsMap[key] == false) {
				failures = failures + 1;
				BroadcastMessage ("Fail", globalConstraints.IndexOf(key));
			}
		}
		return failures;
	}

	private void IncrementLevel() {
		level = level + 1;

        if (globalConstraints.Count < level)
        {
            Destroy(maw);
            Destroy(endLevel);
            gameObject.GetComponent<AudioController>().enabled = false;
        }
        else
        {
            currentConstraintsMap = new Dictionary<GameObject, bool>();
            for (var i = 0; i < level; i++)
            {
                currentConstraintsMap.Add(globalConstraints[i], false);
                BroadcastMessage("Reveal", i);
            }
        }
	}
}
