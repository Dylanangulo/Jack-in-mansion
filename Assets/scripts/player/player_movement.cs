
using UnityEngine;


public class player_movement : MonoBehaviour
{
    private Rigidbody2D rb;
    private float Horizontal;

    [SerializeField] private float speed;
    [SerializeField] private int atk;
    [SerializeField] private float range_atk;
    [SerializeField] private float jump_force;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundRadius = 0.2f;
    [SerializeField] private LayerMask floor;
    [SerializeField] private LayerMask enemys;
    [SerializeField] private float atk_cooldown;
    private float lastAttackTime;
    private bool jump;
    private Animator anim;
    private SpriteRenderer sprite;
    private bool isDead = false;
    
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();   
        sprite = GetComponent<SpriteRenderer>();
        
    }


    void Update()
    {
        if (isDead) return;

        Horizontal = Input.GetAxis("Horizontal");
        float sense = sprite.flipX ? -1f : 1f;
        if (Input.GetButtonDown("Jump"))
        {
            if (Is_grounded())
            {
                jump = true;
            }
        }
        if (Input.GetButtonDown("Fire1")&& Time.time >= lastAttackTime + atk_cooldown)
        {
            lastAttackTime = Time.time;

            if (Input.GetKey(KeyCode.UpArrow))
            {
                anim.SetTrigger("atkUp");
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, range_atk, enemys);
                Debug.DrawRay(transform.position, Vector2.up * range_atk, Color.red, 1f);
                if (hit.collider != null)
                {
                    movement_enemy enemy = hit.collider.GetComponentInParent<movement_enemy>();
                    boss_mov boss = hit.collider.GetComponentInParent<boss_mov>();
                    if (enemy != null)
                    {
                        //enemy.Take_damage_enemy(atk);
                    }
                    if (boss != null)
                    {
                        boss.Take_damage_boss(atk);
                    }
                }
            }
            else
            {
                anim.SetTrigger("attack");

                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * sense, range_atk, enemys);
                Debug.DrawRay(transform.position, Vector2.right * sense * range_atk, Color.blue, 1f);
                if (hit.collider != null)
                {
                    movement_enemy enemy = hit.collider.GetComponentInParent<movement_enemy>();
                    boss_mov boss = hit.collider.GetComponentInParent<boss_mov>();
                    if (enemy != null)
                    {
                        //enemy.Take_damage_enemy(atk);
                    }
                    if (boss != null)
                    {
                        boss.Take_damage_boss(atk);
                    }
                }
            }
        }
    }
        
    private void FixedUpdate()
    {
        if (isDead) return;

        rb.linearVelocityX = Horizontal * speed;
        if (jump)
        {
            rb.linearVelocityY = jump_force;
            jump = false;
        }
        anim.SetFloat("Speed", Mathf.Abs(rb.linearVelocityX));

        if (rb.linearVelocityX > 0.1)
        {
            sprite.flipX = false;

        }
        else if (rb.linearVelocityX < -0.1)
        {
            sprite.flipX = true;
        }

        if (rb.linearVelocityY > 0.1)
        {
            anim.SetBool("jumping", true);
            anim.SetBool("falling", false);
        }
        else if (rb.linearVelocityY < -0.1)
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

    bool Is_grounded()
    {
        if (Physics2D.OverlapCircle(groundCheck.position, groundRadius, floor))
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    public void die()
    {
        isDead = true;
        
        rb.linearVelocity = Vector2.zero;
        
    }

}
