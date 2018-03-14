using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGrid : MonoBehaviour {

    [SerializeField] private int s_GridXsize = 10;
    [SerializeField] private int s_GridYsize = 10;

    [SerializeField] GameObject nodeObjectPrefab;
    [SerializeField] GameObject testnodeObjectPrefab;

    [SerializeField] GameObject[] ObjectLayers;

    private TileNode[,] s_NodeGrid;

    // Use this for initialization
    void Start () {
        InstantiateNewGrid();
        SetNeighboursOfNode();
	}

    public void InstantiateNewGrid()
    {
        s_NodeGrid = new TileNode[s_GridXsize+2, s_GridYsize+2];
        for (int i = 0; i < s_GridXsize+2; i++)
        {
            for (int j = 0; j < s_GridYsize + 2; j++)
            {
                TileNode newNode = new TileNode();
                TileBools bools = new TileBools();
                newNode.IsFilled = false;
                if (i == 0 || i == s_GridXsize + 1 || j == 0 || j == s_GridYsize + 1)
                {
                    newNode.IsEdgeStone = true;
                    GameObject tileObj = Instantiate(testnodeObjectPrefab, new Vector3(i, j, 0), Quaternion.identity);
                    newNode.TileObject = tileObj;
                }
                else
                {
                    GameObject tileObj = Instantiate(nodeObjectPrefab, new Vector3(i, j, ObjectLayers[j - 1].transform.position.z), Quaternion.identity, ObjectLayers[j - 1].transform);
                    ClickedOnTile TileClicker = tileObj.GetComponent<ClickedOnTile>();
                    TileClicker.Grid = this;
                    TileClicker.m_TilePosX = i;
                    TileClicker.m_TilePosY = j;
                    newNode.TileObject = tileObj;
                }
                if (i == 1 && j == 1)
                {
                    newNode.IsFilled = true;
                    bools.Right = true;
                    bools.Up = true;
                    bools.Middle = true;
                    newNode.IsFilled = true;
                }
                else if (i == s_GridXsize && j == 1)
                {
                    newNode.IsFilled = true;
                    bools.Left = true;
                    bools.Up = true;
                    bools.Middle = true;
                    newNode.IsFilled = true;
                }
                else if (i == 1 && j == s_GridYsize)
                {
                    newNode.IsFilled = true;
                    bools.Right = true;
                    bools.Down = true;
                    bools.Middle = true;
                    newNode.IsFilled = true;
                }
                else if (i == s_GridXsize && j == s_GridYsize)
                {
                    newNode.IsFilled = true;
                    bools.Left = true;
                    bools.Down = true;
                    bools.Middle = true;
                    newNode.IsFilled = true;
                }
                else if (i == 5 && j == 5)
                {
                    newNode.IsFilled = true;
                    bools.Left = true;
                    bools.Up = true;
                    bools.Middle = true;
                    bools.Down = true;
                    bools.Right = true;
                    newNode.IsFilled = true;
                }
                newNode.Bools = bools;
                newNode.UpdateArt();
                s_NodeGrid[i, j] = newNode;
            }
        }
    }

    public void SetNeighboursOfNode()
    {
        if (s_NodeGrid == null)
        {
            throw new System.ArgumentException("The NodeGrid cannot be null", "original");
        }
        else
        {
            for (int i = 0; i < s_GridXsize + 2; i++)
            {
                for (int j = 0; j < s_GridYsize + 2; j++)
                {
                    if(!s_NodeGrid[i, j].IsEdgeStone)
                    {
                        Neighbours neighbours = new Neighbours();
                        neighbours.up = s_NodeGrid[i, j + 1];
                        neighbours.down = s_NodeGrid[i, j - 1];
                        neighbours.right = s_NodeGrid[i + 1, j];
                        neighbours.left = s_NodeGrid[i - 1, j];
                        s_NodeGrid[i, j].AllNeighbours = neighbours;
                    }
                }
            }
        }
    }

    public bool PlaceNewCard(int x, int y, bool UpCard, bool RightCard, bool DownCard, bool LeftCard, bool MiddleCard)
    {
        if(s_NodeGrid[x,y].CheckPlacement(UpCard,RightCard,DownCard,LeftCard) && !s_NodeGrid[x, y].IsFilled)
        {
            TileBools bools = new TileBools();
            bools.Up = UpCard;
            bools.Right = RightCard;
            bools.Down = DownCard;
            bools.Left = LeftCard;
            bools.Middle = MiddleCard;
            s_NodeGrid[x, y].Bools = bools;
            s_NodeGrid[x, y].IsFilled = true;
            s_NodeGrid[x, y].UpdateArt();
            return true;
        }
        else
        {
            return false;
        }
    }
}
