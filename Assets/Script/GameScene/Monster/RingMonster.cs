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
        _soundManager = GameObject.FindWithTag("SoundManager").GetComponent<SoundManager>();

    }

    // Update is called once per frame
    protected override void Update()
    {

        if (_startTimer <= 0)
        {
            base.Move();
            base.CollsionCheck();
            if(_HP<=0)
                base.Dead();
            if (CameraResolution.screenLeftBottom.y + 0.5f > transform.position.y)
            {
                _hero.ResetCombo();
                _earthHeart.Hit();
                base.Dead();
            }
        }
        else
        {
            CollsionCheck();
            _startTimer -= Time.deltaTime;

        }
#if UNITY_EDITOR
        drawCollisionBox();
#endif
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _startTimer = 1.0f;
        _HP = 5;


    }

    protected override void CollsionCheck()
    {
        for (int i = 0; i < collsionList.Count; i++)
        {
          
            
            if (collsionList[i].GetType() == typeof(Bullet)||collsionList[i].GetType() == typeof(MisslePet)||collsionList[i].GetType() == typeof(SpaceShipPet))
            {
                if (AABBCollisionCheck(collsionList[i]))
                {
                    _hero.PlusCombo();
                    GetDamage(collsionList[i]);
                    float randXPos = (Random.Range(0, 15) - 7)/10.0f;
                    float randYPos = (Random.Range(0, 5) - 2)/10.0f;

                    ObjectPool.SpawnPoolObj(hitDust.name, collsionList[i].transform.position+ new Vector3(randXPos,randYPos,0), Quaternion.identity);
                    _soundManager.PlaySFXOnce(1, 0.3f);

                    DestroyColObj(collsionList[i]);
                }
            }

            if ((collsionList[i].GetType() == typeof(Satellite)||collsionList[i].GetType() == typeof(Monster)
                                                               ||collsionList[i].GetType() == typeof(Meteor))&&collsionList[i] != this)
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
