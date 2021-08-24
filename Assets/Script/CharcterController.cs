using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CharcterController : MonoBehaviour
{
    [SerializeField]
    private Vector3 _targetPos;
    
    private Camera _camera;

    private float _moveSpeed;

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
        Move();
        if (Input.GetMouseButtonDown(0))
        {
            SetTarget(Input.mousePosition);
            Attack();
        }
        
    }

    void SetTarget(Vector3 mousePos)
    {
        _targetPos = _camera.ScreenToWorldPoint(mousePos);
    }

    void Move()
    {
        Vector3 targetVec3 = new Vector3(_targetPos.x,_targetPos.y,0) - transform.position;
        Vector3 targetDir = targetVec3.normalized;
        
        if (targetVec3.sqrMagnitude > 0.01f)
        {
            transform.position = transform.position+ _moveSpeed * targetDir * Time.deltaTime;
        }
        else
        {
            transform.position = new Vector3(_targetPos.x,_targetPos.y,0);
        }
    }

    void Attack()
    {
        Instantiate(bullet,transform.position,Quaternion.identity);
    }
}
