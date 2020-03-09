using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example : MonoBehaviour
{
    void Start()
    {
        Uniredis.Set(this, "Hello", "World", (error, result) =>
        {
            if (error == null)
            {
                Debug.Log(result);
            }
        });

        Uniredis.Get(this, "Hello", (error, result) =>
        {
            if (error == null)
            {
                Debug.Log(result);
            }
        });
    }
}
