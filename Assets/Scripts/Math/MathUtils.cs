using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathUtils
{

    public static int GetClosestPointIndex(Vector3 from, Vector3[] points)
    {
        float closest = Mathf.Infinity;
        int closestIndex = 0;

        for (int i = 0; i < points.Length; i++)
        {
            float sqrDistance = (points[i] - from).sqrMagnitude;

            if (sqrDistance < closest)
            {
                closestIndex = i;
                closest = sqrDistance;
            }
        }

        return closestIndex;
    }
}
