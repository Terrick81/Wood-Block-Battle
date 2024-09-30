using UnityEngine;

public class MoveCell : Cell
{
    private Vector2Int _position;

    public void Touch()
    {
        PieceCell.MoveSelected(_position);
    }

    override public void SetPosition(Vector2Int position)
    {
        if (position[0] == -1)
            Destroy(gameObject);
        UpdateSize();
        _position = position;
        base.SetPosition(position);
    }
}
