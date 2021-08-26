using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MoveObject
{
    private float bulletspeed = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * bulletspeed * Time.deltaTime;
        if (CameraResolution.screenRightTop.y < transform.position.y)
        {
            DestroyColObj(this);
        }
    }
}
