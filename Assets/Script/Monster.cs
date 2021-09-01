using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MoveObject
{
    
    // Update is called once per frame
    protected virtual void Update()
    {
        Move();
        if (CameraResolution.screenLeftBottom.y > transform.position.y||_HP<=0)
        {
            Dead();
        }

        CollsionCheck();
        drawCollisionBox();
    }

    protected override void Move()
    {
        transform.position += Vector3.down*this._speed*Time.deltaTime;
        
    }

    protected override void Dead()
    {
        DestroyColObj(this);
    }

    protected override void CollsionCheck()
    {
        for (int i = 0; i < collsionList.Count; i++)
        {
            if (collsionList[i].GetType() == typeof(Hero))
            {
                if (AABBCollisionCheck(collsionList[i]))
                    Dead();
            }
            
            if (collsionList[i].GetType() == typeof(Bullet))
            {
                if (AABBCollisionCheck(collsionList[i]))
                {
                    GetDamage(collsionList[i]);
                    transform.position += Vector3.up * 0.5f;
                    DestroyColObj(collsionList[i]);
                }
            }

            if (collsionList[i].GetType() == typeof(Satellite)&&collsionList[i] != this)
            {
                if (AABBCollisionCheck(collsionList[i]))
                {
                    collsionList[i].transform.position += Vector3.up * 1f;
                }
            }
        }
    }
}
