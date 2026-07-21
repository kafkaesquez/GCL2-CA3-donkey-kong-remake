using UnityEngine;

public class FloorEffect : MonoBehaviour
{
    public float speedMultiplier = 1f;   
    public float acceleration = 10f;     

    // (Jermaine) defult mario movement speed
    public float defaultSpeedMultiplier = 1f;
    public float defaultAcceleration = 10f;
    private void OnTriggerEnter2D(Collider2D collision) //when mario enters the sticky floor collider
    {
        if (collision.TryGetComponent<MarioMovement>(out var mario)) 
        {
            mario.speedMultiplier = speedMultiplier; //follows whatever speed set in inspector
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