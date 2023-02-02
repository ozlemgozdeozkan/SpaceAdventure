using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    public static bool isRestart = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void restartGame()
    {   isRestart = true;
        PlayerController.coinCount = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);//Aktif sahneyi (son sahneyi) baştan yüklemeye yarar.
    }
    public void quitGame()
    {
        Application.Quit();// Oyundan çıkma komutudur.
    }
}
