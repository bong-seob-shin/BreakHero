using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorMessage : MonoBehaviour
{
    private float disappearTime;
    // Start is called before the first frame update
    void Start()
    {
        disappearTime = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf)
        {
            disappearTime -= Time.deltaTime;

            if (disappearTime < 0)
            {
                disappearTime = 3.0f;
                gameObject.SetActive(false);
            }
        }
    }
}
