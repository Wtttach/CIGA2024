using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;

public class RainPool : MonoBehaviour
{
    public GameObject RainPrefab;
    public Transform ParentTransform;
    public float DropHeight;
    public float RainingRadius;
    [HideInInspector]
    private ObjectPool<GameObject> _rainPoolInstance;
    
    private void Start()
    {
        _rainPoolInstance = new ObjectPool<GameObject>(
            () =>
            {
                var area= Instantiate(RainPrefab, transform.position, Quaternion.identity, transform);
                return area;
            },
            (go) =>
            {
                var xz = RainingRadius * Random.insideUnitCircle;
                go.transform.position=new Vector3(xz.x,DropHeight,xz.y);
                go.transform.SetParent(ParentTransform);
                go.SetActive(true);
            },
            (go) =>
            {
                go.SetActive(false);
                go.transform.SetParent(transform);
            },
            (go) => { Destroy(go); }
        );
    }

    public GameObject Get()
    {
        return _rainPoolInstance.Get();
    }
        
    public void Release(GameObject rain)
    {
        _rainPoolInstance.Release(rain);
    }
}
