using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Satellite : Monster
{
    public GameObject chaneObject;
    private EarthHeart _earthHeart;

    // Start is called before the first frame update
    void Start()
    {
        _HP = 5;
        _speed = 2.0f;
        _damage = 1;
        _earthHeart = EarthHeart.Instance;
        _soundManager = GameObject.FindWithTag("SoundManager").GetComponent<SoundManager>();


    }

    // Update is called once per frame
    protected override void Update()
    {
        if (chaneObject == null || !chaneObject.activeSelf) 
        {
            base.Move();
            base.CollsionCheck();
        }
        else//위성이 서로 붙어있을 때
        {
            Move();
            CollsionCheck();
            
        }
        
        
        if (CameraResolution.screenLeftBottom.y+0.5f > transform.position.y)//이부분 중복되어 사용하는데 정리가 애매함
        {
            _hero.ResetCombo();
            _earthHeart.Hit();
            base.Dead();
        }
        
        if (_HP<=0) //이부분 중복되어 사용하는데 정리가 애매함
        {
            base.Dead();
        }
        
#if UNITY_EDITOR
        drawCollisionBox();
#endif
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _HP = 5;
    }
    
    
    protected override void Move()
    {
        transform.position = new Vector3(transform.position.x,chaneObject.transform.position.y, transform.position.z );
    }

    protected override void CollsionCheck()
    {
        for (int i = 0; i < collsionList.Count; i++)
        {
          
            
            if (collsionList[i].GetType() == typeof(Bullet)||collsionList[i].GetType() == typeof(MisslePet)||collsionList[i].GetType() == typeof(SpaceShipPet))
            {
                if (AABBCollisionCheck(collsionList[i]))
                {
                    GetDamage(collsionList[i]);
                    _hero.PlusCombo();
                    chaneObject.transform.position += Vector3.up * 0.5f;
                    float randXPos = (Random.Range(0, 15) - 7)/10.0f;
                    float randYPos = (Random.Range(0, 5) - 2)/10.0f;

                    ObjectPool.SpawnPoolObj(hitDust.name, collsionList[i].transform.position+ new Vector3(randXPos,randYPos,0), Quaternion.identity);
                    _soundManager.PlaySFXOnce(1, 0.3f);

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
