using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed;

    private void Start()
    {
        SetStartRotation();
    }

    private void SetStartRotation() 
    {
        Quaternion randomRotation = Random.rotation;
        randomRotation.x = 0;
        randomRotation.y = 0;
        transform.rotation = randomRotation;
    }

    private void Update()
    {
        transform.Rotate(0, 0, _rotateSpeed * Time.deltaTime);
    }
}
