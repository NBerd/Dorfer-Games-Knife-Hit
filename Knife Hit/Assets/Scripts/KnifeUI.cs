using UnityEngine;
using UnityEngine.UI;

public class KnifeUI : MonoBehaviour
{
    [SerializeField] private Image _knifeImage;

    public void EnableKnife() 
    {
        _knifeImage.enabled = true;
        gameObject.SetActive(true);
    }

    public void DisableKnife() 
    {
        gameObject.SetActive(false);
    }

    public void UpdateState() 
    {
        _knifeImage.enabled = false;
    }
}
