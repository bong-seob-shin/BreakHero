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

    public static List<MoveObject> collsionList = new List<MoveObject>();
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        Vector2 spriteSize = _spriteRenderer.size;
        _width = spriteSize.x*transform.localScale.x;
        _height = spriteSize.y*transform.localScale.y;
        
        collsionList.Add(this);

    }

    protected virtual void Move()
    {
    }

    protected virtual void Dead()
    {
    }

    protected virtual void CollsionCheck()
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

    protected bool AABBCollisionCheck(MoveObject colPos)
    {
        float left = transform.position.x - _width / 2;
        float right = transform.position.x + _width / 2;
        float top = transform.position.y + _height / 2;
        float bottom = transform.position.y - _height / 2;

        float colLeft = colPos.transform.position.x - colPos._width / 2;
        float colRight = colPos.transform.position.x + colPos._width / 2;
        float colTop =   colPos.transform.position.y + colPos._height / 2;
        float colBottom =colPos.transform.position.y - colPos._height / 2;

        if (colLeft > right || colRight < left)
            return false;
        if (colBottom > top || colTop < bottom)
            return false;
        return true;
    }
    protected void DestroyColObj(MoveObject mo)
    {
        int deleteIndex = -1;
        for (int i = 0; i < collsionList.Count; i++)
        {
            if (collsionList[i] == mo)
            {
                deleteIndex = i;
                break;
            }
        }

        if (deleteIndex > -1)
        {
            Destroy(collsionList[deleteIndex].gameObject);
            collsionList.RemoveAt(deleteIndex);
        }
    }

    protected void GetDamage(MoveObject mo)
    {
        this._HP -= mo._damage;
    }
}
