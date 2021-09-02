using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    private float _disappearTime;

    private Hero myHero;

    public GameObject[] onHearts;
    // Start is called before the first frame update
    void Start()
    {
        _disappearTime = 1.5f;
        myHero  = Hero.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        _disappearTime -= Time.deltaTime;

        if (_disappearTime < 0)
        {
            _disappearTime = 1.5f;
            gameObject.SetActive(false);
        }

        int heroHp = myHero.GetHP();


        if (onHearts[heroHp].activeSelf)
        {
            onHearts[heroHp].SetActive(false);
        }
    }

    private void OnEnable()
    {
        _disappearTime = 1.5f;
    }
}
