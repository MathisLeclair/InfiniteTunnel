using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

	
	[Range(1, 100)] public int dist = 10;
	[Range(1, 20)] public int space = 1;
	[SerializeField] Color startColor;
	[SerializeField] Color endColor;
	[SerializeField] Material mat;
	[SerializeField] bool update;
	void Start () {
		if(!update) { drawGrid(0); }
	}

	private void Update()
	{
		if (update)
			drawGrid(0.2f);
	}
	
	void drawGrid(float duration){
		int i = Mathf.RoundToInt(transform.position.x);
		while (i < dist)
		{
			DrawLine(new Vector3(i, transform.position.y, transform.position.z), new Vector3(i, transform.position.y, dist), startColor, endColor, duration);
			DrawLine(new Vector3(transform.position.x, transform.position.y, i), new Vector3(dist, transform.position.y, i), startColor, endColor, duration);
			i += space;
		}
	}

 	void DrawLine(Vector3 start, Vector3 end, Color colorS, Color colorE, float duration = 0.2f)
	{
		GameObject myLine = new GameObject();
		myLine.transform.parent = this.gameObject.transform;
		myLine.transform.position = start;
		myLine.AddComponent<LineRenderer>();
		LineRenderer lr = myLine.GetComponent<LineRenderer>();
		lr.material = mat;
		lr.startColor = colorS; lr.endColor = colorE;
		lr.startWidth = 0.1f; lr.endWidth = 0.1f;
		lr.SetPosition(0, start);
		lr.SetPosition(1, end);
		if (duration > 0)
			GameObject.Destroy(myLine, duration);
	}

}
