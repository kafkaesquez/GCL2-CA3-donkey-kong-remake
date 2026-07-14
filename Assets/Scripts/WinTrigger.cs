using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    public WinSequence winSequence;

    private bool hasWon = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (hasWon)
            return;


        if (collision.CompareTag("Player"))
        {
            hasWon = true;
            winSequence.StartWinSequence();
        }

        GetComponent<BoxCollider2D>().enabled = false;
    }

}
