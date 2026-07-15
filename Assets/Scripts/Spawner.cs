using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject normalBarrelPrefab;
    public GameObject droppingBarrelPrefab;
    public float minTime = 2f;
    public float maxTime = 4f;

    [Range(0, 1)] public float skipBarrelChance = 0.15f;

    private void OnEnable()
    {
        Invoke(nameof(Spawn), minTime);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void Spawn()
    {
        GameObject prefabToSpawn = (Random.value < skipBarrelChance)
            ? droppingBarrelPrefab
            : normalBarrelPrefab;

        Instantiate(prefabToSpawn, transform.position, Quaternion.identity);

        Invoke(nameof(Spawn), Random.Range(minTime, maxTime));
    }
}
