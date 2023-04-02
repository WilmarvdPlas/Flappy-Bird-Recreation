using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;

public class PipeScript : MonoBehaviour
{
    public GameControllerScript gameController;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControllerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (Vector3.left * gameController.pipeMoveSpeed) * Time.deltaTime;
        if (transform.position.x < gameController.pipeDeadZone)
        {
            Destroy(gameObject);
        }
    }
}
