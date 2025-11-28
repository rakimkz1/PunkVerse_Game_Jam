using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float mouseSentivity;
    public Vector3 diractionX;
    public Vector3 diractionY;

    private Vector3 cameraRotation;
    private void Update()
    {
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");

        cameraRotation += new Vector3(-y * 0.8f, x, 0f) * mouseSentivity * Time.deltaTime;
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
