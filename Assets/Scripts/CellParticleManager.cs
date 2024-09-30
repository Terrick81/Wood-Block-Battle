using UnityEngine;

public class CellParticleManager : MonoBehaviour
{
    public static int size;

    private ParticleSystem _particleSystem;
    private RectTransform  _rectTransform;
    
    private void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        _rectTransform = GetComponent<RectTransform>();
    }

    

    public void Play(Vector3 position, Color color, int size)
    {
        _rectTransform.localPosition = position;
        Debug.Log(size);
        gameObject.transform.localScale = new Vector3( size / 100, size / 100, size / 100);

        var col = _particleSystem.colorOverLifetime;
        Gradient grad = new Gradient();
        grad.SetKeys(
            new GradientColorKey[] {
            new GradientColorKey(color, 0.0f),
            new GradientColorKey(color, 1.0f) },
            col.color.gradient.alphaKeys);
        col.color = grad;

        _particleSystem.Play();
    }


}
