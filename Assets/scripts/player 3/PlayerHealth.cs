using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHealth : MonoBehaviour, IDamageable, IHealing
{
    [SerializeField] private float maxLife;
    public float CurrentLife { get; private set; }
    public float MaxLife => maxLife;

    public event Action<float> OnTakeDamage;
    public event Action<float> OnHeal;
    public event Action OnDeath;

    private PlayerMovement movement;

    private void Awake()
    {
        CurrentLife = maxLife;
        movement = GetComponent<PlayerMovement>();
    }

    public void Take_damage(float damage)
    {
        CurrentLife = Mathf.Clamp(CurrentLife - damage, 0f, maxLife);
        OnTakeDamage?.Invoke(CurrentLife);

        if (CurrentLife <= 0f)
        {
            StartCoroutine(DeathSequence());
        }
    }

    public void Take_health(float healthAmount)
    {
        CurrentLife = Mathf.Clamp(CurrentLife + healthAmount, 0f, maxLife);
        OnHeal?.Invoke(CurrentLife);
    }

    private IEnumerator DeathSequence()
    {
        movement?.Die();
        OnDeath?.Invoke();

        // Espera para que la animación de muerte se reproduzca 
        yield return new WaitForSeconds(1f);

        // Destruir el GameObject o dejar que el GameManager lo haga
        Destroy(gameObject);

       
    }
}
