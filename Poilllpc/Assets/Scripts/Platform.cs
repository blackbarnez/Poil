using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public bool isMoveable;
    public bool isIced;
    public bool isGhost;
    public bool isDestructible;
    public bool isCloud;
    public Vector2 start;
    public Vector2 end;
    public PhysicsMaterial2D ice;
    public float timeLeft;
    ParticleSystem destroyEffect;
    float y,x ;

    void Start()
    {
        destroyEffect = GetComponentInChildren<ParticleSystem>();
        y = transform.position.y;
        x = transform.position.x;

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
    void Update()
    {
        if (isMoveable)
        {
            transform.position = new Vector2(Remap(Mathf.Sin(Time.time), -1, 1, start.x, end.x),
                Remap(Mathf.Sin(Time.time), -1, 1, start.y, end.y));
            if (isCloud)
            {
                transform.position = new Vector2(Remap(Mathf.Sin(Time.time/5), -1, 1, start.x, end.x),
                Remap(Mathf.Sin(Time.time/5), -1, 1, start.y, end.y));

            }


        }

        if (isGhost)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, Remap(Mathf.Sin(Time.time), -1, 1, 0.5f, 1));
            gameObject.GetComponent<Collider2D>().enabled = false;


        }






    }


    void OnCollisionEnter2D(Collision2D other)
    {
        if (isDestructible)
        {
            if (other.gameObject.tag == "Player")
            {
            
                if (!destroyEffect.isPlaying)
                destroyEffect.Play();
                StartCoroutine("DestroyPlatform", timeLeft);
                //Invoke("DestroyPlatform", timeLeft);

            }
        }
    }

    IEnumerator DestroyPlatform()
    {

        float alpha = gameObject.GetComponent<SpriteRenderer>().color.a;
        for (float t = 0f; t < 1f; t += Time.deltaTime / 2)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, 0, t));
            if(newColor.a<=0.5f){
                  transform.position = new Vector2(transform.position.x, Mathf.Lerp(transform.position.y, y-1,Time.deltaTime));
            }
           
            gameObject.GetComponent<SpriteRenderer>().color = newColor;
            yield return new WaitForEndOfFrame();
        }
        if (gameObject.GetComponent<SpriteRenderer>().color.a <= 0.1f)
        {
           
            gameObject.GetComponent<Collider2D>().enabled = false;
            
            Invoke("RestorePlatform", timeLeft);
        }


    }

    void RestorePlatform()
    {
        destroyEffect.Stop();
        gameObject.GetComponent<Collider2D>().enabled = true;
        transform.position = new Vector2(x, y);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
    }

}








