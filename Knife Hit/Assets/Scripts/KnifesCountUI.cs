using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifesCountUI : MonoBehaviour
{
    [SerializeField] private Transform _knifeContainer;

    private Stack<KnifeUI> _avalibleKnifes;
    private KnifeUI[] _knifes;
    private int _childCount;

    private void Start()
    {
        GetChilds();
    }

    private void GetChilds() 
    {
        _childCount = _knifeContainer.childCount;
        _knifes = new KnifeUI[_childCount];

        for(int i = 0; i < _childCount; i++) 
        {
            if (_knifeContainer.GetChild(i).TryGetComponent(out KnifeUI knife))
                _knifes[i] = knife;
        }
    }

    public void SetKnifeCount(int knifeCount) 
    {
        DisableAllKnifes();

        _avalibleKnifes = new Stack<KnifeUI>(knifeCount);

        for(int i = 1; i <= knifeCount; i++) 
        {
            KnifeUI knife = _knifes[_childCount - i];
            knife.EnableKnife();
            _avalibleKnifes.Push(knife);
        }
    }

    private void DisableAllKnifes() 
    {
        foreach(KnifeUI knife in _knifes) 
        {
            knife.DisableKnife();
        }
    }

    public void UpdateKnifeState() 
    {
        if (_avalibleKnifes.Count > 0)
            _avalibleKnifes.Pop().UpdateState();
    }
}
