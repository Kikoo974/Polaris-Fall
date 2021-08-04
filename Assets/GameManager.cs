using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int highscore;
    // Start is called before the first frame update
    static GameManager _instance;
    public static GameManager managerInstance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindGameObjectWithTag("manager").GetComponent<GameManager>();
                //Tell unity not to destroy this object when loading a new scene!
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null)
        {
            //If I am the first instance, make me the Singleton
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            //If a Singleton already exists and you find
            //another reference in scene, destroy it!
            if (this != _instance)
                Destroy(this.gameObject);
        }
    }

    void Start()
    {
        highscore = PlayerPrefs.GetInt("highscore", highscore);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SaveScore(int score)
    {
        if (score > highscore)
            highscore = score;
        PlayerPrefs.SetInt("highscore", highscore);
    }
}
