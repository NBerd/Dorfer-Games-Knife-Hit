using UnityEngine;

public class Apple : DropobleObject, IHitable
{
    private BoxCollider2D _collider;

    public override void Awake()
    {
        base.Awake();
        _collider = GetComponent<BoxCollider2D>();
    }

    public void GetHit(Knife knife)
    {
        GlobalEventManager.AppleHit();
        Destroy(gameObject);
    }

    public override void Drop(Vector3 position, float explosionForce)
    {
        _collider.enabled = false;
        base.Drop(position, explosionForce);
    }
}
