using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DungeonMap : MonoBehaviour
{
    public GameObject roomPrefab; 
    private int mapWidth =8; 
    private int mapHeight =8; 
    private int roomSize = 41;

    void Start()
    {
        GenerateMap();
    }

    void GenerateMap()
    {
        
        Vector2Int startPosition = new Vector2Int(mapWidth / 2, mapHeight / 2);
        CreateRoom(Vector3.zero);

    }

    void CreateRoom(Vector3 position)
    {
        Vector3 roomPosition = new Vector3(position.x * roomSize, 0, position.y * roomSize);
        GameObject newRoom = Instantiate(roomPrefab, roomPosition, Quaternion.identity);
    }

}