using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example : MonoBehaviour 
{
	void Start () 
	{
		Uniredis.Connect();
		Debug.Log(Uniredis.SetKey("pomettini", "ciao"));
		Debug.Log(Uniredis.GetKey("pomettini"));
		Uniredis.Disconnect();
	}
}
