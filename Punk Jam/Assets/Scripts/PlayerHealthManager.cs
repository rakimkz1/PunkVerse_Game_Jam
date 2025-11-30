using DG.Tweening;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthManager : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public Image healthBar;
    public float reancarnationDelay;
    public int rebeatNumberToAlive;
    [SerializeField] private AudioClip getHit;
    [SerializeField] private AudioClip getCure;
    [SerializeField] private Animator anim;
    [SerializeField] protected TactMachine tackMachine;
    [SerializeField] private TextMeshProUGUI tip;
    [SerializeField] private GameObject DeathBar;
    [SerializeField] private AnimationCurve tiptextCurve;
    private PlayerMovement movement;
    private int _nowBeatNumber;
    private bool _isBeatable;
    private bool _isDead;

    private void Start()
    {
        movement = GetComponent<PlayerMovement>();
        tip.transform.DOScale(1.4f, 0.5f).SetEase(tiptextCurve).From(1f).SetLoops(-1,LoopType.Yoyo);
    }
    public void TakeDamage(float damage)
    {
        if(!_isDead)
            health -= damage;
        AudioManager.instance.PlayAudioOneShot(getHit, 1f);
        ShowHealth();
        if (health <= 0 && !_isDead)
            Dead();
    }

    private void ShowHealth()
    {
        healthBar.fillAmount = health / maxHealth;
    }

    private void Dead()
    {
        _isDead = true;
        movement.OnStopMoving?.Invoke();
        _nowBeatNumber = 0;
        DeathBar.SetActive(true);
        anim.SetTrigger("stun");
        StartReancarnationGame();
    }

    private void Update()
    {
        if(_isBeatable && Input.GetKeyDown(KeyCode.Space) && tackMachine.IsBeatTact() || _isBeatable  && Input.GetMouseButtonDown(0) && tackMachine.IsBeatTact())
        {
            _nowBeatNumber += 1;
            AudioManager.instance.PlayAudioOneShot(getCure, 1f);
            if(_nowBeatNumber == rebeatNumberToAlive)
            {
                Reancornate();
            }
        }
    }

    private async Task Reancornate()
    {
        _isBeatable = false;
        DeathBar.SetActive(false);
        anim.SetTrigger("revive");
        await Task.Delay(2700);
        _isDead = false;
        health = maxHealth;
        ShowHealth();
        movement.OnStartMoving?.Invoke();
    }

    private async Task StartReancarnationGame()
    {
        await Task.Delay((int)(reancarnationDelay * 1200f));
        _isBeatable = true;
    }
}