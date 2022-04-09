using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dust : MonoBehaviour
{
    private float disableTime;
    // Start is called before the first frame update

    private void Start()
    {
        disableTime = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        disableTime -= Time.deltaTime;
        if (disableTime < 0)
        {
            disableTime = 0.5f;
            gameObject.SetActive(false);
        }
    }

    
    private void OnDisable()
    {
        ObjectPool.ReturnPoolObj(this.gameObject);
    }
}
