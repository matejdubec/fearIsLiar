using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class CSaveSystem
{
	private static string path = Application.persistentDataPath + "/playerData.bin";
	public static void SavePlayerData(bool tutorialDone)
	{
		BinaryFormatter formatter = new BinaryFormatter();
		FileStream stream = new FileStream(path, FileMode.Create);

		CPlayerData data = new CPlayerData(tutorialDone);

		formatter.Serialize(stream, data);
		stream.Close();

	}

	public static CPlayerData LoadPlayerData()
	{
		if(File.Exists(path))
		{
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream stream = new FileStream(path, FileMode.Open);

			CPlayerData data = formatter.Deserialize(stream) as CPlayerData;

			stream.Close();

			return data;
		}
		else
		{
			Debug.LogError($"Save file not found in {path}");
			CPlayerData data = new CPlayerData(false);

			return data;
		}
	}
}
