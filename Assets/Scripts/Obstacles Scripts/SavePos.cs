using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePos : MonoBehaviour
{
	public Transform checkPoint;

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Player")
		{
			Debug.Log("Save checkpoint"+checkPoint.position);
			col.gameObject.GetComponent<PlayerControls>().checkPoint = checkPoint.position;
		}
	}
}
