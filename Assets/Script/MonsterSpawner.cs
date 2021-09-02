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

    private int _monsterWave;

    private bool _isOperate;
    // Start is called before the first frame update
    void Start()
    {
        _spawnTime = 3;
        _currentSpawnTime = _spawnTime;
        _spawnPoint.Add(new Vector3(-1.7f, 6.0f, 0));
        _spawnPoint.Add(new Vector3(0.0f, 6.0f, 0));
        _spawnPoint.Add(new Vector3(1.7f, 6.0f, 0));
        _isOperate = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isOperate)
        {
            _currentSpawnTime -= Time.deltaTime;

            if (_currentSpawnTime < 0 && _monsterWave < 10)
            {
                _currentSpawnTime = _spawnTime;
                int rand = 1;

                int monsterType = Random.Range(0, 100);
                if (monsterType <70)
                {
                    rand = Random.Range(0, 2);
                }

                Instantiate(monster[monsterType], _spawnPoint[rand], quaternion.identity);
                _monsterWave++;
            }

            if (_monsterWave >= 10)
            {
                Instantiate(jupiter, new Vector3(0.0f, 12.0f, 0.0f), quaternion.identity);
                _isOperate = false;
            }
        }
    }
}
