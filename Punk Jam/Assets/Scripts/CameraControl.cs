using System;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float mouseSentivity;
    public float settingsAumount;
    public Vector3 diractionX;
    public Vector3 diractionY;
    public PlayerMovement player;

    private Vector3 cameraRotation;
    private bool isLookable = true;


    private void Start()
    {
        settingsAumount = PlayerPrefs.GetFloat("MouseSensitivity");
        Settings.instance.OnMusicVolumeChanged += SetMouseSence;
        player.OnStartMoving += () =>
        {
            isLookable = true;
        };
        player.OnStopMoving += () =>
        {
            isLookable = false;
        };
    }

    private void SetMouseSence(float obj)
    {
        settingsAumount = obj;
    }

    private void Update()
    {
        if (!isLookable)
            return;
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");

        cameraRotation += new Vector3(-y * 0.8f, x, 0f) * mouseSentivity * settingsAumount * Time.deltaTime;
        cameraRotation = LimitCamera(cameraRotation);

        diractionX = new Vector3(Mathf.Sin(cameraRotation.y * Mathf.Deg2Rad), 0f, Mathf.Cos(cameraRotation.y * Mathf.Deg2Rad));
        diractionY = new Vector3(Mathf.Sin((cameraRotation.y + 90f) * Mathf.Deg2Rad), 0f, Mathf.Cos((cameraRotation.y + 90f) * Mathf.Deg2Rad));
        transform.rotation = Quaternion.Euler(cameraRotation);
    }

    private Vector3 LimitCamera(Vector3 cameraRotation)
    {
        cameraRotation.x = Mathf.Clamp(cameraRotation.x, -90f, 90f);
        return cameraRotation;
    }
}
