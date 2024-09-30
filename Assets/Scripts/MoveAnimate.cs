using UnityEngine;

public class MoveAnimate : MonoBehaviour
{
    private float timeElapsed = 0;
    private const float duration = 0.5f;
    private Vector2 _startPosition;
    private Vector2 _endPosition;
    private RectTransform _transform;
    public void Create(Vector2 startPosition, Vector2 endPosition, RectTransform transform)
    {
        _startPosition = startPosition;
        _endPosition = endPosition;
        _transform = transform;
    }

    void Update()
    {
        if (_transform != null)
        {
            _transform.localPosition = Vector2.Lerp(_startPosition, _endPosition, timeElapsed / duration);
            timeElapsed += Time.deltaTime;

            if ((Vector2)_transform.localPosition == _endPosition)
            {
                Destroy(this);
            }
        }
    }
}
