using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;

public class MyPlayerData : MonoBehaviour {

    
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


    private GameObject toolSlot1;
    private GameObject toolSlot2;

    //For future
    //private GameObject toolSlot3;
    //private GameObject toolSlot4;
    //private GameObject toolSlot5;

    [SerializeField] private string SarmL;
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


    [SerializeField] private int speed;
    [SerializeField] private int health;
    [SerializeField] private int shield;     //To be used with non regenerating shield
    [SerializeField] private int energy;     // Works as both shield and booster energy
    [SerializeField] private int acceleration;
    [SerializeField] private int energyRegeneration;
    [SerializeField] private int mass;
    
    //JSON STUFF
    static private string allPartsPath = Application.persistentDataPath + "/AllParts.json";
    private string currentPath = Application.persistentDataPath + "/CurrentParts.json";
    static string jsonString = File.ReadAllText(allPartsPath);
    JSONObject allParts = (JSONObject)JSON.Parse(jsonString);
    

    //FUNCTIONS

    void Save()
    {
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

    void UpdateParts()
    {
        JSONObject currentParts = new JSONObject();
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


     


    void Load() 
    {
        

        //SET ARM NAME
        //SarmName = allParts["01A"]["name"];
        
    }
}
