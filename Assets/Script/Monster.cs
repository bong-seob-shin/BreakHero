using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MoveObject
{
    // Start is called before the first frame update
    void Start()
    {
        this._speed = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < collsionList.Count; i++)
        {
            if (collsionList[i].GetType() == typeof(Bullet))
            {
                // if(AABBCollisionCheck(collsionList[i]))
                //     Debug.Log("monster Collision!!");
            }
        }
        
        if (CameraResolution.screenLeftBottom.y > transform.position.y)
        {
            DestroyColObj(this);
        }
        transform.position += Vector3.down*this._speed*Time.deltaTime;
    }
}
