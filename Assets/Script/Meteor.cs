using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : Monster
{
    void Start()
    {
        _HP = 5;
        _speed = 2.0f;
    }
 
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
