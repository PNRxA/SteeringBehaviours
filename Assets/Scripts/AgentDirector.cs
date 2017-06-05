using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGL;

public class AgentDirector : MonoBehaviour
{
    public Transform selectedTarget;
    public float rayDistance = 1000f;
    public LayerMask selectionLayer;
    private AIAgent[] agents;
    // Use this for initialization
    void Start()
    {
        // Set agents to FindObjectsOfType AIAgent
        agents = FindObjectsOfType<AIAgent>();

    }

    // Apply selected target to all agents
    void ApplySelection()
    {
        agents = FindObjectsOfType<AIAgent>();
        foreach (AIAgent agent in agents)
        {
            // SET pathFollowing to agent.GetComponent<PathFollowing>();
            PathFinding pathFinding = agent.GetComponent<PathFinding>();
            // IF pathFollowing is not null
            if (pathFinding != null)
            {
                // SET pathFollowing.target to selectedTarget
                pathFinding.target = selectedTarget;
                // CALL pathFollowing.UpdatePath()
                pathFinding.UpdatePath();
            }
        }
    }

    // Constantly checking foro input
    void CheckSelection()
    {
        // SET ray to ray from Camera
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // SET hit to new RaycastHit
        RaycastHit hit = new RaycastHit();
        // IF Physics.Raycast() and pass ray, out hit, rayDistance, selectionLayer
        if (Physics.Raycast(ray, out hit, rayDistance, selectionLayer))
        {
            // CALL GizmosGL.AddSphere() and pass hit.point, 5f, Quaternion.Identity, any color
            GizmosGL.AddSphere(hit.point, 5f, Quaternion.identity, Color.red);
            // IF user clicked left mouse button
            if (Input.GetMouseButtonDown(0))
            {
                // SET selectedTarget to hit.collider.transform
                selectedTarget = hit.collider.transform;
                // CALL ApplySelection();
                ApplySelection();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // CALL CheckSelection()
        CheckSelection();
    }
}
