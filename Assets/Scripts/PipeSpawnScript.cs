using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawnScript : MonoBehaviour
{

    public GameObject pipe;
    public GameObject lastPipeSpawned;

    public GameControllerScript gameController;

    // Start is called before the first frame update
    void Start()
    {
        SpawnPipe();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x - lastPipeSpawned.transform.position.x > gameController.pipeDistance)
        {
            SpawnPipe();
        }
    }

    void SpawnPipe()
    {
        float lowestPoint = transform.position.y - gameController.pipeHeightOffset;
        float highestPoint = transform.position.y + gameController.pipeHeightOffset;

        float randomHeight = Random.Range(lowestPoint, highestPoint);

        Vector3 location = new(transform.position.x, randomHeight, transform.position.z);

        lastPipeSpawned = Instantiate(pipe, location, transform.rotation);
    }
}
