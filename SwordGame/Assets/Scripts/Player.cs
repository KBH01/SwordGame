using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private Material postProcessingMaterial;

    [SerializeField] private Camera playerCam;
    [SerializeField] private GameObject sword;

    [SerializeField] private AudioClip deathSound;

    [SerializeField] private GameObject healthPoint;
    [SerializeField] private GameObject healthPoint1;
    [SerializeField] private GameObject healthPoint2;
    
    public float damageIntensity;
    public float deathIntensity = 0;


    private AudioSource playerAudio;
    
    public float scoreSceneTimer = 0;

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
            scoreSceneTimer += 1 * Time.deltaTime;
            deathIntensity += 0.025f;
            deathIntensity = Mathf.Clamp(deathIntensity, 0, 17);
            playerCam.gameObject.GetComponent<Rigidbody>().useGravity = true;
            sword.GetComponent<SwordScript>().enabled = false;
            sword.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            GetComponent<BoxCollider>().enabled = false;
            if (scoreSceneTimer > 5)
            {
                SceneManager.LoadScene("Score");
            }
        }
    }
    
    private void OnCollisionEnter(Collision other)
    {
        health -= 1;
        damageIntensity = 1;
        playerAudio.Play();

        switch (health)
        {
            case 2:
                Destroy(healthPoint);
                break;
            case 1:
                Destroy(healthPoint1);
                break;
            case 0:
                Destroy(healthPoint2);
                break;
        }

        if (health == 0)
        {
            playerAudio.PlayOneShot(deathSound, 2f);
        }
    }
}
