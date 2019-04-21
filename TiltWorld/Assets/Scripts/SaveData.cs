using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct SaveData
{

    public List<World> fullWorlds;
	//public List<MagicCubeData> magicCubes;
}

[Serializable]
public struct MagicCubeData
{
	public string id;
    public string date;
	public bool isRed;
}

//------------------------------------
[Serializable]
public struct World
{
    public string date;
    public int id;
    public List<ObjectControllerData> objects;
    public List<WorldContainer> worlds;
    public Character character;
    public PlayerController player;
}

//Inside the world

[Serializable]
public struct PlayerControllerData
{
    public Vector3 position;
    public Quaternion rotation;
}

[Serializable]
public struct ObjectControllerData
{
    public int id;
    public Vector3 position;
    public Quaternion rotation;
}

[Serializable]
public struct WorldContainer
{
    public int worldNum;
    public Vector3 position;
    public Quaternion rotation;
}

[Serializable]
public struct Character
{
    public Vector3 position;
    public Quaternion rotation;
}