using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGrid : MonoBehaviour {

    [SerializeField] private int m_GridXsize = 10;
    [SerializeField] private int m_GridYsize = 10;

    [SerializeField] GameObject nodeObjectPrefab;
    [SerializeField] GameObject testnodeObjectPrefab;

    [SerializeField] GameObject[] ObjectLayers;

    private TileNode[,] m_NodeGrid;

    private TileNode[] m_PlayerStartNodes;

    private TileNode[] m_NodeRoad;


    // Use this for initialization
    void Start () {
        InstantiateNewGrid();
        SetNeighboursOfNode();
	}

    #region void functions
    public void InstantiateNewGrid()
    {
        int amountOfPlayersPlaceholder = 4;
        Debug.LogError("the amount of players still needs to be recieved");
        m_PlayerStartNodes = new TileNode[amountOfPlayersPlaceholder];

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
                    newNode.IsStartPoint=true;
                    m_PlayerStartNodes[2] = newNode;
                }
                else if (i == m_GridXsize && j == 1)
                {
                    newNode.IsFilled = true;
                    bools.Left = true;
                    bools.Up = true;
                    bools.Middle = true;
                    newNode.IsFilled = true;
                    newNode.IsStartPoint = true;
                    m_PlayerStartNodes[1] = newNode;
                }
                else if (i == 1 && j == m_GridYsize)
                {
                    newNode.IsFilled = true;
                    bools.Right = true;
                    bools.Down = true;
                    bools.Middle = true;
                    newNode.IsFilled = true;
                    newNode.IsStartPoint = true;
                    m_PlayerStartNodes[0] = newNode;
                }
                else if (i == m_GridXsize && j == m_GridYsize)
                {
                    newNode.IsFilled = true;
                    bools.Left = true;
                    bools.Down = true;
                    bools.Middle = true;
                    newNode.IsFilled = true;
                    newNode.IsStartPoint = true;
                    m_PlayerStartNodes[3] = newNode;
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
                    newNode.IsEndpoint = true;
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
    #endregion

    #region return value functions
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

    public bool CompleteRoad(int startPlayer)
    {
        int amountOfPlayers = 4;
        Debug.LogError("the amount of players still needs to be recieved");
        int currentRound = startPlayer - 1; ;
        int roundsMade = 0;

        bool roadIsMade = false;

        bool goNext = false;

        for (int i = 0; i < amountOfPlayers; i++)
        {
            List<TileNode> checkedNodes     = new List<TileNode>();
            List<TileNode> checkNodes       = new List<TileNode>();

            checkNodes.Add(m_PlayerStartNodes[currentRound]);
            checkedNodes.Add(m_PlayerStartNodes[currentRound]);

            goNext = true;
            int infiniteLoopPrevention0 = 0;

            while (goNext == true && infiniteLoopPrevention0 < 10000)
            {
                if(checkNodes.Count >= 1)
                {
                    for (int j = 0; j < checkNodes.Count; j++)
                    {
                        TileNode currentNode = checkedNodes[j];
                        checkedNodes.RemoveAt(j);
                        if (currentNode.IsEndpoint)
                        {
                            int infiniteLoopPrevention1 = 0;

                            bool loopThroughParrents = true;
                            TileNode tempNode = currentNode.RoadParent;
                            List<TileNode> tempRoadNodes = new List<TileNode>();
                            tempRoadNodes.Add(currentNode);
                            while(loopThroughParrents && infiniteLoopPrevention1 < 10000)
                            {
                                tempRoadNodes.Add(tempNode);
                                if(tempNode.IsStartPoint)
                                {
                                    loopThroughParrents = false;
                                    m_NodeRoad = tempRoadNodes.ToArray();
                                    roadIsMade = true;
                                }
                                else
                                {
                                    tempNode = tempNode.RoadParent;
                                }
                                infiniteLoopPrevention1++;
                                Debug.Assert(infiniteLoopPrevention1 > 9000, "stuck in infinite loop TileGrid.cs line 199 ");                                
                            }

                        }
                        else
                        {
                            if (currentNode.AccesableNeighbours.up != null && currentNode.AccesableNeighbours.up.IsFilled && !currentNode.AccesableNeighbours.up.IsChecked)
                            {
                                checkNodes.Add(currentNode.AccesableNeighbours.up);
                                checkedNodes.Add(currentNode.AccesableNeighbours.up);
                                currentNode.AccesableNeighbours.up.RoadParent = currentNode;
                                currentNode.AccesableNeighbours.up.IsChecked = true;
                            }
                            if (currentNode.AccesableNeighbours.right != null && currentNode.AccesableNeighbours.right.IsFilled && !currentNode.AccesableNeighbours.right.IsChecked)
                            {
                                checkNodes.Add(currentNode.AccesableNeighbours.right);
                                checkedNodes.Add(currentNode.AccesableNeighbours.right);
                                currentNode.AccesableNeighbours.right.RoadParent = currentNode;
                                currentNode.AccesableNeighbours.right.IsChecked = true;
                            }
                            if (currentNode.AccesableNeighbours.down != null && currentNode.AccesableNeighbours.down.IsFilled && !currentNode.AccesableNeighbours.down.IsChecked)
                            {
                                checkNodes.Add(currentNode.AccesableNeighbours.down);
                                checkedNodes.Add(currentNode.AccesableNeighbours.down);
                                currentNode.AccesableNeighbours.down.RoadParent = currentNode;
                                currentNode.AccesableNeighbours.down.IsChecked = true;
                            }
                            if (currentNode.AccesableNeighbours.left != null && currentNode.AccesableNeighbours.left.IsFilled && !currentNode.AccesableNeighbours.left.IsChecked)
                            {
                                checkNodes.Add(currentNode.AccesableNeighbours.left);
                                checkedNodes.Add(currentNode.AccesableNeighbours.left);
                                currentNode.AccesableNeighbours.left.RoadParent = currentNode;
                                currentNode.AccesableNeighbours.left.IsChecked = true;
                            }
                        }
                    }
                }
                else
                {
                    goNext = false;
                }

                infiniteLoopPrevention0++;
                Debug.Assert(infiniteLoopPrevention0 > 9000, "stuck in infinite loop TileGrid.cs line 186 ");
            }

            currentRound++;
            if(currentRound >= amountOfPlayers)
            {
                currentRound = 0;
            }

            roundsMade++;

        }
        return roadIsMade;
    }
    #endregion
}
