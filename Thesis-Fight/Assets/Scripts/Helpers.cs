using UnityEngine;

public class Helpers {

    private static int floorMask;

    // Remove layer parameter and update code references
    public static Vector3 RaycastFloor(LayerMask layer)
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(camRay, out hit, Mathf.Infinity, layer))
        {
            return hit.point;
        }

        return Vector3.zero;
    }

    public static GameObject RaycastLayer(LayerMask layer)
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(camRay, out hit, Mathf.Infinity, layer))
        {
            return hit.transform.gameObject;
        }

        return null;
    }
}

public enum MouseButtonDown
{
    MBD_LEFT = 0,
    MBD_RIGHT,
    MBD_MIDDLE,
}