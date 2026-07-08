using UnityEngine;

public class HammerPickup : MonoBehaviour
{
    //private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       // GetComponent<MarioMovement>();
        //animator = GetComponent<Animator>();
    }

    // (Jermaine) Hammer pickup
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")  //(Jermaine) when mario picks up hammer
        {
            Destroy(gameObject);  //(Jermaine) removes hammer sprite after picking up
            //PickUpItem(collision.gameObject);

        }
    }
    private void PickUpItem(GameObject item)
    {
        // (Jermaine) Mario change to attack animation
       // animator.SetBool("hasHammer", true);

       // Destroy(gameObject);  //(Jermaine) removes hammer sprite after picking up
    }
}