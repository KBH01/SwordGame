using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private uint health;
    [SerializeField] private Material postProcessingMaterial;

    [SerializeField] private Camera playerCam;
    [SerializeField] private GameObject sword;
    
    public float damageIntensity;
    public float deathIntensity = 0;
        
    
    private AudioSource playerAudio;

    void Awake()
    {
        playerAudio = GetComponent<AudioSource>();
        playerCam.gameObject.GetComponent<Rigidbody>().excludeLayers = LayerMask.GetMask("Default");

    }

    void Update()
    {
        damageIntensity -= 2f * Time.deltaTime;
        damageIntensity = Mathf.Clamp(damageIntensity, 0, 1);
        postProcessingMaterial.SetFloat("_DamageIntensity", damageIntensity);
        postProcessingMaterial.SetFloat("_DeathIntensity", deathIntensity);

        if (health <= 0)
        {
            deathIntensity += 0.025f;
            playerCam.gameObject.GetComponent<Rigidbody>().useGravity = true;
            sword.GetComponent<SwordScript>().enabled = false;
            sword.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }
    }
    
    private void OnCollisionEnter(Collision other)
    {
        health -= 1;
        damageIntensity = 1;
        playerAudio.Play();
    }
}
