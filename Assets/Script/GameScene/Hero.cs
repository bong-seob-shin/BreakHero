using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.IK;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

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

    public Sprite[] bulletSprite ;
    
    public GameObject heart;

    
    private InGameManager _igm;

    private long _weaponType;
    private long _petType;
    public GameObject[] weapons;
    public Animation[] weaponAnims;

    
    public GameObject[] heroSkins;

    private long comboPoint;

    private long maxComboPoint;
    // Start is called before the first frame update

    private IKManager2D _ikManger;

    public LimbSolver2D[] leftSolver;
    public LimbSolver2D[] rightSolver;

    private GameDataBaseManager _gameDataBaseManager;

    public GameObject[] pets;
    
    private float _petTimer;
    private Vector3[] _petSpawnPos = {new Vector3(-1.7f, -5f, 0), new Vector3(0, -5f, 0), new Vector3(1.7f, -5f, 0)};


    public GameObject auraEffect;
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
        _soundManager = GameObject.FindWithTag("SoundManager").GetComponent<SoundManager>();

        _gameDataBaseManager = GameDataBaseManager.Instance;
        
        comboPoint = 0;
        maxComboPoint = 0;
        _petTimer = 6.0f;
        _width = _width * 1.5f;
        _ikManger = GetComponent<IKManager2D>();

        _weaponType = _gameDataBaseManager._gamePlayData.GetWeaponType();
        _petType = _gameDataBaseManager._gamePlayData.GetPetType() - 1; // pet이 없을 경우를 포함하여 인덱싱 하기 위하여 -1을 함 -1이 펫이 없을 경우 
        
        
        weapons[_weaponType].SetActive(true);
        heroSkins[_gameDataBaseManager._gamePlayData.GetSkinType()].SetActive(true);
        _ikManger.solvers[0] = leftSolver[_weaponType];
        _ikManger.solvers[1] = rightSolver[_weaponType];

    }

    // Update is called once per frame
    void Update()
    {
        if (_igm.GetIsStart())
        {
            ClickInput();
            CollsionCheck();
#if UNITY_EDITOR
            drawCollisionBox();
#endif
            if (_HP <= 0)
            {
                Dead();
            }

            if (_petType >= 0)
            {
                if (_petTimer < 0)
                {
                    int rand = Random.Range(0, 3);
                    ObjectPool.SpawnPoolObj(pets[_petType].name, _petSpawnPos[rand], Quaternion.identity);
                    _petTimer = 3.0f;
                }
                else
                {
                    _petTimer -= Time.deltaTime;
                }
            }

            if (comboPoint >= 30)
            {
                if(!auraEffect.activeSelf)
                    auraEffect.SetActive(true);
            }
            else
            {
                if(auraEffect.activeSelf)
                    auraEffect.SetActive(false);


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
        var spawnBullet =ObjectPool.SpawnPoolObj("Bullet", transform.position + Vector3.up * 0.7f, Quaternion.identity);
        spawnBullet.GetComponent<Bullet>().bulletSprite = bulletSprite[_weaponType];

        _soundManager.PlaySFXOnce(0, 0.3f);
        weaponAnims[_weaponType].Play();
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
        if(maxComboPoint<comboPoint)
            maxComboPoint = comboPoint;
        comboPoint = 0;
    }
    
    public long GetMaxCombo()
    {
        return maxComboPoint;
    }
    public long GetCombo()
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
                    _soundManager.PlaySFXOnce(2, 0.3f);

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
       gameObject.SetActive(false);
    }
}
