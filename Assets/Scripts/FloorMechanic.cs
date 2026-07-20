using UnityEngine;

public class FloorEffect : MonoBehaviour
{
    public float speedMultiplier = 1f;   // e.g. 1.5 for ice (faster/slidey), 0.5 for sticky (slower)
    public float acceleration = 10f;     // e.g. 2 for ice (slow to speed up/stop), 30 for sticky (grippy)

    [Header("Defaults to restore on exit")]
    public float defaultSpeedMultiplier = 1f;
    public float defaultAcceleration = 10f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<MarioMovement>(out var mario))
        {
            mario.speedMultiplier = speedMultiplier;
            mario.acceleration = acceleration;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<MarioMovement>(out var mario))
        {
            mario.speedMultiplier = defaultSpeedMultiplier;
            mario.acceleration = defaultAcceleration;
        }
    }
}