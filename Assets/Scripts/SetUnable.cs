using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUnable : MonoBehaviour
{
    public GameObject Level;

    // Update is called once per frame
    private void Update()
    {
        if(Level.activeSelf)
            Level.SetActive(false);
    }
}
