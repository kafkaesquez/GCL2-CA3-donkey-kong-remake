using UnityEngine;

public class LadderItem : MonoBehaviour
{

    public Animator marioanim;
    public bool holdingLadder;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        marioanim = GetComponent<Animator>();
        holdingLadder = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            holdingLadder |= true;
            marioanim.SetBool("HoldingLadder", holdingLadder);
            Destroy(collision.gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
