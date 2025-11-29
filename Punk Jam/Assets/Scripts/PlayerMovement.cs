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

    public event Action OnStopMoving;
    public event Action OnStartMoving;

    [SerializeField] private CameraControl cameraControl;
    public Transform bodyView;
    [SerializeField] private PlayerAnimation anim;
    [SerializeField] private AudioClip dashSound;
    private Rigidbody _rb;
    private TactMachine _tackMachine;
    private PlayerZip _playerZip;
    private float _nowDashingTime;
    private bool isMoveable = true;
    private bool _isDashing;
    private bool _isTargetRotate;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _tackMachine = GetComponent<TactMachine>();
        _playerZip = GetComponent<PlayerZip>();
        OnStartMoving += () =>
        {
            isMoveable = true;
        };
        OnStopMoving += () =>
        {
            isMoveable = false;
        };
    }

    public void Update()
    {
        Movement();
        Dash();
        BodyRotate();
    }

    private void Dash()
    {
        if (!isMoveable)
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
            StartDash();
        }
    }

    private async Task StartDash()
    {
        _isDashing = true;
        _nowDashingTime = 0f;
        anim.DashAnim();
        AudioManager.instance.PlayAudioOneShot(dashSound, 0.5f);
        await Task.Delay((int)(dashingTime * 1000f));
        _isDashing = false;
    }

    private void Movement()
    {
        if (_isDashing || _playerZip.isZiping || !isMoveable)
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
        if (_playerZip.isZiping || !isMoveable || _isTargetRotate)
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

    public void TargetRotate(Vector3 pos, float duraction)
    {
        _isTargetRotate = true;
        float time = 0f;
        Vector3 diraction = pos - transform.position;
        Quaternion rot = Quaternion.Euler(Quaternion.LookRotation(diraction, Vector3.up).eulerAngles);
        while (time < duraction)
        {
            bodyView.rotation = Quaternion.RotateTowards(bodyView.rotation, rot, bodyRotateSpeed * Time.deltaTime);
            time += Time.deltaTime;
        }
        _isTargetRotate = false;
    }
}