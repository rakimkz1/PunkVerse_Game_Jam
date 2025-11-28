using System;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float bodyRotateSpeed;
    public float dashingTime;
    public float dashSpeed;
    public AnimationCurve dashCurve;

    [SerializeField] private CameraControl cameraControl;
    [SerializeField] private Transform bodyView;
    private Rigidbody _rb;
    private TactMachine _tackMachine;
    private float _nowDashingTime;
    private bool _isDashing;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _tackMachine = GetComponent<TactMachine>();
    }

    public void Update()
    {
        Movement();
        Dash();
        BodyRotate();
    }

    private void Dash()
    {
        if (_isDashing)
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");

            Vector3 diraction = cameraControl.diractionX * y + cameraControl.diractionY * x;

            _rb.velocity = diraction.normalized * dashCurve.Evaluate(_nowDashingTime / dashingTime) * dashSpeed;
            _nowDashingTime += Time.deltaTime;
        }

        if(Input.GetKeyDown(KeyCode.Space) && _tackMachine.IsBeatTact())
        {
            Debug.Log("Dash");
            StartDash();
        }
    }

    private async Task StartDash()
    {
        _isDashing = true;
        _nowDashingTime = 0f;
        await Task.Delay((int)(dashingTime * 1000f));
        _isDashing = false;
    }

    private void Movement()
    {
        if (_isDashing)
            return;
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 diraction = cameraControl.diractionX * y + cameraControl.diractionY * x;

        _rb.velocity = diraction.normalized * speed;
    }

    private void BodyRotate()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector3 diraction = -cameraControl.diractionX * x + cameraControl.diractionY * y;


        if (x != 0f || y != 0f)
        {
            Quaternion rot = Quaternion.Euler(Quaternion.LookRotation(diraction, Vector3.up).eulerAngles);
            bodyView.rotation = Quaternion.RotateTowards(bodyView.rotation, rot, bodyRotateSpeed * Time.deltaTime);
        }
    }
}