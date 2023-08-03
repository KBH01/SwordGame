using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private GameObject Projectile;

    [SerializeField] private float spawnFrequency;

    private float timer;

    void Start()
    {
        //InvokeRepeating("Attack", 0f, spawnFrequency);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnFrequency)
        {
            Attack();
            timer = 0;
            if (spawnFrequency > 0.2f)
            {
                spawnFrequency -= 0.005f;
            }
        }
    }

    void Attack()
    {
        Instantiate(Projectile);
    }
}
