
using System.Collections;
using UnityEngine;



public class movement_enemy : MonoBehaviour, IDamageable
{

    [SerializeField] private GameObject player;
    [SerializeField] private float speed;
    [SerializeField] private float life;
    [SerializeField] private Animator anim;
    [SerializeField] private float distance;
    [SerializeField] private float chase_distance;
    [SerializeField] private Transform[] puntos_mov;
    [SerializeField]private float range_atk;
    [SerializeField]private float attackCooldown;
    private SpriteRenderer sprite;
    private Rigidbody2D rb;
    private float Horizontal; 
    private bool isDead = false;
    private float lastAttackTime;
    private int siguiente_paso = 0;
    private IDamageable playerDamageable;

    [SerializeField] private float distancia_min;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        playerDamageable = player.GetComponent<IDamageable>();

    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) return;

        float distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance <= range_atk)
        {
            Horizontal = 0;
            atk_player();
            return;
        }

        if (chase_player())
        {
            return;
        }

        Horizontal = 0;
    }


    bool chase_player()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance <= chase_distance)
        {
            Vector2 direction = (player.transform.position - transform.position).normalized;
            float verticalDifference = Mathf.Abs(player.transform.position.y - transform.position.y);
            if (verticalDifference > 0.5f) 
            {
                Horizontal = direction.x;
            }
            else
            {
                Horizontal = 0;
            }
            
            return true;
        }
        else
        {
            Horizontal = 0;
            return false;
        }
    }
       

    void atk_player()
    {
        Horizontal = 0;
        Debug.Log("Estoy dentro del rango de ataque");
        if (Time.time > lastAttackTime + attackCooldown)
        {
            anim.SetTrigger("atk");
            Debug.Log("Ataco al jugador!");
            lastAttackTime = Time.time;

        }
        else
        {
            Debug.Log("No ataco porque estoy en cooldown");
        }

    }



    private void FixedUpdate()
    {

        if (isDead)
        {
            speed = 0;
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            anim.SetFloat("speed", 0);
            return;
        }

        if (Horizontal != 0)
        {
            rb.linearVelocity = new Vector2(Horizontal * speed, rb.linearVelocity.y);
            
        }

        else
        {
            patrol();
        }


        anim.SetFloat("speed", Mathf.Abs(rb.linearVelocityX));


        if (rb.linearVelocityX > 0.1f)
        {
            sprite.flipX = false;

        }
        else if (rb.linearVelocityX < -0.1f)
        {
            sprite.flipX = true;
        }


    }
    void patrol()
    {
        float direction = Mathf.Sign(puntos_mov[siguiente_paso].position.x - transform.position.x);
        rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);
        rb.linearVelocityY = rb.linearVelocityY;

        if (Vector2.Distance(transform.position, puntos_mov[siguiente_paso].position) < distancia_min)
        {
            siguiente_paso++;
            if (siguiente_paso >= puntos_mov.Length)
            {
                siguiente_paso = 0;
            }
            spin();
        }
    }
    
    private void spin()
    {

        if (transform.position.x < puntos_mov[siguiente_paso].position.x)
        {
            sprite.flipX = false;

        }
        else
        {
            sprite.flipX = true;
        }
    }

    void damage_to_player()

    {
        playerDamageable?.Take_damage(1f);
    }

    public void Take_damage(float damage)
    {
        if (isDead) return;


        life -= damage;
        anim.SetTrigger("take_damage");
        Debug.Log("Vida enemiga: " + life);

        if (life <= 0)
        {
            isDead = true;
            StartCoroutine(Death());
        }


    }

    private IEnumerator Death()
    {
        if (anim != null)
        {
            anim.SetTrigger("dead");
            
        }
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
       
    }
}   