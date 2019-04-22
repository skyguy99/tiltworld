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
    public List<WorldContainerData> worlds;
    public CharacterData character;
    public PlayerControllerData player;
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
    public bool isInWorld;
}

[Serializable]
public struct WorldContainerData
{
    public int worldNum;
    public Vector3 position;
    public Quaternion rotation;
}

[Serializable]
public struct CharacterData
{
    public Vector3 position;
    public Quaternion rotation;
}