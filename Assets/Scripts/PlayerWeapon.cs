using UnityEngine;



[System.Serializable]

public class PlayerWeapon {

	public string name = "Railgun";
	public int weaponType; //TYPES -> 0 - BULLET, 1 - ENERGY
	public int damage = 10;
	public float range = 1000f;
	public float fireRate = 0f;
	public int maxBullets = 20;

	[HideInInspector]
	public int bullets;
	public float reloadTime = 1f;
	public float heatGeneration;
	public float recoil;
	public float maxHeat;
	public float currentHeat;
	public GameObject graphics;
	public PlayerWeapon ()
	{

		bullets = maxBullets;

	}



}