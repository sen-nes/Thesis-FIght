using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour {

    private Transform camTransform;

    private void Awake()
    {
        camTransform = Camera.main.transform;
    }

    private void Update()
    {
        transform.LookAt(camTransform);
    }
}
