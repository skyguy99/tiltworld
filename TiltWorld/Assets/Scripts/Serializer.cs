using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class Serializer : MonoBehaviour
{
    static readonly string SAVE_FILE = "player.json";

    //public GameObject player;
    string filename;
    string dataSavedToPrefs;
    SaveData saveData;

    public ObjectController[] objects;
    public WorldController[] worlds;
    public CharController character;
    public PlayerController player;

    void Start()
    {
        objects = GameObject.FindObjectsOfType<ObjectController>();
        worlds = GameObject.FindObjectsOfType<WorldController>();
        character = GameObject.FindObjectOfType<CharController>();
        player = GameObject.FindObjectOfType<PlayerController>();

     
        //SaveData data = new SaveData()
        //{
        //    name = "Sloan",
        //    armour = 5,
        //    items = new System.Collections.Generic.List<string>(),
        //    position = player.transform.position,
        //    rotation = player.transform.rotation
        //};

        //data.items.Add("Sword");
        //data.items.Add("Shield");
        //data.items.Add("Potion");

        //string json = JsonUtility.ToJson(data);

        //filename = Path.Combine(Application.persistentDataPath, SAVE_FILE);



        //if (File.Exists(filename))
        //{
        //    File.Delete(filename);
        //}

        //File.WriteAllText(filename, json);

        //Debug.Log("Player saved to " + filename);

        //string jsonFromFile = File.ReadAllText(filename);

        //SaveData copy = JsonUtility.FromJson<SaveData>(jsonFromFile);
        //Debug.Log("READ: "+copy.name);

        //player.transform.position = copy.position;
        //player.transform.rotation = copy.rotation;

    }

    private void Update()
    {
     
    }

    public void LoadPositions()
    {

        dataSavedToPrefs = PlayerPrefs.GetString("GameData");
        saveData = JsonUtility.FromJson<SaveData>(dataSavedToPrefs);

        //SaveData copy = JsonUtility.FromJson<SaveData>(jsonFromFile);
        print("Got data: " + saveData.ToString());

        //player.transform.position = copy.position;
        //player.transform.rotation = copy.rotation;
    }
    public void SavePositions()
    {
        objects = GameObject.FindObjectsOfType<ObjectController>();

        List<SaveData> datas = new List<SaveData>();

        SaveData data1 = new SaveData()
        {
            objId = player.serializeId,
            position = player.transform.position,
            rotation = player.transform.rotation
        };
        datas.Add(data1);
        SaveData data2 = new SaveData()
        {
            objId = character.serializeId,
            position = character.transform.position,
            rotation = character.transform.rotation
        };
        datas.Add(data2);
        foreach (WorldController w in worlds)
        {
            SaveData data = new SaveData()
            {
                objId = w.serializeId,
                position = w.transform.position,
                rotation = w.transform.rotation
            };
            datas.Add(data);
        }
        foreach(ObjectController o in objects)
        {
            SaveData data = new SaveData()
            {
                objId = o.serializeId,
                position = o.transform.position,
                rotation = o.transform.rotation
            };
            datas.Add(data);
        }

        //Add it all here
        List<string> jsons = new List<string>();
        foreach(SaveData d in datas)
        {
            string json = JsonUtility.ToJson(d);
            jsons.Add(json);
        }
        //string json = JsonUtility.ToJson(datas[0]);

        //PlayerPrefs.SetString("GameData", json);
        filename = Path.Combine(Application.persistentDataPath, SAVE_FILE);


        if (File.Exists(filename))
        {
            File.Delete(filename);
        }

        foreach(string j in jsons)
        {
            File.WriteAllText(filename, j);
            //File.AppendText(filename, j);
        }
      
        Debug.Log("Data saved to " + filename);
    }


}
