using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] //Unity içinden ulaşılsın ama diğer sınıflardan ulaşılmasın.
    Rigidbody2D rb; // bu kodu yazdıktan sonra unityde rigidbodye player atanması yapmak gerekir.

    [SerializeField]
    float speed = 2f;

    [SerializeField]
    Animator anim;

    public static int coinCount = 0;//public her yerden ulaşmaya static ise tek bir değişken olsun. Leveller değişsede coin her levelde devam edecek.

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
            playGamePanel.SetActive(false);//restarttan sonra play paneli gelmemesi için play panelini kapatırız.
        }
        coinCountText.text = coinCount.ToString();
        
    }

    // Update is called once per frame
    void Update()
    {

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
    /*Çarpışmalar
    -Eğer iki cisim çarpışsın içinden geçme olayı olmasın (Düşman ile çarpışmak)
    Çarptığını algılamak istiyorsan : private void OnCollisionEnter2D(Collision2D collision)
    collision/other çarptığın nesnedir. 
    -Eğer çarptıktan sonra çıkana kadar ki süre içinde olan çarpışmalar
    private void OnCollisionStay2D(Collision2D collision)
    -Eğer o nesne ile bağlantısı koptuğu an çalışsın istiyorsan
    private void OnCollisionExit2D(Collision2D collision)
    -Eğer bir nesnenin hem içinden geçip hemde çarpışmayı algılamak istiyorsak colliderda trigger aktif olmalıdır.
    Eğer is trigger açık ise;
    OnTriggerEnter2D=> çarpışıldığı an 
    OnTriggerStay2D=> çarpışma sürdüğü an
    OnTriggerExit2D=> çarpışma bittiği an
    */
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "coin")
        {
            coinCount++;
            sound.Play();
            coinCountText.text = coinCount.ToString();
            Destroy(other.gameObject);//Yok etme metodudur.İçerisine gameobj alır. other bir collider'ken onu game objecte çeviririz.
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
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);//bir sonraki levele geçmek
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
        restartGamePanel.SetActive(true);//Paneli aktif etmek
        lastCoinCountText.text = coinCount.ToString();
        Destroy(this.gameObject);
    }


}
//canvasta rect transformda image yerini alt+shift beraber basılı tutarak en sol üst seçilir. Bu cihazlarda değişiklik göstermemesini sağlar. Ek: ctrl+D tutuğunuz şeyi kopyalar.
