using UnityEngine;

public class Center : MonoBehaviour, IHitable
{
    [SerializeField] private float _minExplosionForce;
    [SerializeField] private float _maxExplosionForce;
    [SerializeField] private GameObject _woodPartsPrefab;
    [SerializeField] private GameObject _woodParticle;
    [SerializeField] private Transform _container;
    [SerializeField] private Knife _knifePrefab;
    [SerializeField] private Apple _applePrefab;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void GetHit(Knife knife)
    {
        knife.IsHit = true;
        AttachToCenter(knife.transform, -1);

        if (knife.IsThrow)
        {
            GlobalEventManager.KnifeAttached();
            VibrationManager.VibratePop();
            _animator.SetTrigger("Hit");
            GameObject woodParticle = Instantiate(_woodParticle, Vector2.zero, Quaternion.identity);
            Destroy(woodParticle, 1f);
        }
    }

    private void AttachToCenter(Transform targetTransform, int diractionMultiplier = 1) 
    {
        Vector3 diraction = (targetTransform.position - transform.position).normalized;

        targetTransform.position = transform.position + diraction;
        targetTransform.up = diraction * diractionMultiplier;
        targetTransform.SetParent(_container);
    }

    public void SpawnStartKnifes(int knifesCount, float circlePartValue, int angleOffset) 
    {
        Vector3 startPosition = transform.position;

        for (int i = 0; i < knifesCount; i++) 
        {
            float angle = (circlePartValue / knifesCount * i) + angleOffset * Mathf.Deg2Rad;

            Vector3 diraction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle));
            Knife knife = Instantiate(_knifePrefab, startPosition + diraction, Quaternion.identity);
            knife.IsHit = true;
            AttachToCenter(knife.transform, -1);
        }
    }

    public void SpawnApples(int applesCount, float circlePartValue, int angleOffset) 
    {
        Vector3 startPosition = transform.position;

        for (int i = 0; i < applesCount; i++) 
        {
            float angle = (circlePartValue / applesCount * i) + angleOffset * Mathf.Deg2Rad;

            Vector3 diracton = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle));
            Apple apple = Instantiate(_applePrefab, startPosition + diracton, Quaternion.identity);

            AttachToCenter(apple.transform);
        }
    }

    public void Drop() 
    {
        Vector3 position = transform.position;

        foreach(Transform child in _container.transform) 
        {
            if (child.TryGetComponent(out DropobleObject obj)) 
            {
                float explosionForce = Random.Range(_minExplosionForce, _maxExplosionForce);
                child.SetParent(null);
                obj.Drop(position, explosionForce);
                Destroy(obj.gameObject, 2f);
            }
        }

        Destroy(gameObject);
        GameObject woodParts = Instantiate(_woodPartsPrefab, position, Quaternion.identity);
        Destroy(woodParts, 2f);

        foreach(Transform child in woodParts.transform) 
        {
            if (child.TryGetComponent(out DropobleObject obj))
            {
                float explosionForce = Random.Range(_minExplosionForce, _maxExplosionForce);
                obj.Drop(position, explosionForce);
            }
        }
    }
}