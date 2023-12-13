using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    [Header("Variable Lists:")]
    //public List<Camera> cameras;
    public List<CinemachineVirtualCamera> vcameras;

    [Header("Current Camera")]
    public int initialCameraID;

    void Awake()
    {
        //foreach (Camera camera in cameras)
        //{
        //    camera.gameObject.SetActive(false);
        //}
        //cameras[initialCameraID].gameObject.SetActive(true);

        foreach (CinemachineVirtualCamera vcamera in vcameras)
        {
            vcamera.gameObject.SetActive(false);
        }
        vcameras[initialCameraID].gameObject.SetActive(true);
    }

    public void changeCamera(int ID)
    {
        initialCameraID = ID;

        //foreach (Camera camera in cameras)
        //{
        //    camera.gameObject.SetActive(false);
        //}
        //cameras[initialCameraID].gameObject.SetActive(true);

        foreach (CinemachineVirtualCamera vcamera in vcameras)
        {
            vcamera.gameObject.SetActive(false);
        }
        vcameras[initialCameraID].gameObject.SetActive(true);
    }
}