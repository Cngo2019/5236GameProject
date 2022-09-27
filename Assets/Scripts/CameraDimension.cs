using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBoundary : MonoBehaviour {
    
    public static float getCameraVerticalBoundary(string cameraTag) {
        Camera camera = GameObject.FindGameObjectWithTag(cameraTag).GetComponent<Camera>();
        return camera.orthographicSize;
        
    }

    public static float getCameraHorizontalBoundary(string cameraTag) {
        Camera camera = GameObject.FindGameObjectWithTag(cameraTag).GetComponent<Camera>();
        return camera.aspect * camera.orthographicSize;
    }
}