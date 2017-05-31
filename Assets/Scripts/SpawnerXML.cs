using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[RequireComponent(typeof(Spawner))]
public class SpawnerXML : MonoBehaviour
{
    /// <summary>
    /// Stores individual data associated with each spawned object
    /// </summary>
    public class SpawnerData
    {
        public Vector3 position;
        public Quaternion rotation;
    }
    [XmlRoot]
    public class XMLContainer
    {
        [XmlArray]
        public SpawnerData[] spawners;
    }

    public string fileName = "DefaultFileName";

    private Spawner spawner;
    private string fullPath;

    /// <summary>
    /// Data container for XML
    /// </summary>
    private XMLContainer data;

    /// <summary>
    /// Saves XMlContainer instance to a file as XML format 
    /// </summary>
    /// <param name="path">
    /// Path to file
    /// </param>
    void SaveToPath(string path)
    {
        // Create a serializer of type XMLContainer
        XmlSerializer serializer = new XmlSerializer(typeof(XMLContainer));
        // Open a file stream at path using create file node
        using (FileStream stream = new FileStream(path, FileMode.Create))
        {
            // Serialize stream to data
            serializer.Serialize(stream, data);
        }
    }

    /// <summary>
    /// Load XMLContainer from path (NOTE: only run if the file definitely exists)
    /// </summary>
    /// <param name="path">
    /// Path to file
    /// </param>
    XMLContainer Load(string path)
    {
        // Create a serializer of type XMLContainer
        XmlSerializer serializer = new XmlSerializer(typeof(XMLContainer));
        // Open a file stream at path
        using (FileStream stream = new FileStream(path, FileMode.Open))
        {
            // RETURN the deserialized stream as XMLContainer
            return serializer.Deserialize(stream) as XMLContainer;
        }
    }

    /// <summary>
    /// Saves whatever data value is to XML file
    /// </summary>
    public void Save()
    {
        // SET data to new Data
        data = new XMLContainer();
        // SET objects to objects in spawner
        List<GameObject> objects = spawner.objects;
        // SET data to new SpawnerData[obejcts.count]
        data.spawners = new SpawnerData[objects.Count];
        // FOR i = 0 to objects.Count
        for (int i = 0; i < objects.Count; i++)
        {
            // SET spawner to new Spawnerdata
            SpawnerData spawnerData = new SpawnerData();
            // SET item to objects[i]
            GameObject item = objects[i];
            // SET spawners position and rotation to item's
            spawnerData.position = item.transform.position;
            spawnerData.rotation = item.transform.rotation;
            // SET data.spawners[i] = spawner;
            data.spawners[i] = spawnerData;
        }
        // CALL SaveToPath(fullPath)
        SaveToPath(fullPath);
    }

    /// <summary>
    /// Applies the saved data to the scene uUsing spawner)
    /// /// </summary>
    void Apply()
    {
        // SET spawners to data.spawners
        XMLContainer spawners = new XMLContainer();
        spawners = data;
        // FOR i = 0 to spawners.Length
        for (int i = 0; i < spawners.spawners.Length; i++)
        {
            // SET data to spawners[i]
            SpawnerData d = spawners.spawners[i];
            // CALL spawner.spawn() and pass daata.position, data.rotation
            spawner.Spawn(d.position, d.rotation);
        }

    }

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        // SET spawner to Spawner Components
        spawner = GetComponent<Spawner>();
    }

    // Use this for initialization
    void Start()
    {
        // SET fullPath to Application.datapath + "/" + fileName + ".xml"
        fullPath = Application.dataPath + "/" + fileName + ".xml";
        // IF file exists
        if (File.Exists(fullPath))
        {
            data = Load(fullPath);
            Apply();
        }
    }
}
