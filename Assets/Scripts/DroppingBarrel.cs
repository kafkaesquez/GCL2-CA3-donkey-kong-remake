using UnityEngine;

public class DroppingBarrel : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    public float lifetime = 5f;

    private int platformLayer;
    //(Matthew) Dropping Barrel logic

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        platformLayer = LayerMask.NameToLayer("Platform");
    }

    private void Start()
    {
        Physics2D.IgnoreLayerCollision(gameObject.layer, platformLayer, true);
        Destroy(gameObject, lifetime);
    }
}
