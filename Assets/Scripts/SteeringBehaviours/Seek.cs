﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : SteeringBehaviour
{
    public Transform target;
    public float stoppingDistance = 0f;


    public override Vector3 GetForce()
    {
        Vector3 force = Vector3.zero;

        // IF there is no target, then return force
        if (target == null)
        {
            return force;
        }
        // SET desiredForce to direction from target to position
        Vector3 desiredForce = target.position - transform.position;
        // SET desiredForce y to zero
        desiredForce.y = 0;
        // IF direction is greater than stopping distance
        if (desiredForce.magnitude > stoppingDistance)
        {
            // SET desiredForce to normalized and multipl|y by weighting
            desiredForce = desiredForce.normalized * weighting;
            // SET force to desiredForce and subtract owner's velocity            
            force = desiredForce - owner.velocity;
        }

        return force;
    }
}
// Flee seek