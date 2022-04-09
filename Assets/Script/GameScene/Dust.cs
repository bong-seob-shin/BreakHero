using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dust : MonoBehaviour
{
    private float disableTime;
    private const float CoolDown = 0.5f;
    // Start is called before the first frame update

    private void Start()
    {
        disableTime = CoolDown;
    }

    // Update is called once per frame
    void Update()
    {
        disableTime -= Time.deltaTime;
        if (disableTime < 0)
        {
            disableTime = CoolDown;
            gameObject.SetActive(false);
        }
    }

    
    private void OnDisable()
    {
        ObjectPool.ReturnPoolObj(this.gameObject);
    }
}
