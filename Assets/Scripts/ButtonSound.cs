using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class ButtonSound : MonoBehaviour
{
    
    [SerializeField] Sprite _soundOff;
    [SerializeField] Sprite _soundOn;

    private void Start()
    {
        UpdateVolime();
    }
    public void SwithVolime()
    {
        GameManager.volumeOn = !GameManager.volumeOn;
        UpdateVolime();
    }

    private void UpdateVolime()
    {
        if (GameManager.volumeOn)
        {
            AudioListener.volume = 1.0f;
            GetComponent<Image>().sprite = _soundOn;
        }
        else
        {
            AudioListener.volume = 0.0f;
            GetComponent<Image>().sprite = _soundOff;
        }
    }
}
