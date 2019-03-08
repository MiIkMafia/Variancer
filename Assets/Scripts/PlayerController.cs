using System.Collections;

using System.Collections.Generic;

using UnityEngine;



[RequireComponent(typeof(PlayerMotor))]

public class PlayerController : MonoBehaviour {

//LIMIT TO MAX SPEED 6f -> DONE

//INTRODUCE BOOST SPEED WITH SHIFT

//MAKE ENERGY METER

//WEAPONS AND SHOOTING

//DRONES AND TOOLS

//FIX OPPOSITE ADAD MOVEMENT (INVERT DEACCELERATION)



	public MyPlayerData data;
//STATS FROM MyPlayerData
	




//PLAYER STATS

	[SerializeField]

	private float currentSpeed = 0f;

	[SerializeField]

	private float maxSpeed = 20f; 

	[SerializeField]

	public float _speed = 0f;

	public float thrusterForce = 1000f;

	private float dashForce = 9000f; //SET FROM MASS

	private int dashEnergyDrain = 20; //SET FROM MASS LATER

	private float speedMassMultiplier = 0f;// CALCULATE FROM MASS

	[SerializeField]

	private float acceleration = 0f; //CALCULATE FROM MASS

	public bool energyCharge = true;

	private Vector2 lateralAccelerationMultiplier =Vector2.zero; //CALCULATE FROM MASS, FOR AD MOVEMENT,

	// DEPENDS ON DIFFERENCE OF MASS B/W LEFT AND RIGHT SIDE, ->(A/LEFT,D/RIGHT)

	[SerializeField]

	private float reflex = 3f;  //MINIMUM SPEED FOR WASD SWITCHINGS

	private float reflexCopy = 0f;

	public bool isDisabled = false;

	public float speedMaxLateralModifier = 0f; 

	[SerializeField]

	private float energyMax = 100f; //Calculate from player parts

	[SerializeField]

	private float energyCurrent = 0f; //Constantly increasing

	private float energyRefill = 5f; //Refill rate

	private float energyConsumptionRate = 3f; //SET FROM PLAYER PARTS 

	private float jetpackEnergyConsumption = 5f; //SET FROM PLAYER PARTS 



	[SerializeField]

	private float deaccelerationMultiplier = 5f;

	private float accelerationCopy = 0f;

	// private float acclerationMassMultiplier = 0f; {ACCELERATION MODIFIER CALCULATED FROM MASS -Current status unknown}

	private float sideAccelerationMultiplier = 0f; // LESS THAN ZERO

	[SerializeField]

	private Vector2 lookSensitivity = Vector2.zero; //TAKE FROM MASS DIFFERENCE BETWEEN LEFT AND RIGHT ARM

	//private float look = 3f; REDUNDANT

	//private float lookSensitivityRight = 0f; //DIVIDE BETWEEN LEFT AND RIGHT   -> REDUNDANT

	public bool canMove = true; // CanMove

	public Camera cam;

	private Vector3 velocity = Vector3.zero; 

	private Vector3 rotation = Vector3.zero;

	[SerializeField]

	private Vector3 velocityCopy = Vector3.zero;



	//MOVEMENT VARIABLES

	[SerializeField]

	private float xMov = 0;

	[SerializeField]

	private float zMov = 0;

	private float _xMov = 0; 

	private float _zMov = 0; 

	//CAMERA ROTATION

	private float cameraRotationX = 0f;

	private float currentCameraRotationX = 0f;

	

	public GameObject spawnPoint;

	[SerializeField]

	private float cameraRotationLimit = 85f;

	public PlayerMotor xv;

	

	void Start ()

	{

		xv = GetComponent<PlayerMotor>();	

	    _speed = maxSpeed;
		data.SetPath();
		//data.SetDefaults();
		//data.SetPartValues();
		//data.Load();

	}

	public void Boost()

	{

		if (energyCurrent > 0 ) //&& SOME KEY TO BOOST WITH IS PRESSED (SHIFT)

 		{

		maxSpeed = 16f;

		accelerationCopy = 3* acceleration;

		deaccelerationMultiplier = 1f;

		}

	}

	private void CalcTotalMass()
	{
		
	}


	public void Final()

	{

		if (currentSpeed > maxSpeed)

		{currentSpeed = Mathf.Min(currentSpeed, maxSpeed);}

		else if (currentSpeed < 0)

		{currentSpeed = Mathf.Max(currentSpeed, 0);}
		

	}





	public void AccelerationControl()

