using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointTowardsCamera : MonoBehaviour
{
    public bool updateInUpdate;

    public Transform target;


    void Update()
    {
        if (updateInUpdate)
            transform.rotation = Quaternion.LookRotation((target.position - transform.position).normalized, Vector3.up);
    }

    public void UpdateLook()
    {
        transform.rotation = Quaternion.LookRotation((target.position - transform.position).normalized, Vector3.up);
    }
}
