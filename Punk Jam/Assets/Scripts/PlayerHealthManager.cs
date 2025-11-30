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
    [SerializeField] private Animator anim;
    [SerializeField] protected TactMachine tackMachine;
    [SerializeField] private TextMeshProUGUI tip;
    [SerializeField] private GameObject DeathBar;
    [SerializeField] private AnimationCurve tiptextCurve;
    private PlayerMovement movement;
    private int _nowBeatNumber;
    private bool _isBeatable;

    private void Start()
    {
        movement = GetComponent<PlayerMovement>();
        tackMachine.onTactBeat += () =>
        {
            tip.transform.DOScale(1.4f, 0.5f).SetEase(tiptextCurve);
        };
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        AudioManager.instance.PlayAudioOneShot(getHit, 1f);
        ShowHealth();
        if (health <= 0)
            Dead();
    }

    private void ShowHealth()
    {
        healthBar.fillAmount = health / maxHealth;
    }

    private void Dead()
    {
        movement.OnStopMoving?.Invoke();
        _nowBeatNumber = 0;
        DeathBar.SetActive(true);
        anim.SetTrigger("stun");
        StartReancarnationGame();
    }

    private void Update()
    {
        if(_isBeatable && tackMachine.IsBeatTact() && Input.GetKeyDown(KeyCode.Space))
        {
            _nowBeatNumber += 1;
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
        await Task.Delay(2000);
        movement.OnStartMoving?.Invoke();
    }

    private async Task StartReancarnationGame()
    {
        await Task.Delay((int)(reancarnationDelay * 1000f));
        _isBeatable = true;
    }
}