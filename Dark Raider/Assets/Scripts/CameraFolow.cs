﻿using UnityEngine;
using System.Collections;

public class CameraFolow : MonoBehaviour {

	public GameObject follow;
	public Vector2 minCamPos, maxCamPos;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float posX = follow.transform.position.x;
		float posY = follow.transform.position.y;

			 transform.position= new Vector3(
			Mathf.Clamp(posX, minCamPos.x, maxCamPos.x), 
			Mathf.Clamp(posY, minCamPos.y, maxCamPos.y),
			transform.position.z);
	}
}
