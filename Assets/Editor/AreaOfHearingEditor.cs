using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof (AreaOfHearing))]
public class AreaOfHearingEditor : Editor {

	void OnSceneGUI() {
		AreaOfHearing aoh = (AreaOfHearing)target;
		Handles.color = Color.white;
		//Handles.DrawWireArc (aoh.transform.position, Vector3.up, Vector3.forward, 360, aoh.viewRadius);
		Vector3 viewAngleA = aoh.DirFromAngle (-360 / 2, false);
		Vector3 viewAngleB = aoh.DirFromAngle (360 / 2, false);

		Handles.DrawLine (aoh.transform.position, aoh.transform.position + viewAngleA * aoh.hearingRadius);
		Handles.DrawLine (aoh.transform.position, aoh.transform.position + viewAngleB * aoh.hearingRadius);

		Handles.color = Color.red;
		foreach (Transform visibleTarget in aoh.heardTargets) {
			Debug.Log (visibleTarget.position);
			Handles.DrawLine (aoh.transform.position, visibleTarget.position);
		}
	}
}
