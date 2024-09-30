using UnityEngine;

public class Cell : MonoBehaviour
{
    #region Fields

    protected const int MAX_SIZE = 300;
    protected const int MIN_SIZE = 40;

    protected static Vector2 _sizeGrid;
    protected static int _sizeCell = 10;
    protected static GameObject _moveCellPrefab;
    protected static GameObject _moveCellGrid;
    protected static AnimationClip[] _animations;
    protected static CellParticleManager _particleManager;

    #endregion


    public static void SetSizeCell(int sizeCell)
    {
        if (sizeCell > MAX_SIZE)
            _sizeCell = MAX_SIZE;
        else if (sizeCell < MIN_SIZE)
            _sizeCell = MIN_SIZE;
        else
            _sizeCell = sizeCell;
    }

    virtual public void SetPosition(Vector2Int position)
    {
        RectTransform rt = GetComponent<RectTransform>();
        rt.localPosition = GetCoord(position);
    }

    virtual public Vector3 GetCoord(Vector2Int position)
    {
        return new Vector2(
            _sizeCell * position[1] - _sizeGrid[0] / 2 + _sizeCell / 2,
            -_sizeCell * position[0] + _sizeGrid[1] / 2 - _sizeCell / 2);
    }

    virtual protected void UpdateSize()
    {
        RectTransform rt = GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(_sizeCell, _sizeCell);
    }

    public static void MyAwake()
    {
        Storage storage = GameObject.Find("GameManager").GetComponent<Storage>();
        _sizeGrid = GameObject.Find("Pieces").GetComponent<RectTransform>().sizeDelta;
        _moveCellPrefab = storage.moveCellPrefab;
        _moveCellGrid = GameObject.Find("MoveCells");
        _particleManager = GameObject.Find("CellParticleSystem").GetComponent<CellParticleManager>() ;
        _animations = storage.cellAnimations;
    }


    protected void Dst()
    {
        Destroy(gameObject);
    }
}



