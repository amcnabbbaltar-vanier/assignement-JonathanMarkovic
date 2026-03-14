using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector3(0, 3.5f, -10);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position + offset;    
    }
}
