using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbHandler : MonoBehaviour
{
	[SerializeField]
	private float r1;

	[SerializeField]
	private float r2;

	[SerializeField]
	private Transform firstBone;

	[SerializeField]
	private Transform secondBone;

	private (Vector2 a, Vector2 b) intersections;

	private void Update()
	{
		Vector2 firstBonePosition = FlattenVector(firstBone.position);
		Vector2 handlerPosition = FlattenVector(transform.position);
		if (FindCircleCircleIntersections(firstBonePosition, r1, handlerPosition, r2, out intersections.a, out intersections.b) > 0)
		{
			firstBone.forward = Convexify(intersections.a, firstBone.position.z) - firstBone.position;
			secondBone.position = Convexify(intersections.a, secondBone.position.z);
			secondBone.forward = transform.position - secondBone.position;
		}
	}

	private Vector2 FlattenVector(Vector3 v)
	{
		return new Vector2(v.x, v.y);
	}

	private Vector3 Convexify(Vector2 v, float z)
	{
		return new Vector3(v.x, v.y, z);
	}

	// Find the points where the two circles intersect.
	private int FindCircleCircleIntersections(Vector2 c0, float r0, Vector2 c1, float r1, out Vector2 intersection1, out Vector2 intersection2)
	{
		// Find the distance between the centers.
		double dx = c0.x - c1.x;
		double dy = c0.y - c1.y;
		double dist = Math.Sqrt(dx * dx + dy * dy);

		if (Math.Abs(dist - (r0 + r1)) < 0.00001)
		{
			intersection1 = Vector2.Lerp(c0, c1, r0 / (r0 + r1));
			intersection2 = intersection1;
			return 1;
		}

		// See how many solutions there are.
		if (dist > r0 + r1)
		{
			// No solutions, the circles are too far apart.
			intersection1 = new Vector2(float.NaN, float.NaN);
			intersection2 = new Vector2(float.NaN, float.NaN);
			return 0;
		}
		else if (dist < Math.Abs(r0 - r1))
		{
			// No solutions, one circle contains the other.
			intersection1 = new Vector2(float.NaN, float.NaN);
			intersection2 = new Vector2(float.NaN, float.NaN);
			return 0;
		}
		else if ((dist == 0) && (r0 == r1))
		{
			// No solutions, the circles coincide.
			intersection1 = new Vector2(float.NaN, float.NaN);
			intersection2 = new Vector2(float.NaN, float.NaN);
			return 0;
		}
		else
		{
			// Find a and h.
			double a = (r0 * r0 -
						r1 * r1 + dist * dist) / (2 * dist);
			double h = Math.Sqrt(r0 * r0 - a * a);

			// Find P2.
			double cx2 = c0.x + a * (c1.x - c0.x) / dist;
			double cy2 = c0.y + a * (c1.y - c0.y) / dist;

			// Get the points P3.
			intersection1 = new Vector2(
				(float)(cx2 + h * (c1.y - c0.y) / dist),
				(float)(cy2 - h * (c1.x - c0.x) / dist));
			intersection2 = new Vector2(
				(float)(cx2 - h * (c1.y - c0.y) / dist),
				(float)(cy2 + h * (c1.x - c0.x) / dist));

			return 2;
		}
	}
}
