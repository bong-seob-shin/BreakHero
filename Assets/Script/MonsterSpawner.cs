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

    public GameObject monster;
    // Start is called before the first frame update
    void Start()
    {
        _spawnTime = 3;
        _currentSpawnTime = _spawnTime;
        _spawnPoint.Add(new Vector3(-1.7f, 4.0f, 0));
        _spawnPoint.Add(new Vector3(0.0f, 4.0f, 0));
        _spawnPoint.Add(new Vector3(1.7f, 4.0f, 0));

    }

    // Update is called once per frame
    void Update()
    {
        _currentSpawnTime -= Time.deltaTime;

        if (_currentSpawnTime < 0)
        {
            _currentSpawnTime = _spawnTime;
            int rand  = Random.Range(0, 2);
            Instantiate(monster, _spawnPoint[1], quaternion.identity);
        }
    }
}
