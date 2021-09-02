using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    private static InGameManager _instance = null;

    public static InGameManager Instance
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

    private bool _isStart;
    // Start is called before the first frame update
    void Start()
    {
        if (_instance == null)
            _instance = this;

        _isStart = false;

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
