using System;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour, IDamageable
{
    [SerializeField] private int health = 100;

    private void Update()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void OnDamage()
    {
        health -= 10;
    }
}
