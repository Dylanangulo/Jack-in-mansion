using UnityEngine;
using UnityEngine.SceneManagement;


public class scrip_pausa : MonoBehaviour
{

    [SerializeField] private GameObject button_pause;
    [SerializeField] private GameObject button_music_on;
    [SerializeField] private GameObject button_music_off;
    [SerializeField] private GameObject pause_menu;
    [SerializeField] AudioSource audioSource;
    private bool gamepause = false;
    

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamepause)
            {
                resume();
            }
            else 
            {
                pause();
            }
        }

        button_music_on.SetActive(!audioSource.mute);
        button_music_off.SetActive(audioSource.mute);

    }

    public void pause()
    {
        gamepause = true;
        Time.timeScale = 0f;
        button_pause.SetActive(false);  
        pause_menu.SetActive(true);    
    }

    public void resume()
    {
        gamepause = false;
        Time.timeScale = 1f;
        button_pause.SetActive(true);
        pause_menu.SetActive(false);
    }

    public void restart()
    {
        Time .timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }

    public void Backtomenu()
    {
        SceneManager.LoadScene("menu");
    }

    public void buttom_music()
    {
        audioSource.mute = !audioSource.mute;
        button_music_on.SetActive(!audioSource.mute);
        button_music_off.SetActive(audioSource.mute);
    }

   
}
