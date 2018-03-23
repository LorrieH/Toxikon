using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TileGrid : MonoBehaviour
{

    public static TileGrid s_Instance;

    [SerializeField] private int m_GridXSize = 10;
    [SerializeField] private int m_GridYSize = 10;

    [SerializeField] private GameObject m_NodeObjectPrefab;

    private TileNode[,] m_NodeGrid;
    private Bridges[,] m_NodeBridges;

    private float TileSpawnOffsetY = 0.2f;

    private TileNode[] m_PlayerStartNodes;
    public List<TileNode> NodeRoad { get; set; }

    public List<TileNode> CheckedNodes { get; set; }
    public bool RoadCompleted { get; set; }

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
        SetNeighboursOfNodes();
    }
    #endregion

    #region void functions
    public void InstantiateNewGrid()
    {
        m_PlayerStartNodes = new TileNode[PlayersManager.s_Instance.Players.Count];
        m_NodeBridges = new Bridges[m_GridXSize + 2, m_GridYSize + 2];
        m_NodeGrid = new TileNode[m_GridXSize + 2, m_GridYSize + 2];
        for (int i = 0; i < m_GridXSize + 2; i++)
        {
            for (int j = 0; j < m_GridYSize + 2; j++)
            {
                TileNode newNode = new TileNode();
                Bridges newbridge = new Bridges();
                TileBools bools = new TileBools();
                newNode.IsFilled = false;
                Debug.LogError("debug modus is on");
                newNode.DebugModus = true;
                newNode.IsDestructable = true;
                if (i == 0 || i == m_GridXSize + 1 || j == 0 || j == m_GridYSize + 1)
                {
                    newNode.IsEdgeStone = true;
                    newNode.IsDestructable = false;
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
                
                if (i == 1 && j == m_GridYSize)
                {
                    newNode.IsFilled = true;
                    newNode.IsDestructable = false;
                    bools.Right = true;
                    bools.Down = true;
                    bools.Middle = true;
                    bools.Middle = false;
                    newNode.IsStartPoint = true;
                    m_PlayerStartNodes[0] = newNode;
                }
                if (i == m_GridXSize && j == 1)
                {
                    newNode.IsFilled = true;
                    newNode.IsDestructable = false;
                    bools.Left = true;
                    bools.Up = true;
                    bools.Middle = false;
                    newNode.IsFilled = true;
                    newNode.IsStartPoint = true;
                    m_PlayerStartNodes[1] = newNode;
                }
                if (i == 1 && j == 1 && PlayersManager.s_Instance.Players.Count >= 3)
                {
                    newNode.IsFilled = true;
                    newNode.IsDestructable = false;
                    bools.Right = true;
                    bools.Up = true;
                    bools.Middle = false;
                    newNode.IsFilled = true;
                    newNode.IsStartPoint = true;
                    m_PlayerStartNodes[2] = newNode;
                }
                if (i == m_GridXSize && j == m_GridYSize && PlayersManager.s_Instance.Players.Count >= 4)
                {
                    newNode.IsFilled = true;
                    newNode.IsDestructable = false;
                    bools.Left = true;
                    bools.Down = true;
                    bools.Middle = false;
                    newNode.IsFilled = true;
                    newNode.IsStartPoint = true;
                    m_PlayerStartNodes[3] = newNode;
                }
                else if (i == (m_GridXSize+1)/2 && j == (m_GridYSize+1)/2)
                {
                    newNode.IsFilled = true;
                    newNode.IsDestructable = false;
                    bools.Left = true;
                    bools.Up = true;
                    bools.Middle = false;
                    bools.Down = true;
                    bools.Right = true;
                    newNode.IsFilled = true;
                    newNode.IsEndpoint = true;
                }
                newNode.Bools = bools;
                newNode.UpdateArt();
                m_NodeGrid[i, j] = newNode;
                m_NodeBridges[i,j] = newbridge;
            }
        }
    }

    public void SetAccesableNeighboursOfNodes()
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

    public void SetNeighboursOfNodes()
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
    public bool CanMoveNode(int xMovingNode, int yMovingNode, int xTargetNode, int yTargetNode)
    {
        TileNode MovingNode = m_NodeGrid[xMovingNode, yMovingNode];
        if (PlaceNewCard(xTargetNode, yTargetNode, MovingNode.Bools.Up, MovingNode.Bools.Right, MovingNode.Bools.Down, MovingNode.Bools.Left, MovingNode.Bools.Middle))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool MoveNode(int xMovingNode, int yMovingNode, int xTargetNode, int yTargetNode, float delay = 0)
    {
        TileNode MovingNode = m_NodeGrid[xMovingNode, yMovingNode];
        if (PlaceNewCard(xTargetNode, yTargetNode, MovingNode.Bools.Up, MovingNode.Bools.Right, MovingNode.Bools.Down, MovingNode.Bools.Left, MovingNode.Bools.Middle))
        {
            DestroyNode(xMovingNode, yMovingNode, delay);
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsDestroyable(int x, int y)
    {
        if (!m_NodeGrid[x, y].IsEdgeStone && m_NodeGrid[x, y].IsFilled && m_NodeGrid[x, y].IsDestructable)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool DestroyNode(int x, int y, float delay = 0)
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

            if(delay>0)
            {
                StartCoroutine(UpdateArt(m_NodeGrid[x, y], delay));
            }
            else
            {
                m_NodeGrid[x, y].UpdateArt();
            }
            StartCoroutine(DestroyBridges(m_NodeBridges[x, y], delay));
            return true;
        }
        else
        {
            return false;
        }
    }
    IEnumerator UpdateArt(TileNode T, float delay)
    {
        yield return new WaitForSeconds(delay);
        T.UpdateArt();
    }
    IEnumerator DestroyBridges(Bridges B, float delay)
    {
        yield return new WaitForSeconds(delay);
        if(B.bridgeUp != null)
        {
            Destroy(B.bridgeUp);
        }
        if (B.bridgeRight != null)
        {
            Destroy(B.bridgeRight);
        }
        if (B.bridgeDown != null)
        {
            Destroy(B.bridgeDown);
        }
        if (B.bridgeLeft != null)
        {
            Destroy(B.bridgeLeft);
        }
    }

    public bool PlaceNewCard(int x, int y, bool UpCard, bool RightCard, bool DownCard, bool LeftCard, bool MiddleCard, float delay = 0)
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
            m_NodeGrid[x, y].TileObject.transform.position = new Vector3(x, y+TileSpawnOffsetY, 0);
            m_NodeGrid[x, y].TileObject.transform.DOMoveY(y, 0.4f);
            if (delay > 0)
            {
                StartCoroutine(UpdateArt(m_NodeGrid[x, y], delay));
            }
            else
            {
                m_NodeGrid[x, y].UpdateArt();
            }
            ;
            if (UpCard && m_NodeGrid[x, y].AllNeighbours.up.IsFilled)
            {
                GameObject Bridge = Instantiate(TileArtLib.s_Bridges[0], new Vector3(x , y+ TileSpawnOffsetY + 0.5f, 0), Quaternion.identity, m_NodeGrid[x,y].TileObject.transform);
                m_NodeBridges[x, y].bridgeUp = Bridge;
                m_NodeBridges[x, y+1].bridgeDown = Bridge;
            }
            if (RightCard && m_NodeGrid[x, y].AllNeighbours.right.IsFilled)
            {
                GameObject Bridge = Instantiate(TileArtLib.s_Bridges[1], new Vector3(x +0.5f, y+ TileSpawnOffsetY, 0), Quaternion.identity, m_NodeGrid[x, y].TileObject.transform);
                m_NodeBridges[x, y].bridgeRight = Bridge;
                m_NodeBridges[x+1, y].bridgeLeft = Bridge;
            }
            if (DownCard && m_NodeGrid[x, y].AllNeighbours.down.IsFilled)
            {
                GameObject Bridge = Instantiate(TileArtLib.s_Bridges[0], new Vector3(x , y+ TileSpawnOffsetY - 0.5f, 0), Quaternion.identity, m_NodeGrid[x, y].TileObject.transform);
                m_NodeBridges[x, y].bridgeDown = Bridge;
                m_NodeBridges[x, y - 1].bridgeUp = Bridge;
            }
            if (LeftCard && m_NodeGrid[x, y].AllNeighbours.left.IsFilled)
            {
                GameObject Bridge = Instantiate(TileArtLib.s_Bridges[1], new Vector3(x - 0.5f, y+ TileSpawnOffsetY, 0), Quaternion.identity, m_NodeGrid[x, y].TileObject.transform);
                m_NodeBridges[x, y].bridgeLeft = Bridge;
                m_NodeBridges[x - 1, y].bridgeRight = Bridge;
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
        NodeRoad = new List<TileNode>();
        CheckedNodes = new List<TileNode>();
        m_PlayerStartNodes[startPlayer].GetChecked(this, null);
        
        for (int i = 0; i < CheckedNodes.Count; i++)
        {
            CheckedNodes[i].IsChecked = false;
            CheckedNodes[i].UpdateArt();
        }

        return RoadCompleted;
    }
    #endregion
}

public struct Bridges
{
    public GameObject bridgeUp;
    public GameObject bridgeRight;
    public GameObject bridgeDown;
    public GameObject bridgeLeft;
}