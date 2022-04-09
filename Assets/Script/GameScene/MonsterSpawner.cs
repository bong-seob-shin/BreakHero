using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class MonsterSpawner : MonoBehaviour
{
    private float _spawnTime;
    private float _currentSpawnTime;

    private List<Vector3> _spawnPoint = new List<Vector3>();

    public GameObject[] monster;

    public GameObject jupiter;
    public GameObject saturn;
    public GameObject sun;

    private int _monsterWave;

    [SerializeField]
    private bool _isOperate;

    private InGameManager _igm;

    // Start is called before the first frame update
    void Start()
    {
        _spawnTime = 3;
        _currentSpawnTime = _spawnTime;
        _spawnPoint.Add(new Vector3(-1.7f, 6.0f, 0));
        _spawnPoint.Add(new Vector3(0.0f, 6.0f, 0));
        _spawnPoint.Add(new Vector3(1.7f, 6.0f, 0));
        _isOperate = true;
        
        _igm = GameObject.FindWithTag("InGameManager").GetComponent<InGameManager>();


    }

    // Update is called once per frame
    void Update()
    {
        if (_igm.GetIsStart())
        {

            if (_isOperate)
            {
                _currentSpawnTime -= Time.deltaTime;

                if (_currentSpawnTime < 0)
                {
                    _currentSpawnTime = _spawnTime;
                    int rand = 1;

                    int monsterRand = Random.Range(0, 100);
                    int monsterType = 0;
                    if (monsterRand < 70)
                    { 
                        rand = Random.Range(0, 3);
                        monsterType = 0;
                        ObjectPool.SpawnPoolObj(monster[monsterType].name, _spawnPoint[rand], quaternion.identity);

                    }
                    else
                    {
                        monsterType = 1;
                        var leftWing= ObjectPool.SpawnPoolObj(monster[monsterType].transform.GetChild(0).name, _spawnPoint[0], quaternion.identity);
                        var body =ObjectPool.SpawnPoolObj(monster[monsterType].transform.GetChild(1).name, _spawnPoint[1], quaternion.identity);
                        var rightWing =ObjectPool.SpawnPoolObj(monster[monsterType].transform.GetChild(2).name, _spawnPoint[2], quaternion.identity);

                        leftWing.GetComponent<Satellite>().chaneObject = body;
                        rightWing.GetComponent<Satellite>().chaneObject = body;
                        body.GetComponent<Satellite>().chaneObject = null;

                    }

                    _monsterWave++;
                }
            }
            if (_monsterWave == 10)
            {
                Instantiate(jupiter, new Vector3(0.0f, 12.0f, 0.0f), quaternion.identity);
                _monsterWave++;
                _isOperate = false;
            }

            if (_monsterWave == 20)
            {
                Instantiate(saturn, new Vector3(0.0f, 8.0f, 0.0f), quaternion.identity);
                _monsterWave++;
                _isOperate = false;
            }
            
            if (_monsterWave == 30)
            {
                Instantiate(sun, new Vector3(0.0f, 8.0f, 0.0f), quaternion.identity);
                _monsterWave++;
                _isOperate = false;
            }
        }
    }


    public void OnOperator()
    {
        _isOperate = true;
        _currentSpawnTime = _spawnTime;

    }

    public void OffOperator()
    {
        _isOperate = false;
    }
}
