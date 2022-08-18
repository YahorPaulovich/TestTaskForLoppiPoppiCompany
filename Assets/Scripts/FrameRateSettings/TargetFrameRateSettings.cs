using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFrameRateSettings : MonoBehaviour
{
    private void Start()
    {
#if UNITY_EDITOR
        Application.targetFrameRate = -1;
#elif UNITY_ANDROID
        Application.targetFrameRate = 60;
#elif UNITY_IOS
    Application.targetFrameRate = 60;
#endif
    }
}
