using UnityEngine;

public class Helpers {

    private static int floorMask;

    public static Vector3 GetFloorPoint(LayerMask layer)
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(camRay, out hit, Mathf.Infinity, layer))
        {
            return hit.point;
        }

        return Vector3.zero;
    }
}

public enum MouseButtonDown
{
    MBD_LEFT = 0,
    MBD_RIGHT,
    MBD_MIDDLE,
};