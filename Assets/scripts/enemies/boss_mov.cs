using System.Collections;
using UnityEngine;


public class boss_mov : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Animator anim;
    [SerializeField] private float speed_boss;
    [SerializeField] private int atk_boss;
    [SerializeField] private float atk_distance;
    [SerializeField] private float atk_cooldawn;
    [SerializeField] private float life_boss;
    [SerializeField] private float distance;
    [SerializeField] private float chase_distance_boss;

    private bool isDead = false;
    private float Horizontal;
    private float lastAttackTime;
    

    private SpriteRenderer sprite;
    private Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (isDead) return;

        float distance = Vector2.Distance(transform.position, player.transform.position);
        if (Time.time > lastAttackTime + atk_cooldawn)
        {
            if (distance <= atk_distance)
            {
                Horizontal = 0;
                Debug.Log("En rango de ataque");
                if (life_boss > 5)
                {
                    basic_atk_player();
                    return;
                }
                else
                {
                    
                    atk_player_f2();
                    return;
                }
            }
        }   

        if (chase_player())
            {
                return;
            }

        Horizontal = 0;
 
    }

    private void FixedUpdate()
    {
        if (isDead)
        {
            
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            anim.SetFloat("speed", 0);
            return;
        }

        if (Horizontal != 0)
        {
            rb.linearVelocity = new Vector2(Horizontal * speed_boss, rb.linearVelocity.y);

        }
        else
        {
            
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }

        anim.SetFloat("speed", Mathf.Abs(rb.linearVelocity.x));


        if (rb.linearVelocityX > 0.1f)
        {
            sprite.flipX = false;

        }
        else if (rb.linearVelocityX < -0.1f)
        {
            sprite.flipX = true;
        }

    }



    void basic_atk_player()
    {
        // Horizontal = 0;
        // Debug.Log("Estoy dentro del rango de ataque");
        // if (Time.time > lastAttackTime + atk_cooldawn)
        // {
        //     anim.SetTrigger("atk_1");
        //     Debug.Log("Ataco al jugador!");
        //     lastAttackTime = Time.time;

        // }
        // else
        // {
        //     Debug.Log("No ataco porque estoy en cooldown");
        // }

        anim.SetTrigger("atk_1");
        Debug.Log("Ataco al jugador!");
        lastAttackTime = Time.time;
    }

    void atk_player_f2()
    {
        // Horizontal = 0;
        // Debug.Log("Estoy dentro del rango de ataque");
        // if (Time.time > lastAttackTime + atk_cooldawn)
        // {
        //     anim.SetTrigger("atk_2");
        //     Debug.Log("Ataco al jugador!");
        //     lastAttackTime = Time.time;

        // }
        // else
        // {
        //     Debug.Log("No ataco porque estoy en cooldown");
        // }
        anim.SetTrigger("atk_2");
        Debug.Log("Ataco al jugador!");
        lastAttackTime = Time.time;

    }


    bool chase_player()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance <= chase_distance_boss)
        {
            Vector2 direction = (player.transform.position - transform.position).normalized;
            Horizontal = direction.x;
            return true;
        }
        else
        {
            Horizontal = 0;
            return false;
        }
    }

    void damage_to_player()

    {
        player.GetComponent<life_player>().Take_damage(atk_boss);
    }

    void damage_to_player_f2()
    {
        player.GetComponent<life_player>().Take_damage(atk_boss+1);
    }
    
    public void Take_damage_boss(int damage)
    {
        if (isDead) return;


        life_boss -= damage;
        anim.SetTrigger("take_dam");
        Debug.Log("Vida enemiga: " + life_boss);

        if (life_boss <= 0)
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

