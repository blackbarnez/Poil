using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BonusScript : MonoBehaviour
{

    public static int bonus = 0;
    
    public AudioClip bonusSound;


  
  

    void Start()
    {
       
    }


    void Update()
    {
        PlayerPrefs.SetInt("Bonus", bonus);
    }

    public void BonusPlus(){
        bonus++;
        //backButton.text = bonus;
        PlayerPrefs.SetInt("Bonus", bonus);
        Debug.Log(PlayerPrefs.GetInt("Bonus"));
              
    }

    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>().PlayOneShot(bonusSound);
            Destroy(gameObject);
            BonusPlus();
            //isBonus = true;
            // Debug.Log("бонус");
            // Debug.Log(bonus);
            
            

        }
        if(collision.gameObject.tag == "Player"&&gameObject.tag == "Amulet") {
            Debug.Log("You win");
            Application.Quit();
        }
    }
    
}
