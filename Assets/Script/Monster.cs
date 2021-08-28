using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MoveObject
{
    // Start is called before the first frame update
    void Start()
    {
        _HP = 5;
        _speed = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (CameraResolution.screenLeftBottom.y > transform.position.y||_HP<=0)
        {
            Dead();
        }

        CollsionCheck();




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

           
        }
    }
}
