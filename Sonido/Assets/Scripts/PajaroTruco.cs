using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PajaroTruco : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int seed = (int)Time.time;
        System.Random rd = new System.Random(seed);
        
        var x = rd.Next(-10, 10);
        var z = rd.Next(-10, 10);
        
        gameObject.transform.position = new Vector3(x, 1, z);
    }
}
