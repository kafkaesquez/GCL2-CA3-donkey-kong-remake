using UnityEngine;

public class Barrels : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 1f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            rb.AddForce(collision.transform.right * speed, ForceMode2D.Impulse);
        }

        if (collision.gameObject.CompareTag("HammerHitbox"))
        {
            BlooperBarrel blooper = GetComponent<BlooperBarrel>();

            if (blooper != null) //if found squid barrel, run the code, if not, skip
            {
                blooper.ActivateInk();
            }

            Destroy(gameObject);
        }

    }

}
