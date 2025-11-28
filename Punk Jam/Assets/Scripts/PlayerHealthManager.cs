using System;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    public float health;
    public float maxHealth;

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
