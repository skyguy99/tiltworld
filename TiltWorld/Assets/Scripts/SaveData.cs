using System;
using System.Collections.Generic;

[Serializable]
public struct SaveData
{
    public int test;

	public List<MagicCubeData> magicCubes;
}

[Serializable]
public struct MagicCubeData
{
	public string id;
    public string date;
	public bool isRed;
}