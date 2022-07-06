using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool saved;

    public float posX;
    public float posY;
    AudioSource audioSource;
    public AudioClip jumpSound;
    public AudioClip bonusSound;
    public Vector2 jumpDirection;
    public float jumpForce = 2;
    public float ChargeLevel = 0f;
    public float ChargeMultiplier = 10f;
    public float maxJumpForce = 10f;
    public float minJumpForce = 1f;
    public float forceBack = 5f;

    public bool isGrounded;
    Rigidbody2D rb;

    Transform mainCam;
     Transform backG;
    public Transform arrow;

    public Sprite jumpSpriteFrog;
    public Sprite jumpSpriteCat;
    public static Sprite mainSprite;
    public static  SpriteRenderer spriteRenderer;
    //public static GameObject player;

    public ParticleSystem particles;
    //public ParticleSystemRenderer particleRender;



    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").transform;
        backG = GameObject.FindGameObjectWithTag("BG").transform;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        //mainCam.transform;
        particles = GetComponentInChildren<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
        mainSprite = GetComponent<SpriteRenderer>().sprite;

        PlayerPrefs.SetFloat("PosX", transform.position.x);
        PlayerPrefs.SetFloat("PosY", transform.position.y);
        saved = true;



    }

    void Update()
    {

        if (mainCam.GetComponent<Camera>().orthographicSize > 5) mainCam.GetComponent<Camera>().orthographicSize -= 0.02f;
         mainCam.position = Vector3.Lerp(mainCam.position, transform.position + new Vector3(0, 1, -10), 0.2f);
        backG.position = Vector3.Lerp(backG.position, mainCam.position + new Vector3(0, -0.01f, 100), 0.1f);

        if (Input.GetMouseButton(0) && isGrounded)
        {


            ChargeLevel += Time.deltaTime * ChargeMultiplier;

            jumpForce = Mathf.Clamp(ChargeLevel, minJumpForce, maxJumpForce);


            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 charPos = new Vector2(transform.position.x, transform.position.y);

            jumpDirection = mousePos - charPos;
            jumpDirection.Normalize();

            //if(jumpForce>=maxJumpForce/2) mainCam.GetComponent<Camera>().orthographicSize = Remap(jumpForce,maxJumpForce/2, maxJumpForce, 5,4f);


            if (!particles.isPlaying)
                particles.Play();



            arrow.gameObject.SetActive(true);
            //arrow.GetChild(2).transform.localScale = new Vector3(1,jumpForce/10,1);
            arrow.GetChild(2).transform.localScale = new Vector3(1, Remap(jumpForce, minJumpForce, maxJumpForce, 0, 1), 1);



            arrow.rotation = Quaternion.LookRotation(Vector3.forward, jumpDirection);
        }



        if (Input.GetMouseButtonUp(0))
        {

            if (transform.position.y + jumpForce > transform.position.y + maxJumpForce / 2)//проверить высоту прыжка
            {
                if (Shoping.isFrog)
                {
                    spriteRenderer.sprite = jumpSpriteFrog;
                }

                if (Shoping.isCat)
                {
                    spriteRenderer.sprite = jumpSpriteCat;
                }

               


                audioSource.PlayOneShot(jumpSound);

            }



     
            rb.AddForce(jumpDirection * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;


            ChargeLevel = 0f;
            jumpForce = 0f;
            arrow.gameObject.SetActive(false);



        }

        // particles.startSize = jumpForce * 0.1f;
        // particles.maxParticles = (int)jumpForce;



    }
    void OnCollisionEnter2D(Collision2D other)
    {
        spriteRenderer.sprite = mainSprite;
        //if(BonusScript.isBonus==true){
        //    audioSource.PlayOneShot(bonusSound);
        //}




    }

    void OnCollisionStay2D(Collision2D other)
    {

        if (other.gameObject.tag == "Platform")
        {
            transform.parent = other.transform;
            posX = other.transform.position.x;
            posY = other.transform.position.y + 1;
            savePrefs();
        }
        isGrounded = true;

    }


    void OnCollisionExit2D()
    {

        isGrounded = false;
        //transform.parent = null;
    }



    public float Remap(float from, float fromMin, float fromMax, float toMin, float toMax)
    {
        var fromAbs = from - fromMin;
        var fromMaxAbs = fromMax - fromMin;

        var normal = fromAbs / fromMaxAbs;

        var toMaxAbs = toMax - toMin;
        var toAbs = toMaxAbs * normal;

        var to = toAbs + toMin;

        return to;
    }


    public void savePrefs()
    {
        //PlayerPrefs.SetInt("coins",bonus);
        PlayerPrefs.SetFloat("PosX", posX);
        PlayerPrefs.SetFloat("PosY", posY);
        saved = true;
        //Debug.Log(PlayerPrefs.GetFloat("PosY"));
    }

    // public void loadPrefs()
    // {


    //    transform.position = new Vector2(PlayerPrefs.GetFloat("PosX"), PlayerPrefs.GetFloat("PosY"));
    //    BonusScript.bonus = PlayerPrefs.GetInt("Bonus");
        




    // }
}
