using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour {

	[SerializeField]GameObject world;
	[SerializeField]float rotateSpeed = 10f;
	[SerializeField]bool debug = false;
	[SerializeField]manager manager;

	void Start(){
		Input.gyro.enabled = true;
		if (!manager)
			manager = GameObject.Find("Manager").GetComponent<manager>();
		//Invoke("calibrate", 0.5f);
	}

	[SerializeField]float jumpSpeed = 2f;
	bool canJump = true;

	void jump(){
		transform.Translate(Vector3.up * jumpSpeed * 1.2f, Space.World);
		if (transform.position.y >= 4f)
			canJump = false;
	}

	float barrelTime = 0f;
	bool inBarrel = false;
	[SerializeField] AnimationCurve barrelCurve;
	void barrel(){
		barrelTime += 0.1f;
		inBarrel = true;
		transform.eulerAngles = new Vector3(barrelCurve.Evaluate(barrelTime) * 360, 90, 90);
		if (barrelTime > 1){
			CancelInvoke("barrel");
			barrelTime = 0;
		}
	}

	void FixedUpdate () {

		if (transform.position.y <= 0)
		{
			canJump = true;
			inBarrel = false;
		}
		if (transform.position.y > 0 && !canJump)
			transform.Translate(Vector3.down * jumpSpeed * 0.7f, Space.World);
		if (transform.position.y > 2f && !inBarrel)
			InvokeRepeating("barrel", 0, 0.02f);


		if (!debug && !manager.end){

			world.transform.Rotate(Vector3.forward * (Input.acceleration.x - gyroCalib) * -rotateSpeed);
			if (canJump && Input.touchCount > 0)
				jump();
			else
				canJump = false;

		} else { // code for control on pc
			if (Input.GetKey(KeyCode.LeftArrow))
				world.transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime * 25);
			else if (Input.GetKey(KeyCode.RightArrow))
				world.transform.Rotate(Vector3.back * rotateSpeed * Time.deltaTime * 25);
			if (canJump && Input.GetKey(KeyCode.Space))
				jump();
			else
				canJump = false;
		}
	}


	float gyroCalib = 0f;
	void calibrate(){
		gyroCalib = Input.acceleration.y;
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.tag == "void" && transform.position.y <= 0.1f || other.tag == "wall")
			manager.loose();
		else if (other.tag == "fast")
			manager.hyperdrive(other.GetComponent<subsection>().getSpeedMod());
		else if (other.tag == "end")
			manager.endLevel();
	}

	private void OnCollisionEnter(Collision other)
	{	
		manager.loose();
	}
}
