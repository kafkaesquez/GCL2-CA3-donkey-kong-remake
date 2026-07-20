using UnityEngine;

public class HammerHitbox : MonoBehaviour
{
    private Collider2D hitboxCollider;

    private void Awake()
    {
        hitboxCollider = GetComponent<Collider2D>();

        hitboxCollider.enabled = false; // (Jermaine) Disables the hammer collider before picking up hammer
    }



    public void DisableHitbox()
    {
        hitboxCollider.enabled = false;
        Debug.Log("DisableHitbox called — collider enabled = " + hitboxCollider.enabled);
    }

    public void EnableHitbox()
    {
        hitboxCollider.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"[{gameObject.name} | ID:{gameObject.GetInstanceID()}] Hammer trigger hit: {collision.gameObject.name} | this collider enabled: {hitboxCollider.enabled}", this);
        if (collision.CompareTag("Barrel"))
        {
            if (collision.CompareTag("Barrel"))
            {
                BlooperBarrel blooper = collision.GetComponent<BlooperBarrel>();

                if (blooper != null)
                {
                    blooper.ActivateInk();
                }

                Destroy(collision.gameObject);
            }
        }

    }
}
