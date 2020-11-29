using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    public enum Type { Good, Bad, Bonus }
    
    public Type type;
    
    public GameObject explosion;
    
    private const float yDie = -40.0f;

    private Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        explosion.GetComponent<AudioSource>().volume = GameManager.sounds;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < yDie)
        {
            Destroy(gameObject);
        }
    }

    private void OnMouseDown()
    {
        if (Input.touchCount == 0)
        {
            DestroyObject();
        }
    }

    public void DestroyObject()
    {
        if (GameManager.gameMode == "Countdown" && GameManager.currentSeconds == 0 ||
            GameManager.gameMode == "Survival" && GameManager.currentLives == 0 ||
            GameObject.Find("Canvas").GetComponent<InterfaceMainGame>().IsPauseGame())
        {
            return;
        }
        
        Destroy(Instantiate(explosion, transform.position, Quaternion.identity), 3.5f);
        Destroy(gameObject);
        
        switch (gameObject.GetComponent<Stone>().type)
        {
            case Type.Good:
                GameManager.score += 5;
                break;
            case Type.Bad:
                GameManager.score -= 10;
                GameManager.currentSeconds -= 5;
                GameManager.currentLives--;
                break;
            case Type.Bonus:
                GameManager.score += 20;
                GameManager.currentSeconds += 5;
                GameManager.currentLives++;
                break;
        }
        
        if (GameManager.currentSeconds < 0)
        {
            GameManager.currentSeconds = 0;
        }

        if (GameManager.currentLives < 0)
        {
            GameManager.currentLives = 0;
        }
    }
    
}
