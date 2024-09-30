using UnityEngine;

public class ButtonVisial : MonoBehaviour
{
    Animation _animation;
    AudioSource _audioSource;
    void Start()
    {
        _animation = GetComponent<Animation>();
        _audioSource = GetComponent<AudioSource>();
    }

    public void Play()
    {
        _animation.Play();
        _audioSource.Play();
    }
}
