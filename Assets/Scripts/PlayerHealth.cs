using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int lives = 3;
    public float invincibilityDuration = 2f;
    public float postmortem = 1.5f;

    

    private bool isInvincible = false;
    private SpriteRenderer sr;
    private int playerLayer;
    private int barrelLayer;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        playerLayer = LayerMask.NameToLayer("Player");
        barrelLayer = LayerMask.NameToLayer("Barrel");

        Debug.Log($"playerLayer: {playerLayer}, barrelLayer: {barrelLayer}"); //test
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Barrel") && !isInvincible)
        {
            LoseLife();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Barrel") && !isInvincible)
        {
            LoseLife();
        }
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


}
