﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPivot : MonoBehaviour
{
    [Range(0, 4)]
    public int icoSphereRecursions = 2;
    public float radius = 5;

    public float directionGizmoLength = 0.5f;

    Vector3[] points;

    public Vector3[] Points { get { return points; } }

    public static CameraPivot[] GetAllInScene()
    {
        return FindObjectsOfType<CameraPivot>();
    }

    private void OnValidate()
    {

        points = IcoSphere.GetIcoSpherePoints(radius, icoSphereRecursions);

        if (!Application.isPlaying)
            Debug.Log("Number of points: " + points.Length);
    }

    private void OnDrawGizmos()
    {
        if (points == null) return;

        for (int i = 0; i < points.Length; i++)
        {
            Vector3 origin = transform.TransformPoint(points[i]);
            Vector3 dir = transform.position - origin;

            Gizmos.color = new Color(1f, 1f, 1f, 1f);
            Gizmos.DrawSphere(origin, 0.05f);
            Gizmos.color = new Color(0.4f, 1f, 1f, 0.4f);
            Gizmos.DrawRay(origin, dir.normalized * directionGizmoLength);
        }
    }
}
