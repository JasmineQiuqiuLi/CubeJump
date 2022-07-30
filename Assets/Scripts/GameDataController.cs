using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.Reflection;

public enum Page
{
    Main,
    End
}
public class GameDataController : MonoBehaviour
{

    public GameData data;
    public static GameDataController instance;

    static bool isLaunched=false;

    //List of GameResources
    public List<Sprite> skinSpritesForward;
    public List<Sprite> skinSpritesBack;


    //when rank is clicked, save current page. So when the Goback button is clicked, the app knows to load the previous page
    Page page = Page.Main;
    public Page PrePage { get { return page; } set { page = value; } }


    private void Awake()
    {
        instance = this;

        if (Read())
        {
            Debug.Log("GameData has been successfully read");
        }
        else
        {
            ResetGameData();
        }

        if (!isLaunched)
        {
            DontDestroyOnLoad(this.gameObject);
            isLaunched = true;
        }

       
    }

    private bool Read()
    {
        if (!File.Exists(Application.persistentDataPath + "/GameData5.data")) return false;
        try
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (FileStream fs = File.Open(Application.persistentDataPath + "/GameData5.data", FileMode.Open))
            {
                data = (GameData)binaryFormatter.Deserialize(fs);
                return true;
            }        
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
            return false;
        }
    }

    public void Save()
    {
        try
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using(FileStream fs = File.Create(Application.persistentDataPath + "/GameData5.data"))
            {
                binaryFormatter.Serialize(fs, data);  
            }
        }
        catch(System.Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    public void ResetGameData()
    {
       
        bool[] skinUnlocked = { true, false, false, false }; //hard code
        data = new GameData(true, new int[3], 0, skinUnlocked, 0);
        Save();
    }

}
