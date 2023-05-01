using UnityEngine;

public class SpikeGenerator : MonoBehaviour
{
    public GameObject spike;
    public BGScroll BGScroll;       // new

    public float MinSpeed;
    public float MaxSpeed;
    public float currentSpeed;

    public float SpeedMultiplier;

    // Start is called before the first frame update
    void Awake()
    {
        currentSpeed = MinSpeed;
        generateSpike();
    }

    public void generateSpike()
{
    float xPos = 2.5f; // fixed distance between spikes
    Vector3 spawnPosition = new Vector3(transform.position.x + xPos, transform.position.y, transform.position.z);

    GameObject SpikeIns = Instantiate(spike, spawnPosition, transform.rotation);

    SpikeIns.GetComponent<SpikeScript>().spikeGenerator = this;

    Invoke("generateSpike", Random.Range(2.0f, 4.0f)); // call generateSpike() again after a random delay
}
// Changed "spikeIns" to "SpikeIns" and "getComponent" to "GetComponent". changed "SpikeSprite"
    // Update is called once per frame
    void Update()
    {
        if(currentSpeed < MaxSpeed)
        {
            currentSpeed += SpeedMultiplier;
            BGScroll.scrollSpeed += SpeedMultiplier; // Increase the scroll speed         // new
        }
    }
}
