using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum MouseButton
{
    MB_LEFT = 0,
    MB_RIGHT,
    MB_MIDDLE,
};

public class CameraController : MonoBehaviour
{

    public float speed;

    //[SerializeField]
    private Vector3 focus = Vector3.zero;
    //[SerializeField]
    private GameObject focusObject = null;
    private Vector3 oldPos;

    private void setupFocusObject(string name)
    {
        GameObject obj = focusObject = new GameObject(name);
        obj.transform.position = focus;
        obj.transform.LookAt(transform.position);
    }

    private void Start()
    {
        if (focusObject == null)
            setupFocusObject("CameraFocus");

        //Transform trans = transform;
        transform.parent = focusObject.transform;

        transform.LookAt(focus);
    }

    private void Update()
    {
        mouseEvent();
    }

    private void mouseEvent()
    {
        float delta = Input.GetAxis("Mouse ScrollWheel");
        if (delta != 0.0f)
            this.mouseWheelEvent(-delta);

        if (Input.GetMouseButtonDown((int)MouseButton.MB_MIDDLE))
            oldPos = Input.mousePosition;

        mouseDragEvent(Input.mousePosition);
    }

    private void mouseWheelEvent(float delta)
    {
        Vector3 focusToPosition = transform.position - focus;

        Vector3 post = focusToPosition * (1.0f + delta);
        if (post.magnitude > 20 && post.magnitude < 70)
            transform.position = focus + post;
    }

    private void mouseDragEvent(Vector3 mousePos)
    {

        Vector3 diff = mousePos - oldPos;

        if (Input.GetMouseButton((int)MouseButton.MB_MIDDLE))
        {
            if (diff.magnitude > Vector3.kEpsilon)
                cameraTranslate(-diff / 100.0f);
        }

        oldPos = mousePos;
    }

    private void cameraTranslate(Vector3 vector)
    {
        Transform focusTrans = focusObject.transform;

        vector.x *= -speed;
        vector.y *= speed;

        focusTrans.Translate(Vector3.right * vector.x);
        focusTrans.Translate(Vector3.up * vector.y);

        focus = focusTrans.position;
    }
}