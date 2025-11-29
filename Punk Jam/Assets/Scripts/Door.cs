using System;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isKeyFound;
    public GameObject keyView;
    public float keyRotateTime;
    public float KeyRotateSpeed;

    public void StartTrail()
    {
        keyView.SetActive(true);
    }

    public void RotateKey()
    {
        float time = 0f;
        Vector3 initialRot = keyView.transform.localEulerAngles;

        while (time < keyRotateTime)
        {
            initialRot += Vector3.up * KeyRotateSpeed * Time.deltaTime;
            keyView.transform.rotation = Quaternion.Euler(initialRot);
            time += Time.deltaTime;
        }
    }

    public void OpenTheDoor()
    {

    }
}
