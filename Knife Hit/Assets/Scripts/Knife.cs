using UnityEngine;

public class Knife : DropobleObject, IHitable
{
    [SerializeField] private float _speed;
    [SerializeField] private float _bounceForce;
    [SerializeField] private float _maxTorgue;
    [SerializeField] private float _minTorgue;

    public bool IsHit { get; set; } = false;
    public bool IsThrow { get; set; } = false;

    private Animator _animator;
    private BoxCollider2D _collider;

    public override void Awake()
    {
        base.Awake();
        _animator = GetComponent<Animator>();
        _collider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (IsHit == true || IsThrow == false) 
            return;

        RigidBody.MovePosition(RigidBody.position + _speed * Time.deltaTime * Vector2.up);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IHitable hit) && IsThrow == true) 
            hit.GetHit(this);
    }

    public void GetHit(Knife knife)
    {
        if (IsHit == true && knife.IsHit == false) 
        {
            Vector2 diraciton = (knife.transform.position - transform.position).normalized;
            knife.BounceBack(diraciton);

            LevelManager.Instance.LevelFailed();
            VibrationManager.VibratePeek();
        }
    }

    public void BounceBack(Vector2 diraction) 
    {
        IsThrow = false;
        _collider.enabled = false;

        int torgueDiraction = Random.Range(0, 100) < 50 ? -1 : 1;
        float torgue = Random.Range(_minTorgue, _maxTorgue) * torgueDiraction;

        RigidBody.bodyType = RigidbodyType2D.Dynamic;
        RigidBody.AddForce(diraction * _bounceForce, ForceMode2D.Impulse);
        RigidBody.AddTorque(torgue, ForceMode2D.Impulse);

        Destroy(gameObject, 2f);
    }

    public override void Drop(Vector3 position, float explosionForce)
    {
        _collider.enabled = false;
        base.Drop(position, explosionForce);
    }

    public void PlayStartAnimation() 
    {
        _animator.SetTrigger("Appear");
    }
}
