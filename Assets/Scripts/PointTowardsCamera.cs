using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointTowardsCamera : MonoBehaviour
{
    public Transform target;
    void Update()
    {
        transform.rotation = Quaternion.LookRotation((target.position - transform.position).normalized, Vector3.up);
    }
}
