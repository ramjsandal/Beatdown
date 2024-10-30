using Meta.WitAi;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShapeBehavior : MonoBehaviour
{
    // the 3 explosion prefabs to use
    public GameObject smallHit;
    public GameObject mediumHit;
    public GameObject largeHit;

    public Rigidbody rb;
    public VelocityTracker left;
    public VelocityTracker right;
    public OVRInput.Controller controller;
    public bool isLeft;
    public bool menuShape;
    public string sceneName;

    private void MenuShape()
    {
       SceneManager.LoadScene(sceneName); 
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            if (menuShape)
            {
                Invoke("MenuShape", 3);
                Break(3.5f, false);
                this.gameObject.GetComponent<Renderer>().enabled = false;
                this.gameObject.GetComponent<Collider>().enabled = false;
                return;
            }
            // get fist speed
            //Vector3 velocity = OVRInput.GetLocalControllerVelocity(controller);
            Vector3 velocity = Vector3.zero;
            if (other.gameObject.CompareTag("RightController"))
            {
                velocity = right.GetVelocity();
            }

            if (other.gameObject.CompareTag("LeftController"))
            {
                velocity = left.GetVelocity();
            }


            // if we hit the shape with the right fist
            if ((isLeft && other.gameObject.CompareTag("LeftController")) ||
                (!isLeft && other.gameObject.CompareTag("RightController")))
            {
                // spawn different thing based on our controller velocity
                Break(velocity.magnitude);
            }
            else
            // otherwise
            {
                rb.AddForce(velocity * 100);
                Destroy(this.gameObject, 2);
                Debug.Log("wrong");
            }
        }
    }

    private void Break(float speed, bool destroy = true)
    {
        Debug.Log($"called break with speed: {speed}");
        if (speed < .5)
        {
            // do nothing 
            return;
        }
        else if (speed <= 1)
        {
            var ob = Instantiate(smallHit, transform.position, Quaternion.identity);
        }
        else if (speed <= 2)
        {
            var ob = Instantiate(mediumHit, transform.position, Quaternion.identity);
        }
        else if (speed > 2)
        {
            var ob = Instantiate(largeHit, transform.position, Quaternion.identity);
        }

        Explode();
        if (destroy)
        {
            Destroy(this.gameObject);
        }
    }

    private void GetControllerVelocities()
    {
        List<OVRInput.Controller> controllers = new List<OVRInput.Controller>() { OVRInput.Controller.Touch, OVRInput.Controller.RHand,
            OVRInput.Controller.LHand, OVRInput.Controller.Hands,
            OVRInput.Controller.Gamepad, OVRInput.Controller.Remote, OVRInput.Controller.RTouch, OVRInput.Controller.LTouch };

        foreach (var controller in controllers)
        {
            Debug.Log($"{controller}: {OVRInput.GetLocalControllerVelocity(controller)}");
        }
    }
    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 1);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(1, transform.position, 1, 3.0F);
        }
    }

}
