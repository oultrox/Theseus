﻿using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class RoomManager : MonoBehaviour
{
    [Serializable]
    public class Count
    {
        public int minimum;
        public int maximum;

        public Count (int min, int max)
        {
            minimum = min;
            maximum = max;
        }
    }

    public int columns;
    public int rows;
    public Count wallCount = new Count(5, 9);
    public Count foodCount = new Count(1, 5);
    public GameObject exit;
    public GameObject[] floorTiles;
    public GameObject[] wallTiles;
    public GameObject[] foodTiles;
    public GameObject[] enemyTiles;
    public GameObject[] outerWallTiles;
    public GameObject[] outerWallCornerTiles;

    private Transform _roomHolder;
    private List<Vector3> _gridPositions = new List<Vector3>();
    private Cell _roomCell;

    void InitialiseList ()
    {
        _gridPositions.Clear();

        for (int x = 0; x < columns - 1; x++)
        {
            for (int y = 0; y < rows - 1; y++)
            {
                _gridPositions.Add(new Vector3(x,y,0f));
            }
        }
    }

    void RoomSetup ()
    {
        _roomHolder = new GameObject("Room").transform;
        float rotation = 0.0f;
        int outerWall = 0;

        for (int x = -1; x < columns + 1; x++)
        {
            for (int y = -1; y < rows + 1; y++)
            {
                GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];
                rotation = 0;
                outerWall = 0;
                
                if (x == -1)
                {
                    rotation = 90;
                    outerWall++;
                }
                else if(x == columns)
                {
                    rotation = 270;
                    outerWall++;
                } 
                if (y == -1)
                {
                    if (x != -1)
                        rotation = 180;
                    outerWall++;
                }
                else if (y == rows)
                {
                    if(x != columns)
                    rotation = 0;
                    outerWall++;
                }

                if(outerWall == 1)
                    toInstantiate = outerWallTiles[Random.Range(0, outerWallTiles.Length)];
                else if(outerWall == 2)
                    toInstantiate = outerWallCornerTiles[Random.Range(0, outerWallCornerTiles.Length)];


                GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                instance.transform.Rotate(0, 0, rotation);

                instance.transform.SetParent(_roomHolder);
            }
        }
    }

    void PlaceDoors()
    {
        if (_roomCell.TopNeighbour != CellType.Empty)
        {
            var instance = Instantiate(exit, new Vector3(columns / 2, rows, 0f), Quaternion.identity) as GameObject;
        }

        if (_roomCell.RightNeighbour != CellType.Empty)
        {
            var instance = Instantiate(exit, new Vector3(columns, rows / 2, 0f), Quaternion.identity) as GameObject;
            instance.transform.Rotate(0, 0, -90);
        }

        if (_roomCell.BottomNeighbour != CellType.Empty)
        {
            var instance = Instantiate(exit, new Vector3(columns / 2, -1, 0f), Quaternion.identity) as GameObject;
            instance.transform.Rotate(0, 0, 180);
        }

        if (_roomCell.LeftNeighbour != CellType.Empty)
        {
            var instance = Instantiate(exit, new Vector3(-1, rows / 2, 0f), Quaternion.identity) as GameObject;
            instance.transform.Rotate(0, 0, 90);
        }
    }

    Vector3 RandomPosition()
    {
        int randomIndex = Random.Range(0, _gridPositions.Count);
        Vector3 randomPosition = _gridPositions[randomIndex];
        _gridPositions.RemoveAt(randomIndex);

        return randomPosition;
    }

    void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum)
    {
        int objectCount = Random.Range(minimum, maximum + 1);

        for (int i = 0; i < objectCount; i++)
        {
            Vector3 randomPosition = RandomPosition();
            GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];
            Instantiate(tileChoice, randomPosition, Quaternion.identity);
        }
    }

    public void SetupRoom(int level, Cell cell)
    {
        _roomCell = cell;

        RoomSetup();
        InitialiseList();
        PlaceDoors();
        //LayoutObjectAtRandom(wallTiles, wallCount.minimum, wallCount.maximum);
        //LayoutObjectAtRandom(foodTiles, foodCount.minimum, foodCount.maximum);
        int enemyCount = (int) Mathf.Log(level, 2f);
        //LayoutObjectAtRandom(enemyTiles, enemyCount, enemyCount);
        //Instantiate(exit, new Vector3(-1, rows / 2, 0f), Quaternion.identity);
    }
}
