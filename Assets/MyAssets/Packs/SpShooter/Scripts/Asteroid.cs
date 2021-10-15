using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private float rotateSpeeed = 10f;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private SpawnManager spawnManager;

    private void Start()
    {
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if (spawnManager == null) Debug.LogError("SpawnManager is null in Asteroid");

        if (explosionPrefab == null) Debug.LogError("Explosion prefab is null in Asteroid");

        transform.position = Vector3.up * 3f;
    }

    void Update()
    {
        transform.Rotate(0, 0, rotateSpeeed * Time.deltaTime, Space.Self);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Laser"))
        {
            Destroy(other.gameObject);
            spawnManager.StartSpawnning();
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject, 0.25f);
        }
    }
}
