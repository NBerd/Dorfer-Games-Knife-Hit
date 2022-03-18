using UnityEngine;

public class DropobleObject : MonoBehaviour
{
    [SerializeField] protected float MinDropTorue;
    [SerializeField] protected float MaxDropTorgue;

    protected Rigidbody2D RigidBody;

    public virtual void Awake()
    {
        RigidBody = GetComponent<Rigidbody2D>();
    }

    public virtual void Drop(Vector3 position, float explosionForce) 
    {
        RigidBody.bodyType = RigidbodyType2D.Dynamic;

        int torgueDiraction = Random.Range(0, 100) < 50 ? -1 : 1;
        float randomTorgue = Random.Range(MinDropTorue, MaxDropTorgue) * torgueDiraction;

        Vector2 diraction = (transform.position - position).normalized;

        RigidBody.AddTorque(randomTorgue, ForceMode2D.Impulse);
        RigidBody.AddForceAtPosition(diraction * explosionForce, position, ForceMode2D.Impulse);
    }
}
