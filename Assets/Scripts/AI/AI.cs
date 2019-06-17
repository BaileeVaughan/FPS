using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    public float maxVelocity = 15f, maxDist = 10f;
    public Vector3 velocity;
    public SteeringBehaviour[] behaviours;
    public NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        behaviours = GetComponents<SteeringBehaviour>();
    }

    private void Update()
    {
        //set force to zero
        Vector3 force = Vector3.zero;

        //step 1. loop through all behaviours and get forces
        foreach (var behaviour in behaviours)
        {
            force += behaviour.GetForce(this);
        }
        //step 2. apply forces to velocity
        velocity += force * Time.deltaTime;
        //step 3. limit velocity to max velocity
        if (velocity.magnitude > maxVelocity)
        {
            velocity = velocity.normalized * maxVelocity;
        }
        //step 4. apply velocity to NavMeshAgent
        if (velocity.magnitude > 0)
        {
            //position for next frame using velocity
            Vector3 desiredPos = transform.position + velocity * Time.deltaTime;
            NavMeshHit hit;
            //check is desiredPos is within the NavMesh
            if (NavMesh.SamplePosition(desiredPos, out hit, maxDist, -1))
            {
                agent.SetDestination(hit.position);
            }
        }
    }
}
