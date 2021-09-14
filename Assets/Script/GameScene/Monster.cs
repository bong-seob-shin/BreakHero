using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MoveObject
{
    protected Hero _hero;

    protected override void Awake()
    {
        base.Awake();
        _hero = Hero.Instance;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Move();
       
        
        if(_HP<=0)
            Dead();

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
           
            
            if (collsionList[i].GetType() == typeof(Bullet))
            {
                if (AABBCollisionCheck(collsionList[i]))
                {
                    GetDamage(collsionList[i]);
                    _hero.PlusCombo();
                    transform.position += Vector3.up * 0.5f;
                    DestroyColObj(collsionList[i]);
                }
            }

            if ((collsionList[i].GetType() == typeof(Satellite)||collsionList[i].GetType() == typeof(Monster)
                                                               ||collsionList[i].GetType() == typeof(Meteor)
                                                               ||collsionList[i].GetType() == typeof(Jupiter)) && collsionList[i] != this)
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
