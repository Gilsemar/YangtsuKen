﻿using UnityEngine;
using System.Collections;

public class TituloScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.KeypadEnter)||Input.GetKey(KeyCode.Return))
		{
			Application.LoadLevel("Fase1");
		}
	}
}