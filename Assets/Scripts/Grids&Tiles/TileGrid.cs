using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGrid : MonoBehaviour
{

    public static TileGrid s_Instance;

    [SerializeField] private int m_GridXSize = 10;
    [SerializeField] private int m_GridYSize = 10;

    [SerializeField] private GameObject m_NodeObjectPrefab;

    private TileNode[,] m_NodeGrid;
    private GameObject[,] m_NodeBridges;


    private TileNode[] m_PlayerStartNodes;
    private TileNode[] m_NodeRoad;

    #region startUp
    private void Awake()
    {
        Init();
    }

    //make instance
    private void Init()
    {
        if (s_Instance == null)
            s_Instance = this;
        else
            Destroy(gameObject);
    }


    // Use this for initialization
    void Start()
    {
        InstantiateNewGrid();
        SetNeighboursOfNode();
    }
    #endregion

    #region void functions
    public void InstantiateNewGrid()
    {
        int amountOfPlayersPlaceholder = 4;
        Debug.LogError("the amount of players still needs to be recieved");
        m_PlayerStartNodes = new TileNode[amountOfPlayersPlaceholder];
        m_NodeBridges = new GameObject[m_GridXSize, m_GridYSize];
        m_NodeGrid = new TileNode[m_GridXSize + 2, m_GridYSize + 2];
        for (int i = 0; i < m_GridXSize + 2; i++)
        {
            for (int j = 0; j < m_GridYSize + 2; j++)
            {
                TileNode newNode = new TileNode();
                TileBools bools = new TileBools();
                newNode.IsFilled = false;
                newNode.IsDestructable = true;
                if (i == 0 || i == m_GridXSize + 1 || j == 0 || j == m_GridYSize + 1)
                {
                    newNode.IsEdgeStone = true;
                    newNode.IsDestructable = false;
                    //GameObject tileObj = Instantiate(new GameObject(), new Vector3(i, j, 0), Quaternion.identity);
                    newNode.TileObject = null;
                }
                else
                {
                    GameObject tileObj = Instantiate(m_NodeObjectPrefab, new Vector3(i, j, 0), Quaternion.identity, transform);
                    ClickedOnTile TileClicker = tileObj.GetComponent<ClickedOnTile>();
                    TileClicker.TilePosX = i;
                    TileClicker.TilePosY = j;
                    newNode.TileObject = tileObj;
                }
                if (i == 1 && j == 1)
                {
                    newNode.IsFilled = true;
                    newNode.IsDestructable = false;
                    bools.Right = true;
                    bools.Up = true;
                    bools.Middle = true;
                    newNode.IsFilled = true;
                    newNode.IsStartPoint = true;
                    m_PlayerStartNodes[2] = newNode;
                }
                else if (i == m_GridXSize && j == 1)
                {
                    newNode.IsFilled = true;
                    newNode.IsDestructable = false;
                    bools.Left = true;
                    bools.Up = true;
                    bools.Middle = true;
                    newNode.IsFilled = true;
                    newNode.IsStartPoint = true;
                    m_PlayerStartNodes[1] = newNode;
                }
                else if (i == 1 && j == m_GridYSize)
                {
                    newNode.IsFilled = true;
                    newNode.IsDestructable = false;
                    bools.Right = true;
                    bools.Down = true;
                    bools.Middle = true;
                    newNode.IsFilled = true;
                    newNode.IsStartPoint = true;
                    m_PlayerStartNodes[0] = newNode;
                }
                else if (i == m_GridXSize && j == m_GridYSize)
                {
                    newNode.IsFilled = true;
                    newNode.IsDestructable = false;
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
                    newNode.IsDestructable = false;
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

    public void SetAccesableNeighboursOfNode()
    {
        if (m_NodeGrid == null)
        {
            throw new System.ArgumentException("The NodeGrid cannot be null", "original");
        }
        else
        {
            for (int i = 0; i < m_GridXSize + 2; i++)
            {
                for (int j = 0; j < m_GridYSize + 2; j++)
                {
                    if (!m_NodeGrid[i, j].IsEdgeStone)
                    {
                        m_NodeGrid[i, j].SetAccesableNeighbours();
                    }
                }
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
            for (int i = 0; i < m_GridXSize + 2; i++)
            {
                for (int j = 0; j < m_GridYSize + 2; j++)
                {
                    if (!m_NodeGrid[i, j].IsEdgeStone)
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

    public bool MoveNode(int xMovingNode, int yMovingNode, int xTargetNode, int yTargetNode)
    {
        TileNode MovingNode = m_NodeGrid[xMovingNode, yMovingNode];
        if (PlaceNewCard(xTargetNode, yTargetNode, MovingNode.Bools.Up, MovingNode.Bools.Right, MovingNode.Bools.Down, MovingNode.Bools.Left, MovingNode.Bools.Middle))
        {
            DestroyNode(xMovingNode, yMovingNode);
            return true;
        }
        else
        {
            return false;
        }
    }


    public bool DestroyNode(int x, int y)
    {
        if (!m_NodeGrid[x, y].IsEdgeStone && m_NodeGrid[x, y].IsFilled && m_NodeGrid[x, y].IsDestructable)
        {
            TileBools bools = new TileBools();
            bools.Up = false;
            bools.Right = false;
            bools.Down = false;
            bools.Left = false;
            bools.Middle = false;
            m_NodeGrid[x, y].Bools = bools;
            m_NodeGrid[x, y].IsFilled = false;
            m_NodeGrid[x, y].UpdateArt();
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool PlaceNewCard(int x, int y, bool UpCard, bool RightCard, bool DownCard, bool LeftCard, bool MiddleCard)
    {
        if (m_NodeGrid[x, y].CheckPlacement(UpCard, RightCard, DownCard, LeftCard) && !m_NodeGrid[x, y].IsFilled)
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
            if (UpCard && m_NodeGrid[x, y].AllNeighbours.up.IsFilled)
            {
                GameObject Bridge = Instantiate(TileArtLib.s_Bridges[0], new Vector3(x , y + 0.5f, 0), Quaternion.identity, m_NodeGrid[x,y].TileObject.transform);
                //m_NodeBridges[] = Bridge;
            }
            if (RightCard && m_NodeGrid[x, y].AllNeighbours.right.IsFilled)
            {
                GameObject Bridge = Instantiate(TileArtLib.s_Bridges[1], new Vector3(x +0.5f, y, 0), Quaternion.identity, m_NodeGrid[x, y].TileObject.transform);
                //m_NodeBridges[] = Bridge;
            }
            if (DownCard && m_NodeGrid[x, y].AllNeighbours.down.IsFilled)
            {
                GameObject Bridge = Instantiate(TileArtLib.s_Bridges[0], new Vector3(x , y- 0.5f, 0), Quaternion.identity, m_NodeGrid[x, y].TileObject.transform);
                //m_NodeBridges[] = Bridge;
            }
            if (LeftCard && m_NodeGrid[x, y].AllNeighbours.left.IsFilled)
            {
                GameObject Bridge = Instantiate(TileArtLib.s_Bridges[1], new Vector3(x - 0.5f, y, 0), Quaternion.identity, m_NodeGrid[x, y].TileObject.transform);
                //m_NodeBridges[] = Bridge;
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CompleteRoad(int startPlayer)
    {
        bool roadIsMade = false;

        List<TileNode> checkedNodes = new List<TileNode>();
        List<TileNode> checkNodes = new List<TileNode>();

        checkNodes.Add(m_PlayerStartNodes[2]);
        checkedNodes.Add(m_PlayerStartNodes[2]);

        SetAccesableNeighboursOfNode();

        if (checkNodes.Count > 0)
        {
            for (int j = 0; j < checkNodes.Count; j++)
            {
                SetAccesableNeighboursOfNode();
                TileNode currentNode = checkNodes[j];
                checkNodes.RemoveAt(j);
                if (currentNode.IsEndpoint)
                {
                    roadIsMade = true;
                    int infiniteLoopPrevention1 = 0;

                    bool loopThroughParrents = true;
                    TileNode tempNode = currentNode.RoadParent;
                    List<TileNode> tempRoadNodes = new List<TileNode>();
                    tempRoadNodes.Add(currentNode);
                    while (loopThroughParrents && infiniteLoopPrevention1 < 10000)
                    {
                        tempRoadNodes.Add(tempNode);
                        if (tempNode.IsStartPoint)
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
                    Debug.Log("checkNeighbours");
                    Debug.Log(currentNode.AccesableNeighbours.up);
                    Debug.Log(currentNode.AccesableNeighbours.up.IsEdgeStone);
                    Debug.Log(currentNode.AccesableNeighbours.up.IsChecked);

                    if (currentNode.AccesableNeighbours.up != null && !currentNode.AccesableNeighbours.up.IsEdgeStone && !currentNode.AccesableNeighbours.up.IsChecked)
                    {
                        Debug.Log("checkNeighbours up");
                        checkNodes.Add(currentNode.AccesableNeighbours.up);
                        checkedNodes.Add(currentNode.AccesableNeighbours.up);
                        currentNode.AccesableNeighbours.up.RoadParent = currentNode;
                        currentNode.AccesableNeighbours.up.IsChecked = true;
                        currentNode.AccesableNeighbours.up.UpdateArt();
                    }
                    if (currentNode.AccesableNeighbours.right != null && !currentNode.AccesableNeighbours.right.IsEdgeStone && !currentNode.AccesableNeighbours.right.IsChecked)
                    {
                        Debug.Log("checkNeighbours right");
                        checkNodes.Add(currentNode.AccesableNeighbours.right);
                        checkedNodes.Add(currentNode.AccesableNeighbours.right);
                        currentNode.AccesableNeighbours.right.RoadParent = currentNode;
                        currentNode.AccesableNeighbours.right.IsChecked = true;
                        currentNode.AccesableNeighbours.right.UpdateArt();
                    }
                    if (currentNode.AccesableNeighbours.down != null && !currentNode.AccesableNeighbours.down.IsEdgeStone && !currentNode.AccesableNeighbours.down.IsChecked)
                    {
                        Debug.Log("checkNeighbours down");
                        checkNodes.Add(currentNode.AccesableNeighbours.down);
                        checkedNodes.Add(currentNode.AccesableNeighbours.down);
                        currentNode.AccesableNeighbours.down.RoadParent = currentNode;
                        currentNode.AccesableNeighbours.down.IsChecked = true;
                        currentNode.AccesableNeighbours.down.UpdateArt();
                    }
                    if (currentNode.AccesableNeighbours.left != null && !currentNode.AccesableNeighbours.left.IsEdgeStone && !currentNode.AccesableNeighbours.left.IsChecked)
                    {
                        Debug.Log("checkNeighbours left");
                        checkNodes.Add(currentNode.AccesableNeighbours.left);
                        checkedNodes.Add(currentNode.AccesableNeighbours.left);
                        currentNode.AccesableNeighbours.left.RoadParent = currentNode;
                        currentNode.AccesableNeighbours.left.IsChecked = true;
                        currentNode.AccesableNeighbours.left.UpdateArt();
                    }
                }
            }
        }
        else
        {
            //goNext = false;
        }


        for (int a = 0; a < checkedNodes.Count; a++)
        {
            checkedNodes[a].IsChecked = false;
        }
        return roadIsMade;
    }
    #endregion
}
/*using System.Collections;
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
}*/
