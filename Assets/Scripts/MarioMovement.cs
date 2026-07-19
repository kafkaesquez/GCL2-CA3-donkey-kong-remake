using System.Threading;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MarioMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float currentMoveSpeed;
    public float jumpForce = 7f;
    public float currentJumpForce;


    public float speedMultiplier = 1f; // (Jermaine) Floor speed: ill add on tmr to fix
    public float acceleration = 10f;   

    
    public Rigidbody2D mariorb;
    public Vector2 mariomovement;

    //ground check
    public Transform groundCheck;
    public float groundCheckRadius = 0.15f;
    public LayerMask groundLayer;
    public bool isGrounded;

    //hammer
    public bool hasHammer;
    public float hammerWeaponDuration = 11f;
    public HammerHitbox hammerHitbox;
    public bool IsHoldingHammer => isHoldingHammer;
    private bool isHoldingHammer = false;
    public float hammerTimer;
    private float timer1;

    //slow mo
    public bool inSlowmo;
    public float slomoDuration = 10f;
    public float slowmoTimer;
    public SpriteRenderer blueoverlay;
    

    public Animator marioanim;

    public bool canMove = true; //shin's ending scene

    //public GameObject hammer;


    [SerializeField] private Ladder laddercode;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer1 = hammerWeaponDuration;
        //timer2 = slomoDuration;
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
            hammerTimer -= Time.unscaledDeltaTime;
             
            if (hammerTimer <= 0f)
            {
                isHoldingHammer = false;
                hasHammer = false;
                marioanim.SetBool("hasHammer", false);
                hammerHitbox.DisableHitbox(); // (Jermaine) turns collider off
            }
        }





        float currentMoveSpeed = moveSpeed;
        float currentJumpForce = jumpForce;
        if (inSlowmo)
        {
            
            slowmoTimer -= Time.unscaledDeltaTime;
            Time.timeScale = 0.5f;
            currentMoveSpeed = moveSpeed / Time.timeScale;
            currentJumpForce = jumpForce / Time.timeScale;
            blueoverlay.enabled = true;   
            //if (slowmoTimer <= 1.0f)
            //{
            //    colour.a = 0.5f;
            //}

            // (Jeremy) gravity + ladder climb speed is changed in Jovan's ladder script since i cant get it to work here :(






            // (Jeremy) Exit slomo
            if ((slowmoTimer <= 0.0f) && isGrounded || !inSlowmo)
            {
                inSlowmo = false;
                Time.timeScale = 1f;
                print("slowmo end");
                mariorb.linearVelocity = new Vector2(mariomovement.x * currentMoveSpeed, mariorb.linearVelocity.y);
                currentJumpForce = jumpForce / (Time.timeScale * 2f);
                blueoverlay.enabled = false;
            }


        }

        





        // (Jeremy) Left Right Movement
        mariomovement.x = Input.GetAxisRaw("Horizontal");
        mariorb.linearVelocity = new Vector2(mariomovement.x * currentMoveSpeed, mariorb.linearVelocity.y);


        // (Jeremy) Jumping, (Jermaine) Not jumping when holding hammer
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        if (isGrounded && Input.GetButtonDown("Jump") && !isHoldingHammer)
        {
            mariorb.linearVelocity = new Vector2(mariorb.linearVelocity.x, currentJumpForce);
            print("jumped");
        }

        // (Jeremy) Flip Sprite based on direction
        if (mariorb.linearVelocity.x > 0.01f)
            transform.localScale = new Vector2(-0.7866277f, transform.localScale.y); // (Jeemy) specific x value as mario sprite is resized from default, pls dont change
        else if (mariorb.linearVelocity.x < -0.01f)
            transform.localScale = new Vector2(0.7866277f, transform.localScale.y);

        // (Jeremy) Animation values
        marioanim.SetFloat("XVelocity", Mathf.Abs(mariorb.linearVelocity.x)); // Velocity Check
        marioanim.SetBool("isGrounded", isGrounded);

        

    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Hammer") // HAMMER
        {
            Destroy(collision.gameObject); // (Jermaine) destroy this hammer Mario collides with hammer
            marioanim.SetBool("hasHammer", true);
            hasHammer = true;
            isHoldingHammer = true;      
            hammerTimer = hammerWeaponDuration;

            hammerHitbox.EnableHitbox(); // (Jermaine) turns hammer collider on

            
        }

        if (collision.gameObject.tag == "Slowmo") // SLOW MO
        {
            Destroy(collision.gameObject); // (Jeremy) destroy this slowmo pickup
            //marioanim.SetBool("inSlowmo", true);
            slowmoTimer = slomoDuration;
            inSlowmo = true;
            print("slowmo start");
        }

    }
   

}