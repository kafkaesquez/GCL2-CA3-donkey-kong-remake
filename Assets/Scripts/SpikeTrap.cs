using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PlayerDeath : MonoBehaviour
{
    public GameObject player;
    public Transform respawnPoint;


    void Start()
    {

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))    // Check if the object we collided with is tagged "Player"
        {
            player.transform.position = respawnPoint.position;   //when player collides with objects tagged, player dies and gets set back to the respawn point
        }
    }

}
