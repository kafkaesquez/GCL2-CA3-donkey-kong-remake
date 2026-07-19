using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.AnimatedValues;

public class Ladder : MonoBehaviour
{
    public float vertical;
    [SerializeField] public float Speed; //so that its easier to edit on inspector depending on the movement script that jeremy comes up with.
    [SerializeField] public float gravity; //reference to jeremy's gravity ltr on   

    [SerializeField] public float currentSpeed;

    public bool isLadder;
    public bool isClimbing;
    public bool isClimbingFinish;

    public Animator marioanim;

    [SerializeField] private Rigidbody2D rb; //so we can reference mario's hitbox
    [SerializeField] private MarioMovement marioMovement; //so we can reference mario's controller

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        marioanim = GetComponent<Animator>();
        if (marioMovement == null)
            marioMovement = GetComponent<MarioMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = true;
            print("im in the ladder");

        }
        if (collision.CompareTag("ClimbFinish"))
        {
            print("climb has finished");
            isClimbingFinish = true;
            marioanim.SetBool("isClimbingFinish", isClimbingFinish); //to trigger the animations for mario finishing his ladder climb
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = false;
            isClimbing = false;
            marioanim.speed = 1f;
            marioanim.SetBool("isClimbing", isClimbing);
        }
        if (collision.CompareTag("ClimbFinish"))
        {
            isClimbingFinish = false;
            marioanim.SetBool("isClimbingFinish", isClimbingFinish);
            print("left climb finish zone");
        }
    }

    // Update is called once per frame
    void Update()
    {
        vertical = Input.GetAxis("Vertical");

        if (isLadder && Mathf.Abs(vertical) > 0 && (marioMovement == null || !marioMovement.IsHoldingHammer))
        {
            isClimbing = true;
        }

        // (Jermaine) If already climbing and hammer gets picked up mid-climb, cancel it
        if (isClimbing && marioMovement != null && marioMovement.IsHoldingHammer)
        {
            isClimbing = false;
        }
    }

    private void FixedUpdate() //cause we are handling physics aka the gravity
    {
        float currentSpeed = Speed;


        if (marioMovement.inSlowmo) //(Jeremy) slo-mo effect added by Jeremy, can't get it to work on MarioMovement script no matter what i try, sorry! rest of the script works as intended!
        {
            //print("ladder recieved in slow mo");
            currentSpeed = Speed / Time.timeScale;
            if (!isClimbing)
            {
                gravity = 4f; // (Jeremy) Ensure mario can climb ladders normally even in slow mo
            }
            else gravity = 0f;
        }
        else
        {
            currentSpeed = (Speed = Time.timeScale);
            gravity = 1f;
        }






        if (isClimbing)
        {
            print("im gonna climbnow");
            rb.gravityScale = 0f;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, vertical * currentSpeed); //makes it so mario can go up and down without gravity weighing on him
            marioanim.SetBool("isClimbing", isClimbing);
            marioanim.speed = Mathf.Abs(vertical) > 0f ? 1f : 0f; //to calculate the climbing frames when mario moves instead of it being a static sprite when going up
        }
        else
        {
            rb.gravityScale = gravity;
            marioanim.speed = 1f;
        }
    }
}

