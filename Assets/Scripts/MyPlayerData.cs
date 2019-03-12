using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;

[System.Serializable]
public class MyPlayerData {

//GAME OBJECTS (SHIFT TO PLAYERCONTROLLER?)
    [SerializeField] private GameObject armL;
    [SerializeField] private GameObject armR;
    [SerializeField] private GameObject core;
    [SerializeField] private GameObject legL;
    [SerializeField] private GameObject legR;
    [SerializeField] private GameObject jetPack;
    [SerializeField] private GameObject weaponSlot1;
    [SerializeField] private GameObject weaponSlot2;
    [SerializeField] private GameObject weaponSlot3;
    [SerializeField] private GameObject weaponSlot4;
    private string armName;
    private string legName;
    private string coreName;
    private string jetpackName;
    private string weaponSlot1Name;
    //For future

    //private GameObject weaponSlot5;
    //private GameObject weaponSlot6;
    //private GameObject weaponSlot7;
    //private GameObject weaponSlot8;

//STRINGS
    private GameObject toolSlot1;
    private GameObject toolSlot2;

    //For future
    //private GameObject toolSlot3;
    //private GameObject toolSlot4;
    //private GameObject toolSlot5;

    [SerializeField] public string SarmL;
    [SerializeField] private string SarmR;
    [SerializeField] private string Score;
    [SerializeField] private string SlegL;
    [SerializeField] private string SlegR;
    [SerializeField] private string SjetPack;
    [SerializeField] private string SweaponSlot1;
    [SerializeField] private string SweaponSlot2;
    [SerializeField] private string SweaponSlot3;
    [SerializeField] private string SweaponSlot4;    

    private string StoolSlot1;
    private string StoolSlot2;

//FINAL MECH VALUES
    public int speed = 0;
    public int health = 0;     //make arrays prolly
    public int shield = 0;     //To be used with non regenerating shield
    public int energyUse = 0;     // Works as both shield and booster energy
    public int coreEnergy = 0;
    public int[] energyParts = new int[12];     //ENERGY PER PART
    public int[] healthParts = new int[12];     //HEALTH PER PART
    public int[] shieldParts = new int[12];     //SHIELD PER PART
    public int energyJetpackConsume = 0;
    public int acceleration = 0;
    public int energyRegeneration = 0;
    public float mass = 0;
    public string x;
    
//JSON STUFF
    static private string allPartsPath;  
    static private string currentPath;  
    static string allPartsJsonString;
    static string currentPartsJsonString;
    public JSONObject allParts;
    public JSONObject currentParts;


//FUNCTIONS

    //FINDS PARTS FROM CURRENTPARTS BY NAME, THEN FIND THEIR VALUES IN ALLPARTS 
    public void SetPartValues()
    {
        for (int i = 0; i < currentParts.Count; i++) 
        {
            string name = currentParts[i]["name"];
            for (int j = 0; j < allParts.Count; j++)
            {
                if (allParts[j]["name"] == name && allParts[j]["unlocked"] == true)
                {
                    if (allParts[j]["type"] == "Core")
                    {
                        coreEnergy = allParts[j]["energy max"];
                    }
                    if (allParts[j]["type"] == "JetPack")
                    {
                        energyJetpackConsume = allParts[j]["energy consumed"]; 
                    }
                    mass = mass + (float)allParts[j]["mass"];                       // ADD MASS TOGETHER
                    energyUse = energyUse + allParts[j]["energy use"];
                    energyParts[i] = allParts[j]["energy use"];
                    healthParts[i] = allParts[j]["health"];
                    shieldParts[i] = allParts[j]["shield"];

                }    
                
            }
        }
    } 

