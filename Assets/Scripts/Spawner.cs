using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	[SerializeField]GameObject world;
	[SerializeField]GameObject section;

	private void FixedUpdate()
	{
		if (world.transform.childCount < 6)
			spawn();
	}

	void spawn(){
		GameObject t = Instantiate(section);
		t.transform.parent = world.transform;
		t.transform.rotation = new Quaternion(0,0,0,0);
		t.transform.localPosition = new Vector3(-2.83f, 8.26f, (world.transform.GetChild(world.transform.childCount - 2).transform.localPosition.z + 26f));
	}

}
