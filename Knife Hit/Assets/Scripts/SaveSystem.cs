using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    private const string KEY = "Json";

    public SaveData LoadData() 
    {
        string json = PlayerPrefs.GetString(KEY, null);
        SaveData data = JsonUtility.FromJson<SaveData>(json);
        return data;
    }

    public void SaveData(SaveData data) 
    {
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(KEY, json);
    }
}
