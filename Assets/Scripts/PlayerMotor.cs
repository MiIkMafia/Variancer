﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {

	[SerializeField]
	private Camera cam;

	private Vector3 velocity = Vector3.zero;
	private Vector3 rotation = Vector3.zero;
	private float cameraRotationX = 0f;
	private float currentCameraRotationX = 0f;
	private Vector3 thrusterForce = Vector3.zero;
	private float xMov = 0;
	private float zMov = 0;
	private bool rotationEnabled = true;

	[SerializeField]
	private float cameraRotationLimit = 85f;

	private Rigidbody rb;

	void Start ()
	{
		rb = GetComponent<Rigidbody>();
	}

	
	public void CheckMovement(float _xMov, float _zMov)
	{
		_xMov = xMov;
		_zMov = zMov;
	
		
	}

	public void CannotLookAt(bool state)
	{
		rotationEnabled = state;
	}

	// Gets a movement vector
	public void Move (Vector3 _velocity)
	{
		velocity = _velocity;
	}

	// Gets a rotational vector
	public void Rotate(Vector3 _rotation)
	{
		rotation = _rotation;
	}

	// Gets a rotational vector for the camera
	public void RotateCamera(float _cameraRotationX)
	{
		cameraRotationX = _cameraRotationX;
	}
	
	// Get a force vector for our thrusters
	public void ApplyThruster (Vector3 _thrusterForce)
	{
		thrusterForce = _thrusterForce;
		
	}

	// Run every physics iteration
	void FixedUpdate ()
	{
		PerformMovement();
		if (rotationEnabled)
		{PerformRotation();}
	}

	public void Dash(Vector3 _dashForce)
	{
		rb.AddForce(_dashForce * Time.fixedDeltaTime, ForceMode.Acceleration);
	}

	//Perform movement based on velocity variable
	void PerformMovement ()
	{
		Vector3 _velocity = velocity;
		

		if (velocity != Vector3.zero)
		{
			rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
			
		}
		//ELSE

		if (thrusterForce != Vector3.zero)
		{
			print(thrusterForce);
			rb.AddForce(thrusterForce * Time.fixedDeltaTime, ForceMode.Acceleration);
		}

	}

	//Perform rotation
	void PerformRotation ()
	{
		rb.MoveRotation(rb.rotation * Quaternion.Euler (rotation));
		if (cam != null)
		{
			// Set our rotation and clamp it
			currentCameraRotationX -= cameraRotationX;
			currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

			//Apply our rotation to the transform of our camera
			cam.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
			
		}
	}
}