	{

		

		float _percentage = (currentSpeed/maxSpeed) * 100f;

		if (_percentage > 0f && _percentage <= 20f ) { accelerationCopy = acceleration * 0.6f; deaccelerationMultiplier = 9.5f;}

		else if (_percentage > 20f && _percentage <= 30f ) {accelerationCopy =  acceleration * 0.6f; deaccelerationMultiplier = 8f;}

		else if (_percentage > 30f && _percentage <= 50f ) {accelerationCopy =  acceleration * 0.6f; deaccelerationMultiplier = 7f; }

		else if (_percentage > 50f && _percentage <= 100f ) {accelerationCopy = acceleration * 0.6f; deaccelerationMultiplier = 5f;}

		

		

	}

	

	

	 public void CanMove()

	{	

		//OPPOSITE

		if (((_xMov == -(xMov) && zMov == 0) || (_zMov == -(zMov) && xMov == 0)) && currentSpeed > reflex)

		{

			canMove = false;

		}

		else if (currentSpeed == 0) {canMove = true;}

		else if (currentSpeed == reflex) {canMove = true;}

		if (xMov != 0) {_xMov = xMov;}

		if (zMov != 0) {_zMov = zMov;}

		

	}

	public void Accelerate()

	{

		

		if ((xMov != 0 || zMov != 0) && canMove == true)

		{

		currentSpeed += accelerationCopy * Time.deltaTime;

		currentSpeed = Mathf.Min(currentSpeed, maxSpeed);

		}

		else if ((xMov == 0 || zMov == 0) || (canMove == false && currentSpeed >= reflex))

		{

			currentSpeed -= deaccelerationMultiplier*accelerationCopy * Time.deltaTime;

			if ((xMov != 0 || zMov != 0) && canMove == false && currentSpeed < reflex)

			{currentSpeed = Mathf.Max(currentSpeed, reflex);

			}

			else {currentSpeed = Mathf.Max(currentSpeed, 0); }

		}

		

	}



	

//TAKES ENERGY VALUE FROM EXPENING PART AND DEDUCT FROM CURRENT ENERGY RESERVES



//ENERGY FUNCTIONS
	public void Load()
	{data.Load();}

	public void Save()
	{
		data.Save();
	}

	public void SetPath()
	{
		data.SetPath();
	}

	public void UpdateParts()
	{
		data.UpdateParts();
	}

	public void UnlockParts()
	{
		
		data.UnlockParts();
	}
	public void ExpendEnergy(float energyToExpend, int mode)

	{

		//SMOOTH DECREASE

		if (mode == 0)

		{

		if (energyCurrent >0)

		{

			energyCurrent -= energyToExpend * Time.fixedDeltaTime;

			if (energyCurrent < 0) { energyCurrent = 0;}

		}

		StartCoroutine(PauseEnergy(1f));

		}

		if (mode == 1)

		{

			if (energyCurrent >0)

			{

				energyCurrent -= energyToExpend;

				if (energyCurrent < 0) { energyCurrent = 0;}

			}

		}

	}

//RECHARGES ENERGY

	public void EnergyRegen()

	{

		if (energyCurrent < energyMax && energyCharge == true )

		{

			energyCurrent += energyRefill * Time.fixedDeltaTime;

		}

	}

	

	public void Dash(float _energyUse)

	{

		if (Input.GetButtonDown("Fire2") && isDisabled == false) //CHANGE LATER

		{ 

			if ((energyCurrent - _energyUse) >= 0)

			{

				Vector3 dashVector = Vector3.zero;

				if (xMov != 0){dashVector = this.transform.right * xMov;}

				else if (zMov != 0){dashVector = this.transform.forward * zMov;}



				xv.Dash(dashVector*dashForce);

				ExpendEnergy(_energyUse, 1); //1 IS MODE

				StartCoroutine(Disable(0.5f));

			}

		}

	}

	

	//LOCK ON CODE

	public void LookAt()

	{

		if (Input.GetButton("Fire3"))

		{

			Vector3 _pos = (new Vector3 (-1.9f, 0.762f, 5.6f)) - transform.position; 

			Quaternion _newRot = Quaternion.LookRotation(_pos);

			transform.rotation = Quaternion.Lerp(transform.rotation, _newRot, Time.fixedDeltaTime * 20f);

			xv.CannotLookAt(false);

		}

		else if (xv.rotationEnabled == false)

		{

			xv.CannotLookAt(true);
			Vector3 _tempRotation = transform.eulerAngles;
			_tempRotation.x = 0f;	
			transform.localEulerAngles = _tempRotation;

		}



	}

	//SLOW MAX SPEED DECREASE

	public void LateralAcceleration()

	{

		if (xMov != 0 && zMov == 0) 

		{

			maxSpeed = _speed * speedMaxLateralModifier;

			/* while (maxSpeed > finalSpeed)

			{

				maxSpeed -= 5f* deaccelerationMultiplier * accelerationCopy * Time.fixedDeltaTime;

				print(maxSpeed);

			}*/

		}

		 else if (xMov != 0  && zMov != 0)

		{

			maxSpeed = _speed * speedMaxLateralModifier;



		} 

		

		else if (xMov == 0)

		{

			maxSpeed = _speed;

		}

	}

