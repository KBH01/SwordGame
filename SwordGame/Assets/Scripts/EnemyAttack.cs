using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private GameObject Projectile;

    [SerializeField] private float spawnFrequency;

    void Start()
    {
        InvokeRepeating("Attack", 0f, spawnFrequency);
    }

    void Attack()
    {
        Instantiate(Projectile);
    }
}
