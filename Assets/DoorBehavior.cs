using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehavior : MonoBehaviour
{
    public float speed = .025f;
    public AudioClip hit;
    void Update()
    {
        this.transform.position -= new Vector3(0, 0, speed);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                AudioSource.PlayClipAtPoint(hit, transform.position, 1.5f);
                LevelManager.Instance.LosePoints(100);
                Destroy(this.gameObject);
            }
        }
    }
    
}
