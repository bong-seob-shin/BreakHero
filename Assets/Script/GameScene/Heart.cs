using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    private float _disappearTime;

    private Hero myHero;
    private int maxHp = 3;
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


        for (int i = 0; i < maxHp; i++)
        {
            if (i >= heroHp)
            {
                if (onHearts[i].activeSelf)
                {
                    onHearts[i].SetActive(false);
                }
            }
        }
    }

    private void OnEnable()
    {
        _disappearTime = 1.5f;
    }
}
