using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private uint health;
    [SerializeField] private Material postProcessingMaterial;
    
    public float damageIntensity;

    void Update()
    {
        damageIntensity -= 2f * Time.deltaTime;
        damageIntensity = Mathf.Clamp(damageIntensity, 0, 1);
        postProcessingMaterial.SetFloat("_Intensity", damageIntensity);
    }
    
    private void OnCollisionEnter(Collision other)
    {
        health -= 1;
        damageIntensity = 1;
    }
}
