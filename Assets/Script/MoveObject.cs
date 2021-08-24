using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    private float _HP;
    private float _speed;
    private float _damage;
    private float _width;
    private float _height;

    
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        Vector2 spriteSize = _spriteRenderer.size;
        _width = spriteSize.x;
        _height = spriteSize.y;
        
    }

    protected virtual void Move()
    {
        
    }

    protected virtual void Dead()
    {
        
    }

    protected bool AABBCollisionCheck(Vector2 colPos)
    {
        float left = transform.position.x - _width / 2;
        float right = transform.position.x + _width / 2;
        float top = transform.position.y + _height / 2;
        float bottom = transform.position.y - _height / 2;
        
        if (colPos.x < left || colPos.x > right)
            return false;
        if (colPos.y < bottom || colPos.y > top)
            return false;
        return true;
    }

}
