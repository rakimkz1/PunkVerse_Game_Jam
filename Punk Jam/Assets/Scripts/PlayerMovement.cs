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

    public bool isMovable;

    [SerializeField] private CameraControl cameraControl;
    [SerializeField] private Transform bodyView;
    [SerializeField] private PlayerAnimation anim;
    private Rigidbody _rb;
    private TactMachine _tackMachine;
    private PlayerZip _playerZip;
    private float _nowDashingTime;
    private bool _isDashing;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _tackMachine = GetComponent<TactMachine>();
        _playerZip = GetComponent<PlayerZip>();
    }

    public void Update()
    {
        Movement();
        Dash();
        BodyRotate();
    }

    private void Dash()
    {
        if (!isMovable)
            return;
        if (_isDashing)
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");

            Vector3 diraction = cameraControl.diractionX * y + cameraControl.diractionY * x;

            _rb.velocity = diraction.normalized * dashCurve.Evaluate(_nowDashingTime / dashingTime) * dashSpeed;
            _nowDashingTime += Time.deltaTime;
        }

        if(Input.GetKeyDown(KeyCode.Space) && !_playerZip.isZiping &&  _tackMachine.IsBeatTact())
        {
            Debug.Log("Dash");
            StartDash();
        }
    }

    private async Task StartDash()
    {
        _isDashing = true;
        _nowDashingTime = 0f;
        anim.DashAnim();
        await Task.Delay((int)(dashingTime * 1000f));
        _isDashing = false;
    }

    private void Movement()
    {
        if (_isDashing || _playerZip.isZiping || !isMovable)
            return;
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        float gravity = _rb.velocity.y;
        Vector3 diraction = cameraControl.diractionX * y + cameraControl.diractionY * x;

        if(x != 0f || y != 0f) 
            anim.isMoving = true;
        else
            anim.isMoving = false;

        _rb.velocity = Vector3.ClampMagnitude(diraction, 1f) * speed;
        _rb.velocity += Vector3.up * gravity;
    }

    private void BodyRotate()
    {
        if (_playerZip.isZiping)
            return;

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