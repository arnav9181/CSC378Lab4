using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility : MonoBehaviour
{
    public static bool isFlipped = false;

    public static float getAngleTowardsMouse(Vector3 pos)
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 objectPos = Camera.main.WorldToScreenPoint(pos);
        return getAngle2D(objectPos, mousePos);
    }

    public static float getAngle2D(Vector2 drn) {
        if (isFlipped) {
            drn *= -1;
        }
        return Mathf.Atan2(drn.y, drn.x) * Mathf.Rad2Deg;
    }

    public static float getAngle2D(Vector3 curPos, Vector3 destPos) {
        Vector2 drn = new Vector2(destPos.x - curPos.x, destPos.y - curPos.y);
        return getAngle2D(drn);
    }

    public static int getDirection() {
        if (isFlipped) {
            return -1;
        }
        return 1;
    }
}
