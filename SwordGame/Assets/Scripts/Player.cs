using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private uint health;
    [SerializeField] private Material postProcessingMaterial;
    private AudioSource audio;
    
    public float damageIntensity;

    void Awake(){
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        damageIntensity -= 2f * Time.deltaTime;
        damageIntensity = Mathf.Clamp(damageIntensity, 0, 1);
        postProcessingMaterial.SetFloat("_DamageIntensity", damageIntensity);
    }
    
    private void OnCollisionEnter(Collision other)
    {
        health -= 1;
        audio.Play();
        damageIntensity = 1;
    }
}
