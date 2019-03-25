using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class subsection : MonoBehaviour {

	[SerializeField] Material fast;
	[SerializeField]float speedModifier = 0f;

	public void setVoid(){
		GetComponent<MeshRenderer>().enabled = false;
		if(transform.childCount > 0)
			transform.GetChild(0).gameObject.SetActive(false);
		transform.tag = "void";
	}

	public void setFast(){
		GetComponent<MeshRenderer>().material = fast;
		transform.tag = "fast";
	}

	public float getSpeedMod(){
		return speedModifier;
	}


}
