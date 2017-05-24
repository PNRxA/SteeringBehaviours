using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAgent : MonoBehaviour
{
    public Vector3 force;
    public Vector3 velocity;
    public float maxVelocity = 100f;

    private List<SteeringBehaviour> behaviours;

    // Use this for initialization
    void Start()
    {
        // This is Scott
        SteeringBehaviour[] ohBehave = GetComponents<SteeringBehaviour>();
        behaviours = new List<SteeringBehaviour>(GetComponents<SteeringBehaviour>());
    }

    // Update is called once per frame
    void Update()
    {
        ComputeForces();
        ApplyVelocity();
    }

    void ComputeForces()
    {
        // Reset the force before computing
        force = Vector3.zero;

        // FOR each behavioiur attached to AIAgent
        foreach (var behaviour in behaviours)
        {
            // Check if behaviour is !active
            if (!behaviour.enabled)
            {
                // CONTINUE to the next behaviour
                continue;
            }
            else
            {
                // Get force from behaviour
                force += behaviour.GetForce();
                // IF forces are too big 
                if (force.magnitude > maxVelocity)
                {
                    // Clamp force to the max velocity
                    //force = Vector3.ClampMagnitude(force, maxVelocity);
                    force = force.normalized * maxVelocity;
                    // EXIT for loop
                    break;
                }
            }
        }
    }

    void ApplyVelocity()
    {
        // Append force to velocity with deltaTime
        velocity += force * Time.deltaTime;
        // IF velocity is greater than maxVelocity 
        if (velocity.magnitude > maxVelocity)
        {
            // Clamp velocity
            //force = Vector3.ClampMagnitude(force, maxVelocity);
            force = force.normalized * maxVelocity;
        }
        // IF velocity != zero
        if (velocity != Vector3.zero)
        {
            // Append transform position by velocity
            transform.position += velocity * Time.deltaTime;
            // Transform rotate by velocity
            transform.rotation = Quaternion.LookRotation(velocity);
        }
    }
}
