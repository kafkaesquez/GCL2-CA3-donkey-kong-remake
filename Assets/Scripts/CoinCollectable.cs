using JetBrains.Annotations;
using UnityEngine;

public class CoinCollectable : MonoBehaviour

{

    public CoinManager cm;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            cm.coinCount += 1;
        }
    }
    

}