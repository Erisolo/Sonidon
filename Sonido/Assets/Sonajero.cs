using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class Sonajero : MonoBehaviour
{
    [SerializeField]
    private StudioEventEmitter _emitter;
    private bool sonajero = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E) && !_emitter.IsActive)
        {
            sonajero = true;
            _emitter.Play();
        }
        else if (!Input.GetKey(KeyCode.E))
        {
            sonajero = false;
            _emitter.Stop();
        }
    }
    
    public bool GetSonajero() { return sonajero; }
    
}
