using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameManager : MonoBehaviour
{
    private static InGameManager _instance = null;

    private Hero _hero;
  
    private bool _isStart;
    // Start is called before the first frame update
    void Start()
    {
        if (_instance == null)
            _instance = this;

        _isStart = true;
        _hero = Hero.Instance;
    }

    private void Update()
    {
     
    }

    public bool GetIsStart()
    {
        return _isStart;
    }

    public void SetIsStart(bool isStart)
    {
        _isStart = isStart;
    }
}
