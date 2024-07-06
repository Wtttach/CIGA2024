using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeRotate : MonoBehaviour
{
    public bool IsFreeRotate = false;
    public float RotateSpeed = 1;
    public InputManager InputManag;
    private Vector3 _lastPosition;
    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0)&&InputManag.IsAllowRotate)
        {
            var currentPosition = Input.mousePosition;
            var moveDir = (currentPosition - _lastPosition);
            transform.Rotate(new Vector3(0, 1, 0), -moveDir.x*RotateSpeed, Space.World);
            if(IsFreeRotate)
                transform.Rotate(new Vector3(1, 0, 0), moveDir.y*RotateSpeed,Space.World);
            _lastPosition = currentPosition;
        }
        else
        {
            _lastPosition = Input.mousePosition;
        }
    }
}
