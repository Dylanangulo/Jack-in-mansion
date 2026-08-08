using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement movement;
    private PlayerCombat combat;
    private PlayerAnimation anim;
    private PlayerHealth health;

    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        combat = GetComponent<PlayerCombat>();
        anim = GetComponent<PlayerAnimation>();
        health = GetComponent<PlayerHealth>();

        if (health != null) health.OnDeath += OnPlayerDeath;
    }

    private void OnDestroy()
    {
        if (health != null) health.OnDeath -= OnPlayerDeath;
    }

    private void OnPlayerDeath()
    {
        // Desactivar sistemas que no deben seguir funcionando
        if (combat != null) combat.enabled = false;

        // Opcional: notificar GameManager, reproducir UI, etc.
        StartCoroutine(LoadDefeatScene());
    }

    private IEnumerator LoadDefeatScene()
    {
        // Espera para que la animación de muerte termine
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("defeat");
    }
}
