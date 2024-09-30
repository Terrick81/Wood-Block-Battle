using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Slider _slider2;

    [SerializeField] private Image _sliderImage;
    [SerializeField] private Image _sliderImage2;

    [SerializeField] private GameObject _twoPlayerScore;
    [SerializeField] private GameObject _manyPlayerScore;
    [SerializeField] private RectTransform[] _fillsRT;
    [SerializeField] private TextMeshProUGUI _textMeshPro;
    [SerializeField] private GameObject _winPage;
    
    private int _countPlayer;



    public void UpdateCountPlayer(int countPlayers)
    {
        _countPlayer = countPlayers;
        if (_countPlayer > 2 || _countPlayer < 2)
        {
            _manyPlayerScore.SetActive(true);
            _twoPlayerScore.SetActive(false);
            /*
            if (_manyPlayerScore.transform.childCount > _fills.Length)
            {
                foreach (GameObject obj in _fills)
                {
                     
                } 
            }
            */
        }
        else if (_countPlayer == 2)
        {
            _manyPlayerScore.SetActive(false);
            _twoPlayerScore.SetActive(true);

            _sliderImage.color = GameManager.colorPlayers[0].color;
            _sliderImage2.color = GameManager.colorPlayers[2].color;

            _slider.value = 0; _slider2.value = 0;
        }
    }


    public void UpdateScore(int[] scorePlayers)
    {
        if (scorePlayers.Length == _countPlayer)
        {
            if (_countPlayer == 2)
            {
                _slider.value = scorePlayers[0];
                _slider2.value = scorePlayers[1];
            }
        }
        string score = "";
        foreach(int s in scorePlayers)
        {
            score += (s + "/");
        }
        score = score.TrimEnd('/');
        _textMeshPro.text = score;
    }
}
