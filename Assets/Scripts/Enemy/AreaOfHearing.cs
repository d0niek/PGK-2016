using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AreaOfHearing : MonoBehaviour {

	public float hearingRadius;

	public LayerMask playerAndFollowerLayer;
	public LayerMask obstacleLayer;

	[HideInInspector]
	public List<Transform> heardTargets = new List<Transform> ();

	public float meshResolution;
	public int edgeResolveIterations;
	public float edgeDstThreshold;

	public MeshFilter hearingMeshFilter;
	Mesh hearingMesh;

	void Start() {
		hearingMesh = new Mesh ();
		hearingMesh.name = "Hearing mesh";
		hearingMeshFilter.mesh = hearingMesh;

		StartCoroutine ("FindTargetsWithDelay", 0.2f);
	}

	IEnumerator FindTargetsWithDelay(float delay) {
		while (true) {
			yield return new WaitForSeconds (delay);
			FindPlayerOrFollower ();
		}
	}

	void LateUpdate() {
		DrawAreaOfHearing ();
	}

	void FindPlayerOrFollower() {
		heardTargets.Clear ();
		Collider[] targetsInHearingRadius = Physics.OverlapSphere (transform.position, hearingRadius, playerAndFollowerLayer);

		for (int i = 0; i < targetsInHearingRadius.Length; i++) {
			Transform target = targetsInHearingRadius [i].transform;
			Vector3 dirToTarget = (target.position - transform.position).normalized;
			if (Vector3.Angle(transform.forward, dirToTarget) < 360 / 2) {
				float dstToTarget = Vector3.Distance (transform.position, target.position);

				if (!Physics.Raycast (transform.position, dirToTarget, dstToTarget, obstacleLayer)) {
					heardTargets.Add (target);
				}
			}
		}
	}

	void DrawAreaOfHearing() {
		int stepCount = Mathf.RoundToInt(360 * meshResolution);
		float setpAngleSize = 360 / stepCount;
		List<Vector3> hearingPoints = new List<Vector3> ();
		HearingCastInfo oldHearingCast = new HearingCastInfo ();

		for (int i = 0; i <= stepCount; i++) {
			float angle = transform.eulerAngles.y - 360 / 2 + setpAngleSize * i;
			HearingCastInfo newHearingCast = HearingCast (angle);

			if (i > 0) {
				bool edgeDstThresholdExceeded = Mathf.Abs (oldHearingCast.dst - newHearingCast.dst) > edgeDstThreshold;
				if (oldHearingCast.hit != newHearingCast.hit || (oldHearingCast.hit && newHearingCast.hit && edgeDstThresholdExceeded)) {
					EdgeInfo edge = FindEdge (oldHearingCast, newHearingCast);
					if (edge.pointA != Vector3.zero) {
						hearingPoints.Add (edge.pointA);
					}
					if (edge.pointB != Vector3.zero) {
						hearingPoints.Add (edge.pointB);
					}
				}
			}

			hearingPoints.Add (newHearingCast.point);
			oldHearingCast = newHearingCast;
		}

		int vertexCount = hearingPoints.Count + 1;
		Vector3[] vertices = new Vector3[vertexCount];
		int[] triangles = new int[(vertexCount - 2) * 3];

		vertices [0] = Vector3.zero;
		for (int i = 0; i < vertexCount - 1; i++) {
			vertices [i + 1] = transform.InverseTransformPoint(hearingPoints [i]);

			if (i < vertexCount - 2) {
				triangles [i * 3] =	0;
				triangles [i * 3 + 1] = i + 1;
				triangles [i * 3 + 2] = i + 2;
			}
		}

		hearingMesh.Clear ();
		hearingMesh.vertices = vertices;
		hearingMesh.triangles = triangles;
		hearingMesh.RecalculateNormals ();
	}

	EdgeInfo FindEdge(HearingCastInfo minHearingCast, HearingCastInfo maxHearingCast) {
		float minAngle = minHearingCast.angle;
		float maxAngle = maxHearingCast.angle;
		Vector3 minPoint = Vector3.zero;
		Vector3 maxPoint = Vector3.zero;

		for (int i = 0; i < edgeResolveIterations; i++) {
			float angle = (minAngle + maxAngle) / 2;
			HearingCastInfo newHearingCast = HearingCast (angle);

			bool edgeDstThresholdExceeded = Mathf.Abs (minHearingCast.dst - newHearingCast.dst) > edgeDstThreshold;
			if (newHearingCast.hit == minHearingCast.hit && edgeDstThresholdExceeded == false) {
				minAngle = angle;
				minPoint = newHearingCast.point;
			} else {
				maxAngle = angle;
				maxPoint = newHearingCast.point;
			}
		}

		return new EdgeInfo (minPoint, maxPoint);
	}

	HearingCastInfo HearingCast(float globalAngle) {
		Vector3 dir = DirFromAngle(globalAngle, true);
		RaycastHit hit;

		if (Physics.Raycast(transform.position, dir, out hit, hearingRadius, obstacleLayer)) {
			return new HearingCastInfo(true, hit.point, hit.distance, globalAngle);
		}

		return new HearingCastInfo(false, transform.position + dir * hearingRadius, hearingRadius, globalAngle);
	}

	public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal) {
		if (angleIsGlobal == false) {
			angleInDegrees += transform.eulerAngles.y;
		}

		return new Vector3 (Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
	}

	public struct HearingCastInfo {
		public bool hit;
		public Vector3 point;
		public float dst;
		public float angle;

		public HearingCastInfo(bool hit, Vector3 point, float dst, float angle) {
			this.hit = hit;
			this.point = point;
			this.dst = dst;
			this.angle = angle;
		}
	}

	public struct EdgeInfo {
		public Vector3 pointA;
		public Vector3 pointB;

		public EdgeInfo(Vector3 pointA, Vector3 pointB) {
			this.pointA = pointA;
			this.pointB = pointB;
		}
	}
}
