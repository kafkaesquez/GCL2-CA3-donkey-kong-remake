using UnityEngine;
using UnityEngine.SceneManagement;

public class Bomboclat : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            print("scene end");
            SceneManager.LoadScene("Scene End");
        }
    }
}