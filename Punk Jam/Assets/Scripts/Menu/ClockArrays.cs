using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockArrays : MonoBehaviour
{
	public float speed = 5f;

	[SerializeField] private Transform array;
    [SerializeField] private Vector3[] eulerAngles;

	private Vector3 rotation;
	private Vector3 nextAngle;

	private bool isSetted = true;



	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			Button1();
		}
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			Button2();
		}
		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			Button3();
		}
		if (Input.GetKeyDown(KeyCode.Alpha4))
		{
			Button4();
		}

		if (!isSetted)
		{
			rotation = Vector3.Lerp(rotation, nextAngle, speed * Time.deltaTime);
			array.localEulerAngles = rotation;
			
			if ((rotation - nextAngle).magnitude <= 0.01f)
			{
				rotation = nextAngle;
				array.localEulerAngles = rotation;
				isSetted = true;
			}
		}
;
	}

	public void Button0(int value)
	{
		nextAngle = eulerAngles[value];
		isSetted = false;
	}

	public void Button1()
	{
		nextAngle = eulerAngles[0];
		isSetted = false;
	}
	public void Button2()
	{
		nextAngle = eulerAngles[1];
		isSetted = false;
	}
	public void Button3()
	{
		nextAngle = eulerAngles[2];
		isSetted = false;
	}
	public void Button4()
	{
		nextAngle = eulerAngles[3];
		isSetted = false;
	}
}
