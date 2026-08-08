using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private SpriteRenderer sprite;
    private PlayerMovement movement;
    private PlayerCombat combat;
    private PlayerHealth health;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        movement = GetComponent<PlayerMovement>();
        combat = GetComponent<PlayerCombat>();
        health = GetComponent<PlayerHealth>();
    }

    private void OnEnable()
    {
        if (combat != null)
        {
            combat.OnAttackRequested += HandleAttackRequested;
            combat.OnAttackUpRequested += HandleAttackUpRequested;
        }

        if (health != null)
        {
            health.OnTakeDamage += HandleTakeDamage;
            health.OnDeath += HandleDeath;
            health.OnHeal += HandleHeal;
        }
    }

    private void OnDisable()
    {
        if (combat != null)
        {
            combat.OnAttackRequested -= HandleAttackRequested;
            combat.OnAttackUpRequested -= HandleAttackUpRequested;
        }

        if (health != null)
        {
            health.OnTakeDamage -= HandleTakeDamage;
            health.OnDeath -= HandleDeath;
            health.OnHeal -= HandleHeal;
        }
    }

    private void Update()
    {
        if (movement == null || movement.IsDead) return;

        anim.SetFloat("Speed", Mathf.Abs(movement.VelocityX));

        if (sprite != null) sprite.flipX = movement.FacingLeft;

        if (movement.VelocityY > 0.1f)
        {
            anim.SetBool("jumping", true);
            anim.SetBool("falling", false);
        }
        else if (movement.VelocityY < -0.1f)
        {
            anim.SetBool("falling", true);
            anim.SetBool("jumping", false);
        }
        else
        {
            anim.SetBool("jumping", false);
            anim.SetBool("falling", false);
        }
    }

    // Eventos de combate
    private void HandleAttackRequested() => anim.SetTrigger("attack");
    private void HandleAttackUpRequested() => anim.SetTrigger("atkUp");

    // Eventos de salud
    private void HandleTakeDamage(float currentLife) => anim.SetTrigger("t_damage");
    private void HandleDeath() => anim.SetTrigger("isdead");
    private void HandleHeal(float currentLife) => anim.SetTrigger("heal");

    // Método auxiliar para Animation Event
    public void AnimationEvent_InvokeApplyHit()
    {
        combat?.ApplyAttackHit();
    }
    public void AnimationEvent_EndDamage()
    {
        anim.ResetTrigger("t_damage"); // opcional, asegura que no quede activo
    }
}
