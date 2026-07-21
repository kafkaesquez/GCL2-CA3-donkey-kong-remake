using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinSequence : MonoBehaviour
{
    public GameObject mario;
    public GameObject heart;

    public Animator donkeyKongAnimator;
    public Animator paulineAnimator;

    public Spawner barrelSpawner;

    public Sprite marioIdleSprite;
    public void StartWinSequence()
    {
        StartCoroutine(PlayWinSequence());
    }

    IEnumerator PlayWinSequence()
    {
        paulineAnimator.enabled = false;
        //stop pauline animation

        MarioMovement marioMovement = mario.GetComponent<MarioMovement>();
        Animator marioAnimator = mario.GetComponent<Animator>();

        //stop Mario from moving
        marioMovement.canMove = false;

        //stop whatever animation is currently playing
        marioAnimator.enabled = false;

        //force the sprite to be the idle sprite
        SpriteRenderer marioSprite = mario.GetComponent<SpriteRenderer>();
        marioSprite.sprite = marioIdleSprite;



        Rigidbody2D rb = mario.GetComponent<Rigidbody2D>();

        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        //stops mario from moving

        barrelSpawner.enabled = false; //pause barrel spawner

        GameObject[] barrels = GameObject.FindGameObjectsWithTag("Barrel");

        foreach (GameObject barrel in barrels)
        {
            Rigidbody2D barrelrb = barrel.GetComponent<Rigidbody2D>();

            if (barrelrb != null)
            {
                barrelrb.linearVelocity = Vector2.zero;
                barrelrb.angularVelocity = 0f;
                barrelrb.simulated = false; //freezes all physics 
            }
        }

        yield return new WaitForSecondsRealtime(1f);
        //pause 1 sec

        heart.SetActive(true);
        //reveal da heart

        yield return new WaitForSecondsRealtime(2f);
        //pause 2 secs

        donkeyKongAnimator.SetTrigger("Shock");
        //play donkey kong shocked animation 

        yield return new WaitForSecondsRealtime(5f);
        //pause 5 secs

        SceneManager.LoadScene("WinScene");
    }

}
