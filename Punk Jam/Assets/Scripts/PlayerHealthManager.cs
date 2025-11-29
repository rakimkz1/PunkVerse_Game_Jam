using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthManager : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public Image healthBar;
    [SerializeField] private AudioClip getHit;
    private PlayerMovement movement;

    private void Start()
    {
        movement = GetComponent<PlayerMovement>();
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
        Reaincarnation();
    }

    private async Task Reaincarnation()
    {
        await Task.Delay((int)(1000 * 3f));
        health = maxHealth;
        movement.OnStartMoving?.Invoke();
    }
}
