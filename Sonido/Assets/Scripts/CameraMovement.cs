using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private float _cameraSens = 15.0F;
    
    private float _verticalRotationClamp = 90.0f;
    private float _rotationY = 0.0F;
    private float _rotationX = 0;
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
        //float rotationX = _transform.localEulerAngles.y + Input.GetAxis("Mouse X") * _cameraSens;

        //_rotationY += Input.GetAxis("Mouse Y") * _cameraSens;
        //_rotationY = Mathf.Clamp (_rotationY, -_verticalRotationClamp, _verticalRotationClamp);

        _rotationX += (Input.GetAxis("Mouse X") * _cameraSens);
        _rotationY += Input.GetAxis("Mouse Y") * _cameraSens;
        _rotationY = Mathf.Clamp(_rotationY, -_verticalRotationClamp, _verticalRotationClamp);

        _transform.localEulerAngles = new Vector3(-_rotationY, 0, 0);
        //rotamos a nuestro padre tmbn
        _transform.parent.transform.localEulerAngles = new Vector3(0, _rotationX, 0);
    }
}