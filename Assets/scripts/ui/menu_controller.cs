using UnityEngine;
using UnityEngine.SceneManagement;

public class menu_control : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Startgame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("game"); 

    }    

    public void Exitgame()
    {
        Application.Quit();
    }

    public void Backtomenu()
    {
        SceneManager.LoadScene("menu");
    }
}
