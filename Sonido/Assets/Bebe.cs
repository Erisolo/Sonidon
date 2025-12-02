using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class Bebe : MonoBehaviour
{
    private StudioEventEmitter emitter;
    
    [SerializeField] 
    private GameObject player;
    private Transform playerTransform;
    private Sonajero sonajero;
    
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = player.transform;
        sonajero = player.GetComponent<Sonajero>();
        emitter = GetComponent<StudioEventEmitter>();
    }

    // Update is called once per frame
    void Update()
    {
        emitter.SetParameter("Distance", Vector3.Distance(player.transform.position, transform.position));

        int aux;
        if (sonajero.GetSonajero())
            aux = 1;
        else
            aux = 0;
        
        emitter.SetParameter("Sonajero", aux);
    }
}
