using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class section : MonoBehaviour {

	[SerializeField]float speed = 20f;
	[SerializeField]List<GameObject> subsections;
	[SerializeField]List<GameObject> obstacle;
	[SerializeField]bool startSection = false;

	manager mg;

	[SerializeField]AnimationCurve DifficultieCurvePillar;
	[SerializeField]AnimationCurve DifficultieCurveHole;
	[SerializeField]AnimationCurve DifficultieCurveHyperdrive;

	[SerializeField]bool levelMode;
	private void Awake()
	{
		if (levelMode)
			return ;
		mg = GameObject.Find("Manager").GetComponent<manager>();
		if (startSection)
			return;
		for (int i = 0; i <  Mathf.FloorToInt(Random.Range(0, DifficultieCurveHyperdrive.Evaluate(mg.getTimeInt()))); i++) //random for accelerator
		{
			int r = Random.Range(0, subsections.Count);
			if (subsections[r].tag != "void")
			subsections[r].GetComponent<subsection>().setFast();
		}
		for (int i = 0; i < Mathf.FloorToInt(Random.Range(0, DifficultieCurveHole.Evaluate(mg.getTimeInt()))); i++) //random for holes
		{
			int r = Random.Range(0, subsections.Count);
			subsections[r].GetComponent<subsection>().setVoid();
		}
		for (int i = 0; i < Mathf.FloorToInt(Random.Range(0, DifficultieCurvePillar.Evaluate(mg.getTimeInt()))); i++) //random for obstacles
		{
				int r = Random.Range(0, obstacle.Count);
				obstacle[r].SetActive(true);
		}
	}
	
	void FixedUpdate(){
		if (!mg)
			mg = GameObject.Find("Manager").GetComponent<manager>();
		speed = mg.getSpeed();
		transform.Translate(0,0, Time.deltaTime * -speed);
		if (transform.position.z <= -30)
			Destroy(this.gameObject);
	}
}
