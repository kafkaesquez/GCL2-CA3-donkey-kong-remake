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
        Debug.Log("animation is trigger");
        if (inkActive)
            return;

        inkActive = true;

        blooperParentAnimator.SetTrigger("Appear");
        blooperAnimator.SetTrigger("Appear");
        inkAnimator.SetTrigger("Appear");

        Invoke(nameof(ResetInk), inkDuration); //reset ink in desired (5) seconds
    }

    private void ResetInk()
    {
        inkActive=false;
    }

}
