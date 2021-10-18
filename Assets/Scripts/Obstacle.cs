using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public void Disjoint()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        rb.isKinematic = false;//Physic enable
        GetComponent<Collider>().enabled = false;

        Vector3 forcePositon = transform.parent.position;

        float distance = forcePositon.x - GetComponent<MeshRenderer>().bounds.center.x;

        Vector3 dir = (distance < 0 ? Vector3.right : Vector3.left) + Vector3.up * 1.5f;

        float force = Random.Range(20, 35);
        float torque = Random.Range(110, 180);

        rb.AddForceAtPosition(dir.normalized * force, forcePositon, ForceMode.Impulse);

        rb.AddTorque(Vector3.left * torque);

        rb.velocity = Vector3.down;
    }
}
