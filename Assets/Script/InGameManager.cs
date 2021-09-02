using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameManager : MonoBehaviour
{
    private static InGameManager _instance = null;

    private Hero _hero;
    private EarthHeart _earthHeart;
  
    private bool _isStart;
    // Start is called before the first frame update
    void Start()
    {
        if (_instance == null)
            _instance = this;

        _isStart = false;
        _hero = Hero.Instance;
        _earthHeart = EarthHeart.Instance;
    }

    private void Update()
    {
        if (_hero.GetHP() <= 0 || _earthHeart.GetHP() <= 0)
        {
            MoveObject.collsionList.Clear();
            
            SceneManager.LoadScene(0);
        }
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
