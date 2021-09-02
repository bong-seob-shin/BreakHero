using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance = null;

    public static UIManager Instance
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

    private InGameManager _igm;
    
    public Button menuButton;
    public GameObject backGroundMenu;
    
    
    // Start is called before the first frame update
    void Start()
    {
        if (_instance == null)
            _instance = this;

        _igm = InGameManager.Instance;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMenuButtonClick()
    {
        if (backGroundMenu.activeSelf)
        {
            backGroundMenu.SetActive(false);
            Time.timeScale = 1;
            _igm.SetIsStart(true);

        }
        else
        {
            backGroundMenu.SetActive(true);
            Time.timeScale = 0;
            _igm.SetIsStart(false);
        }
    }

    public void OnQuitButtonClick()
    {
        Application.Quit();
    }

    public void OnResumeButtonClick()
    {
        //backGroundMenu.SetActive(false);
    }

    public void OnStartButtonClick()
    {
        _igm.SetIsStart(true);
    }
}
