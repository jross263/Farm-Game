using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;
    
    private void Awake()
    {
        current = this;
    }

    public event Action<Vector3, float> OnCameraFocus;
    public void CameraFocus(Vector3 position, float offset)
    {
        OnCameraFocus?.Invoke(position, offset);
    }
}
