using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MoveObject
{
    public GameObject hitDust;
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
#if UNITY_EDITOR
        drawCollisionBox();
#endif  
    }

    protected override void Move()
    {
        transform.position += Vector3.down*this._speed*Time.deltaTime;
        
    }

    protected override void Dead()
    {
        DestroyColObj(this);
    }
    private void OnDisable()
    {
        ObjectPool.ReturnPoolObj(this.gameObject);
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
                    transform.position += Vector3.up * 0.5f;
                    float randXPos = (Random.Range(0, 15) - 7)/10.0f;
                    float randYPos = (Random.Range(0, 5) - 2)/10.0f;

                    ObjectPool.SpawnPoolObj(hitDust.name, collsionList[i].transform.position+ new Vector3(randXPos,randYPos,0), Quaternion.identity);
                    _soundManager.PlaySFXOnce(1, 0.3f);

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
