using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class PlayerShoot : NetworkBehaviour {

		public PlayerWeapon weapon;
		[SerializeField]
		public Camera cam;
		[SerializeField]
		public LayerMask mask;
		Vector3 _distance;
		


		void Start()
		{
			if (cam == null)
			{
				Debug.LogError("PlayerShoot: No camera referenced!");
				this.enabled = false;
			}
		}

		void Update()
		{
			if (Input.GetButtonDown("Fire3"))
			{
				Shoot();
			}

		}


		

		void Shoot()
		{
			RaycastHit _hit;
			if (Physics.Raycast(cam.transform.position, cam.transform.forward, out _hit, weapon.range, mask))
			{
				_distance = _hit.point;
				Debug.DrawRay(cam.transform.position, _distance, Color.red, 200f, true);
				_distance.y = _distance.y - 0.5f;
				Debug.DrawRay(cam.transform.position, _distance, Color.white, 200f, true);
				_distance.y = _distance.y - 1f;
				Debug.DrawRay(cam.transform.position, _distance, Color.blue, 200f, true);
				//Vector3 _actualLocation = 
				
			}
			
		}
}

