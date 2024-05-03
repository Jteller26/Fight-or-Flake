// This script adjusts the camera's viewport height to maintain a consistent aspect ratio.
// It ensures that the game is displayed correctly across different screen resolutions and devices.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AspectRatioManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float screenAspectRatio = (float)Screen.width / Screen.height;
        float targetAspectRatio = 16f / 9f;
        float desiredViewportHeight = screenAspectRatio / targetAspectRatio;
        Camera.main.rect = new Rect(0f, (1f - desiredViewportHeight) / 2f, 1f, desiredViewportHeight);
    }

}
