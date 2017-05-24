using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AIAgent))]
public class SteeringBehaviour : MonoBehaviour
{
    public float weighting = 7.5f;
    public Vector3 force;
    [HideInInspector] public AIAgent owner;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        owner = GetComponent<AIAgent>();
    }

    public virtual Vector3 GetForce()
    {
        return Vector3.zero;
    }
}
