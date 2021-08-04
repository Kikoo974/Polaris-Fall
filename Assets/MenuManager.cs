using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject ButtonCredit, CreditImage, text;
    [SerializeField] GameObject[] Buttons;
    [SerializeField] Text score;
    int highscore;

    // Start is called before the first frame update

    void Start()
    {
        highscore = PlayerPrefs.GetInt("highscore", highscore);
        score.text = "HighScore = " + highscore;

    }
    // Update is called once per frame
    void Update()
    {
    }
  
    public void Onclick(int num)
    {
        switch(num)
        {
            case 0:
                SceneManager.LoadScene(1);
                break;
            case 1:
                StartCoroutine(MoveButtonLeft());
                break;
            case 2:
                Application.Quit();
                break;
            case 3:
                StartCoroutine(MoveButtonRight());
                break;
        }
    }
    IEnumerator MoveButtonLeft()
    {
        foreach(GameObject g in Buttons)
        {
            g.GetComponent<Button>().interactable = false;
        }
        float X = Buttons[0].transform.position.x;
        while (X -500 < Buttons[0].transform.position.x)
        {
            Buttons[0].transform.position = new Vector2(Buttons[0].transform.position.x - 200 * Time.deltaTime, Buttons[0].transform.position.y);
            text.transform.position = new Vector2( Buttons[0].transform.position.x, text.transform.position.y);
            Buttons[1].transform.position = new Vector2(Buttons[1].transform.position.x - 380 * Time.deltaTime, Buttons[1].transform.position.y);
            Buttons[2].transform.position = new Vector2(Buttons[2].transform.position.x - 520 * Time.deltaTime, Buttons[2].transform.position.y);
            yield return new WaitForEndOfFrame();
        }
        CreditImage.SetActive(true);
       
        ButtonCredit.SetActive(true);
        print("ok");
    }
    IEnumerator MoveButtonRight()
    {
        CreditImage.SetActive(false);
        ButtonCredit.SetActive(false);
        float X = Buttons[0].transform.position.x;
        while (X + 500 > Buttons[0].transform.position.x)
        {
            Buttons[0].transform.position = new Vector2(Buttons[0].transform.position.x + 200 * Time.deltaTime, Buttons[0].transform.position.y);
            text.transform.position = new Vector2(Buttons[0].transform.position.x, text.transform.position.y);
            Buttons[1].transform.position = new Vector2(Buttons[1].transform.position.x + 380* Time.deltaTime, Buttons[1].transform.position.y);
            Buttons[2].transform.position = new Vector2(Buttons[2].transform.position.x + 520 * Time.deltaTime, Buttons[2].transform.position.y);
            yield return new WaitForEndOfFrame();
        }

        foreach (GameObject g in Buttons)
        {
            g.GetComponent<Button>().interactable = true;
        }
        print("ok");
    }

}
