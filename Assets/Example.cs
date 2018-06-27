using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example : MonoBehaviour 
{
	void Start () 
	{
		Uniredis.Set(this, "ciao", "bello", (error, result) =>
		{
			if (error == null)
			{
				Debug.Log(result);
			}
		});

		Uniredis.Get(this, "ciao", (error, result) =>
		{
			if (error == null)
			{
				Debug.Log(result);
			}
		});
	}
}
