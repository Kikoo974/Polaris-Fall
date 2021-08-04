using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterScript : MonoBehaviour
{
    public Transform OtherCharacter;
    public bool parent = false;
    public bool left = false;
    public bool attraction = true;
    public bool plusfacingother = false;

    float magnetspeedconst = 150f;

    public int player = 0;

    bool immune = false;
    bool switched = false;

    [SerializeField] Sprite[] changeImage;
    [SerializeField] Image Q, S, D, Left, Down, Right, Switch;
    [SerializeField] LevelManager levelManager;
    public bool outofbounds = false;
    Transform Bup, Bdown, Bleft, Bright;
    AudioSource[] audio;
    AudioSource hit, touch;

    [SerializeField] Sprite[] anim;
    SpriteRenderer perso;
    Sprite currentSprite;
    bool isSpriteTouch = false;
    // Start is called before the first frame update
    void Start()
    {
        if (name.Contains("2"))
            left = true;
        if (left)
            plusfacingother = true;
        if (GetComponent<Transform>().parent != null)
        {
            OtherCharacter = GetComponent<Transform>().parent;
        
        }
        else
        {
            OtherCharacter = GetComponent<Transform>().GetChild(0);
            parent = true;
        }
        Bup = GameObject.Find("Bound1").transform;
        Bdown = GameObject.Find("Bound B").transform;
        Bleft = GameObject.Find("Bound L").transform;
        Bright = GameObject.Find("Bound R").transform;
        audio = GetComponents<AudioSource>();
        hit = audio[0];
        touch = audio[1];
        perso = GetComponent<SpriteRenderer>();
    }
          
           

    // Update is called once per frame
    void Update()
    {
        float time = Time.deltaTime;
        //GetAttraction
        if ((plusfacingother && !OtherCharacter.GetComponent<CharacterScript>().plusfacingother) || (!plusfacingother && OtherCharacter.GetComponent<CharacterScript>().plusfacingother))
            attraction = true;
        else if ((plusfacingother && OtherCharacter.GetComponent<CharacterScript>().plusfacingother) || (!plusfacingother && !OtherCharacter.GetComponent<CharacterScript>().plusfacingother))
                attraction = false;

        if (transform.position.x > Bright.position.x - 2 || transform.position.x < Bleft.position.x + 2 || transform.position.y < Bdown.position.y + 2 || transform.position.y > Bup.position.y - 2)
        {
            outofbounds = true;
            Debug.Log("WORKS");
        }
        else
            outofbounds = false;

        //rotfix
        if (!parent)
        {
            transform.localRotation = new Quaternion(0, 0, 0, 0);
            transform.localPosition = new Vector3(transform.localPosition.x, 0, 0);
        }


        //Passive Effects
        //Attraction
        float magnetspeed = (1 / vectorlength(VectorFromTwoPos(transform.position, OtherCharacter.position)) + 0.01f) ;
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        GetComponent<Rigidbody2D>().angularVelocity = 0;
        if (attraction && !parent)
        {

            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            GetComponent<Rigidbody2D>().angularVelocity = 0;
            GetComponent<Rigidbody2D>().AddForce(-VectorFromTwoPos(transform.position, OtherCharacter.position) * magnetspeed * magnetspeed * magnetspeedconst * time * 400);

        }

        else if (!attraction && !parent && !outofbounds)
        {
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            GetComponent<Rigidbody2D>().angularVelocity = 0;
            GetComponent<Rigidbody2D>().AddForce(VectorFromTwoPos(transform.position, OtherCharacter.position) * magnetspeed * magnetspeed * magnetspeedconst * time * 400);
        }
        //Getoutofbounds
        if (outofbounds && !OtherCharacter.GetComponent<CharacterScript>().outofbounds && !attraction && !parent)
            GetComponent<Rigidbody2D>().AddForce(-VectorFromTwoPos(transform.position, OtherCharacter.position));
        else if (outofbounds && OtherCharacter.GetComponent<CharacterScript>().outofbounds && !parent)
            GetComponent<Rigidbody2D>().AddForce(-VectorFromTwoPos(transform.position, new Vector2(0, 0)));










        //Controls
        //Attraction
        if (left && !parent)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                perso.sprite = anim[1];
                gameObject.transform.localScale = new Vector2(transform.localScale.x *-1, transform.localScale.y);
                if (plusfacingother == true)
                {
                    plusfacingother = false;
                }
                else if (plusfacingother == false)
                {
                    plusfacingother = true;
                }
            }
        }
        else if (!left && !parent)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                perso.sprite = anim[1];
                gameObject.transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
                if (plusfacingother == true)
                {
                    plusfacingother = false;

                }
                else if (plusfacingother == false)
                {
                    plusfacingother = true;
                }
            }
        }









        //Rotation
        if (left && parent && !OtherCharacter.GetComponent<CharacterScript>().outofbounds)
        {
            if (Input.GetKey(KeyCode.Q))
            {
                //Rotate Left
                transform.Rotate(new Vector3(0, 0, -80f * time));
            }
            else if (Input.GetKey(KeyCode.D))
            {
                //Rotate Right
                transform.Rotate(new Vector3(0, 0, +80f * time));
            }
        }
        else if (!left && parent && !OtherCharacter.GetComponent<CharacterScript>().outofbounds)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                //Rotate Left
                transform.Rotate(new Vector3(0, 0, -80f * time));
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                //Rotate Right
                transform.Rotate(new Vector3(0, 0, +80f * time));
            }
        }








        //Switch Parent
        if (Input.GetKeyDown(KeyCode.Space) )
        {
            if (!isSpriteTouch)
            {
                if (left)
                {
                    if (parent)
                        perso.sprite = anim[1];
                    else
                        perso.sprite = anim[0];
                }
                else
                {
                    if (parent)
                        perso.sprite = anim[0];
                    else
                        perso.sprite = anim[1];
                }
            }
            else
            {
                if (left)
                {
                    if (parent)
                        currentSprite = anim[1];
                    else
                        currentSprite = anim[0];
                }
                else
                {
                    if (parent)
                        currentSprite = anim[0];
                    else
                        currentSprite = anim[1];
                }
            }
          

            if (switched == false && left)
                SwitchParent();
           

        }
        switched = false;
    }
    private void FixedUpdate()
    {
        
    }

    public void SwitchParent()
    {

        if (parent == true && switched == false)
        {
           
            Q.sprite = changeImage[1];
            S.sprite = changeImage[2];
            D.sprite = changeImage[5];
            Left.sprite = changeImage[6];
            Down.sprite = changeImage[9];
            Right.sprite = changeImage[10];
            Switch.sprite = changeImage[12];

            transform.DetachChildren();
            transform.SetParent(OtherCharacter);

            //rigidbody stuff
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            GetComponent<Rigidbody2D>().freezeRotation = true;

            
            OtherCharacter.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
            OtherCharacter.GetComponent<Rigidbody2D>().freezeRotation = false;


            parent = false;
            OtherCharacter.GetComponent<CharacterScript>().parent = true;
            switched = true;
        }
        else
        {
           

            Q.sprite = changeImage[0];
            S.sprite = changeImage[3];
            D.sprite = changeImage[4];
            Left.sprite = changeImage[7];
            Down.sprite = changeImage[8];
            Right.sprite = changeImage[11];

            Switch.sprite = changeImage[13];

            OtherCharacter.transform.DetachChildren();
            OtherCharacter.transform.SetParent(transform);

            //rigidbody stuff
            OtherCharacter.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            OtherCharacter.GetComponent<Rigidbody2D>().freezeRotation = true;


            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
            GetComponent<Rigidbody2D>().freezeRotation = false;

            if (OtherCharacter.GetComponent<CharacterScript>().attraction == false)
                attraction = false;
            else
                attraction = true;
            OtherCharacter.GetComponent<CharacterScript>().parent = false;
            parent = true;
            switched = true;
        }
    }
    float vectorlength(Vector2 input)
    {
        float result = Mathf.Sqrt(Mathf.Pow(input.x, 2) + Mathf.Pow(input.y, 2)) - 1;

        return result;
    }
    Vector2 VectorFromTwoPos(Vector2 pos1, Vector2 pos2)
    {
        Vector2 result = new Vector2(pos1.x - pos2.x, pos1.y - pos2.y);
        
        return result;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("bruh");
        if (collision.collider.tag == "EnemyPlanet" && !immune)
        {
            Debug.Log("bruh2");
            hit.Play();

            levelManager.LifeMinus(player);
            Destroy(collision.collider.gameObject);
            StartCoroutine(HitInvulnerability());
            Debug.Log("bruh3");
        }
      
        if (collision.collider.tag == "Player")
        {
            touch.Play();
            isSpriteTouch = true;
            currentSprite = perso.sprite;
            perso.sprite = anim[2];

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bonus")
        {

            levelManager.LifeUp(player, collision.gameObject);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            isSpriteTouch = false;
            perso.sprite = currentSprite;

        }
    }


    public IEnumerator HitInvulnerability()
    {
        immune = true;
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().enabled = true;

        yield return new WaitForSeconds(0.5f);

        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().enabled = true;

        yield return new WaitForSeconds(0.5f);

        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().enabled = true;
        immune = false;
    }
    
}
