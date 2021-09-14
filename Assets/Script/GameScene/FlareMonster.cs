using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class FlareMonster : Monster
{
    private EarthHeart _earthHeart;

    private float _moveTime;

    private float _horizontalSpeed;
    // Start is called before the first frame update
    void Start()
    {
        _HP = 5;
        _speed = 2.5f;
        _horizontalSpeed = _speed * 1.5f;
        _damage = 1;
        _earthHeart = EarthHeart.Instance;
        _moveTime = 0;
    }

    // Update is called once per frame
    protected override void Update()
    {

        Move();
        CollsionCheck();
        if (_HP <= 0)
            base.Dead();
        if (CameraResolution.screenLeftBottom.y + 0.5f > transform.position.y)
        {
            _hero.ResetCombo();
            _earthHeart.Hit();
            base.Dead();
        }
        drawCollisionBox();

    }

    protected override void Move()
    {
        base.Move();
        _moveTime += Time.deltaTime*_horizontalSpeed;
        float xPos = Mathf.Sin(_moveTime) * 1.7f;
        transform.position = new Vector3(xPos, transform.position.y,0);
    }
    
    protected override void CollsionCheck()
    {
        for (int i = 0; i < collsionList.Count; i++)
        {
          
            
            if (collsionList[i].GetType() == typeof(Bullet))
            {
                if (AABBCollisionCheck(collsionList[i]))
                {
                    _hero.PlusCombo();
                    GetDamage(collsionList[i]);
                    DestroyColObj(collsionList[i]);
                }
            }
            
        }
    }
}
