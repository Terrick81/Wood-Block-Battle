using System.Runtime.ConstrainedExecution;
using Unity.VisualScripting;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class PieceCell : Cell
{
    #region Fields
    private static GameObject _selected;
    
    private Vector2Int _position;
    [SerializeField] private TypeCell _type;
    [SerializeField] private bool _lock;

    #endregion


    public void SetTypeAnimate(TypeCell type)
    {
        _particleManager.Play(GetCoord(_position), GetComponent<Image>().color, _sizeCell);
        PlayAnimationClip(Animations.Create);

        SetType(type);
    }

    public void SetType(TypeCell type)
    {
        int clr = 0;
        bool @lock = false;
        _type = type;


        if (type == TypeCell.empty)
        {
            Destroy(gameObject);
        }
        if ((byte)type >= GameManager.FIRST_PL && (byte)type < GameManager.FIRST_PL + GameManager.MAX_COUNT_PL)
        {
            clr = (byte)type - GameManager.FIRST_PL;
            @lock = false;
        }
        else if ((byte)type >= GameManager.FIRST_PL + GameManager.MAX_COUNT_PL
            && (byte)type < GameManager.FIRST_PL + GameManager.MAX_COUNT_PL * 2)
        {
            clr = (byte)type - GameManager.FIRST_PL - GameManager.MAX_COUNT_PL;
            @lock = true;
        }
        SetLock(@lock);
        GetComponent<Image>().color = GameManager.colorPlayers[clr].color;
    }

    public void SetLock(bool localLock)
    {
        if (localLock)
        {
            _lock = true;
            transform.GetChild(0).gameObject.SetActive(true);
            if ((byte)_type < GameManager.FIRST_PL + GameManager.MAX_COUNT_PL)
                _type = (TypeCell)((byte)_type + GameManager.MAX_COUNT_PL);
        }
        else
        {
            _lock = false;
            transform.GetChild(0).gameObject.SetActive(false);
            if ((byte)_type > GameManager.FIRST_PL + GameManager.MAX_COUNT_PL)
                _type = (TypeCell)((byte)_type - GameManager.MAX_COUNT_PL);
        }
    }

    public byte GetNumberPlayer()
    {
        if ((byte)_type >= GameManager.FIRST_PL && (byte)_type < GameManager.FIRST_PL + GameManager.MAX_COUNT_PL)
            return (byte)(_type - GameManager.FIRST_PL);
        else 
            return (byte)(_type - GameManager.FIRST_PL - GameManager.MAX_COUNT_PL);
    }



    override protected void UpdateSize()
    {
        base.UpdateSize();
        RectTransform rt2 = transform.GetChild(0).gameObject.GetComponent<RectTransform>();
        rt2.sizeDelta = new Vector2(_sizeCell, _sizeCell);
    }

    public void Create(Vector2Int position, TypeCell type)
    {
        PlayAnimationClip(Animations.Create);
        SetPosition(position);
        UpdateSize();
        SetType(type);
    }


    private void PlayAnimationClip(Animations type)
    {
        Animation animation = GetComponent<Animation>();
        animation.clip = _animations[(int)type];
        animation.Play();
    }



    public void Touch()
    {
        if (_lock)
            return;
        
        if (_selected == gameObject)
        {
            //убрать возможные ходы
            ClearMovement();
            _selected = null;
        }
        else
        {
            if (_selected != null)
            {
                //убрать возможные ходы предыдушего селекта
                ClearMovement();
            }
            _selected = gameObject;
            CreateMoveCell();
        }
    }

    private void ClearMovement()
    {
        for (int i = 0; i < _moveCellGrid.transform.childCount; i++)
            Destroy(_moveCellGrid.transform.GetChild(i).gameObject);
    }


    public void CreateMoveCell()
    {
        Vector2Int[] mcCoord = GameManager.GetMovementCoord(_position);
        foreach (Vector2Int coord in mcCoord)
        {
            GameObject mc = Instantiate(_moveCellPrefab, _moveCellGrid.transform);
            //Debug.Log(coord);
            mc.GetComponent<MoveCell>().SetPosition(coord);
        }
    }

    public override void SetPosition(Vector2Int position)
    {
        _position = position;
        base.SetPosition(_position);
    }

    public static void MoveSelected(Vector2Int movement)
    {
        _selected.GetComponent<PieceCell>().Move(movement);
    }

    private void Move(Vector2Int movement)
    {
        //ѕеремещение €чейки
        GameManager.MoveUpdate(_position, movement, _type);
        ClearMovement();
        RectTransform rt = GetComponent<RectTransform>();
        gameObject.transform.SetSiblingIndex(transform.parent.childCount);
        gameObject.AddComponent<MoveAnimate>();
        PlayAnimationClip(Animations.Move);
        gameObject.GetComponent<MoveAnimate>().Create(GetCoord(_position), GetCoord(movement), rt);
        _position = movement;
        _selected = null;
    }

    public void Destroy()
    {
        GameManager.DestroyUpdate(_position, transform.gameObject);
        Invoke("Dst", 1);
    }


}   
