using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float jumpForce = 100f;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;

    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private Rigidbody2D rb;
    private bool isGrounded;



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (GameManager.Instance.isGameOver == true) return;

        float horizontal = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);

        if (horizontal > 0) { spriteRenderer.flipX = false; }
        if (horizontal < 0) { spriteRenderer.flipX = true; }

        CheckGrounded();

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            animator.SetBool("IsJump",true);
            
        }



        if (isGrounded)
        {
            animator.SetBool("IsJump", false);
            if (horizontal == 0)
            {
                animator.SetBool("IsMove", false);
            }
            else
            {
                animator.SetBool("IsMove", true);
            }
        }
        else
        {
            animator.SetBool("IsMove", false);
        }
    }

    private void CheckGrounded()
    {
        Collider2D collider = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
        isGrounded = collider != null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Carrot"))
        {
            Destroy(collision.gameObject);
            GameManager.Instance.isGameWin = true;
            GameManager.Instance.isGameOver = true;
            animator.SetBool("IsJump", true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spike"))
        {
            GameManager.Instance.isGameWin = false;
            GameManager.Instance.isGameOver = true;
            animator.SetBool("IsJump", false);
            animator.SetBool("IsDead", true);
        }
    }
}
