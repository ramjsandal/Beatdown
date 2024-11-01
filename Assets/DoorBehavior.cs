using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehavior : MonoBehaviour
{
    public float speed = .025f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position -= new Vector3(0, 0, speed); 
    }

    
    private void OnCollisionEnter(Collision other)
    {
        Destroy(this.gameObject);
    }
}
