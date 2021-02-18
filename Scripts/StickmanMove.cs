using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class StickmanMove : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    float moveInput;
    bool playerAlive = true;

    Rigidbody2D rb;
    [SerializeField] Animator animator;

    bool isGrounded;
    [SerializeField] Transform groundCheck;
    [SerializeField] float chechkRadius;
    [SerializeField] LayerMask whatIsGround;

    int extraJumps;
    [SerializeField] int extraJumpsValue;

    void Start()
    {
        extraJumps = extraJumpsValue;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, chechkRadius, whatIsGround);
        if (playerAlive)
        {
            Move();
        }
    }

    private void Move()
    {
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed * Time.deltaTime, rb.velocity.y);
        animator.SetTrigger("Run");
    }

    void Update()
    {
        if (playerAlive)
        {
            Jump();
            
        }

    }

    private void Jump()
    {
        if (isGrounded == true)
        {
            extraJumps = extraJumpsValue;
        }

        if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)
        {
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
            animator.SetTrigger("Jump");
        }

        else if (Input.GetKeyDown(KeyCode.Space) && extraJumps == 0 && isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpForce;
            animator.SetTrigger("Jump");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Hazards")
        {
            Die();
        }
            
    }


    private void Die()
    {
        
            playerAlive = false;
            Destroy(this);
        StartCoroutine(LoadScene());


    }

    IEnumerator LoadScene()
    {
        
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(0);
    }

   
    

}
