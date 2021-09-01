using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Satellite : Monster
{
    public GameObject chaneObject;

    // Start is called before the first frame update
    void Start()
    {
        _HP = 5;
        _speed = 2.0f;
    }

    // Update is called once per frame
    protected override void Update()
    {
        if ( chaneObject != null)
        {
            Move();
            CollsionCheck();
        }
        else
        {
            base.Move();
            base.CollsionCheck();
        }
        
        if (CameraResolution.screenLeftBottom.y > transform.position.y||_HP<=0) //이부분 중복되어 사용하는데 정리가 애매함
        {
            base.Dead();
        }
        drawCollisionBox();
    }

    protected override void Move()
    {
        transform.position = new Vector3(transform.position.x,chaneObject.transform.position.y, transform.position.z );
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
                    chaneObject.transform.position += Vector3.up * 0.5f;
                    DestroyColObj(collsionList[i]);
                }
            }

           
        }
    }
}
