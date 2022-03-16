using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    [SerializeField] private bool _rotateCanvas = true;
    [SerializeField] private bool _rotateXAxis = false;
    private GameObject _worldCanvas;

    private void Start()
    {
        _worldCanvas = this.gameObject;
    }
    private void Update()
    {
        Vector3 position = Camera.main.transform.position - transform.position;
        switch (_rotateCanvas)
        {
            case true:
                position.x = 0;
                position.z = 0;

                _worldCanvas.transform.LookAt(Camera.main.transform.position - position);
                Quaternion newRot;
                if (_rotateXAxis)
                {
                    newRot = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, Camera.main.transform.rotation.eulerAngles.z);
                }
                else
                {
                    newRot = Quaternion.Euler(Camera.main.transform.rotation.eulerAngles.x, Camera.main.transform.rotation.eulerAngles.y, Camera.main.transform.rotation.eulerAngles.z);
                }
                //_worldCanvas.transform.rotation = (Camera.main.transform.rotation);
                _worldCanvas.transform.rotation = (newRot);
                break;


            case false:
                position.x = 0;
                position.z = 0;

                _worldCanvas.transform.LookAt(Camera.main.transform.position - position);
                _worldCanvas.transform.rotation = (Camera.main.transform.rotation);
                _worldCanvas.transform.Rotate(0, 180, 0);
                break;
        }

    }
}