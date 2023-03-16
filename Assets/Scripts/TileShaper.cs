using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileShaper : MonoBehaviour
{
    public GameObject LeftTileSensor;
    public GameObject RightTileSensor;
    public GameObject AboveTileSensor;
    public GameObject BelowTileSensor;

    void AnalyzeAdjacentTiles()
    {
        AssignHeightValue();
        AssignHeightValue();
        AssignHeightValue();
        AssignHeightValue();
    }
    // Start is called before the first frame update
    int AssignHeightValue()
    {
        string landType = "";
        int heightValue = 0;
        switch (landType)
        {
            case "OpenWater":
                heightValue = 1;
                break;
            case "CoastWater":
                heightValue = 2;
                break;
            case "Shoreline":
                heightValue = 3;
                break;
            case "Inland":
                heightValue = 4;
                break;
            case "Highland":
                heightValue = 5;
                break;
            default:
                break;
        }
        return heightValue;
    }
}
