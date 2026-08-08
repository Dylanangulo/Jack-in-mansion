using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundRadius = 0.2f;
    [SerializeField] private LayerMask floor;

    private Rigidbody2D rb;
    private float horizontal;
    private bool jumpRequested;
    private bool isDead;

    public bool IsDead => isDead;
    public float VelocityX => rb != null ? rb.linearVelocity.x : 0f;
    public float VelocityY => rb != null ? rb.linearVelocity.y : 0f;
    public bool FacingLeft { get; private set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isDead) return;

        horizontal = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            jumpRequested = true;
        }
    }

    private void FixedUpdate()
    {
        if (isDead || rb == null) return;

        // Mover en X manteniendo la componente Y
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);

        // Saltar (escribir Y)
        if (jumpRequested)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpRequested = false;
        }

        // Actualizar FacingLeft según velocidad horizontal
        if (rb.linearVelocity.x > 0.1f) FacingLeft = false;
        else if (rb.linearVelocity.x < -0.1f) FacingLeft = true;
    }

    private bool IsGrounded()
    {
        if (groundCheck == null) return false;
        return Physics2D.OverlapCircle(groundCheck.position, groundRadius, floor);
    }

    public void Die()
    {
        isDead = true;
        if (rb != null) rb.linearVelocity = Vector2.zero;
    }
}
