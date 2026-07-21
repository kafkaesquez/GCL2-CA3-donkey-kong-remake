using UnityEngine;

public class Barrels : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 1f;
    //(Matthew) Barrel logic
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 36f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            rb.AddForce(collision.transform.right * speed, ForceMode2D.Impulse);
        }

    }

}
