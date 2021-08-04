using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlanetScript : MonoBehaviour
{
    public float speed = 1f;
    public float speedconstant = 10f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Avancée
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        GetComponent<Rigidbody2D>().angularVelocity = 0;
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, speed*speedconstant *Time.deltaTime*100));

    }

    public void SetScale(float multiplier)
    {
        transform.localScale*= multiplier; 
    }
}
