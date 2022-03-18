using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] private SaveSystem _saveSystem;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _appleText;

    private int _score = 0;
    private int _highScore = 0;
    private int _appleCount;

    private void Start()
    {
        GlobalEventManager.OnKnifeAttached += UpdateScore;
        GlobalEventManager.OnAppleHit += UpdateAppleCount;

        SaveData data = _saveSystem.LoadData();

        if (data != null) 
        {
            _highScore = data.HighScore;
            _appleCount = data.AppleCount;

            _appleText.text = _appleCount.ToString();
        }
    }

    private void UpdateScore() 
    {
        _score++;
        _scoreText.text = _score.ToString();

        if (_score > _highScore) 
            _highScore = _score;
    }

    private void UpdateAppleCount() 
    {
        _appleCount++;
        _appleText.text = _appleCount.ToString();
    }

    public void ResetScore() 
    {
        _score = 0;
        _scoreText.text = _score.ToString();
    }

    public int GetHighScore() 
    {
        return _highScore;
    }

    private void OnApplicationPause(bool pause) { if (pause == true) SetData(); }

    private void OnApplicationQuit() { SetData(); }

    private void SetData() 
    {
        SaveData data = new SaveData
        {
            HighScore = _highScore,
            AppleCount = _appleCount
        };

        _saveSystem.SaveData(data);
    }
}
