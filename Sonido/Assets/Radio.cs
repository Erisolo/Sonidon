using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class Radio : MonoBehaviour
{

    private StudioEventEmitter emitter;
    [SerializeField][Range(0.0f, 1.0f)]
    private float emisora;
    [SerializeField][Range(0.0f, 1.0f)]
    private float volumen = 0.8f;
    
    void Start()
    {
        emitter = GetComponent<StudioEventEmitter>();
        
    }
    
    // Update is called once per frame
    void Update()
    {
        emitter.SetParameter("Emisora", emisora);
        emitter.SetParameter("Volumen Radio", volumen);
    }
}
