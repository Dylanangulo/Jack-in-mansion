
using UnityEngine;
using UnityEngine.UI;

public class bar_life : MonoBehaviour
{
    //[SerializeField] private Slider slider_bar;
    //[SerializeField] private life_player life_Player;

    //public void Start()
    //{
    //    life_Player = FindFirstObjectByType<life_player>();

    //    life_Player.player_take_damage += Change_life_bar_damage;

    //    life_Player.player_take_health += Change_life_bar_health;

    //    Star_life_bar(life_Player.get_maxlife(), life_Player.get_actual_life());
    //}



    //private void OnDisable()
    //{
    //    life_Player.player_take_damage -= Change_life_bar_damage;
    //    life_Player.player_take_health -= Change_life_bar_health;
    //}

    //private void Star_life_bar(float maxlife, float actual_life)
    //{
    //    slider_bar.maxValue = maxlife;
    //    slider_bar.value = actual_life;
    //}

    //private void Change_life_bar_damage(float actual_life)
    //{
    //    slider_bar.value = actual_life;
    //}
    //private void Change_life_bar_health(float actual_life)
    //{
    //    slider_bar.value = actual_life; 
    //}

    [SerializeField] private Slider slider_bar;
    [SerializeField] private PlayerHealth playerHealth;

    private void Awake()
    {
        if (playerHealth != null)
        {
            InitLifeBar(playerHealth.MaxLife, playerHealth.CurrentLife);
        }
    }

    private void OnEnable()
    {
        if (playerHealth != null)
        {
            playerHealth.OnTakeDamage += UpdateLifeBar;
            playerHealth.OnHeal += UpdateLifeBar;
            playerHealth.OnDeath += HandleDeath;
        }
    }

    private void OnDisable()
    {
        if (playerHealth != null)
        {
            playerHealth.OnTakeDamage -= UpdateLifeBar;
            playerHealth.OnHeal -= UpdateLifeBar;
            playerHealth.OnDeath -= HandleDeath;
        }
    }

    private void InitLifeBar(float maxLife, float actualLife)
    {
        slider_bar.maxValue = maxLife;
        slider_bar.value = actualLife;
    }

    private void UpdateLifeBar(float currentLife)
    {
        slider_bar.value = currentLife;
    }

    private void HandleDeath()
    {
        slider_bar.value = 0;
        // Opcional: ocultar la barra o mostrar "Game Over"
    }

}
