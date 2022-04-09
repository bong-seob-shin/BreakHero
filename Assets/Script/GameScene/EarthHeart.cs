using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthHeart : MonoBehaviour
{
    
    private float _disappearTime;

    private static EarthHeart _instance;
    public GameObject[] onHearts;
    private int maxHp = 3;
    private SoundManager _soundManager;

    public static EarthHeart Instance
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

    // Start is called before the first frame update
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        
        _disappearTime = 0f;


        
    }

    private void Start()
    {
        _soundManager = GameObject.FindWithTag("SoundManager").GetComponent<SoundManager>();
    }

    private void Update()
    {
        _disappearTime -= Time.deltaTime;

        if (_disappearTime <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void Hit()
    {
        gameObject.SetActive(true);
        _disappearTime = 1.5f;
        _soundManager.PlaySFXOnce(2, 0.3f);

        if(maxHp>0)
            maxHp -= 1;
        onHearts[maxHp].SetActive(false);
        
    }

    public int GetHP()
    {
        return maxHp;
    }
}
