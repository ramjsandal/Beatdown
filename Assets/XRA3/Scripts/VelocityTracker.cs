using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityTracker : MonoBehaviour
{
    private Vector3 prevPos;
    private Vector3 currentPos;
    void Start()
    {
        prevPos = transform.position;
        currentPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        prevPos = currentPos;
        currentPos = transform.position;
    }

    public Vector3 GetVelocity()
    {
        return (currentPos - prevPos) * (1.0f / Time.smoothDeltaTime);
    }
}
