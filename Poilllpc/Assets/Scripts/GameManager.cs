using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    int currentLevel;// Start is called before the first frame update
    public static bool isNewGame = false;
    public Text bonusText;
    public GameObject canvas;
    public Slider soundSlider;
    public AudioSource myMusic;
    public Sprite[] SoundIcons = new Sprite[2];
    public Image SoundIcon;


    public GameObject mainGame;
    public GameObject mainMenu;
    public GameObject market;

    public GameObject player;

    public Sprite jab;

    Rigidbody2D playerRb;
    Transform mainCam;
    Vector3 mainCamOrig;
    Vector2 playerPos;
    Text btText;
    Text marketText;

    void Avake()
    {


        mainGame = GameObject.FindGameObjectWithTag("GameWindow");
        mainMenu = GameObject.FindGameObjectWithTag("Menu");
        market = GameObject.FindGameObjectWithTag("Market");
        if (float.IsNaN(PlayerPrefs.GetFloat("PosX")))
        {
            playerPos = new Vector2(PlayerPrefs.GetFloat("PosX"), PlayerPrefs.GetFloat("PosY"));
        }
        else playerPos = new Vector2(15, 3);
    }
    void Start()
    {

        mainCam = GameObject.FindGameObjectWithTag("MainCamera").transform;
        mainCamOrig = mainCam.position;

        playerRb = player.GetComponent<Rigidbody2D>();


        
            GameObject bT = GameObject.Find("BonusCount");
            btText = bT.GetComponent<Text>();

            GameObject mT = GameObject.Find("MarketCount");
            marketText = mT.GetComponent<Text>();

    }


    void Update()
    {
        PlayerPrefs.SetInt("Bonus", BonusScript.bonus);
        btText.text = "" + PlayerPrefs.GetInt("Bonus");
        marketText.text = "" + PlayerPrefs.GetInt("Bonus");
        //bonusText.text = "" + BonusScript.bonus;
        myMusic.volume = soundSlider.value;
        if (soundSlider.value == 0)
        {
            SoundIcon.sprite = SoundIcons[0];
        }
        else
        {
            SoundIcon.sprite = SoundIcons[1];
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            loadMeny();
        }
    }


    public void Close()
    {
        Application.Quit();
    }

    public static void SoundManager(AudioSource myMusic, Slider soundSlider, Image SoundIcon, Sprite[] SoundIcons)
    {
        myMusic.volume = soundSlider.value;

        if (!myMusic.isPlaying)
        {
            myMusic.Play();
        }
        else { myMusic.Stop(); }


    }

    public void ChangeScene()
    {
        //player.transform.position = new Vector2(PlayerPrefs.GetFloat("PosX"), PlayerPrefs.GetFloat("PosY"));
        string s = EventSystem.current.currentSelectedGameObject.name;
        if (s == "NewGameButton")
        {

            isNewGame = true;
            PlayerPrefs.DeleteAll();
           
            loadGame();

        };
        if (s == "ContinueButton")
        {
            isNewGame = false;
            //PlayerController playerScript = player.GetComponent<PlayerController>();
            loadGame();
        };
        if (s == "MarketButton")
        {
            loadMarket();
        };
        if (s == "BackButton")
        {
            loadMeny();

        };

    }


    public void loadMeny()
    {

        mainCam.position = mainMenu.transform.position + new Vector3(0, 1, -10);
        mainGame.SetActive(false);
        mainMenu.SetActive(true);
        market.SetActive(false);

    }
    public void loadGame()
    {


        mainGame.SetActive(true);
        mainMenu.SetActive(false);
        market.SetActive(false);
        GameObject p = GameObject.FindGameObjectWithTag("Player");

        if (isNewGame)
        {
             
            Debug.Log("NEW");
            // if (p != null){
            //     Destroy(p);
            //     Debug.Log("Destroid");
            // }
            if (p == null){
                p = Instantiate(player, new Vector2(15, 3), transform.rotation);
                p.GetComponent<SpriteRenderer>().sprite = jab;
                PlayerController.mainSprite =  jab;
            }
                
            else {
                p.transform.position = new Vector2(15, 3);
                p.GetComponent<SpriteRenderer>().sprite = jab;
               PlayerController.mainSprite =  jab;
              
            }
            isNewGame = false;
        }
        else
        {
            Debug.Log("Save Found");
            //player.GetComponent<PlayerController>().loadPrefs();
            //player.transform.position =  new Vector2(PlayerPrefs.GetFloat("PosX"), PlayerPrefs.GetFloat("PosY"));
            BonusScript.bonus = PlayerPrefs.GetInt("Bonus");
            if (GameObject.FindGameObjectWithTag("Player") == null && !float.IsNaN(PlayerPrefs.GetFloat("PosX")))
                Instantiate(player, new Vector2(PlayerPrefs.GetFloat("PosX"), PlayerPrefs.GetFloat("PosY")), transform.rotation);
        }





    }

    public void loadMarket()
    {

        mainGame.SetActive(false);
        mainMenu.SetActive(false);
        market.SetActive(true);
    }



}

