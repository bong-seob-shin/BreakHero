using System;
using System.Collections;
using System.Collections.Generic;
using Firebase.Database;
using Firebase.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameDataBaseManager : MonoBehaviour
{
    private static GameDataBaseManager _instance = null;

    public static GameDataBaseManager Instance
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
    
    public class User
    {
        public string name;
        public long score;
        public long weaponType;
        public long skinType;
        public long petType;
        public User() { }

        public User(string name, long score, long weaponType = 0, long skinType =0, long petType =0)
        {
            this.name = name;
            this.score = score;
            this.weaponType = weaponType;
            this.skinType = skinType;
            this.petType = petType;

        }

        public void SetInfo(string name, long score,  long skinType = 0,long weaponType = 0, long petType =0)
        {
            this.name = name;
            this.score = score;
            this.weaponType = weaponType;
            this.skinType = skinType;
            this.petType = petType;
        }

        public string GetName()
        {
            return name;
        }

        public void SetScore(long score)
        {
            this.score = score;
        }
        public long GetScore()
        {
            return score;
        }

        public long GetWeaponType()
        {
            return weaponType;
        }

        public long GetSkinType()
        {
            return skinType;
        }

        public void SetWeaponType(long typeNum)
        {
            this.weaponType = typeNum;
        }

        public void SetSkinType(long typeNum)
        {
            this.skinType = typeNum;
        }

        public long GetPetType()
        {
            return petType;
        }

        public void SetPetType(long typeNum)
        {
            this.petType = typeNum;
        }

      
    }

    public class ScoreLeader
    {
        public string name;
        public long score;

        public ScoreLeader() { }

        public ScoreLeader(string name, long score)
        {
            this.name = name;
            this.score = score;
        }
    }

    private DatabaseReference _reference;

    public bool isNewUSer;


    public string UserId;

    public long UserScore;

    public List<ScoreLeader> leader;
    

    public User _gamePlayData;
    // Start is called before the first frame update
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
            isNewUSer = false;
            _reference = FirebaseDatabase.DefaultInstance.RootReference;
            UserScore = 0;
            UserId = " ";
            LoadUserInfoFromDataBase();
            leader = new List<ScoreLeader>();
            _gamePlayData = new User();
            ReadScoreBoard();
        }
        else
        {
            Destroy(this.gameObject);
        }

        
    }
    
    

    public void WriteNewUser(string name)
    {
        _gamePlayData.SetInfo(name,0,0);
       
        string json = JsonUtility.ToJson(_gamePlayData);
        AuthManager authManager = AuthManager.Instance;

        _reference.Child("Player").Child(authManager.GetUId()).SetRawJsonValueAsync(json);
        UserId = name;
        isNewUSer = false;
    }

    public void WriteUserInfo()
    {
        
        string json = JsonUtility.ToJson(_gamePlayData);
        AuthManager authManager = AuthManager.Instance;

        _reference.Child("Player").Child(authManager.GetUId()).SetRawJsonValueAsync(json);
        isNewUSer = false;
    }
    
    public void TransactionUpdateScoreCheck(long score, string userName)
    {
        bool isUpdate = false;
        _reference.Child("ScoreLeader").RunTransaction(data =>
        {
            List<object> users = data.Value as List<object>;

            if (users == null)
            {
                users = new List<object>();
            }
            else
            {
                object sameNameVal = null;

                foreach (var child in users)
                {
                    if (!(child is Dictionary<string, object>))
                        continue;
                    long childScore = (long) (((Dictionary<string, object>) child)["score"]);
                    string childName = (string) (((Dictionary<string, object>) child)["name"]);

                    Debug.Log(childName +"cname" + userName +"uname");
                    if (childName == userName)
                    {
                        sameNameVal = child;
                        isUpdate = true;
                        if (childScore > score)
                        {
                            score = childScore;
                        }
                        
                        break;

                    }
                }

                if(!isUpdate)
                {
                    return TransactionResult.Abort(); // 이름이 다르면 중단
                }
                
                users.Remove(sameNameVal);
            }

            Dictionary<string, object> newScoreDictionary = new Dictionary<string, object>();

            newScoreDictionary["score"] = score;
            newScoreDictionary["name"] = userName;

            users.Add(newScoreDictionary);
            data.Value = users;
            return TransactionResult.Success(data);
        });
        
    }
    
    
    public void TransactionWriteNewScore(long score, string userName)
    {

        const int maxRecordCount = 5;
        _reference.Child("ScoreLeader").RunTransaction(data =>
        {
            List<object> users = data.Value as List<object>;

            if (users == null)
            {
                users = new List<object>();
            }
            else if (data.ChildrenCount >= maxRecordCount)
            {
                long minScore = long.MaxValue;
                object minVal = null;
                foreach (var child in users)
                {
                    if (!(child is Dictionary<string, object>))
                        continue;
                    long childScore = (long) (((Dictionary<string, object>) child)["score"]);

                    if (childScore < minScore)
                    {
                        minScore = childScore;
                        minVal = child;
                    }
                }

                if (minScore > score)
                {
                    return TransactionResult.Abort(); //내점수가 랭킹에 오르지 못하면 중단
                }

                users.Remove(minVal);
            }

            Dictionary<string, object> newScoreDictionary = new Dictionary<string, object>();

            newScoreDictionary["score"] = score;
            newScoreDictionary["name"] = userName;

            users.Add(newScoreDictionary);
            data.Value = users;
            return TransactionResult.Success(data);
            
        });

    }

    private void LoadUserInfoFromDataBase()
    {       
        AuthManager authManager = AuthManager.Instance;
        DatabaseReference r = FirebaseDatabase.DefaultInstance.GetReference("Player");
        
        r.GetValueAsync().ContinueWith(task =>
        {
           
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                foreach (DataSnapshot data in snapshot.Children)
                {
                    string key = data.Key;
                    IDictionary userData = (IDictionary)data.Value;

                    if(authManager.GetUId() != key)
                    {
                        isNewUSer = true; //데이터베이스에 등록되지 않은 유저인지 판별
                    }
                    else//등록되어있으면 데이터를 불러옴
                    {

                        UserId = (string)userData["name"];
                        UserScore = (long)userData["score"]; //int64의 반환값으로 long으로 캐스팅하여 사용
                        isNewUSer = false;
                        _gamePlayData.SetInfo((string)userData["name"],(long)userData["score"],
                            (long)userData["skinType"], (long)userData["weaponType"],(long)userData["petType"]);

                        break;
                    }
                    
                }
                


            }
            
        });

    }
    
    public void ReadScoreBoard()
    {       
        
        DatabaseReference r = FirebaseDatabase.DefaultInstance.GetReference("ScoreLeader");
        
        r.GetValueAsync().ContinueWith(task =>
        {
           
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                leader.Clear();

                foreach (DataSnapshot data in snapshot.Children)
                {
                    IDictionary userData = (IDictionary)data.Value;

                    
                    leader.Add(new ScoreLeader((string)userData["name"] ,(long)userData["score"]));
                        
                }

            }
            
        });
        
    }
}
