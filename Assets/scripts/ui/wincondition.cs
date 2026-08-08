using UnityEngine;
using UnityEngine.SceneManagement;

public class wincondition : MonoBehaviour
{
    void Update()
    {
        // Busca todos los enemigos por TAG
        GameObject[] enemigos = GameObject.FindGameObjectsWithTag("enemy");

        if (enemigos.Length == 0)
        {
            GanarJuego();
        }
       
    }

    void GanarJuego()
    {
        Debug.Log("¡GANASTE EL JUEGO!");
        SceneManager.LoadScene("victory");
    }
}
