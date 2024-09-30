using UnityEngine;



// Отвечает за логику игры и изменения карты
public class GameManager : MonoBehaviour
{
    #region Fields
    public const byte MAX_COUNT_PL = 3;
    public const byte FIRST_PL = 2;
    private static GameManager _gameManager;
    public static bool volumeOn = false;
    
    
    public static byte[,] map;
    public static int countPlayers;

    private static MyColor[] colors = new MyColor[MAX_COUNT_PL]
{
        new MyColor(-1, UnityEngine.Color.white),
        new MyColor(-1, UnityEngine.Color.blue),
        new MyColor(-1, UnityEngine.Color.green)
};
    
    public static MyColor[] colorPlayers = new MyColor[MAX_COUNT_PL]
    {
            colors[0], colors[1], colors[2]
    };




    public static GameObject[,] gameObjectMap;
    
    public static Vector2Int mapSize;

    private GameObject _gridField;
    private GameObject _gridPieces;
    
    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private GameObject _backgroundCell;
    [SerializeField] private GameObject _сell;

    #endregion

    void Awake()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _gridField = GameObject.Find("Field");
        _gridPieces = GameObject.Find("Pieces");
        Cell.MyAwake();
        StartLevel(0);
    }


    private void StartLevel(int number)
    {
        map = Levels.levels[number].map;
        countPlayers = Levels.levels[number].countPlayers;
        UpdateMapSize();
        GenerateField(map);
        GeneratePieces(map);
        _scoreManager.UpdateCountPlayer(countPlayers);
        _scoreManager.UpdateScore(ScoreCellCounter());
    }


    private static int[] ScoreCellCounter()
    {
        int[] quantityCell = new int[countPlayers];

        foreach (GameObject cell in gameObjectMap)
        {
            if ( cell != null) 
            {
                int s = cell.GetComponent<PieceCell>().GetNumberPlayer();
                quantityCell[s]++;
            }
            
        }
        return quantityCell;
    }


    public static Vector2Int[] GetMovementCoord(Vector2Int _position)
    {
        return MovementLogic.GetMovementCoord(_position, map); 
    }
    
    public void GenerateField(byte[,] fieldPlan)
    {
        Vector2 sizeGrid = _gridField.GetComponent<RectTransform>().sizeDelta;
        int sizeCellX = (int)(sizeGrid[0] / mapSize[1]);
        int sizeCellY = (int)(sizeGrid[1] / mapSize[0]);
        int _sizeCell = System.Math.Min(sizeCellX, sizeCellY);
        Cell.SetSizeCell(_sizeCell);

        for (int i = 0; i < mapSize[0]; i++)
        {
            for (int j = 0; j < mapSize[1]; j++)
            {
                if ((TypeCell)fieldPlan[i, j] != TypeCell.none)
                {
                    GameObject cell = Instantiate(_backgroundCell, _gridField.transform);
                    
                    cell.GetComponent<Cell>().SetPosition(new Vector2Int(i,j));
                }
            }
        }
    }

    public void GeneratePieces(byte[,] fieldPlan)
    {
        Debug.Log("CreateFunction");
        UpdateMapSize();
        gameObjectMap = new GameObject[mapSize[0], mapSize[1]];
        for (int i = 0; i < _gridPieces.transform.childCount; i++)
            Destroy(_gridPieces.transform.GetChild(i));
        
        for (int i = 0; i < mapSize[0]; i++)
        {
            for (int j = 0; j < mapSize[1]; j++)
            {
                if ((TypeCell)fieldPlan[i, j] != TypeCell.none )
                {
                    if ((TypeCell)fieldPlan[i, j] != TypeCell.empty)
                    {
                        GameObject cell = Instantiate(_сell, _gridPieces.transform);
                        Debug.Log("Create");
                        cell.GetComponent<PieceCell>()
                            .Create(new Vector2Int(i,j), type: (TypeCell)fieldPlan[i, j]);
                        gameObjectMap[i, j] = cell;
                    }
                }
            }
        }
        map = MovementLogic.UpdateAll(map);
    }


    private void UpdateMapSize()
    {
        mapSize = new Vector2Int(map.GetLength(0), map.GetLength(1));
    }

    public static void CellUpdate(Vector2Int coord, TypeCell type)
    {
        if (gameObjectMap[coord[0], coord[1]] != null)
            gameObjectMap[coord[0], coord[1]].GetComponent<PieceCell>().SetTypeAnimate(type);
        else
            _gameManager.CreateCell(coord, type);
    }

    public void CreateCell(Vector2Int coord, TypeCell type)
    {
        GameObject cell = Instantiate(_сell, _gridPieces.transform);
        cell.GetComponent<PieceCell>()
            .Create(new Vector2Int(coord[0], coord[1]), type: type);
        gameObjectMap[coord[0], coord[1]] = cell;
    }

    public static void DestroyUpdate(Vector2Int coord, GameObject cell)
    {
        gameObjectMap[coord[0], coord[1]] = null;
        map[coord[0], coord[1]] =  (byte)TypeCell.empty;
    }

    public static void MoveUpdate(Vector2Int coord, Vector2Int movement, TypeCell type)
    {
        gameObjectMap[movement[0], movement[1]] = gameObjectMap[coord[0], coord[1]];
        gameObjectMap[coord[0], coord[1]] = null;
        
        map[movement[0], movement[1]] = (byte)type;
        map[coord[0], coord[1]] = (byte)TypeCell.empty;
        //Debug.Log((TypeCell)map[movement[0], movement[1]] + " " + (TypeCell)map[coord[0], coord[1]]);
        map = MovementLogic.UpdateMap(map, coord, movement);
    }

    /* 
        for (int i = 0; i < MovementLogic.mapSize[0]; i++)
        {
            for (int j = 0; j < MovementLogic.mapSize[1]; j++)
            {
                Debug.Log(_map[i, j] + " ");
            }
            Debug.Log("\n");
        }
    */

}
