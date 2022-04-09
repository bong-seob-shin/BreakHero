using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipPet : MoveObject
{
    private const float SpaceShipMoveRangeToRight = 1.7f;
    private const float Bulletspeed = 1.0f;
    private const float IncreaseHeight = -0.5f;

    private int _horizontalDir = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        _damage = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
#if UNITY_EDITOR
        drawCollisionBox();
#endif
        if (CameraResolution.screenRightTop.y < transform.position.y)
        {
            DestroyColObj(this);
        }
    }

    protected override void Move()
    {
        if(transform.position.y < IncreaseHeight)
            transform.position += Vector3.up * Bulletspeed*2 * Time.deltaTime;
        else
        {
            transform.position += Vector3.right *_horizontalDir * Bulletspeed/2.0f * Time.deltaTime;
            if (transform.position.x > SpaceShipMoveRangeToRight||transform.position.x < -SpaceShipMoveRangeToRight)
            {
                _horizontalDir *= -1;
            }
            
        }

    }
    private void OnDisable()
    {
        ObjectPool.ReturnPoolObj(this.gameObject);
    }

}
