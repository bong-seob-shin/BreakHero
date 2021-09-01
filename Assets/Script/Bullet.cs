using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MoveObject
{
    private float bulletspeed = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        _damage = 1;
    }

    // Update is called once per frame
    void Update()
    {
        drawCollisionBox();
        transform.position += Vector3.up * bulletspeed * Time.deltaTime;
        if (CameraResolution.screenRightTop.y < transform.position.y)
        {
            DestroyColObj(this);
        }
    }
}
