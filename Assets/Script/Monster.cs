using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MoveObject
{
    // Start is called before the first frame update
    void Start()
    {
        this._speed = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down*this._speed*Time.deltaTime;
    }
}
