using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ladder : MonoBehaviour
{
    private float vertical;
    [SerializeField] private float Speed; //so that its easier to edit on inspector depending on the movement script that jeremy comes up with.
    [SerializeField] private float gravity; //reference to jeremy's gravity ltr on
    [SerializeField] private Collider2D FinalHitbox;  //to trigger the animations for mario finishing his ladder climb, and to differentiate the two trigger hitboxes

    public bool isLadder;
    public bool isClimbing;
    public bool isClimbingFinish;

    public Animator marioanim;

    [SerializeField] private Rigidbody2D rb; //so we can reference mario's hitbox

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        marioanim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = true;
            print("im in the ladder");
        }
        if (collision == FinalHitbox)
        {
            isClimbingFinish = true; 
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = false;
            isClimbing = false;
            isClimbingFinish = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        vertical = Input.GetAxis("Vertical");

        if (isLadder && Mathf.Abs(vertical) > 0)
        {
            isClimbing = true;
        }
    }

        private void FixedUpdate() //cause we are handling physics aka the gravity
    {
        if (isClimbing)
        {
            print("im gonna climbnow");
            rb.gravityScale = 0f;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, vertical * Speed); //makes it so mario can go up and down without gravity weighing on him
            marioanim.SetBool("isClimbing", isClimbing);
            if (isClimbingFinish)
            {
                marioanim.SetTrigger("isClimbingFinish");
            }
        }
        else
        {
            rb.gravityScale = gravity;
        }
    }
}

