using System.Collections;
using UnityEngine;
using System;

public class LevelManager : MonoBehaviour
{
    [SerializeField] LevelData[] _levels;
    [SerializeField] private Center _centerPrefab;
    [SerializeField] private Vector2 _centerPosition;
    [SerializeField] private KnifeSpawner _knifeSpawner;
    [SerializeField] private float _spawnPauseTime;
    [SerializeField] private Menu _menu;

    private Center _currentCenter;
    private LevelData _currentLevel;
    private int _levelKnifesAttachedCount = 0;

    public static LevelManager Instance;

    #region Singleton
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    #endregion

    private void Start()
    {
        GlobalEventManager.OnKnifeAttached += LevelWinCondition;
    }

    public void StartLevel() 
    {
        if (_currentCenter != null) 
            Destroy(_currentCenter.gameObject);

        _currentLevel = GetRandomLevel();

        _knifeSpawner.enabled = true;
        _knifeSpawner.SetKnifeCount(_currentLevel.KnifesCountToComplete);
        _knifeSpawner.SpawnKnife();

        _currentCenter = Instantiate(_centerPrefab, _centerPosition, Quaternion.identity);
        _currentCenter.SpawnStartKnifes(_currentLevel.StartKnifesCount, _currentLevel.KnifesCirclePartValue, _currentLevel.KnifesAngleOffset);
        _currentCenter.SpawnApples(_currentLevel.ApplesCount, _currentLevel.ApplesCirclePartValue, _currentLevel.ApplesAngleOffset);
    }

    private LevelData GetRandomLevel() 
    {
        int id = UnityEngine.Random.Range(0, _levels.Length);
        return _levels[id];
    }

    public void LevelWinCondition() 
    {
        _levelKnifesAttachedCount++;

        if (_levelKnifesAttachedCount >= _currentLevel.KnifesCountToComplete) LevelComplite();
    }

    private void LevelComplite() 
    {
        _levelKnifesAttachedCount = 0;
        _currentCenter.Drop();
        VibrationManager.VibratePeek();

        StartCoroutine(PauseTimer(() => StartLevel()));
    }

    public void LevelFailed() 
    {
        _levelKnifesAttachedCount = 0;
        _knifeSpawner.enabled = false;

        StartCoroutine(PauseTimer(() => EndGame()));
    }

    private void EndGame() 
    {
        _menu.EnableMenu();
    }

    IEnumerator PauseTimer(Action onComplite) 
    {
        yield return new WaitForSeconds(_spawnPauseTime);

        onComplite?.Invoke();
    }
}