	/* IEnumerator X()

	{	

		float finalSpeed = speedMaxLateralModifier * _speed;

		while (maxSpeed >= finalSpeed)

		{

			maxSpeed -= deaccelerationMultiplier*accelerationCopy * Time.deltaTime;

		}

	}*/









//JETPACK

	public void Jetpack()

	{

		Vector3 boostVector = Vector3.zero;

		float _thrusterForce;

		if(Input.GetButton("Jump") && isDisabled == false)

		{

			_thrusterForce = thrusterForce;

			boostVector = _thrusterForce * Vector3.up;

			ExpendEnergy(jetpackEnergyConsumption, 0);

			xv.ApplyThruster(boostVector);

		}

		else if (zMov == 0) {boostVector = Vector3.zero;  xv.ApplyThruster(boostVector); }

		else if (Input.GetButton("Fire1") != true) {_thrusterForce = 0f; xv.ApplyThruster(Vector3.zero);}

		//else if (energyCurrent <= 0){print("works");}
		

	}



	//TIME LAG

	IEnumerator PauseEnergy(float time)

	{

		if (energyCharge == true)

		energyCharge = false;

		yield return new WaitForSeconds(time);

		energyCharge = true;

	}



	IEnumerator Disable(float time)

	{

		if (isDisabled == false)

		{isDisabled = true;}

		yield return new WaitForSeconds(time);

		{isDisabled  = false;}

		

	}

	private void DirectionVariables()

	{

		xMov = Input.GetAxisRaw("Horizontal");

		zMov = Input.GetAxisRaw("Vertical");

		EnergyRegen();

	}

	void FixedUpdate ()



	{

		DirectionVariables();

		//Calculate movement velocity as a 3D vector

		accelerationCopy = acceleration;

		

		xv.CheckMovement(xMov, zMov);

	

		Jetpack();

		Vector3 _movHorizontal = transform.right * xMov;

		Vector3 _movVertical = transform.forward * zMov;



		AccelerationControl();

		CanMove();

		Accelerate();

		LateralAcceleration();

		// FINAL MOVEMENT VECTOR



		Vector3 _velocity = (_movHorizontal + _movVertical).normalized * currentSpeed;

		

		if (_velocity == Vector3.zero && currentSpeed != 0 && velocityCopy.z == 0)

		{

			_velocity = velocityCopy;

			//print(velocityCopy);

		}

		else if (currentSpeed == 0)

		{

			_velocity = Vector3.zero;

		}

		else 

		{velocityCopy = _velocity;}

		print (data.currentParts["head"]["name"]);
		print(data.allParts[0][0]);
		  





		//Apply movement

		if (isDisabled == false)

		{xv.Move(_velocity);}



		

		

		//Calculate rotation as a 3D vector (turning around)



		//ROTATE ACCORDING TO MASS

		float _yRot = Input.GetAxisRaw("Mouse X");

		Vector3 _rotation = new Vector3(0f, _yRot, 0f);

		if (_yRot > 0)

			{_rotation = _rotation * lookSensitivity.x;}



		else if (_yRot < 0)

			{_rotation = _rotation * lookSensitivity.y; }

		



		LookAt();



		//Apply rotation



		xv.Rotate(_rotation);



		//Calculate camera rotation as a 3D vector (turning around)



		float _xRot = Input.GetAxisRaw("Mouse Y");

		

		float _cameraRotationX = _xRot * lookSensitivity.x;

		

		//Apply camera rotation

		xv.RotateCamera(_cameraRotationX);

		Dash(dashEnergyDrain);

	}



	void spawnPart()

	{

		Transform x = spawnPoint.transform;

		Debug.Log(x.position.ToString());



		

		Debug.Log(x.name);	

		int count = this.transform.childCount;

		for (int y = 0; y < count; y++)

		{

			string i = this.transform.GetChild(y).name;

							

			if (this.transform.GetChild(y).name == "PartLocation")

			{

				for (int p = 0; p < this.transform.GetChild(y).transform.childCount; p++)

				{

					Transform subChild = this.transform.GetChild(y).transform.GetChild(p);

					if (subChild.name == "ArmL")

					{

						Debug.Log("done");

						GameObject instance = Resources.Load<GameObject>("Gun");

						GameObject myInstance = Instantiate(instance, subChild.position, Quaternion.identity);

						myInstance.transform.parent = subChild.transform;

					}

				}

				

				

			}

			

		}

	}

		

	

	

}