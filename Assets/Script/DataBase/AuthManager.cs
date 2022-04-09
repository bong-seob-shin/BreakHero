using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Firebase.Auth;
using UnityEngine;

public class AuthManager : MonoBehaviour
{
    private static AuthManager _instance = null;
    private FirebaseAuth _auth;
    private int _isLogin;
    private int _isJoin;
    public static AuthManager Instance
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
    

    // Start is called before the first frame update
    void Awake()
    {
        if (_instance== null)
        {
            _instance = this;
        }
        
        _auth = FirebaseAuth.DefaultInstance;
        _isJoin = -1; //비동기 방식에서 입력이 없을 때 -1로 두기로함 false 0 true 1
        _isLogin = -1;
        DontDestroyOnLoad(this);
        
    }

    public void Join(string email, string password)
    {
        _auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(
            task =>
            {
                if (!task.IsCanceled && !task.IsFaulted)
                {
                    Debug.Log(email + "로 회원가입 되셨습니다.");
                    _isJoin = 1;
                }
                else
                {
                    Debug.Log("회원가입에 실패하셨습니다.");
                    _isJoin = 0;
                    //이메일 중복, 이메일 형식 불량, 비밀번호 6자리 미만 etc..
                }
                
                
            }
        );
    }

    public void Login(string email, string password)
    {
        _auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(
            task =>
            {
                if (task.IsCompleted && !task.IsFaulted && !task.IsCanceled)
                {
#if UNITY_EDITOR
                    Debug.Log(email + "로 로그인 하셨습니다");
#endif
                    _isLogin = 1;
                }
                else
                {
#if UNITY_EDITOR
                    Debug.Log("로그인에 실패하셨습니다.");

#endif
                    _isLogin = 0;
                    
                }
            }
        );

    }

    public int GetIsLogin()
    {
        return _isLogin;
    }

    public int GetIsJoin()
    {
        return _isJoin;
    }

    public void InitIsLogin()
    {
        _isLogin = -1;
    }

    public void InitIsJoin()
    {
        _isJoin = -1;
    }

    public void LogOut()
    {
        _auth.SignOut();
    }

    public void CheckLogIn()
    {
        Firebase.Auth.FirebaseUser user = _auth.CurrentUser;
        if (user != null)
        {
            Debug.Log(user.Email+ "    "+ user.UserId);
        }
    }

    public string GetUId()
    {
        Firebase.Auth.FirebaseUser user = _auth.CurrentUser;

        return user.UserId;
    }
}