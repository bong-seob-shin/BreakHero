using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Image = UnityEngine.UI.Image;

public class LobbyUIManager : MonoBehaviour
{

    public GameObject backGroundMenu;

    public TMP_InputField idInputField;
   
    public TextMeshProUGUI idText;

    private AuthManager _authManager;
    private GameDataBaseManager _gameDataBaseManager;
    public TextMeshProUGUI myScore;
    public TextMeshProUGUI[] scoreTexts;
    // Start is called before the first frame update

    public Image weaponImage;
    public Sprite[] weaponImageSprites;
    public TextMeshProUGUI weaponText;
    private string[] _weaponTextStrings = {"Hero's Sword", "Hero's Magic Stick", "Hero's Super Glove"};

    
    public Image skinImage;
    public Sprite[] skinImageSprite;
    public TextMeshProUGUI skinText;
    private string[] _skinTextStrings = {"Hero's Cloak", "Hero's Angel Wing", "Hero's Butterfly Wing", "Hero's Dr.st Cloak"};

    public TextMeshProUGUI petText;

    private string[] _petTextStrings = {"No Pet", "Missile Pet",  "Space Ship Pet"};

    public GameObject InputIdObj;

    void Start()
    {
        _authManager = AuthManager.Instance;
        _gameDataBaseManager = GameDataBaseManager.Instance;
        Time.timeScale = 1;
        
       
    }

    // Update is called once per frame
    void Update()
    {
        
        idText.text = "ID : "+_gameDataBaseManager._gamePlayData.GetName();
        
        weaponImage.sprite = weaponImageSprites[_gameDataBaseManager._gamePlayData.GetWeaponType()];
        weaponText.text = _weaponTextStrings[_gameDataBaseManager._gamePlayData.GetWeaponType()];
        
        skinImage.sprite = skinImageSprite[_gameDataBaseManager._gamePlayData.GetSkinType()];
        skinText.text = _skinTextStrings[_gameDataBaseManager._gamePlayData.GetSkinType()];
        
        petText.text = _petTextStrings[_gameDataBaseManager._gamePlayData.GetPetType()];
        if (_gameDataBaseManager.isNewUSer && _gameDataBaseManager.UserId == " ")
        {
            if(!InputIdObj.activeSelf)
                InputIdObj.SetActive(true);
        }
        else
        {
            if(InputIdObj.activeSelf)
                InputIdObj.SetActive(false);
        }
    }
    
    public void OnQuitButtonClick()
    {
        _gameDataBaseManager.WriteUserInfo();
        _authManager.LogOut();
        Application.Quit();
    }

    

    public void OnStartButtonClick()
    {
        _gameDataBaseManager.WriteUserInfo();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);//GameScene
        
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

    public void OnIDInputButtonClick()
    {
        if (idInputField.text.Length > 0)
        {
            

            _gameDataBaseManager.WriteNewUser(idInputField.text);

        }
    }

    public void OnScoreBoardButtonClick()
    {
        myScore.text = "ME : " + _gameDataBaseManager._gamePlayData.GetScore();

        _gameDataBaseManager.ReadScoreBoard();
        
        List < GameDataBaseManager.ScoreLeader > tempLeader = _gameDataBaseManager.leader;
        tempLeader.Sort(delegate(GameDataBaseManager.ScoreLeader a, GameDataBaseManager.ScoreLeader b)
        {
            if (a.score > b.score)
            {
                return -1;
            }
            else if (a.score < b.score)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        });
        for (int i = 0; i < scoreTexts.Length; i++)
        {
            scoreTexts[i].text = (i+1)+"."+_gameDataBaseManager.leader[i].name + " : " + _gameDataBaseManager.leader[i].score;
        }
        
    }
    
    public void OnWeaponChangeButtonClick(int buttonIndex)
    {
        _gameDataBaseManager._gamePlayData.SetWeaponType(buttonIndex);
        weaponImage.sprite = weaponImageSprites[buttonIndex];
        weaponText.text = _weaponTextStrings[buttonIndex];
    }
    
    public void OnSkinChangeButtonClick(int buttonIndex)
    {
        _gameDataBaseManager._gamePlayData.SetSkinType(buttonIndex);
        skinImage.sprite = skinImageSprite[buttonIndex];
        skinText.text = _skinTextStrings[buttonIndex];
    }
    
    public void OnPetChangeButtonClick(int buttonIndex)
    {
        _gameDataBaseManager._gamePlayData.SetPetType(buttonIndex);
        petText.text = _petTextStrings[buttonIndex];
    }
}
