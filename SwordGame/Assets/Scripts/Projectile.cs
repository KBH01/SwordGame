using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float projectileSpeed;

     [SerializeField] GameObject sword;

     private SwordScript swordScript;

    private int projectileOrientation = 0;

    void OnEnable()
    {
        swordScript = sword.GetComponent<SwordScript>();
        
        projectileOrientation = (int)Random.Range(0f, 3f);
        Debug.Log(projectileOrientation);

        switch (projectileOrientation)
        {
            case 0:
                transform.position = new Vector3(0, swordScript.left.position.y, swordScript.left.position.z);
                transform.rotation = swordScript.left.rotation;
                break;
            case 1:
                transform.position = new Vector3(0, swordScript.right.position.y, swordScript.right.position.z);
                transform.rotation = swordScript.right.rotation;
                break;
            case 2:
                transform.position = new Vector3(0, swordScript.top.position.y, swordScript.top.position.z);
                transform.rotation = swordScript.top.rotation;
                break;
            case 3:
                transform.position = new Vector3(0, swordScript.bottom.position.y, swordScript.bottom.position.z);
                transform.rotation = swordScript.bottom.rotation;
                break;
        }
    }
    
    void Update()
    {
        transform.Translate(new Vector3(-projectileSpeed, 0, 0) * Time.deltaTime, Space.World);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (projectileOrientation == swordScript.swordOrientation)
        {
            Destroy(gameObject);
        }
    }
}
