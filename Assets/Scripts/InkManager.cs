using UnityEngine;

public class InkManager : MonoBehaviour
{
    public Animator blooperAnimator;
    public Animator inkAnimator;

    public void ShowInk()
    {
        blooperAnimator.SetTrigger("Appear");
        inkAnimator.SetTrigger("Appear");
    }

}
