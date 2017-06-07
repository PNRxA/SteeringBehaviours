using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    public float spawnRate = 1f;

    private float spawnTimer = 0f;
    public List<GameObject> objects = new List<GameObject>();

    /// <summary>
    /// Callback to draw gizmos that are pickable and always drawn.
    /// </summary>
    void OnDrawGizmos()
    {
        // Draw a cube to indicate where the box is that we're spawning objects
        Gizmos.DrawCube(transform.position, transform.localScale);
    }

    /// <summary>
    /// Generates a random point within the transform's scale
    /// </summary>
    /// <returns>
    /// Random Point
    /// </returns>
    Vector3 GenerateRandomPoint()
    {
        // SET halfscale to half of the transform's scales
        Vector3 halfScale = transform.localScale * .5f;
        // SET randomPoint vector to zero
        Vector3 randomPoint = Vector3.zero;
        // SET randomPoint x, y & z to random range between -halfScale to halfScale (HINT can do individually)
        randomPoint = randomiseVector(halfScale);
        // RETURN randomPoint
        return randomPoint;
    }

    /// <summary>
    /// Randomises (-Value, Value) passed to the function
    /// </summary>
    /// <param name="value">
    /// Vector3 to randomise
    /// </param>
    /// <returns>
    /// Randomised Vector3
    /// </returns>
    Vector3 randomiseVector(Vector3 value)
    {
        return new Vector3(Random.Range(-value.x, value.x), Random.Range(-value.y, value.y), Random.Range(-value.z, value.z));
    }

    /// <summary>
    /// Spawns the prefab at a given position and with rotation
    /// </summary>
    /// <param name="position">
    /// Position of prefab
    /// </param>
    /// <param name="rotation">
    /// Rotation of prefab
    /// </param>
    public void Spawn(Vector3 position, Quaternion rotation)
    {
        // SET clone to new instance of prefab
        GameObject clone = Instantiate(prefab, position, rotation);
        // ADD clone to objects list
        objects.Add(clone);
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // SET spawntimer to spawnTimer + deltaTime
        spawnTimer += Time.deltaTime;
        // IF spawnTimer > spawnRate
        if (spawnTimer > spawnRate)
        {
            // CALL Spawn() and pass randomPoint, Quaternion identity
            Spawn(transform.position + GenerateRandomPoint(), Quaternion.identity);
            // SET spawnTimer to zero
            spawnTimer = 0;
        }
    }
}
