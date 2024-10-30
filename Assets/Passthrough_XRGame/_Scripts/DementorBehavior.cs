using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DementorBehavior : MonoBehaviour
{
    public float moveSpeed = 0.2f;

    private Transform player;

    private NavMeshAgent navMeshAgent;

    void Start()
    {
        //transform.LookAt(GameObject.FindGameObjectWithTag("MainCamera").transform);
        player = GameObject.FindGameObjectWithTag("MainCamera").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        //ManualMove();

        var target = Camera.main.transform.position;
        navMeshAgent.destination = target;
        navMeshAgent.speed = moveSpeed;
    }

    void ManualMove()
    {
        transform.LookAt(player.position);
        transform.position = Vector3.MoveTowards(this.transform.position, player.position, moveSpeed * Time.deltaTime);
    }

}
