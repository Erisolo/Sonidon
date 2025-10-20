using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private float _movementSpeed = 0.1f;
    [SerializeField]
    private float _cameraSens = 15.0F;
    
    private float _verticalRotationClamp = 90.0f;
    private float _rotationY = 0.0F;
    private Transform _transform;
    
    // Start is called before the first frame update
    void Start()
    {
        _transform = GetComponent<Transform>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //magia de rotacion de la camara
        float rotationX = _transform.localEulerAngles.y + Input.GetAxis("Mouse X") * _cameraSens;

        _rotationY += Input.GetAxis("Mouse Y") * _cameraSens;
        _rotationY = Mathf.Clamp (_rotationY, -_verticalRotationClamp, _verticalRotationClamp);

        _transform.localEulerAngles = new Vector3(-_rotationY, rotationX, 0);
        
        //movimiento super basico en todos los ejes
        if (Input.GetKey(KeyCode.W))
            _transform.Translate(Vector3.forward * _movementSpeed);
        else if(Input.GetKey(KeyCode.S))
            _transform.Translate(Vector3.back * _movementSpeed);
        
        if(Input.GetKey(KeyCode.A))
            _transform.Translate(Vector3.left * _movementSpeed);
        else if(Input.GetKey(KeyCode.D))
            _transform.Translate(Vector3.right * _movementSpeed);

        if(Input.GetKey(KeyCode.E))
            _transform.Translate(Vector3.up * _movementSpeed);
        else if(Input.GetKey(KeyCode.Q))
            _transform.Translate(Vector3.down * _movementSpeed);
    }
}