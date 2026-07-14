using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinSequence : MonoBehaviour
{
    public GameObject mario;
    public GameObject heart;

    public Animator donkeyKongAnimator;
    public Animator paulineAnimator;

    public void StartWinSequence()
    {
        StartCoroutine(PlayWinSequence());
    }

    IEnumerator PlayWinSequence()
    {
        paulineAnimator.enabled = false;
        //stop pauline animation

        MarioMovement marioMovement = mario.GetComponent<MarioMovement>();

        marioMovement.canMove = false;

        Rigidbody2D rb = mario.GetComponent<Rigidbody2D>();

        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        //stops player from moving

        yield return new WaitForSeconds(1f);
        //pause 1 sec

        heart.SetActive(true);
        //reveal da heart

        yield return new WaitForSeconds(2f);
        //pause 2 secs

        donkeyKongAnimator.SetTrigger("Shock");
        //play donkey kong shocked animation 

        yield return new WaitForSeconds(4f);
        //pause 2 secs

        SceneManager.LoadScene("WinScene");
    }

}
