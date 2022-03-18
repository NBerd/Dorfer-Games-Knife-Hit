using UnityEngine;
using TMPro;

public class Menu : MonoBehaviour
{
    [SerializeField] private Score _score;
    [SerializeField] private LevelManager _levelManager;
    [SerializeField] private TMP_Text _highScoreText;
    [SerializeField] private TMP_Text _scoreText;

    private void Start()
    {
        EnableMenu();
    }

    public void EnableMenu() 
    {
        _highScoreText.text = _score.GetHighScore().ToString();
        _scoreText.enabled = false;
        gameObject.SetActive(true);
    }

    public void StartGame() 
    {
        DisableMenu();
        _levelManager.StartLevel();
    }

    private void DisableMenu() 
    {
        _scoreText.enabled = true;
        _score.ResetScore();
        gameObject.SetActive(false);
    }
}
