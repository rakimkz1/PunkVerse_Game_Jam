using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthManager : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public Image healthBar;

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
            Dead();
    }

    private void Dead()
    {
        Debug.Log("Player");
    }
}
