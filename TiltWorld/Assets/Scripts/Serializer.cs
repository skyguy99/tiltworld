﻿using UnityEngine;
using System.Collections;
using System.IO;

public class Serializer : MonoBehaviour
{
    static readonly string SAVE_FILE = "player.json";

    public GameObject player;
    string filename;

    void Start()
    {
        SaveData data = new SaveData()
        {
            name = "Sloan",
            armour = 5,
            items = new System.Collections.Generic.List<string>(),
            position = player.transform.position,
            rotation = player.transform.rotation
        };

        data.items.Add("Sword");
        data.items.Add("Shield");
        data.items.Add("Potion");

        string json = JsonUtility.ToJson(data);

        filename = Path.Combine(Application.persistentDataPath, SAVE_FILE);

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
        if(Input.GetKey("space"))
        {

        }
    }
    public void SavePos()
    {

    }

    public void LoadPositions()
    {
        Debug.Log("Player saved to " + filename);

        string jsonFromFile = File.ReadAllText(filename);

        SaveData copy = JsonUtility.FromJson<SaveData>(jsonFromFile);
        Debug.Log("READ: " + copy.name);

        player.transform.position = copy.position;
        player.transform.rotation = copy.rotation;
    }


}
