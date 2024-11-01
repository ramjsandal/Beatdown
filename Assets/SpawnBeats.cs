using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnBeats : MonoBehaviour
{
    public GameObject leftShape;
    public GameObject rightShape;
    public int yRange = 2;
    public int xRange = 3;

    private Vector3 position;

    private void Start()
    {
        position = transform.position;
    }


    public void spawnBeat()
    {

        float xPos = position.x + Random.Range(0, xRange + 1);
        float yPos = position.y + Random.Range(0, yRange + 1);

        bool isLeft = Random.Range(0, 2) == 0;

        if (isLeft)
        {
            Instantiate(leftShape, new Vector3(xPos, yPos, position.z), Quaternion.identity);
        } else
        {
            Instantiate(rightShape, new Vector3(xPos, yPos, position.z), Quaternion.identity);
        }

    }
}
