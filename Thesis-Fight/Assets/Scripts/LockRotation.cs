using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockRotation : MonoBehaviour {

    private void LateUpdate()
    {
        transform.rotation = Quaternion.FromToRotation(transform.forward, Camera.main.transform.forward);
    }
}
