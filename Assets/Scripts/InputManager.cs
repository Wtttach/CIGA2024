using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public bool IsAllowRotate = true;


    private void FixedUpdate()
    {
        DetectPlatform();
    }

    public void DetectPlatform()
    {
        var ray=Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit,1000f,1<<6))
        {
            IsAllowRotate = false;
            Debug.Log(hit.transform.name);
        }
        else
        {
            if(Input.GetMouseButton(0))
                return;
            IsAllowRotate = true;
        }
    }
}
