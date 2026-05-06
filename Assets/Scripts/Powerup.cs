using System;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("powerup");
    }
    // Update is called once per frame
    
}
