using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameDataController : MonoBehaviour
{
	public static SaveData saveData;
    public int NumberOfWorlds;

	private void Awake()
	{
		LoadData();
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

        if(data != "")
        {
            saveData = JsonUtility.FromJson<SaveData>(data);

            //print("loading: " + saveData);
        } else
        {
            print("data is null");
        }
    }

	private void OnDisable()
	{
		//SaveGame();
	}

    private void Update()
    {
        if(saveData.fullWorlds != null)
        {
            NumberOfWorlds = saveData.fullWorlds.Count;
        }
       
    }

    public static CharacterData GetCharacterState(int id)
    {
        if (saveData.fullWorlds == null)
            return new CharacterData { };

        if (saveData.fullWorlds.Any(t => t.id == id))
        {
            return saveData.fullWorlds.FirstOrDefault(t => t.id == id).character;
        }

        return new CharacterData { };
    }
    public static PlayerControllerData GetPlayerControllerState(int id)
    {
        if (saveData.fullWorlds == null)
            return new PlayerControllerData { };

        if (saveData.fullWorlds.Any(t => t.id == id))
        {
            return saveData.fullWorlds.FirstOrDefault(t => t.id == id).player;
        }

        return new PlayerControllerData { };
    }
    public static List<ObjectControllerData> GetObjectControllerStates(int id)
    {
        if (saveData.fullWorlds == null)
            return null;

        if (saveData.fullWorlds.Any(t => t.id == id))
        {
            return saveData.fullWorlds.FirstOrDefault(t => t.id == id).objects;
        }

        return null;
    }
    public static List<WorldContainerData> GetWorldControllerStates(int id)
    {
        if (saveData.fullWorlds == null)
            return null;

        if (saveData.fullWorlds.Any(t => t.id == id))
        {
            return saveData.fullWorlds.FirstOrDefault(t => t.id == id).worlds;
        }

        return null;
    }


    public static void SetState(string date, PlayerController player, CharController character, ObjectController[] objects, WorldController[] worlds)
    {

        if (saveData.fullWorlds == null)
            saveData.fullWorlds = new List<World>(); //append to list

       
        print("current: "+player.transform.position + "|"+ player.transform.rotation);
        var playerData = new PlayerControllerData {position = player.transform.position, rotation = player.transform.rotation };
        var characterData = new CharacterData {position = character.transform.position, rotation = character.transform.rotation};

        List<ObjectControllerData> objectControllerDatas = new List<ObjectControllerData>();
        List<WorldContainerData> worldContainerDatas = new List<WorldContainerData>();

        foreach(ObjectController o in objects)
        {
            var objData = new ObjectControllerData {position = o.transform.position, rotation = o.transform.rotation, isInWorld = o.gameObject.active, name = o.name};
            objectControllerDatas.Add(objData);
        }
        foreach(WorldController w in worlds)
        {
            var worldContainerData = new WorldContainerData {position = w.transform.position, rotation = w.transform.rotation, worldNum = w.num };
            worldContainerDatas.Add(worldContainerData);
        }

        var worldData = new World() { id = 0, date = date, player = playerData, character = characterData, objects = objectControllerDatas, worlds = worldContainerDatas}; //get last id++
        saveData.fullWorlds.RemoveAll(t => t.id == worldData.id);
        saveData.fullWorlds.Add(worldData);
    }


    //List<ObjectControllerData> objectsData = new List<ObjectControllerData>();
    //foreach (ObjectController o in allObjects)
    //{
    //    var objData = new ObjectControllerData() { id = o.serializeId, position = o.transform.position, rotation = o.transform.rotation };
    //    objectsData.Add(objData);
    //}
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
}