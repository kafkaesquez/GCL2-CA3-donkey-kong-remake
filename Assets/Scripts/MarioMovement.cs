using UnityEngine;

public class MarioMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D mariorb;
    public Vector2 mariomovement;

    public float jumpForce = 7f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.15f;
    public LayerMask groundLayer;
    public bool isGrounded;

    public Animator marioanim;

    public GameObject hammer;

    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mariorb = GetComponent<Rigidbody2D>();
        marioanim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        // (Jeremy) Left Right Movement
        mariomovement.x = Input.GetAxisRaw("Horizontal");
        mariorb.linearVelocity = new Vector2(mariomovement.x * moveSpeed, mariorb.linearVelocity.y);


        // (Jeremy) Jumping
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            mariorb.linearVelocity = new Vector2(mariorb.linearVelocity.x, jumpForce);
            print("jumped");
        }

        // (Jeremy) Flip Sprite based on direction
        if (mariorb.linearVelocity.x > 0.01f)
            transform.localScale = new Vector2(-0.7866277f, transform.localScale.y);
        else if (mariorb.linearVelocity.x < -0.01f)
            transform.localScale = new Vector2(0.7866277f, transform.localScale.y);

        // (Jeremy) Animation values
        marioanim.SetFloat("XVelocity", Mathf.Abs(mariorb.linearVelocity.x)); // Velocity Check
        marioanim.SetBool("isGrounded", isGrounded);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("Triggered by: " + collision.gameObject.name);

        if (collision.gameObject.tag == "Hammer")  //(Jermaine) when mario picks up hammer
        {
            Destroy(hammer);  //(Jermaine) removes hammer sprite after picking up
            marioanim.SetBool("hasHammer", true);

        }
    }

}