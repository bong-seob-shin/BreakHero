using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MoveObject
{
    private float bulletspeed = 2.0f;

    private Hero _hero;

    public Sprite bulletSprite;
    
    // Start is called before the first frame update
    void Start()
    {
        _damage = 1;
        _hero = Hero.Instance;
        GetComponent<SpriteRenderer>().sprite = bulletSprite;
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        drawCollisionBox();
#endif
        transform.position += Vector3.up * bulletspeed * Time.deltaTime;
        if (CameraResolution.screenRightTop.y < transform.position.y)
        {
            _hero.ResetCombo();
            DestroyColObj(this);
        }
    }

    private void OnDisable()
    {
        ObjectPool.ReturnPoolObj(this.gameObject);
    }
    
   
    
   
}
