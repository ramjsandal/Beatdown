using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeBehavior : MonoBehaviour
{
    // the 3 explosion prefabs to use
    public GameObject smallHit;
    public GameObject mediumHit;
    public GameObject largeHit;

    public Rigidbody rb;
    public OVRInput.Controller controller;
    public bool isLeft;

    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            // get fist speed
            //Vector3 velocity = OVRInput.GetLocalControllerVelocity(controller);
            Vector3 velocity = rb.velocity;
            Debug.Log(velocity);

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
                rb.AddForce(velocity);
                Destroy(this.gameObject, 2);
                Debug.Log("wrong");
            }
        }
    }

    private void Break(float speed)
    {
        Debug.Log($"called break with speed: {speed}");
        if (speed < .5)
        {
            // do nothing 
            return;
        }
        else if (speed <= 1)
        {
            var ob = Instantiate(smallHit);
            ob.GetComponent<Rigidbody>().AddExplosionForce(speed, ob.transform.position, 1);
        }
        else if (speed <= 2)
        {
            var ob = Instantiate(mediumHit);
            ob.GetComponent<Rigidbody>().AddExplosionForce(speed, ob.transform.position, 1);
        }
        else if (speed > 2)
        {
            var ob = Instantiate(largeHit);
            ob.GetComponent<Rigidbody>().AddExplosionForce(speed, ob.transform.position, 1);
        }

        Destroy(this.gameObject);
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

}
