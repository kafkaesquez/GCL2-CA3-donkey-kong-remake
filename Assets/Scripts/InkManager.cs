using UnityEngine;

public class InkManager : MonoBehaviour
{
    public Animator blooperParentAnimator;
    public Animator blooperAnimator;
    public Animator inkAnimator;
    private bool inkActive = false;
    public float inkDuration = 5f;

    public void ShowInk()
    {

        if (inkActive) //is ink playing? if yes, stop here, do nothing
            return;

        inkActive = true; //ink isn't playing initially, so it proceeds to play the ink.

        //start animation of blooper and inks
        blooperParentAnimator.SetTrigger("Appear");
        blooperAnimator.SetTrigger("Appear");
        inkAnimator.SetTrigger("Appear");

        Invoke(nameof(ResetInk), inkDuration); //reset ink in desired (5) seconds
        //something like an alarm clock!
    }

    private void ResetInk() //sets it all back to the initial state, resets it. 
    {
        inkActive=false;
    }

}
