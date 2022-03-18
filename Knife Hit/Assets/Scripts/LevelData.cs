using UnityEngine;

[CreateAssetMenu(fileName = "new Level", menuName = "Level")]
public class LevelData : ScriptableObject
{
    [Range(0, 15)] [SerializeField] private int _knifesCountToComplete;
    [SerializeField] private int _minStartKnifesCount;
    [SerializeField] private int _maxStartKnifesCount;
    [SerializeField] private int _knifesAngleOffset;
    [SerializeField] private CirclePart _knifesCirclePart;
    [SerializeField] private int _applesCount;
    [SerializeField] private int _applesChance;
    [SerializeField] private int _applesAngleOffset;
    [SerializeField] private CirclePart _applesCirclePart;

    public int KnifesCountToComplete => _knifesCountToComplete;
    public int KnifesAngleOffset => _knifesAngleOffset;
    public int StartKnifesCount { get { return Random.Range(_minStartKnifesCount, _maxStartKnifesCount); } }
    public float KnifesCirclePartValue { get { return CircleMathf.CirclePartValue[_knifesCirclePart]; } }
    public int ApplesCount { get { return Random.Range(0, 100) <= _applesChance ? _applesCount : 0; } }
    public float ApplesCirclePartValue { get { return CircleMathf.CirclePartValue[_applesCirclePart]; } }
    public int ApplesAngleOffset => _applesAngleOffset;
}
