 using System;
 using System.Collections;
using System.Collections.Generic;
 using System.Runtime.CompilerServices;
 using UnityEditor;
 using UnityEngine;

public class ObjectPool : MonoBehaviour
{

    private static ObjectPool _instance = null;
   

    public static ObjectPool Instance
    {
        get
        {
            if (_instance == null)
            {
                return null;
            }

            return _instance;

        }
    }

    [Serializable]
    public class PoolObject
    {
        public string objName;
        public GameObject go;
        public int size;
    }

    [SerializeField] private PoolObject[] _poolObjs;
    
    private Dictionary<string, Queue<GameObject>> _poolDictionary;
    
    // Start is called before the first frame update
    void Start()
    {
        if (_instance == null)
        {
            _instance = this;
        }

        _poolDictionary = new Dictionary<string, Queue<GameObject>>();
        
        for (int i = 0; i < _poolObjs.Length; i++)
        {
            resistPoolObj(_poolObjs[i]);
        }
   
    }

    public void resistPoolObj(PoolObject pObj)
    {
        
        if(_poolDictionary.ContainsKey(pObj.objName))
        {
            return;
        }
        else
        {
            Queue<GameObject> poolObjQueue = new Queue<GameObject>();
            _poolDictionary.Add(pObj.objName, poolObjQueue);

            for (int i = 0; i < pObj.size; i++)
            {
                CreatePoolObj(pObj.objName,pObj.go);
            }
            
        }
        
    }

    public PoolObject SearchPoolObj(string tag)
    {
        for (int i = 0; i < _poolObjs.Length; i++)
        {
            if (_poolObjs[i].objName == tag)
            {
                return _poolObjs[i];
            }
        }

        return null;
    }
    private GameObject CreatePoolObj(string tag, GameObject prefab)
    {
        var obj = Instantiate(prefab, transform);
        obj.name = tag;
        obj.SetActive(false);
        return obj;
    }

    public static void ReturnPoolObj(GameObject poolGo)
    {
        if (_instance._poolDictionary.ContainsKey(poolGo.name))
        {
            _instance._poolDictionary[poolGo.name].Enqueue(poolGo);
        }
        else
        {
            Destroy(poolGo);
        }
    }
    public static GameObject SpawnPoolObj(string tag, Vector3 position, Quaternion rotation)
    {
        if (!_instance._poolDictionary.ContainsKey(tag))
        {
            return null;
        }
        if (_instance._poolDictionary[tag].Count <= 0)
        {
            PoolObject pool = _instance.SearchPoolObj(tag);

            if (pool != null)
            {
                var obj = _instance.CreatePoolObj(pool.objName,pool.go);
            }
        }
        GameObject poolGo = _instance._poolDictionary[tag].Dequeue();
        poolGo.transform.position = position;
        poolGo.transform.rotation = rotation;
        poolGo.SetActive(true);

        return poolGo;
    }
  
}
