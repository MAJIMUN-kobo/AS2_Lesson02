using UnityEngine;

public class FlowSpawner : MonoBehaviour
{
    public GameObject flowner;

    private float spawnInterval = 3f;
    private float spawnTimer = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;
        if(spawnTimer >= spawnInterval)
        {
            Spawn();
            spawnTimer = 0f;
        }
    }

    public void Spawn()
    {
        int randomX = Random.Range(-7, 7);
        int randomY = Random.Range(-3, 5);
        Vector3 randomPosition = new Vector3(randomX, randomY, 25);

        GameObject flow = Instantiate(flowner, randomPosition, Quaternion.identity);
        Rigidbody rb = flow.GetComponent<Rigidbody>();
        rb.AddForce(Vector3.forward * -1 * 5, ForceMode.Impulse);
    }
}
