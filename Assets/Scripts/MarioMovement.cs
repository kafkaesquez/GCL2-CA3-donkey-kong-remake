using System.Threading;
using UnityEditor.Experimental.GraphView;
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
    public bool hasHammer;
    public float weaponDuration = 4f;

    private bool isHoldingHammer = false;
    private float hammerTimer;
    private float timer;

    public Animator marioanim;

    public bool canMove = true; //shin's ending scene

    public GameObject hammer;





    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = weaponDuration;
        mariorb = GetComponent<Rigidbody2D>();
        marioanim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove) //! means not, so, canMove = false? 
        {
            mariorb.linearVelocity = new Vector2(0, mariorb.linearVelocity.y);

            marioanim.SetFloat("XVelocity", 0f);

            return;
        } //shin's ending scene 

        if (isHoldingHammer)
        {
            hammerTimer -= Time.deltaTime;
            if (hammerTimer <= 0f)
            {
                isHoldingHammer = false;
                hasHammer = false;
                marioanim.SetBool("hasHammer", false);
            }
        }
    


    // (Jeremy) Left Right Movement
    mariomovement.x = Input.GetAxisRaw("Horizontal");
        mariorb.linearVelocity = new Vector2(mariomovement.x * moveSpeed, mariorb.linearVelocity.y);


        // (Jeremy) Jumping, (Jermaine) Not jumping when holding hammer
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        if (isGrounded && Input.GetButtonDown("Jump") && !isHoldingHammer)
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

        if (collision.gameObject.tag == "Hammer")
        {
            Destroy(collision.gameObject); // (Jermaine) destroy this hammer Mario collides with hammer
            marioanim.SetBool("hasHammer", true);
            hasHammer = true;
            isHoldingHammer = true;      
            hammerTimer = weaponDuration; 

            // (Jermaine) When mario has hammer
            if (isHoldingHammer)
            {
                hammerTimer -= Time.deltaTime;
                // (Jermaine) when hammer duration ends
                if (hammerTimer <= 0f)
                {
                    isHoldingHammer = false;
                    hasHammer = false;
                    marioanim.SetBool("hasHammer", false); // (Jermaine) goes back to idle Mario
                    
                    
                }

            }
        }

    }


}