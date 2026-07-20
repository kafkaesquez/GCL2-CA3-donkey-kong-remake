using UnityEngine;

public class BlooperBarrel : MonoBehaviour
{
    public InkManager inkManager;

    private void Awake() //calls awake when blooper barrel is created, instantiate in spawner script creates it
    {
            inkManager = FindFirstObjectByType<InkManager>(); //finds ink manager gameobj which has ink manager script
    }
    public void ActivateInk()
    {
        if (inkManager != null) //prevent crashing
        {
            inkManager.ShowInk();
        }
    }
}
