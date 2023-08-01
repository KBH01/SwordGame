using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float projectileSpeed;

    [SerializeField] private Transform left;
    [SerializeField] private Transform right;
    [SerializeField] private Transform top;
    [SerializeField] private Transform bottom;

    [SerializeField] public SwordScript swordScript;

<<<<<<< HEAD
     public int projectileOrientation = 0;
     private AudioSource audio; 
=======
     private int projectileOrientation = 0;
>>>>>>> parent of 5c87628 (Added Sound)

     void OnEnable()
    {
        if(name == "Projectile")
        {
            return;
        }
        
        Destroy(gameObject, 10.0f);
        
        projectileOrientation = (int)Random.Range(1, 5);
        Debug.Log(projectileOrientation);

        switch (projectileOrientation)
        {
            case 1:
                transform.position = new Vector3(5, left.position.y, left.position.z);
                transform.rotation = left.rotation;
                break;
            case 2:
                transform.position = new Vector3(5, right.position.y, right.position.z);
                transform.rotation = right.rotation;
                break;
            case 3:
                transform.position = new Vector3(5, top.position.y, top.position.z);
                transform.rotation = top.rotation;
                break;
            case 4:
                transform.position = new Vector3(5, bottom.position.y, bottom.position.z);
                transform.rotation = bottom.rotation;
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
