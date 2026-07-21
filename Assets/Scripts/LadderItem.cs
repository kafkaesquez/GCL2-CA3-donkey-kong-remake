using UnityEngine;

public class LadderItem : MonoBehaviour
{

    public Animator marioanim;
    public bool holdingLadder;
    [SerializeField] private GameObject LadderItemObject;
    [SerializeField] private GameObject ladderPrefab; // to reference which object to "build"
    [SerializeField] private Transform placePoint; // empty object infront of mario to reference where to place the ladder
    [SerializeField] private GameObject LadderInstructions; //to show the player an intuitive reference of what to click to place the ladder down

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        marioanim = GetComponent<Animator>();
        holdingLadder = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("LadderItem"))
        {
            holdingLadder |= true;
            marioanim.SetBool("HoldingLadder", holdingLadder);
            Destroy(LadderItemObject);
            LadderInstructions.SetActive(true);
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (holdingLadder && Input.GetMouseButtonDown(0))
        {
            PlaceLadder(); //left mouse click to build the ladder
        }
    }

    void PlaceLadder()
    {
        Vector3 spawnPos = placePoint != null ? placePoint.position : transform.position; //null is there as a safeholder just in case placepoint is empty, so it falls to marios own position
        Instantiate(ladderPrefab, spawnPos, Quaternion.identity); 

        holdingLadder = false;
        marioanim.SetBool("HoldingLadder", holdingLadder);
    }
}
