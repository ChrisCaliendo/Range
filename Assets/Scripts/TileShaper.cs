using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileShaper : MonoBehaviour
{
    public static GameObject stoneTile;

    public GameObject LeftTileSensor;
    public GameObject RightTileSensor;
    public GameObject AboveTileSensor;
    public GameObject BelowTileSensor;
    int currentTile;

    public static Sprite h;

    static void Awake()
    {
        stoneTile = GameObject.Find("Assets/Prefabs/Tiles/rockwallEmpty.prefab");
    }

    void Start()
    {
        currentTile = AssignCurrentHeightValue(gameObject.tag);
    }

    void ShapeTiles()
    {
        int surroundingTileInfo = AnalyzeAdjacentTiles(LeftTileSensor, RightTileSensor, AboveTileSensor, BelowTileSensor, currentTile);
    }

    static int AnalyzeAdjacentTiles(GameObject LS, GameObject RS, GameObject AS, GameObject BS, int CT)
    {
        int LT = AssignHeightValue(LS.tag, CT);
        int RT = AssignHeightValue(RS.tag, CT);
        int AT = AssignHeightValue(AS.tag, CT);
        int BT = AssignHeightValue(BS.tag, CT);
        return (1000*LT)+(100*RT)+(10*AT)+(1*BT);
    }
    
    static int AssignHeightValue(string tileTag, int CT)
    {
        int heightValue = 0;
        switch (tileTag)
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
                heightValue = CT;
                break;
        }

        heightValue = heightValue - CT;
        
        if(heightValue > 1) //Too Deep to have Border so Turns Tile to Stone
        {
            return 3;  
        }
        else if(heightValue <= 0) //Same or Greater Height than Tile
        {
           return 2; 
        }
        else return 1; //Just Below Current Level meaning Tile is Border
    }

    static int AssignCurrentHeightValue(string tileTag)
    {
        switch (tileTag)
        {
            case "OpenWater":
                return 1;
            case "CoastWater":
                return 2;
            case "Shoreline":
                return 3;
            case "Inland":
                return 4;
            case "Highland":
                return 5;
            default:
                return 0;
        }
    }

    void PickTileTexture(int info)
    {
        switch(info)
        {
            case 2222: //Open Field
                break;

            case 2221: //Bottom Edge
                break;
            
            case 2212: //Above Edge
                break;

            case 2122: //Right Edge
                break;
            
            case 1222: //Left Edge
                break;

            case 1112: //Stem From Bottom
                break;
            
            case 1121: //Stem from Above
                break;

            case 1211: //Stem from Right
                break;
            
            case 2111: //Stem from Left
                break;

            case 1111: //Single Island
                break;

            default: //Open Field Again
                break;
        }
    }

    void TurnToStone()
    {

    }

    void DestroySensors()
    {

    }
}
