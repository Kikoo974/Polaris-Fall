using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemyprefab, Bonusprefab;
    [SerializeField] GameObject[] Enemy;
    public Transform BoundLeft, BoundRight, BoundBottom;

    public LevelManager LvlMan;
    GameObject[] players;
    int t = 5;
    float x, y;

    float timeAttackPlayer = 10.0f;
    bool attackPlayer = false;
    // Start is called before the first frame update
    void Start()
    {
        LvlMan = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        y = BoundBottom.position.y;
        StartCoroutine(Spawner());
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        timeAttackPlayer -= Time.deltaTime;
        if(timeAttackPlayer < 0)
        {
            attackPlayer = true;

        }
    }
    public void Stop()
    {
        StopAllCoroutines();
    }
    public IEnumerator Spawner()
    {
        t = 5 - Mathf.FloorToInt((LvlMan.Distance / 10000) * 3);
        float temp = (Time.deltaTime * 100000 * Mathf.PI / 384*Time.time);
        Random.InitState((int)Mathf.Floor(temp));
        x = Random.Range(BoundLeft.position.x+1, BoundRight.position.x-1);
        Vector3 posspawn;
        
        if (attackPlayer)
        {
            int c = Random.Range(0, 10);
            if(c>5)
                posspawn = new Vector3(players[0].transform.position.x, (y - 5), 0);
            else
                posspawn = new Vector3(players[1].transform.position.x, (y - 5), 0);
            attackPlayer = false;
            timeAttackPlayer = 10.0f;
                
        }
        else
            posspawn = new Vector3(x, (y - 5), 0);
        int randomchance = Random.Range(1, 6);
        GameObject current;
        if (randomchance ==1)
        {
             current = Instantiate<GameObject>(Bonusprefab, posspawn, new Quaternion(0, 0, 0, 0));
            current.GetComponent<EnemyPlanetScript>().SetScale(1f);
        }
        else
        {
            int i = Random.Range(0, 10);
            if(i>5)
             current = Instantiate<GameObject>(Enemy[0], posspawn, new Quaternion(0, 0, 0, 0));
            else
                current = Instantiate<GameObject>(Enemy[1], posspawn, new Quaternion(0, 0, 0, 0));
            current.GetComponent<EnemyPlanetScript>().SetScale(Random.Range(1f, 3f));
        }
            
        current.GetComponent<EnemyPlanetScript>().speed = Random.Range(1f, 10f / t);
        
        yield return new WaitForSeconds(t);
        StartCoroutine(Spawner());
    }
}
