using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingMonster : Monster
{
    private float _startTimer;
    private EarthHeart _earthHeart;

    // Start is called before the first frame update
    void Start()
    {
        _startTimer = 1.0f;
        _HP = 5;
        _speed = 15.0f;
        _damage = 1;
        _earthHeart = EarthHeart.Instance;
    }

    // Update is called once per frame
    protected override void Update()
    {

        if (_startTimer <= 0)
        {
            base.Move();
            base.CollsionCheck();
            if(_HP<=0)
                Dead();
            if (CameraResolution.screenLeftBottom.y + 0.5f > transform.position.y)
            {
                _earthHeart.Hit();
                Dead();
            }
        }
        else
        {
            CollsionCheck();
            _startTimer -= Time.deltaTime;

        }
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
                    DestroyColObj(collsionList[i]);
                }
            }

            if ((collsionList[i].GetType() == typeof(Satellite)||collsionList[i].GetType() == typeof(Monster)
                                                               ||collsionList[i].GetType() == typeof(Meteor)
                                                               ||collsionList[i].GetType() == typeof(Jupiter))&&collsionList[i] != this)
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
