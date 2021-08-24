using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResolution : MonoBehaviour
{
    public static Vector3 screenLeftBottom;
    public static Vector3 screenRightTop; 
    private void Awake()
    {
        Camera camera = GetComponent<Camera>();

        screenLeftBottom = camera.ScreenToWorldPoint(new Vector3(0,0,0));
        screenRightTop = camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        
        Rect rect = camera.rect;


        float screenHeight = ((float) Screen.width / Screen.height) / (9.0f / 16.0f);
        float screenWidth = 1f / screenHeight;

        if (screenHeight < 1)
        {
            rect.height = screenHeight;
            rect.y = (1 - screenHeight) / 2f;
        }
        else
        {
            rect.width = screenWidth;
            rect.x = (1 - screenWidth) / 2f; 
        }

        camera.rect = rect;
    }
}
