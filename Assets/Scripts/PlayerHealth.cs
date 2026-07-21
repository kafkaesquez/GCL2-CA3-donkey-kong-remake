using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int lives = 3;
    public float invincibilityDuration = 2f;
    public float postmortem = 1.5f;
    public MarioMovement marioMovement;

    public float bounceForce = 8f;
    public float parryWindow = 0.2f;
    private float lastJumpPressedTime = -999f;

    private bool isInvincible = false;
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private int playerLayer;
    private int barrelLayer;

    public float flashDuration = 0.15f;
    private Color originalColor;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        playerLayer = LayerMask.NameToLayer("Player");
        barrelLayer = LayerMask.NameToLayer("Barrel");
        originalColor = sr.color;
        Debug.Log($"playerLayer: {playerLayer}, barrelLayer: {barrelLayer}"); //test
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            lastJumpPressedTime = Time.time;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Barrel") || isInvincible) return;
        if (isInvincible || marioMovement.IsHoldingHammer) return;

        ContactPoint2D contact = collision.GetContact(0);
        bool landedOnTop = contact.normal.y > 0.5f && rb.linearVelocity.y <= 0.1f;
        bool withinParryWindow = Time.time - lastJumpPressedTime <= parryWindow;

        if (landedOnTop && withinParryWindow)
        {
            ParryBounce(collision.gameObject);
        }
        else
        {
            LoseLife();
        }
    }

    private void ParryBounce(GameObject barrel)
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, bounceForce);
        Destroy(barrel);
        StartCoroutine(Flash());
        // hook in a sound/particle effect here if you want feedback
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Barrel")) return; 
        if (isInvincible || marioMovement.IsHoldingHammer) return;

        LoseLife();
    }

    private void LoseLife()
    {
        lives--;
        Debug.Log("Lives remaining: " + lives);

        if (lives <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(InvincibilityRoutine());
        }
    }

    private IEnumerator InvincibilityRoutine()
    {
        isInvincible = true;
        Physics2D.IgnoreLayerCollision(playerLayer, barrelLayer, true);

        // flicker the sprite so it's visually clear Mario's invincible
        float elapsed = 0f;
        while (elapsed < invincibilityDuration)
        {
            sr.enabled = !sr.enabled;
            yield return new WaitForSeconds(0.1f);
            elapsed += 0.1f;
        }
        sr.enabled = true;

        Physics2D.IgnoreLayerCollision(playerLayer, barrelLayer, false);
        isInvincible = false;
    }

    private void Die()
    {
        Debug.Log("Game Over");
        StartCoroutine(GameOverRoutine());
    }
    private IEnumerator GameOverRoutine()
    {
        // trigger death animation / sound / UI here
        yield return new WaitForSeconds(postmortem);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private IEnumerator Flash()
    {
        sr.color = Color.green;
        yield return new WaitForSeconds(flashDuration);
        sr.color = originalColor;
    }
}
