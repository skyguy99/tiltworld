using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameDataController : MonoBehaviour
{
	public static SaveData saveData;

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
		var data = PlayerPrefs.GetString("GameData");

        print(data);
		saveData = JsonUtility.FromJson<SaveData>(data);

        print("loading: "+saveData);
    }

	private void OnDisable()
	{
		SaveGame();
	}

	public static bool GetState(MagicCube magicCube)
	{
		if (saveData.magicCubes == null)
			return false;

		if (saveData.magicCubes.Any(t => t.id == magicCube.name))
			return saveData.magicCubes.FirstOrDefault(t => t.id == magicCube.name).isRed;

		return false;
	}

	public static void SetState(MagicCube magicCube, bool isRed)
	{
		if (saveData.magicCubes == null)
			saveData.magicCubes = new List<MagicCubeData>();

		var magicCubeData = new MagicCubeData() { id = magicCube.name, isRed = isRed };
		saveData.magicCubes.RemoveAll(t => t.id == magicCubeData.id);
		saveData.magicCubes.Add(magicCubeData);
	}
}