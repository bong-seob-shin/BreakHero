using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginUIManager : MonoBehaviour
{

    private AuthManager _authManager;
    
    public TMP_InputField loginIdInputField;
    public TMP_InputField loginPasswordInputField;
    
    public TMP_InputField joinIdInputField;
    public TMP_InputField joinPasswordInputField;
    
    public TextMeshProUGUI loginErrorMessage;
    private bool _isLoginButtonClicked;
    public TextMeshProUGUI joinErrorMessage;
    private bool _isJoinButtonClicked;
    
    public GameObject backGroundMenu;

    private void Start()
    {
        _authManager = AuthManager.Instance;
        

        _isJoinButtonClicked = false;
        _isLoginButtonClicked = false;
    }

    private void Update()
    {
        if (_isJoinButtonClicked)
        {
            int isJoinNum = _authManager.GetIsJoin();
            if (isJoinNum == 0)
            {
                joinErrorMessage.text = "JOIN ERROR";
                joinErrorMessage.color = Color.red;
                joinErrorMessage.gameObject.SetActive(true);
                _isJoinButtonClicked = false;
                _authManager.InitIsJoin();
                
            }
            else if (isJoinNum == 1)
            {
                joinErrorMessage.text = "JOIN SUCCESS";
                joinErrorMessage.color = Color.blue;
                joinErrorMessage.gameObject.SetActive(true);
                _isJoinButtonClicked = false;
                _authManager.InitIsJoin();
                _authManager.CheckLogIn();


            }
        }

        if (_isLoginButtonClicked)
        {
            int isLoginNum = _authManager.GetIsLogin();

            if (isLoginNum == 0)
            {
                loginErrorMessage.text = "LOGIN ERROR";
                loginErrorMessage.color = Color.red;
                loginErrorMessage.gameObject.SetActive(true);
                _isLoginButtonClicked = false;
                _authManager.InitIsLogin();

            }
            else if (isLoginNum == 1)
            {
                Debug.Log("로그인됌");
                _isLoginButtonClicked = false;
                _authManager.InitIsLogin();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

            }
        }
    }

    public void OnLoginButtonClick()
    {
        _authManager.Login(loginIdInputField.text, loginPasswordInputField.text);
        _isLoginButtonClicked = true;
    }

    public void OnJoinButtonClick()
    {
        _authManager.Join(joinIdInputField.text, joinPasswordInputField.text);
        _isJoinButtonClicked = true;
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

    public void OnQuitButtonClick()
    {
        
        _authManager.LogOut();

        Application.Quit();
    }
    
  
}
