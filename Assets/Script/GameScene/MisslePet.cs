using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MisslePet : MoveObject
{
    private float bulletspeed = 4.0f;


    private Vector3 _moveDir;
    private float _serchTimer; 
    

    // Start is called before the first frame update
    void Start()
    {
        _damage = 1;
        _moveDir = Vector3.zero;
        _serchTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        _serchTimer -= Time.deltaTime;
        if (_serchTimer < 0)
        {
            SearchTarget();
            _serchTimer = 0.5f;
        }

        Move();
#if UNITY_EDITOR
        drawCollisionBox();
#endif     
        if (CameraResolution.screenRightTop.y < transform.position.y)
        {
            DestroyColObj(this);
        }
    }

    protected override void Move()
    {
        
        transform.position += _moveDir * bulletspeed * Time.deltaTime;

    }

    private void OnDisable()
    {
        ObjectPool.ReturnPoolObj(this.gameObject);
    }
    void SearchTarget()
    {
        float dist = 1000;
        Vector3 _target = Vector3.zero;

        for (int i = 0; i < collsionList.Count; i++)
        {
            
            if (collsionList[i].GetType() == typeof(Satellite) ||collsionList[i].GetType() == typeof(Monster)
                                                               ||collsionList[i].GetType() == typeof(Meteor) 
                                                               ||collsionList[i].GetType() == typeof(Jupiter)
                                                               ||collsionList[i].GetType() == typeof(Saturn)
                                                               ||collsionList[i].GetType() == typeof(Sun)
                                                               ||collsionList[i].GetType() == typeof(RingMonster)
                                                               ||collsionList[i].GetType() == typeof(FlareMonster) 
                                                               && collsionList[i] != this)
            {
                if (collsionList[i].gameObject.activeSelf)
                {
                    Vector3 tempVec = collsionList[i].transform.position - transform.position;
                    float tempDist = tempVec.sqrMagnitude;

                    if (tempDist < dist && dist > 0.1f)
                    {
                        dist = tempDist;
                        _target = collsionList[i].transform.position;
                    }
                }
            }
        }

        if (_target != Vector3.zero)
        {
            _moveDir = (_target - transform.position).normalized;
        }
    }

}
