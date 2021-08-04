using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public float Distance = 0;
    int lifep1 = 3, lifep2 = 3;
    [SerializeField] GameObject[] lifeUIp1;
    [SerializeField] GameObject[] lifeUIp2;
    [SerializeField] GameObject ecranGameOver, cam, planete, ecranGameClear, control, vie, reach, pause, blackHole;
    GameManager gameManager;
    [SerializeField] Text scoreText;
    AudioSource[] audios;
    AudioSource theme, win, lose, lifeUp;
    [SerializeField] GameObject[] persos;
    [SerializeField] EnemySpawner en;
    bool end = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Debut());

       
        scoreText.text = "O AU";
        audios = gameObject.GetComponents<AudioSource>();
        Debug.Log(audios);
        theme = audios[0];
        win = audios[1];
        lose = audios[2];
        lifeUp = audios[3];
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    

    // Update is called once per frame
    void Update()
    {
        
        
        if(Distance >= 5000 )
        {
            if(!end)
            StartCoroutine(GameClear());
        }
        else
        {
            if (!end)
            {
                Distance += 50f * Time.deltaTime;
                scoreText.text = "" + (int)Distance + " UA";
            }
        }
        //win
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            pause.SetActive(true);
        }


    }
    public void LifeMinus(int player)
    {
        if(player == 0)
        {
            lifep1--;
            lifeUIp1[lifep1].SetActive(false);
        }

        else if (player == 1)
        {
            lifep2--;
            lifeUIp2[lifep2].SetActive(false);
        }
        if (lifep1 <= 0 || lifep2 <= 0)
            StartCoroutine(GameOver());
    }
    public void LifeUp(int player, GameObject lifePower)
    {
        if (player == 0 && lifep1<3)
        {

            lifeUIp1[lifep1].SetActive(true);
            lifep1++;
            lifeUp.Play();
            Destroy(lifePower);

        }

        if (player == 1 && lifep2<3)
        {

            lifeUIp2[lifep2].SetActive(true);
            lifep2++;
            lifeUp.Play();
            Destroy(lifePower);

        }
    }
 
    public void OnCLick(int i)
    {

        switch (i)
        {

            case 0:
                SceneManager.LoadSceneAsync(1);
                Time.timeScale = 1;

                break;
            case 1:
                Time.timeScale = 1;
                SceneManager.LoadScene(0);
                break;
            case 2:
                pause.SetActive(false);
                Time.timeScale = 1;
                break;
        }
    }
    IEnumerator GameClear()
    {
        theme.Stop();
        win.Play();
        control.SetActive(false);
        vie.SetActive(false);
        end = true;
        while (planete.transform.position.y < cam.transform.position.y - 6)
        {
            planete.transform.position = new Vector2(planete.transform.position.x, planete.transform.position.y + Time.deltaTime*5);
            yield return new WaitForEndOfFrame();
        }
        Time.timeScale = 0;
        ecranGameClear.SetActive(true);

    }
    IEnumerator GameOver()
    {
        en.Stop();
        foreach(GameObject b in persos)
        {
            b.GetComponent<CharacterScript>().enabled = false;
            b.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            b.GetComponent<Rigidbody2D>().constraints = new RigidbodyConstraints2D(); 
            b.GetComponent<Collider2D>().enabled = false;
            b.transform.parent = null;
        }
        Instantiate(blackHole, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
      
        while ((persos[0].transform.position.x > 0.1 || persos[0].transform.position.x<-0.1) 
            && ( persos[1].transform.position.x >0.1 || persos[1].transform.position.x < -0.1)
            && (persos[0].transform.position.y > 0.1 || persos[0].transform.position.y < -0.1)
            && (persos[1].transform.position.y > 0.1 || persos[1].transform.position.y < -0.1))
        {

            persos[0].transform.position = Vector3.Lerp(persos[0].transform.position, new Vector3(0, 0, 0), Time.deltaTime);
            persos[1].transform.position = Vector3.Lerp(persos[1].transform.position, new Vector3(0, 0, 0), Time.deltaTime );
            yield return new WaitForEndOfFrame();
            Debug.Log("ok");
        }
        yield return new WaitForSeconds(2f);
        lose.Play();
        ecranGameOver.SetActive(true);
        Time.timeScale = 0;
        gameManager.SaveScore((int)Distance);
        Debug.Log("dead");
    }
    IEnumerator Debut()
    {
        Debug.Log("REACH");
        yield return new WaitForSeconds(3f);
        reach.SetActive(false);
    }
}
