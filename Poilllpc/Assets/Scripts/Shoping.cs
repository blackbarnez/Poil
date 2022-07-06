using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Shoping : MonoBehaviour
{
    //public GameObject detectClicks;
    public Text bonusText;
    //public GameObject player;
    //public Sprite playerSkin; 
    private GameObject canvas;
    //public SpriteRenderer[] playerSkins = new SpriteRenderer[4];
    public Sprite[] playerSkins = new Sprite[4];
    public Button[] skinButtons = new Button[4];
    public int frogSkin = 0;
    public int catSkin = 3;
    public int spiderSkin = 20;
    public int pandaSkin = 30;
    public static bool frogBought = true;
    public static bool catBought = false;
    public static bool spiderBought = false;
    public static bool pandaBought = false;
    public static bool isFrog = true;
    public static bool isCat = false;
    public static bool isSpider = false;
    public static bool isPanda = false;

    public Text frogText;
    public Text catText;
    public Text spiderText;
    public Text pandaText;


    GameObject gM;    
    GameManager gmScript;

    GameObject mT;    
    
    void Start()
    {
        gM = GameObject.FindGameObjectWithTag("GameManager");
        gmScript = gM.GetComponent<GameManager>();
        mT = GameObject.Find("MarketText");

        

    }

    void Update()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        //Text bonusText = GameObject.Find("MarketText");
       // bonusText = ;
        mT.GetComponent<Text>().text = "" + PlayerPrefs.GetInt("Bonus");

        if (frogBought)
        {
            frogText.text = "Выбрать";
        }
        else frogText.text = "Купить";

        if (catBought)
        {
            catText.text = "Выбрать";
        }
        else catText.text = "Купить";

        if (spiderBought)
        {
            spiderText.text = "Выбрать";
        }
        else spiderText.text = "Купить";

        if (pandaBought)
        {
            pandaText.text = "Выбрать";
        }
        else pandaText.text = "Купить";
    }

    //public void onClick()
    //{
       //string tb = EventSystem.current.currentSelectedGameObject.name;
       //if (tb == "BuyFrog")
        public void chooseFrog()
        {
            if (BonusScript.bonus >= frogSkin)
            {
                PlayerController.spriteRenderer.sprite = playerSkins[0]; //GameScript.playerSkin.sprite = playerSkins[0]; 
                PlayerController.mainSprite = playerSkins[0];
                Debug.Log("Вы выбрали скин Лягушка");
                isFrog = true;
                isCat = false;
                isSpider = false;
                isPanda = false;
            }

        }

        //if (tb == "BuyCat")
        public void chooseCat()
        {
            if (!catBought || PlayerPrefs.HasKey("Cat") && (PlayerPrefs.GetString("Cat") == null || PlayerPrefs.GetString("Cat") == "")) 
            {
                if (BonusScript.bonus >= catSkin)
                {
                    Debug.Log("Вы выбрали скин Кот");
                    PlayerController.spriteRenderer.sprite = playerSkins[1];
                    PlayerController.mainSprite = playerSkins[1]; // GameScript.playerSkin.sprite = playerSkins[1];
                    //Debug.Log("Вы выбрали скин Лягушка");
                    catBought = true;
                    BonusScript.bonus -= catSkin;
                    isFrog = false;
                    isCat = true;
                    catText.text = "Выбрать";
                    isSpider = false;
                    isPanda = false;
                    PlayerPrefs.SetString("Cat", "true");
                }
                else Debug.Log("Вам не хватает бонусов");
            }
            else
            { 
                Debug.Log("Вы выбрали скин Кот");
                PlayerController.spriteRenderer.sprite = playerSkins[1];
                PlayerController.mainSprite = playerSkins[1]; //GameScript.playerSkin.sprite = playerSkins[1];
                isFrog = false;
                isCat = true;
                isSpider = false;
                isPanda = false;
            }
        }

        //if (tb == "Buy")
        public void chooseSpider()
        {
             if (!spiderBought || PlayerPrefs.HasKey("Spider") &&( PlayerPrefs.GetString("Spider")==null || PlayerPrefs.GetString("Spider")==""))
             {
                if (BonusScript.bonus >= spiderSkin)
                {
                    Debug.Log("Вы выбрали скин Паук");
                    PlayerController.spriteRenderer.sprite = playerSkins[2];
                    PlayerController.mainSprite = playerSkins[2]; //GameScript.playerSkin.sprite = playerSkins[2];
                    //Debug.Log("Вы выбрали скин Лягушка");
                    spiderBought = true;
                    BonusScript.bonus -= spiderSkin;
                    isFrog = false;
                    isCat = false;
                    isSpider = true;
                    isPanda = false;
                    PlayerPrefs.SetString("Spider", "true");
                }
                else Debug.Log("Вам не хватает бонусов");
            }
            else
            {
                Debug.Log("Вы выбрали скин Паук");
                PlayerController.spriteRenderer.sprite = playerSkins[2];
                PlayerController.mainSprite = playerSkins[2];//GameScript.playerSkin.sprite = playerSkins[2];
                isFrog = false;
                isCat = false;
                isSpider = true;
                isPanda = false;
            }
        }

        //if (tb == "Buy")
        public void choosePanda()
        {
            if (!pandaBought || PlayerPrefs.HasKey("Panda") &&( PlayerPrefs.GetString("Panda")==null || PlayerPrefs.GetString("Panda")==""))
            {
                if (BonusScript.bonus >= pandaSkin)
                {
                    Debug.Log("Вы выбрали скин Панда");
                    PlayerController.spriteRenderer.sprite = playerSkins[3];
                    PlayerController.mainSprite = playerSkins[3];//GameScript.playerSkin.sprite = playerSkins[3];
                    //Debug.Log("Вы выбрали скин Лягушка");
                    pandaBought = true;
                    BonusScript.bonus -= pandaSkin;
                    isFrog = false;
                    isCat = false;
                    isSpider = false;
                    isPanda = true;
                     PlayerPrefs.SetString("Panda", "true");
                }
                else Debug.Log("Вам не хватает бонусов");
            }
            else
            {
                Debug.Log("Вы выбрали скин Панда");
                PlayerController.spriteRenderer.sprite = playerSkins[3];
                PlayerController.mainSprite = playerSkins[3]; //GameScript.playerSkin.sprite = playerSkins[3];
                isFrog = false;
                isCat = false;
                isSpider = false;
                isPanda = true;
            }
        }


    //}


    public void ChangeScene()
    {
        //SceneManager.LoadScene(num);
        string s = EventSystem.current.currentSelectedGameObject.name;
        if (s == "BackButton")
        {
           gmScript.loadMeny();
        };
        //if (s == "Buy") Debug.Log("Магазин пока не работает");
        if (s == "MarketButton") gmScript.loadMeny();
    }

}
