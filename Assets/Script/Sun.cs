using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : Monster
{
    
    

    private float _spawnTimer;
    public GameObject flareMonster;

    private MonsterSpawner _monsterSpawner;

    // Start is called before the first frame update
    void Start()
    {
        _HP = 50;
        _speed = 0.1f;
        _monsterSpawner = GameObject.FindWithTag("MonsterSpawner").GetComponent<MonsterSpawner>();
    }

    protected override void Update()
    {

        base.Move();
        if (CameraResolution.screenLeftBottom.y > transform.position.y||_HP<=0)
        {
            _monsterSpawner.OnOperator();

            base.Dead();
        }

        if (transform.position.y > 10.0f)
        {
            _speed = 0.1f;
        }

        _spawnTimer -= Time.deltaTime;

        if (_spawnTimer < 0)
        {
            _spawnTimer = 1.0f;
            SpawnFlareMonster();

        }
        
        CollsionCheck();
        drawCollisionBox();

        
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
                    _hero.PlusCombo();
                    GetDamage(collsionList[i]);
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
    
    void SpawnFlareMonster()
    {
        Instantiate(flareMonster, new Vector3(0, transform.position.y - _height / 2.0f, 0),
            Quaternion.identity);

    }
}
