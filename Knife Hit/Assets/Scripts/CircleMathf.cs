using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CircleMathf
{
    private const float _whoreCircleValue = 2 * Mathf.PI;
    private const float _halfCircle = Mathf.PI;
    private const float _oneFourthCircle = Mathf.PI / 2;

    public static Dictionary<CirclePart, float> CirclePartValue = new Dictionary<CirclePart, float>
    {
        { CirclePart.WhoreCircle, _whoreCircleValue },
        { CirclePart.HalfCircle, _halfCircle },
        { CirclePart.OneFourthCircle, _oneFourthCircle }
    };
}

public enum CirclePart
{
    WhoreCircle,
    HalfCircle,
    OneFourthCircle
}
