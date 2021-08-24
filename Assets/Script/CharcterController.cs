using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CharcterController : MoveObject
{
    [SerializeField]
    private Vector3 _targetPos;
    
    private Camera _camera;

    private float _moveSpeed;

    private bool _isClickHold;
    
    public GameObject bullet;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _targetPos = transform.position;
        _camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        _moveSpeed = 3.0f;
    }

    // Update is called once per frame
    void Update()
    {


        ClickInput();




    }

    void ClickInput()
    {
        if(_isClickHold) 
            Move();
#if UNITY_EDITOR
        _isClickHold = Input.GetMouseButton(0);
        if (Input.GetMouseButtonDown(0))
        {
            _targetPos = _camera.ScreenToWorldPoint(Input.mousePosition);
            if (AABBCollisionCheck(new Vector2(_targetPos.x, _targetPos.y)))
            {
                Attack();
            }
        }
#endif
        
        if (Input.touchCount > 0)
        {
            _targetPos = _camera.ScreenToWorldPoint(Input.GetTouch(0).position);
            if (AABBCollisionCheck(new Vector2(_targetPos.x,_targetPos.y)))
            {
                Attack();
            }
        }
    }
    protected override void Move()
    {
        _targetPos = _camera.ScreenToWorldPoint(Input.mousePosition);

        transform.position = new Vector3(_targetPos.x, _targetPos.y, 0);
    }

    void Attack()
    {
        Instantiate(bullet,transform.position,Quaternion.identity);
    }
}
