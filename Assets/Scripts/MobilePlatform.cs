using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MobilePlatform : MonoBehaviour
{
    public InputManager InputManag;
    public MoveWard Ward=MoveWard.X;
    [Range(-10,0)]
    public int StartOffset=-1;
    [Range(0,10)]
    public int EndOffset=1;
    [Range(0,0.1f)]
    public float MoveSpeed=0.01f;
    
    private Vector3 _lastPosition;
    private GameObject _startObj;
    private GameObject _endObj;

    private void Start()
    {
        var ward=GetWard();
        var origin = transform.position;
        _startObj=new GameObject("StartPosition");
        _startObj.transform.parent = transform.parent;
        _startObj.transform.position = origin+ward * StartOffset;
        _endObj=new GameObject("EndPosition");
        _endObj.transform.parent = transform.parent;
        _endObj.transform.position = origin+ward * EndOffset;
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0) && !InputManag.IsAllowRotate)
        {
            var mainCam = Camera.main;
            var transPos = transform.position;
            var screenOrigin=mainCam.WorldToScreenPoint(transPos);
            
            var ward = GetWard();
            var screenWard = mainCam.WorldToScreenPoint(transPos + ward);
            var screenDir = (screenWard - screenOrigin).normalized;
            
            var currentPosition = Input.mousePosition;
            var moveDir = (currentPosition - _lastPosition);
            transPos += Vector3.Dot(screenDir, moveDir)*MoveSpeed*ward;
            transform.position=LimitePlatform(transPos, ward);
            _lastPosition = currentPosition;
        }
        else
        {
            _lastPosition = Input.mousePosition;
        }
    }

    public enum MoveWard
    {
        X,
        Y,
        Z
    }

    private Vector3 GetWard()
    {
        var ward=Vector3.zero;
        switch (Ward)
        {
            case MoveWard.X:
                ward = transform.right;
                break;
            case MoveWard.Y:
                ward = transform.up;
                break;
            case MoveWard.Z:
                ward = transform.forward;
                break;
        }

        return ward;
    }

    private Vector3 LimitePlatform(Vector3 targetPos,Vector3 ward)
    {
        var startPos=_startObj.transform.position;
        var endPos=_endObj.transform.position;
        var startDir=targetPos - startPos;
        var endDir=targetPos - endPos;
        var startDot=Vector3.Dot(startDir,ward);
        var endDot=Vector3.Dot(endDir,ward);
        if (endDot > 0)
            return endPos;
        if (startDot < 0)
            return startPos;
        return targetPos;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color=Color.cyan;
        var pos=transform.position;
        switch (Ward)
        {
            case MoveWard.X:
                Gizmos.DrawLine(pos+StartOffset*transform.right,pos+EndOffset*transform.right);
                break;
            case MoveWard.Y:
                Gizmos.DrawLine(pos+StartOffset*transform.up,pos+EndOffset*transform.up);
                break;
            case MoveWard.Z:
                Gizmos.DrawLine(pos+StartOffset*transform.forward,pos+EndOffset*transform.forward);
                break;
            default:
                break;
        }
    }
}
