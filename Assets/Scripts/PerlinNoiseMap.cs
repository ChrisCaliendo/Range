using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoiseMap : MonoBehaviour
{
    Dictionary<int, GameObject> tileset;
    Dictionary<int, GameObject> tile_groups;
    public GameObject openWater;
    public GameObject shallowWater;
    public GameObject beach;
    public GameObject plains;
    public GameObject woods;

    int map_width = 300;
    int map_height = 300;
    int mapVBorderSize;
    int mapHBorderSize;
 
    List<List<int>> noise_grid = new List<List<int>>();
    List<List<GameObject>> tile_grid = new List<List<GameObject>>();
 
    // recommend 4 to 20
    float magnification = 10.0f;
 
    int x_offset = 0; // <- +>
    int y_offset = 0; // v- +^

    // Start is called before the first frame update
    void Start()
    {
        //x_offset = UnityEngine.Random.Range(-10000000, 10000000);
        //y_offset = UnityEngine.Random.Range(-10000000, 10000000);
        mapVBorderSize = Convert.ToInt32(map_height/10);
        mapHBorderSize = Convert.ToInt32(map_width/10);
        CreateTileset();
        CreateTileGroups();
        GenerateMap();
    }
    

    void CreateTileset()
    {
        tileset = new Dictionary<int, GameObject>();
        tileset.Add(0, openWater);
        tileset.Add(1, shallowWater);
        tileset.Add(2, beach);
        tileset.Add(3, plains);
        tileset.Add(4, woods);
    }

    void CreateTileGroups()
    {
        tile_groups = new Dictionary<int, GameObject>();
        foreach(KeyValuePair<int, GameObject> prefab_pair in tileset)
        {
            GameObject tile_group = new GameObject(prefab_pair.Value.name);
            tile_group.transform.parent = gameObject.transform;
            tile_group.transform.localPosition = new Vector3(0, 0, 0);
            tile_groups.Add(prefab_pair.Key, tile_group);
        }
    }

    void GenerateMap()
    {
        /** Generate a 2D grid using the Perlin noise fuction, storing it as
            both raw ID values and tile gameobjects **/
 
        for(int x = 0; x < map_width; x++)
        {
            noise_grid.Add(new List<int>());
            tile_grid.Add(new List<GameObject>());
            for(int y = 0; y < map_height; y++)
            {
                int tile_id = GetIdUsingPerlin(x, y);

                tile_id = NormalizeTerrain(x, y, tile_id);

                noise_grid[x].Add(tile_id);
                CreateTile(tile_id, x, y);
            }
        }
    }

    int GetIdUsingPerlin(int x, int y)
    {
        /** Using a grid coordinate input, generate a Perlin noise value to be
            converted into a tile ID code. Rescale the normalised Perlin value
            to the number of tiles available. **/
 
        float raw_perlin = Mathf.PerlinNoise(
            (x - x_offset) / magnification,
            (y - y_offset) / magnification
        );
        float clamp_perlin = Mathf.Clamp01(raw_perlin); 
        float scaled_perlin = clamp_perlin * tileset.Count;
 
        // Replaced 4 with tileset.Count to make adding tiles easier
        if(scaled_perlin == tileset.Count)
        {
            scaled_perlin = (tileset.Count - 1);
        }
        return Mathf.FloorToInt(scaled_perlin);
    }
 
    void CreateTile(int tile_id, int x, int y)
    {
        /** Creates a new tile using the type id code, group it with common
            tiles, set it's position and store the gameobject. **/
 
        GameObject tile_prefab = tileset[tile_id];
        GameObject tile_group = tile_groups[tile_id];
        GameObject tile = Instantiate(tile_prefab, tile_group.transform);
 
        tile.name = string.Format("tile_x{0}_y{1}", x, y);
        tile.transform.localPosition = new Vector3(x, y, 0);
 
        tile_grid[x].Add(tile);
    }

    private int NormalizeTerrain( int x, int y, int tile_id)
    {

        if(WithinBoundary(x, y, (map_height-mapVBorderSize), mapVBorderSize, (map_width-mapHBorderSize), mapHBorderSize))
        {

            if (WithinBoundary(x, y, 270, 165, 140, 30))
            {
                if (WithinBoundary(x, y, 255, 180, 125, 45))
                {
                    return tile_id;
                }
                else if (tile_id > 1) return 1;
                else return 0;
            }
            else if (WithinBoundary(x, y, 270, 165, 270, 165))
            {
                if (WithinBoundary(x, y, 255, 180, 255, 180))
                {
                    return tile_id;
                }
                else if (tile_id > 1) return 1;
                else return 0;
            }
            else if (WithinBoundary(x, y, 140, 30, 140, 30))
            {
                if (WithinBoundary(x, y, 125, 45, 125, 45))
                {
                    return tile_id;
                }
                else if (tile_id > 1) return 1;
                else return 0;
            }
            else if (WithinBoundary(x, y, 140, 30, 270, 165))
            {
                if (WithinBoundary(x, y, 125, 45, 255, 180))
                {
                    return tile_id;
                }
                else if (tile_id > 1) return 1;
                else return 0;
            }
             else return 0;

            
        }
        else return 0;
        
        
    }

    
    private int FormIsland(int x, int y, int tile_id , int upperBound, int lowerBound, int rightBound, int leftBound, double InlandApex,  int levels){

        //Not within island radius
        if(!WithinBoundary(x, y, upperBound, lowerBound, rightBound, leftBound)) return title_id;


        //Check every level of erosion
        double erosionCoef = ((1-InlandApex)/levels);
        for(int l = 1; l < levels; l++){
            double erosion = 1 - (erosionCoef*l);
            if(!WithinBoundary(x, y, (upperBound*erosion), (lowerBound*erosion), (rightBound*erosion), (leftBound*erosion))) 
            {
                return tile_id;
            }
        }
        return tile_id;

        if (WithinBoundary(x, y, 140, 30, 270, 165))
            {
                if (WithinBoundary(x, y, 125, 45, 255, 180))
                {
                    return tile_id;
                }
                else if (tile_id > 1) return 1;
                else return 0;
            }
    }
    

    private bool WithinBoundary(int x, int y, double upperBound, double lowerBound, double rightBound, double leftBound)
    {
        if(x<leftBound || y<lowerBound || x>rightBound || y>upperBound)
        {
            return false;
        }
        else return true;
    }

    
}
