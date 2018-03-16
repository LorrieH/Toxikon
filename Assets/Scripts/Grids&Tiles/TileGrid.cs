using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGrid : MonoBehaviour {

    [SerializeField] private int m_GridXsize = 10;
    [SerializeField] private int m_GridYsize = 10;

    [SerializeField] private GameObject m_NodeObjectPrefab;

    [SerializeField] private GameObject[] m_ObjectLayers;

    private TileNode[,] m_NodeGrid;
    
    void Start () {
        InstantiateNewGrid();
        SetNeighboursOfNode();
	}

    public void InstantiateNewGrid()
    {
        m_NodeGrid = new TileNode[m_GridXsize+2, m_GridYsize+2];
        for (int i = 0; i < m_GridXsize+2; i++)
        {
            for (int j = 0; j < m_GridYsize + 2; j++)
            {
                TileNode newNode = new TileNode();
                TileBools bools = new TileBools();
                newNode.IsFilled = false;
                if (i == 0 || i == m_GridXsize + 1 || j == 0 || j == m_GridYsize + 1)
                {
                    newNode.IsEdgeStone = true;
                }
                else
                {
                    GameObject tileObj = Instantiate(m_NodeObjectPrefab, new Vector3(i, j, m_ObjectLayers[j - 1].transform.position.z), Quaternion.identity, m_ObjectLayers[j - 1].transform);
                    ClickedOnTile TileClicker = tileObj.GetComponent<ClickedOnTile>();
                    TileClicker.Grid = this;
                    TileClicker.TilePosX = i;
                    TileClicker.TilePosY = j;
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
                else if (i == m_GridXsize && j == 1)
                {
                    newNode.IsFilled = true;
                    bools.Left = true;
                    bools.Up = true;
                    bools.Middle = true;
                    newNode.IsFilled = true;
                }
                else if (i == 1 && j == m_GridYsize)
                {
                    newNode.IsFilled = true;
                    bools.Right = true;
                    bools.Down = true;
                    bools.Middle = true;
                    newNode.IsFilled = true;
                }
                else if (i == m_GridXsize && j == m_GridYsize)
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
                m_NodeGrid[i, j] = newNode;
            }
        }
    }

    public void SetNeighboursOfNode()
    {
        if (m_NodeGrid == null)
        {
            throw new System.ArgumentException("The NodeGrid cannot be null", "original");
        }
        else
        {
            for (int i = 0; i < m_GridXsize + 2; i++)
            {
                for (int j = 0; j < m_GridYsize + 2; j++)
                {
                    if(!m_NodeGrid[i, j].IsEdgeStone)
                    {
                        Neighbours neighbours = new Neighbours();
                        neighbours.up = m_NodeGrid[i, j + 1];
                        neighbours.down = m_NodeGrid[i, j - 1];
                        neighbours.right = m_NodeGrid[i + 1, j];
                        neighbours.left = m_NodeGrid[i - 1, j];
                        m_NodeGrid[i, j].AllNeighbours = neighbours;
                    }
                }
            }
        }
    }

    public bool PlaceNewCard(int x, int y, bool UpCard, bool RightCard, bool DownCard, bool LeftCard, bool MiddleCard)
    {
        if(m_NodeGrid[x,y].CheckPlacement(UpCard,RightCard,DownCard,LeftCard) && !m_NodeGrid[x, y].IsFilled)
        {
            TileBools bools = new TileBools();
            bools.Up = UpCard;
            bools.Right = RightCard;
            bools.Down = DownCard;
            bools.Left = LeftCard;
            bools.Middle = MiddleCard;
            m_NodeGrid[x, y].Bools = bools;
            m_NodeGrid[x, y].IsFilled = true;
            m_NodeGrid[x, y].UpdateArt();
            return true;
        }
        else
        {
            return false;
        }
    }
}