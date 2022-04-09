using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jupiter : Monster
{
    private MonsterSpawner _monsterSpawner;
    // Start is called before the first frame update
    void Start()
    {
        _HP = 50;
        _speed = 4.0f;
        _monsterSpawner = GameObject.FindWithTag("MonsterSpawner").GetComponent<MonsterSpawner>();
        _soundManager = GameObject.FindWithTag("SoundManager").GetComponent<SoundManager>();

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Move();
        if (CameraResolution.screenLeftBottom.y > transform.position.y||_HP<=0)
        {
            _monsterSpawner.OnOperator();
            base.Dead();
        }
#if UNITY_EDITOR
        drawCollisionBox();
#endif
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
            
            if (collsionList[i].GetType() == typeof(Bullet)||collsionList[i].GetType() == typeof(MisslePet)||collsionList[i].GetType() == typeof(SpaceShipPet))
            {
                if (AABBCollisionCheck(collsionList[i]))
                {
                    GetDamage(collsionList[i]);
                    _hero.PlusCombo();
                    transform.position += Vector3.up * 0.5f;
                    float randXPos = (Random.Range(0, 15) - 7)/10.0f;
                    float randYPos = (Random.Range(0, 5) - 2)/10.0f;

                    ObjectPool.SpawnPoolObj(hitDust.name, collsionList[i].transform.position+ new Vector3(randXPos,randYPos,0), Quaternion.identity);
                    _soundManager.PlaySFXOnce(1, 0.3f);

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
