using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : SteeringBehaviour
{
    public Transform target;
    public override Vector3 GetForce(AI owner)
    {
        //set force to zero
        Vector3 force = Vector3.zero;
        //if target is null
        if (target)
        {
            //set desiredForce to target - current position
            Vector3 desiredForce = target.position - transform.position;
            //set force to desiredForce normalized * weighting
            force += desiredForce.normalized * weighting;
        }
        //returning force
        return force;
    }
}
