using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyUIManager : MonoBehaviour
{

    public GameObject backGroundMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnQuitButtonClick()
    {
        Application.Quit();
    }

    

    public void OnStartButtonClick()
    {
        
        SceneManager.LoadScene(1);//GameScene
        
    }
    
    public void OnMenuButtonClick()
    {
        if (backGroundMenu.activeSelf)
        {
            backGroundMenu.SetActive(false);

        }
        else
        {
            backGroundMenu.SetActive(true);
        }
    }
}
