using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jupiter : Monster
{
    // Start is called before the first frame update
    void Start()
    {
        _HP = 50;
        _speed = 4.0f;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Move();
        if (CameraResolution.screenLeftBottom.y > transform.position.y||_HP<=0)
        {
            base.Dead();
        }
        drawCollisionBox();

        if (transform.position.y > 10.0f)
        {
            _speed = 4.0f;
        }

        CollsionCheck();
    }

    protected override void CollsionCheck()
    {

        for (int i = 0; i < collsionList.Count; i++)
        {
            if (collsionList[i].GetType() == typeof(Hero))
            {
                if (AABBCollisionCheck(collsionList[i]))
                {
                    _speed = -20.0f;
                }
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

            if (collsionList[i].GetType() == typeof(Satellite)||collsionList[i].GetType() == typeof(Meteor)&&collsionList[i] != this)
            {
                if (AABBCollisionCheck(collsionList[i]))
                {
                    if(collsionList[i].transform.position.y>transform.position.y)
                        collsionList[i].transform.position += Vector3.up * 1f;
                }
            }
        }
    }

}
