using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int lives = 3;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Barrel"))
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
            Respawn();
        }
    }

    private void Respawn()
    {
        // reset Mario's position to spawn point, play hurt animation, etc.
    }

    private void Die()
    {
        // game over logic — reload scene, show game over screen, etc.
        Debug.Log("Game Over");
    }
}
