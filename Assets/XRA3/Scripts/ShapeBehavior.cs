using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShapeBehavior : MonoBehaviour
{
    // the 3 explosion prefabs to use
    public GameObject smallHit;
    public GameObject mediumHit;
    public GameObject largeHit;
    public AudioClip correctClip;
    public AudioClip incorrectClip;
    public LoadScene loadTheScene;

    public Rigidbody ribo;
    public VelocityTracker left;
    public VelocityTracker right;
    public OVRInput.Controller controller;
    public bool isLeft;
    public bool menuShape;
    public string sceneName;
    public float speed = .025f;

    private bool moveForwards = true;

    private void Start()
    {
        var vTrackers = FindObjectsByType<VelocityTracker>(FindObjectsSortMode.InstanceID);
        left = vTrackers.First(a => a.gameObject.CompareTag("LeftController"));
        right = vTrackers.First(a => a.gameObject.CompareTag("RightController"));
        moveForwards = true;
    }

    private void Update()
    {
        if (menuShape)
        {
            return;
        }

        if (moveForwards && !LevelManager.Instance.levelOver)
        {
            this.transform.position -= new Vector3(0, 0, speed);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            if (menuShape)
            {
                if ((other.gameObject.CompareTag("RightController") || other.gameObject.CompareTag("LeftController")))
                {
                    loadTheScene.LoadNewScene(sceneName, 1);
                    Break(1f);
                }
                return;
            }
            // get fist speed
            Vector3 velocity = OVRInput.GetLocalControllerVelocity(controller);
            /*
            Vector3 velocity = Vector3.zero;
            if (other.gameObject.CompareTag("RightController"))
            {
                velocity = right.GetVelocity();
            }

            if (other.gameObject.CompareTag("LeftController"))
            {
                velocity = left.GetVelocity();
            }
            */


            // if we hit the shape with the right fist
            if ((isLeft && other.gameObject.CompareTag("LeftController")) ||
                (!isLeft && other.gameObject.CompareTag("RightController")))
            {
                // spawn different thing based on our controller velocity

                Break(velocity.magnitude);
            }
            else if ((!isLeft && other.gameObject.CompareTag("LeftController")) ||
                (isLeft && other.gameObject.CompareTag("RightController")))
            // otherwise
            {

                AudioSource.PlayClipAtPoint(incorrectClip, transform.position);
                ribo.AddForce(velocity * 100);
                LevelManager.Instance.LosePoints(50);
                moveForwards = false;
                Destroy(this.gameObject, 2);
                Debug.Log("wrong");
            }
        }
    }

    private void Break(float speed)
    {
        int scoreIncrease = 50;
        if (speed < .5)
        {
            // do nothing 
            return;
        }
        else if (speed <= 1)
        {
            scoreIncrease += 10;
            var ob = Instantiate(smallHit, transform.position, Quaternion.identity);
        }
        else if (speed <= 2)
        {
            scoreIncrease += 15;
            var ob = Instantiate(mediumHit, transform.position, Quaternion.identity);
        }
        else if (speed > 2)
        {
            scoreIncrease += 25;
            var ob = Instantiate(largeHit, transform.position, Quaternion.identity);
        }

        if (!menuShape)
        {
            LevelManager.Instance.GainPoints(scoreIncrease);
        }

        AudioSource.PlayClipAtPoint(correctClip, transform.position);
        Explode();
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
    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 1);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(.1f, transform.position, 1, 3.0F);
        }
    }

}
