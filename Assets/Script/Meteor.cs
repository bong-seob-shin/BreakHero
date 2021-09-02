using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : Monster
{
    private EarthHeart _earthHeart;

    void Start()
    {
        _HP = 5;
        _speed = 2.0f;
        _damage = 1;
        _earthHeart = EarthHeart.Instance;
    }
 
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (CameraResolution.screenLeftBottom.y+0.5f > transform.position.y)
        {
            _earthHeart.Hit();
            Dead();
        }
    }
}
