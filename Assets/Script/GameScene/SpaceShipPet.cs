using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipPet : MoveObject
{
    private float _bulletspeed = 1.0f;

    private float increaseHeight = -0.5f;

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
        if(transform.position.y < increaseHeight)
            transform.position += Vector3.up * _bulletspeed*2 * Time.deltaTime;
        else
        {
            transform.position += Vector3.right *_horizontalDir*_bulletspeed/2.0f * Time.deltaTime;
            if (transform.position.x > 1.7f||transform.position.x < -1.7f)
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
