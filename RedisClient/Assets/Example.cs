using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example : MonoBehaviour 
{
	void Start () 
	{
		using (Redis redis = new Redis("192.168.23.127"))
		{
			Debug.Log(redis.SetKey("pomettini", "ciao"));
			Debug.Log(redis.GetKey("pomettini"));
		}
	}
}
