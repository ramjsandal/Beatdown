using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnBeats : MonoBehaviour
{
    public GameObject leftShape;
    public GameObject rightShape;
    public GameObject door;
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
        bool isDoor = Random.Range(0, 20) == 0;

        if (isDoor)
        {
            float scale = 1 + ((float)Random.Range(0, 100) / 100f);
            var ob = Instantiate(door, new Vector3(xPos, yPos, position.z), Quaternion.identity);
            ob.transform.localScale = new Vector3(scale, scale, 1); 
        }
        else if (isLeft)
        {
            Instantiate(leftShape, new Vector3(xPos, yPos, position.z), Quaternion.identity);
        }
        else
        {
            Instantiate(rightShape, new Vector3(xPos, yPos, position.z), Quaternion.identity);
        }

    }
}
