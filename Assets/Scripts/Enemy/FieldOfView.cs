using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FieldOfView : MonoBehaviour {

	public float viewRadius;
	[Range(0, 360)]
	public float viewAngle;

	public LayerMask playerAndFollowerLayer;
	public LayerMask obstacleLayer;

	[HideInInspector]
	public List<Transform> visibleTargets = new List<Transform> ();

	public float meshResolution;
	public MeshFilter viewMeshFilter;
	Mesh viewMesh;

	void Start() {
		viewMesh = new Mesh ();
		viewMesh.name = "View mesh";
		viewMeshFilter.mesh = viewMesh;

		StartCoroutine ("FindTargetsWithDelay", 0.2f);
	}

	IEnumerator FindTargetsWithDelay(float delay) {
		while (true) {
			yield return new WaitForSeconds (delay);
			FindPlayerOrFollower ();
		}
	}

	void LateUpdate() {
		DrawFieldOfView ();
	}

	void FindPlayerOrFollower() {
		visibleTargets.Clear ();
		Collider[] targetsInViewRadius = Physics.OverlapSphere (transform.position, viewRadius, playerAndFollowerLayer);

		for (int i = 0; i < targetsInViewRadius.Length; i++) {
			Transform target = targetsInViewRadius [i].transform;
			Vector3 dirToTarget = (target.position - transform.position).normalized;
			if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2) {
				float dstToTarget = Vector3.Distance (transform.position, target.position);

				if (!Physics.Raycast (transform.position, dirToTarget, dstToTarget, obstacleLayer)) {
					visibleTargets.Add (target);
				}
			}
		}
	}

	void DrawFieldOfView() {
		int setpCount = Mathf.RoundToInt(viewAngle * meshResolution);
		float setpAngleSize = viewAngle / setpCount;
		List<Vector3> viewPoints = new List<Vector3> ();

		for (int i = 0; i <= setpCount; i++) {
			float angle = transform.eulerAngles.y - viewAngle / 2 + setpAngleSize * i;
			ViewCastInfo viewCast = ViewCast (angle);
			viewPoints.Add (viewCast.point);
		}

		int vertexCount = viewPoints.Count + 1;
		Vector3[] vertices = new Vector3[vertexCount];
		int[] triangles = new int[(vertexCount - 2) * 3];

		vertices [0] = Vector3.zero;
		for (int i = 0; i < vertexCount - 1; i++) {
			vertices [i + 1] = transform.InverseTransformPoint(viewPoints [i]);

			if (i < vertexCount - 2) {
				triangles [i * 3] =	0;
				triangles [i * 3 + 1] = i + 1;
				triangles [i * 3 + 2] = i + 2;
			}
		}

		viewMesh.Clear ();
		viewMesh.vertices = vertices;
		viewMesh.triangles = triangles;
		viewMesh.RecalculateNormals ();
	}

	ViewCastInfo ViewCast(float globalAngle) {
		Vector3 dir = DirFromAngle(globalAngle, true);
		RaycastHit hit;

		if (Physics.Raycast(transform.position, dir, out hit, viewRadius, obstacleLayer)) {
			return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
		}

		return new ViewCastInfo(false, transform.position + dir * viewRadius, viewRadius, globalAngle);
	}

	public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal) {
		if (angleIsGlobal == false) {
			angleInDegrees += transform.eulerAngles.y;
		}

		return new Vector3 (Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
	}

	public struct ViewCastInfo {
		public bool hit;
		public Vector3 point;
		public float dst;
		public float angle;

		public ViewCastInfo(bool hit, Vector3 point, float dst, float angle) {
			this.hit = hit;
			this.point = point;
			this.dst = dst;
			this.angle = angle;
		}
	}
}
