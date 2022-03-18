using UnityEngine;

public class KnifeSpawner : MonoBehaviour
{
    [SerializeField] private Knife _knifePrefab;
    [SerializeField] private KnifesCountUI _knifeCountUI;

    private Knife _currentKnife;
    private int _knifeCount;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
                ThrowKnife();
        }
    }

    public void SetKnifeCount(int knifeCount) 
    {
        if (_currentKnife != null) 
            Destroy(_currentKnife.gameObject);

        _knifeCount = knifeCount;
        _knifeCountUI.SetKnifeCount(_knifeCount);
    }

    private void ThrowKnife() 
    {
        _knifeCount--;
        _knifeCountUI.UpdateKnifeState();
        _currentKnife.IsThrow = true;
        SpawnKnife();
    }

    public void SpawnKnife() 
    {
        if (_knifeCount <= 0) 
            return;

        _currentKnife = Instantiate(_knifePrefab, transform.position, Quaternion.identity);
        _currentKnife.PlayStartAnimation();
    }
}
