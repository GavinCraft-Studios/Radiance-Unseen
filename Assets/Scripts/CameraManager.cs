using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public List<GameObject> cameras;
    public int activeCameraID;

    void Awake()
    {
        activeCameraID = 0;
        foreach (GameObject camera in cameras)
        {
            if (camera.name == "Main Camera")
            {
                camera.SetActive(true);
            }
            else
            {
                camera.SetActive(false);
            }
        }
    }

    public void changeCamera(int ID)
    {
        foreach (GameObject camera in cameras)
        {
            camera.SetActive(false);
        }
        cameras[ID].SetActive(true);
        activeCameraID = ID;
    }
}
