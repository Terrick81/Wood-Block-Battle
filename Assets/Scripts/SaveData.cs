using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class SaveData : MonoBehaviour
{
    private const float _AUTOSAVE_INTERVAL = 60f;

    void Start()
    {
        if (YandexGame.SDKEnabled)
        {
            LoadData();
            StartCoroutine(AutoSave());
        }
    }

    private void SaveDataFunc()
    {
        YandexGame.savesData.volumeOn = GameManager.volumeOn;
    }

    private void LoadData()
    {
        GameManager.volumeOn = YandexGame.savesData.volumeOn;
    }


    IEnumerator AutoSave()
    {
        while (true)
        {
            SaveDataFunc();
            yield return new WaitForSeconds(_AUTOSAVE_INTERVAL);
        }
    }
}
