using UnityEngine;

public class BlooperBarrel : MonoBehaviour
{
    public InkManager inkManager;

    private void Awake() //find game object with ink manager script attacged
    {
            inkManager = FindFirstObjectByType<InkManager>();
    }
    public void ActivateInk()
    {
        if (inkManager != null) //prevent crashing
        {
            inkManager.ShowInk();
        }
    }
}
