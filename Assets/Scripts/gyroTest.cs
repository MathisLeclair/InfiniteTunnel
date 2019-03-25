using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gyroTest : MonoBehaviour {

	[SerializeField] Text xTxt;
	[SerializeField] Text yTxt;
	[SerializeField] Text zTxt;

	private void Start()
	{
		Input.gyro.enabled = true;				
	}

	void Update () {
		//xTxt.text = Input.gyro.attitude.x.ToString();
		xTxt.text = Input.acceleration.x.ToString();
		yTxt.text = Input.gyro.attitude.y.ToString();
		zTxt.text = Input.gyro.attitude.z.ToString();
	}
}
