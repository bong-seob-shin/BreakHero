using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Hero : MoveObject
{
    [SerializeField]
    private Vector3 _targetPos;
    
    private Camera _camera;


    private bool _isClickHold;
    
    public GameObject bullet;

    
    // Start is called before the first frame update
    void Start()
    {
        _targetPos = transform.position;
        _camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        ClickInput();
        drawCollisionBox();

    }

    void ClickInput()
    {
        if(_isClickHold) 
            Move();
#if UNITY_EDITOR
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
            transform.position = new Vector3(_targetPos.x, -4.0f, 0);
    }

    void Attack()
    {
        Instantiate(bullet,transform.position,Quaternion.identity);
    }
}
