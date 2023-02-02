using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] 
    Rigidbody2D rb; 

    [SerializeField]
    float speed = 2f;

    [SerializeField]
    Animator anim;

    public static int coinCount = 0;

    [SerializeField]
    Text coinCountText;

    [SerializeField]
    GameObject restartGamePanel;

    [SerializeField]
    Text lastCoinCountText;

    [SerializeField]
    GameObject playGamePanel;

    public static bool isStartGame = false;

    [SerializeField]
    AudioSource sound;

    void Start()
    {
        if (RestartGame.isRestart == true)
        {
            playGamePanel.SetActive(false);
        }
        coinCountText.text = coinCount.ToString();
        
    }

    private void FixedUpdate()
    {
        if (isStartGame == false)
        {
            return; //Oyunu durdurmaya yarar. Panel ektanları için gereklidir.
        }
        float mySpeedX = speedData();//horizontal değerini mySpeedX e tanımladık.
        move(mySpeedX);
        turnPlayer(mySpeedX);
        moveAnimation(mySpeedX);
    }
    public void startGame()
    {
        isStartGame = true;
        playGamePanel.SetActive(false);
      
    }
    float speedData()
    {
        float horizontal = Input.GetAxis("Horizontal");
        return horizontal;
    }
    void move(float h) //Yürüme-Koşma
    {
        rb.velocity = new Vector2(h * speed, rb.velocity.y);
    }

    void turnPlayer(float h)  //Yön Düzeltme
    {
        if (h > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (h < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }

    void moveAnimation(float h)//Koşma/Durma Animasyon
    {
        bool isRun = false;
        if (h != 0)
        {
            isRun = true;
            //Kosuyorum horizontal > 0 || horizontal < 0

        }
        else
        {
            isRun = false;
            //Duruyorum horizontal = 0
        }

        anim.SetBool("horizontal", isRun);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "coin")
        {
            coinCount++;
            sound.Play();
            coinCountText.text = coinCount.ToString();
            Destroy(other.gameObject);
        }
        else if (other.tag == "enemy")
        {
            death();

        }
        else if (other.tag == "death")
        {
            death();
        }
        else if (other.tag == "box")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if (other.tag == "star")
        {
            coinCount += 5;
            sound.Play();
            coinCountText.text = coinCount.ToString();
            Destroy(other.gameObject);
        }

    }
    void death()
    {
        restartGamePanel.SetActive(true);
        lastCoinCountText.text = coinCount.ToString();
        Destroy(this.gameObject);
    }


}

