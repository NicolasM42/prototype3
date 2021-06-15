using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private PlayerController playerControllerScript;

    public GameObject[] obstacles;
    public Vector3 spawnPos = new Vector3(25, 0, 0);
    private int index;
    private bool gamesBegun = false;

    public float startDelay = 2;
    public float repeatRate = 2;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerControllerScript.startSceneActive && !gamesBegun)
        {
            InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
            gamesBegun = true;
        }
    }

    void SpawnObstacle()
    {
        index = Random.Range(0, 4);
        if (playerControllerScript.gameOver == false)
        {
            Instantiate(obstacles[index], spawnPos, obstacles[index].transform.rotation);
        }
    }
}
