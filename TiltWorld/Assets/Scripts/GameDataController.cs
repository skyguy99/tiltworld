using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameDataController : MonoBehaviour
{
	public static SaveData saveData;

	private void Awake()
	{
		//LoadData();
	}

	[ContextMenu("Save Data")]
	public void SaveGame()
	{
        print("saving");
		var data = JsonUtility.ToJson(saveData);
		PlayerPrefs.SetString("GameData", data);
	}

	[ContextMenu("Load Data")]
	public void LoadData()
	{
        string data = "";
        if(PlayerPrefs.HasKey("GameData"))
           {
            data = PlayerPrefs.GetString("GameData");
        }

        print("DATA"+data);
		saveData = JsonUtility.FromJson<SaveData>(data);

        print("loading: "+saveData);
    }

	private void OnDisable()
	{
		//SaveGame();
	}

    //public static int GetState(int id)
    //{
    //    if (saveData.fullWorlds == null)
    //        return -1;

    //    if (saveData.fullWorlds.Any(t => t.id == id))
    //        return saveData.fullWorlds.FirstOrDefault(t => t.id == id).objects.Count;

    //    return -1;
    //}
    public static PlayerController GetPlayerControllerState(int id)
    {
        if (saveData.fullWorlds == null)
            return null;

        if (saveData.fullWorlds.Any(t => t.id == id))
            return saveData.fullWorlds.FirstOrDefault(t => t.id == id).player;

        return null;
    }
    public static void SetState(string date, PlayerController player)
    {

        //Vector3 position, Quaternion rotation

        if (saveData.fullWorlds == null)
            saveData.fullWorlds = new List<World>(); //append to list

        //List<ObjectControllerData> objectsData = new List<ObjectControllerData>();
        //foreach (ObjectController o in allObjects)
        //{
        //    var objData = new ObjectControllerData() { id = o.serializeId, position = o.transform.position, rotation = o.transform.rotation };
        //    objectsData.Add(objData);
        //}
        var playerData = new PlayerControllerData {position = player.transform.position, rotation = player.transform.rotation };

        var worldData = new World() { id = 0, date = date, player = player}; //get last id++
        saveData.fullWorlds.RemoveAll(t => t.id == worldData.id); //THIS
        saveData.fullWorlds.Add(worldData);
    }
    //public static void SetState(string date, ObjectController[] allObjects)
    //{

    //    //Vector3 position, Quaternion rotation

    //    if (saveData.fullWorlds == null)
    //        saveData.fullWorlds = new List<World>(); //append to list

    //    List<ObjectControllerData> objectsData = new List<ObjectControllerData>();
    //    foreach (ObjectController o in allObjects)
    //    {
    //        var objData = new ObjectControllerData() { id = o.serializeId, position = o.transform.position, rotation = o.transform.rotation };
    //        objectsData.Add(objData);
    //    }

    //    var worldData = new World() { id = 0, date = date, objects = objectsData }; //get last id++
    //    saveData.fullWorlds.RemoveAll(t => t.id == worldData.id); //THIS
    //    saveData.fullWorlds.Add(worldData);
    //}


    //public static bool GetState(MagicCube magicCube)
    //{
    //	if (saveData.magicCubes == null)
    //		return false;

    //	if (saveData.magicCubes.Any(t => t.id == magicCube.name))
    //		return saveData.magicCubes.FirstOrDefault(t => t.id == magicCube.name).isRed;

    //	return false;
    //}

    //public static void SetState(MagicCube magicCube, bool isRed)
    //{
    //	if (saveData.magicCubes == null)
    //		saveData.magicCubes = new List<MagicCubeData>();

    //	var magicCubeData = new MagicCubeData() { id = magicCube.name, isRed = isRed };
    //	saveData.magicCubes.RemoveAll(t => t.id == magicCubeData.id);
    //	saveData.magicCubes.Add(magicCubeData);
    //}
}