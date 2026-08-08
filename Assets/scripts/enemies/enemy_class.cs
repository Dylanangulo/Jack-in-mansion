using UnityEngine;

public abstract class enemy_class : MonoBehaviour, IDamageable
{
    [SerializeField] private float atk_damage;
    [SerializeField] private float speed;
    [SerializeField] private float life;
    [SerializeField] private float chase_distance;
    [SerializeField] private GameObject player;

    private bool isDead = false;
    private Rigidbody2D rb;   

    public abstract void Attack();

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Chase_player()
    {
        if(isDead) return;

        float distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance <= chase_distance)
        {
            Vector2 direction = (player.transform.position - transform.position).normalized;
            Vector2 velocity = new Vector2(direction.x * speed, rb.linearVelocity.y);
            rb.linearVelocity = velocity;

        }
        else
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);

        }
    }

    public void Take_damage(float damage)
    {
        if (!isDead) 
        {
            life -= damage;

            Debug.Log("Vida enemiga: " + life); 

            if (life <= 0)
            {
                DieEnemy();
            }
        }

    }

    public void DieEnemy()
    {
        if (isDead) return;
        isDead = true;
        rb.linearVelocity = Vector2.zero;
        Destroy(gameObject);
    }
}
