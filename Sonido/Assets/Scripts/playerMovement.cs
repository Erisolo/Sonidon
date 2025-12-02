using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;
using UnityEngine.UIElements;

public class playerMovement : MonoBehaviour
{
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
        
        if(_movementSpeed < 0.1f)
            walkSpeed = 2;
        else if(_movementSpeed < 0.2f)
            walkSpeed = 1;
        else
            walkSpeed = 0;
            
        _emitter.SetParameter("Velocidad Andar", walkSpeed);
    }

    // Update is called once per frame
    void Update()
    {

        //movimiento super basico en todos los ejes
        if (Input.GetKey(KeyCode.W))
            _rb.velocity = (Vector3.forward * _movementSpeed);
        else if (Input.GetKey(KeyCode.S))
            _rb.velocity = (Vector3.back * _movementSpeed);

        if (Input.GetKey(KeyCode.A))
            _rb.velocity = (Vector3.left * _movementSpeed);
        else if (Input.GetKey(KeyCode.D))
            _rb.velocity = (Vector3.right * _movementSpeed);
        else
        {
            _rb.velocity = Vector3.zero;
            _emitter.Stop();
        }
        
        if(_rb.velocity.magnitude != 0 && !_emitter.IsActive)
            _emitter.Play();
    }
}
