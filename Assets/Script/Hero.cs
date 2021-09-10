using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Hero : MoveObject
{
    private static Hero _instance = null;
    
    public static Hero Instance
    {
        get
        {
            if(_instance == null)
            {
                return null;
            }

            return _instance;
        }
   
    }

    private Vector3 _targetPos;
    
    private Camera _camera;


    private bool _isClickHold;
    
    public GameObject bullet;

    public GameObject heart;


    private InGameManager _igm;

    public Animation weaponAnim;

    private int comboPoint;
    // Start is called before the first frame update


    protected override void Awake()
    {
        base.Awake();
        if (_instance == null)
            _instance = this;
    }

    void Start()
    {
        
        
        _targetPos = transform.position;
        _camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        _HP = 3;
        _igm = GameObject.FindWithTag("InGameManager").GetComponent<InGameManager>();
        comboPoint = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (_igm.GetIsStart())
        {
            ClickInput();
            CollsionCheck();
            drawCollisionBox();
            if (_HP <= 0)
            {
                Dead();
            }
        }
    }

    void ClickInput()
    {
        if(_isClickHold) 
            Move();
#if UNITY_EDITOR ||UNITY_STANDALONE_WIN
        if (Input.GetMouseButtonDown(0))
        {
            _targetPos = _camera.ScreenToWorldPoint(Input.mousePosition);
            if (AABBCollisionCheck(new Vector2(_targetPos.x, _targetPos.y)))
            {
                Attack();
            }
        }

        if (Input.GetMouseButton(0))
        {
            if (AABBCollisionCheck(new Vector2(_targetPos.x, _targetPos.y)))
            {
                _isClickHold = true;

            }

        }
        else
        {
            _isClickHold = false;
        }
#elif UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            _targetPos = _camera.ScreenToWorldPoint(Input.GetTouch(0).position);
            if (AABBCollisionCheck(new Vector2(_targetPos.x, _targetPos.y)))
            {
                if(!_isClickHold)
                    Attack();
                _isClickHold = true;

            }
        }
        else
        {
            _isClickHold = false;

        }
#endif
    }
    protected override void Move()
    {
        _targetPos = _camera.ScreenToWorldPoint(Input.mousePosition);
        
        if(_targetPos.x>CameraResolution.screenLeftBottom.x+this._width/2
           &&_targetPos.x<CameraResolution.screenRightTop.x-this._width/2)
            transform.position = new Vector3(_targetPos.x, -3.5f, 0);
    }

    void Attack()
    {
        Instantiate(bullet,transform.position+Vector3.up*0.7f,Quaternion.identity);
        weaponAnim.Play();
    }

    public int GetHP()
    {
        return _HP;
    }

    public void PlusCombo()
    {
        comboPoint++;
    }

    public void ResetCombo()
    {
        comboPoint = 0;
    }
    public int GetCombo()
    {
        return comboPoint;
    }
    
    protected override void CollsionCheck()
    {
        for (int i = 0; i < collsionList.Count; i++)
        {
            if(collsionList[i].GetType() == typeof(Satellite)||
                collsionList[i].GetType() == typeof(Meteor)||
                collsionList[i].GetType() == typeof(Monster)||
                collsionList[i].GetType() == typeof(RingMonster)||
                collsionList[i].GetType() == typeof(FlareMonster))
            {
                if (AABBCollisionCheck(collsionList[i]))
                {
                    DestroyColObj(collsionList[i]);
                    heart.SetActive(true);
                    _HP -= 1;
                    ResetCombo();
                }
            }
            else if (collsionList[i].GetType() == typeof(Jupiter)||
                     collsionList[i].GetType() == typeof(Saturn))
            {
                if (AABBCollisionCheck(collsionList[i]))
                {
                    heart.SetActive(true);
                    _HP -= 2;
                    ResetCombo();
                }

            }
        }
    }

    protected override void Dead()
    {
        Destroy(gameObject);
    }
}
