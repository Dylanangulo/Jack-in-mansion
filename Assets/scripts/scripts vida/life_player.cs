using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class life_player : MonoBehaviour, IDamageable, IHealing
{
    [SerializeField] private GameObject player;
    public Action<float> player_take_damage;
    public Action<float> player_take_health;

    [SerializeField] private float maxlife;
    [SerializeField] private float actual_life;

    private Animator anim;
    private Rigidbody2D rb;


    private void Awake()
    {
        actual_life = maxlife;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void Take_damage(float damage)
    {
        actual_life = Math.Clamp(actual_life-damage, 0f, maxlife);
        player_take_damage?.Invoke(actual_life);

        if (actual_life > 0)
        {
            if (anim != null)
            {
                anim.SetTrigger("t_damage");
            }
        }
        else
        {
            if (anim != null)

            {
                StartCoroutine(DeathSequence());
            }

        }
        
    }
    private IEnumerator DeathSequence()
    {
        if (anim != null)
        {
            player_movement ps = player.GetComponent<player_movement>();
            ps.die();
            anim.SetTrigger("isdead");

            yield return new WaitForSeconds(1f);

        }

        Destroy_player();
        SceneManager.LoadScene("defeat");
    }

    public void Take_health(float healthAmount)
    {
        actual_life = math.clamp(actual_life + healthAmount, 0f, maxlife);
        player_take_health?.Invoke(actual_life);
    }

    public void Destroy_player()
    {
        Destroy(gameObject,1f);
    }

    public float get_maxlife() => maxlife;
    

    public float get_actual_life() => actual_life;
    
}
