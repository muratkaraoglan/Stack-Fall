using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleHolderController : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(Vector3.up * 100f * Time.deltaTime);
    }
}
