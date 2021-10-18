using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform player, win;
    private float cameraOffsetY = 4f;
    [SerializeField]
    private Vector3 newPos;
    private void Awake()
    {
        player = FindObjectOfType<PlayerController>().transform;
    }
    void Update()
    {
        if ( win == null )
        {
            win = GameObject.Find("win(Clone)").GetComponent<Transform>();
        }

        if ( transform.position.y > player.position.y && transform.position.y > win.position.y + cameraOffsetY )
        {
            newPos = new Vector3(transform.position.x, player.position.y, transform.position.z);
            transform.position = newPos;
        }
    }
}
