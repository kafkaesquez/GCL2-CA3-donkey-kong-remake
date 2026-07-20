using UnityEngine;

public class BlooperBarrel : MonoBehaviour
{
    public InkManager inkManager;

    private void Awake() //find game object with ink manager script attacged
    {
        Debug.Log("i found the ink manager!");

            inkManager = FindFirstObjectByType<InkManager>();
    }
    public void ActivateInk()
    {
        if (inkManager != null) //prevent crashing
        {
            Debug.Log("the ink is showing!");

            inkManager.ShowInk();
        }
    }
}