    //RUN THIS WHENEVER PLAYER IS CREATED, THEN SET CUSTOM VALUES LATER
    public void SetDefaults()
    {
        //currentParts[0]["name"] = allParts["01A"]["name"];
        for (int i = 0; i < currentParts.Count; i++) 
        {
            if (currentParts[i] != null)
            {
                {
                    currentParts[i]["name"] = allParts[i]["name"];
                }
            }
        } 
    }
    public void UnlockParts(string partID) //string part
    {
        allParts[partID]["unlocked"] = true;
    }
    public void Save()
    {
//USING ALLPARTS UPDATE CURRENT PARTS
    
        currentParts["armL"]["name"] = armL.name;
        currentParts["armR"]["name"] = armR.name;
        currentParts["legL"]["name"] = legL.name;
        currentParts["legR"]["name"] = legR.name;
        currentParts["weaponSlot1"]["name"] = weaponSlot1.name;
        currentParts["weaponSlot2"]["name"] = weaponSlot2.name;
        currentParts["weaponSlot3"]["name"] = weaponSlot3.name;
        currentParts["weaponSlot4"]["name"] = weaponSlot4.name;
        currentParts["toolSlot1"]["name"] = toolSlot1.name;
        currentParts["toolSlot2"]["name"] = toolSlot2.name;
        currentParts["jetPack"]["name"] = jetPack.name;


    /*  JSONObject playerJson = new JSONObject();
        
        playerJson.Add("armL", SarmL);
        playerJson.Add("armR", SarmR);
        playerJson.Add("legL", SlegL);
        playerJson.Add("legR", SlegR);
        playerJson.Add("jetpack", SjetPack);
        playerJson.Add("weaponSlot1", SweaponSlot1);
        playerJson.Add("weaponSlot2", SweaponSlot2);
        playerJson.Add("weaponSlot3", SweaponSlot3);
        playerJson.Add("weaponSlot4", SweaponSlot4);
        playerJson.Add("toolSlot1", StoolSlot1);
        playerJson.Add("toolSlot2", StoolSlot2);

         //SAVE JSON IN COMPUTER
        string path = Application.persistentDataPath + "/PlayerSave.json";
        File.WriteAllText(path, playerJson.ToString());
    */

        
    }

    
    //CREATE JSON FILE ON EACH PC
    public void SetPath()
    {
        allPartsPath =  Application.persistentDataPath + "/AllParts.json";
        currentPath = Application.persistentDataPath + "/CurrentParts.json";
        allPartsJsonString = File.ReadAllText(allPartsPath);
        allParts = (JSONObject)JSON.Parse(allPartsJsonString);
        currentPartsJsonString = File.ReadAllText(currentPath);
        currentParts = (JSONObject)JSON.Parse(currentPartsJsonString);
        
    }
    

    /* public string DisplayPartsDetails()
    {
        string name = "01A";
        
        return(allParts["01A"]);
        
    }*/

    public void MoveFiles()
    {
        string aFileName = "AllParts.json";
        string cFileName = "CurrentParts.json";
        string sourcePath = @"C:\Users\Public\TestFolder";
        string targetPath =  @"C:\Users\Public\TestFolder\SubDir";
        
    }

    public void UpdateParts()
    {
        
        if (SarmL != currentParts["name"])
        {
            currentParts["name"] = SarmL;
        } 
        if (SarmR != currentParts["name"])
        {
            currentParts["name"] = SarmR;
        } 
        if (SlegL != currentParts["name"])
        {
            currentParts["name"] = SlegL;
        } 
        if (SlegR != currentParts["name"])
        {
            currentParts["name"] = SlegR;
        } 
        if (Score != currentParts["name"])
        {
            currentParts["name"] = Score;
        } 
        if (SweaponSlot1 != currentParts["name"])
        {
            currentParts["name"] = SweaponSlot1;
        } 
         if (SweaponSlot2 != currentParts["name"])
        {
            currentParts["name"] = SweaponSlot2;
        } 
         if (SweaponSlot3 != currentParts["name"])
        {
            currentParts["name"] = SweaponSlot3;
        } 
         if (SweaponSlot4 != currentParts["name"])
        {
            currentParts["name"] = SweaponSlot4;
        } 
        if (StoolSlot1 != currentParts["name"])
        {
            currentParts["name"] = SweaponSlot4;
        } 
        if (StoolSlot2 != currentParts["name"])
        {
            currentParts["name"] = SweaponSlot4;
        } 
    }


     


    public void Load() 
    {
        
        SarmL = currentParts["armL"]["name"];
        SarmR = currentParts["armR"]["name"];
        SlegL = currentParts["legL"]["name"];
        SlegR = currentParts["legR"]["name"];
        SweaponSlot1 = currentParts["weaponSlot1"]["name"];
        SweaponSlot2 = currentParts["weaponSlot2"]["name"];
        SweaponSlot3 = currentParts["weaponSlot3"]["name"];
        SweaponSlot4 = currentParts["weaponSlot4"]["name"];
        StoolSlot1 = currentParts["toolSlot1"]["name"];
        StoolSlot1 = currentParts["toolSlot2"]["name"]; 
        StoolSlot1 = currentParts["jetPack"]["name"]; 
        //SET ARM NAME
        //SarmName = currentParts["01A"]["name"];
        //SET ALL PART NAMES FROM currentParts
        //INSTANTIATE GAMEOBJECTS LATER IN PLAYERCONTROLLER(?)
    }
}
