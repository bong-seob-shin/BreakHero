using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    private AuthManager _authManager;
    private EarthHeart _earthHeart;
    private MonsterSpawner _monsterSpawner;
    private Hero _hero;

    public GameObject gameEndButton;

    public TextMeshProUGUI maxScore;

    public TextMeshProUGUI currentScore;
    // Start is called before the first frame update
    void Start()
    {
        if (_instance == null)
            _instance = this;

        _igm = GameObject.FindWithTag("InGameManager").GetComponent<InGameManager>();
        _hero= Hero.Instance;
        _authManager = AuthManager.Instance;
        _earthHeart = EarthHeart.Instance;
        _monsterSpawner = GameObject.FindWithTag("MonsterSpawner").GetComponent<MonsterSpawner>();
        maxScore.alignment = TextAlignmentOptions.Center;
        
    }

    // Update is called once per frame
    void Update()
    {
        ComboText.text = _hero.GetCombo().ToString() + "  Combo!!";
        
        
        if (_hero.GetHP() <= 0 || _earthHeart.GetHP() <= 0 && !gameEndButton.activeSelf)
        {
            Time.timeScale = 0;
            GameDataBaseManager gameDataBaseManager = GameDataBaseManager.Instance;
            maxScore.text = "MaxCombo         " + gameDataBaseManager._gamePlayData.GetScore();
            currentScore.text = "COMBO             " + _hero.GetMaxCombo();
            if(gameDataBaseManager._gamePlayData.GetScore()<_hero.GetMaxCombo())
                gameDataBaseManager._gamePlayData.SetScore(_hero.GetMaxCombo());

            _monsterSpawner.OffOperator();
            
           gameEndButton.SetActive(true);
        }
    }

    public void OnEndButtonClick()
    {
        Time.timeScale = 1;
        GameDataBaseManager gameDataBaseManager = GameDataBaseManager.Instance;
        gameDataBaseManager.WriteUserInfo();

        gameDataBaseManager.TransactionWriteNewScore(_hero.GetMaxCombo(),gameDataBaseManager._gamePlayData.GetName());
        
        MoveObject.collsionList.Clear();
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1);
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
        GameDataBaseManager gameDataBaseManager = GameDataBaseManager.Instance;
        gameDataBaseManager.WriteUserInfo();

        _authManager.LogOut();
        Application.Quit();
    }

    public void OnReStartButtonClick()
    {
        MoveObject.collsionList.Clear();

        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1);
    }

    
  
}
