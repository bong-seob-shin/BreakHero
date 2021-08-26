using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    protected float _HP;
    protected float _speed;
    protected float _damage;
    protected float _width;
    protected float _height;

    
    protected SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        Vector2 spriteSize = _spriteRenderer.size;
        _width = spriteSize.x;
        _height = spriteSize.y;
        Debug.Log("부모 awake불림");

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
