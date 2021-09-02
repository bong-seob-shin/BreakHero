using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Button = UnityEngine.UIElements.Button;

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
    public Text ComboText;

    private Hero _hero;
    // Start is called before the first frame update
    void Start()
    {
        if (_instance == null)
            _instance = this;

        _igm = GameObject.FindWithTag("InGameManager").GetComponent<InGameManager>();
        _hero= Hero.Instance;
        

    }

    // Update is called once per frame
    void Update()
    {
        ComboText.text = _hero.GetCombo().ToString() + "  Combo!!";
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
        MoveObject.collsionList.Clear();

        SceneManager.LoadScene(0);
    }

    public void OnStartButtonClick()
    {
        Time.timeScale = 1;

        _igm.SetIsStart(true);
    }
}
