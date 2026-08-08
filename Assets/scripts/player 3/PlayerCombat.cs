using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCombat : MonoBehaviour
{
    //[Header("Attack Settings")]
    //[SerializeField] private float attackDamage;
    //[SerializeField] private float attackRange;
    //[SerializeField] private float attackCooldown;
    //[SerializeField] private LayerMask enemies;

    //private float lastAttackTime;
    //private PlayerMovement movement;

    //public event Action OnAttackRequested;
    //public event Action OnAttackUpRequested;

    //private void Awake()
    //{
    //    movement = GetComponent<PlayerMovement>();

    //}

    //private void Update()
    //{
    //    if (movement == null || movement.IsDead) return;

    //    if (Input.GetButtonDown("Fire1") && Time.time >= lastAttackTime + attackCooldown)
    //    {
    //        lastAttackTime = Time.time;

    //        if (Input.GetKey(KeyCode.UpArrow))
    //            OnAttackUpRequested?.Invoke();

    //        else
    //            OnAttackRequested?.Invoke();
    //    }
    //}

    //// Método público llamado desde Animation Event en el frame de impacto
    //// Si prefieres dos métodos, crea ApplyAttackHitUp() y ApplyAttackHitForward()
    //public void ApplyAttackHit(bool isUp = false)
    //{
    //    Vector2 dir;
    //    if (isUp) dir = Vector2.up;
    //    else dir = movement != null && movement.FacingLeft ? Vector2.left : Vector2.right;

    //    DamageTarget(dir);
    //}

    //private void DamageTarget(Vector2 direction)
    //{
    //    RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, attackRange, enemies);
    //    Debug.DrawRay(transform.position, direction * attackRange, Color.red, 0.5f);

    //    if (hit.collider == null) return;

    //    IDamageable damageable = hit.collider.GetComponentInParent<IDamageable>();
    //    damageable?.Take_damage(attackDamage);
    //}

    [Header("Attack Settings")]
    [SerializeField] private float attackDamage;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackCooldown;
    [SerializeField] private LayerMask enemies;

    // Eventos para iniciar animación / notificar intención
    public event Action OnAttackRequested;
    public event Action OnAttackUpRequested;

    // UnityEvent opcional para efectos conectables desde Inspector
    public UnityEvent OnAttackHitEvent;

    private float lastAttackTime;
    private PlayerMovement movement;

    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (movement == null || movement.IsDead) return;

        if (Input.GetButtonDown("Fire1") && Time.time >= lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;

            if (Input.GetKey(KeyCode.UpArrow))
            {
                Debug.Log("Ataque arriba");
                // Notifica intención de ataque hacia arriba
                OnAttackUpRequested?.Invoke();
            }
            else
            {
                Debug.Log("Ataque frontal");
                // Notifica intención de ataque frontal
                OnAttackRequested?.Invoke();

            }
        }
    }

    // Método público que será llamado desde Animation Event en el frame de impacto
    // Debe estar público para que Animation Event lo pueda invocar
    public void ApplyAttackHit()
    {
        bool isUp = false;
        Debug.Log("ApplyAttackHit ejecutado");
        Vector2 dir;
        if (isUp) dir = Vector2.up;
        else dir = movement != null && movement.FacingLeft ? Vector2.left : Vector2.right;

        DamageTarget(dir);
        // Dispara UnityEvent para efectos (sonido, partículas) conectados desde Inspector
        OnAttackHitEvent?.Invoke();
    }

    // Lógica de raycast y aplicación de daño
    private void DamageTarget(Vector2 direction)
    {
        Debug.Log("Entré a DamageTarget");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, attackRange, enemies);
        Debug.DrawRay(transform.position, direction * attackRange, Color.red, 0.5f);

        Debug.Log("Raycast ejecutado");
        if (hit.collider != null)
        {
            Debug.Log("Golpeó: " + hit.collider.name);
            IDamageable damageable = hit.collider.GetComponent<IDamageable>();
            if (damageable != null)
            {
                Debug.Log("Tiene IDamageable");
                damageable?.Take_damage(attackDamage);
            }
            else
            {
                Debug.Log("NO tiene IDamageable");
            }
        }
        else
        {
            Debug.Log("No golpeó nada");
        }
    }

}
