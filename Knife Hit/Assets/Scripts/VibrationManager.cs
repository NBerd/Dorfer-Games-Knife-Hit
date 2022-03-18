using UnityEngine;

public class VibrationManager : MonoBehaviour
{
    private void OnEnable()
    {
        Vibration.Init();
    }

    private void OnDisable()
    {
        Vibration.Cancel();
    }

    public static void VibratePop() 
    {
        Vibration.VibratePop();
    }

    public static void VibratePeek() 
    {
        Vibration.VibratePeek();
    }
}
