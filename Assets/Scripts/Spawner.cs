using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject normalBarrelPrefab;
    public GameObject droppingBarrelPrefab;
    public GameObject blooperBarrelPrefab;
    public float minTime = 2f;
    public float maxTime = 4f;

    [Range(0, 1)] public float skipBarrelChance = 0.15f; //slider of values 0 to 1
    [Range(0, 1)] public float blooperBarrelChance = 0.1f;

    private void OnEnable()
    {
        Invoke(nameof(Spawn), minTime);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void Spawn() //unity calls spawn every few seconds
    {
        GameObject prefabToSpawn;

        if (Random.value < blooperBarrelChance) //is this a blooper barrel?
        {
            prefabToSpawn = blooperBarrelPrefab;
        }
        else // original barrel spawn script
        {
            prefabToSpawn = (Random.value < skipBarrelChance)
              ? droppingBarrelPrefab
              : normalBarrelPrefab;
        }
          

        Instantiate(prefabToSpawn, transform.position, Quaternion.identity);

        Invoke(nameof(Spawn), Random.Range(minTime, maxTime));
    }
}
