using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;
using UnityEngine.UIElements;

public class playerMovement : MonoBehaviour
{
    bool alfombra;
    [SerializeField]
    private float _movementSpeed = 0.1f;
    //private Transform _transform;
    private Rigidbody _rb;
    
    [SerializeField]
    private StudioEventEmitter _emitter;
    
    // Start is called before the first frame update
    void Start()
    {
        //_transform = GetComponent<Transform>();
        _rb = GetComponent<Rigidbody>();

        int walkSpeed;
        
        if(_movementSpeed < 5f)
            walkSpeed = 2;
        else if(_movementSpeed < 11f)
            walkSpeed = 1;
        else
            walkSpeed = 0;
            
        _emitter.SetParameter("Velocidad Andar", walkSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //_emitter.SetParameters("Superficie", (int) alfombra)
    }
    private void OnCollisionExit(Collision collision)
    {
        //_emitter.SetParameters("Superficie", (int) alfombra)
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vx = Vector3.zero, vy = Vector3.zero;
        //movimiento super basico en todos los ejes
        if (Input.GetKey(KeyCode.W))
            vy = (transform.forward);
        else if (Input.GetKey(KeyCode.S))
            vy = (-transform.forward);

        if (Input.GetKey(KeyCode.A))
            vx = (-transform.right);
        else if (Input.GetKey(KeyCode.D))
            vx= (transform.right);

        _rb.velocity = (vx + vy).normalized * _movementSpeed;
        
        
        if(_rb.velocity.magnitude == 0 && (_emitter.IsActive))
            _emitter.Stop();
        else if (!(_emitter.IsActive) && _rb.velocity.magnitude != 0)
            _emitter.Play();
            
    }
}
