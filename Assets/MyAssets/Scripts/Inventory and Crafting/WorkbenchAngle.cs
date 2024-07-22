using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkbenchAngle : MonoBehaviour
{
    public enum AXIS_WORKBENCH { FRONT, RIGHT, BACK, LEFT}

    [SerializeField] public AXIS_WORKBENCH axis;

    public int getAxis(){
        switch (axis) {
            case AXIS_WORKBENCH.FRONT: return 0;
            case AXIS_WORKBENCH.RIGHT: return 90;
            case AXIS_WORKBENCH.BACK: return 180;
            case AXIS_WORKBENCH.LEFT: return -90;
            default: return 0;
        }
    }
}
