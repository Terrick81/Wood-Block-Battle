using UnityEngine;

public class BackgroundCell : Cell
{
    override public void SetPosition(Vector2Int position)
    {
        base.SetPosition(position);
        UpdateSize();
        Destroy(this);
    }
}
