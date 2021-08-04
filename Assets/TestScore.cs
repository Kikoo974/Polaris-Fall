using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestScore : MonoBehaviour
{
    // Start is called before the first frame update
    int i = 0;
    GameManager gm;
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    public void OnClick()
    {
        switch(i)
        {
            case 0:
                gm.SaveScore(1500);
                break;
            case 1:
                SceneManager.LoadScene(0);
                break;
        }
        i++;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
