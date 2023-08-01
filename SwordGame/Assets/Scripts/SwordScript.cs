using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SwordScript : MonoBehaviour
{
    private static float resolutionRatio = 0;
    
    [SerializeField] float sensitivity = 0;
    
    [SerializeField] private Transform idle;
    [SerializeField] private Transform left;
    [SerializeField] private Transform right;
    [SerializeField] private Transform top;
    [SerializeField] private Transform bottom;

    [SerializeField] private GameObject leftWheelo;
    [SerializeField] private GameObject rightWheelo;
    [SerializeField] private GameObject topWheelo;
    [SerializeField] private GameObject bottomWheelo;
    
    private SpriteRenderer leftWheelr;
    private SpriteRenderer rightWheelr;
    private SpriteRenderer topWheelr;
    private SpriteRenderer bottomWheelr;

    [SerializeField] private Color selectedColor;
    [SerializeField] private Color idleColor;
    [SerializeField] private Color blockColor;

    [SerializeField] private float changeSpeed;

    [SerializeField] private Material postProcessingMaterial;

    [SerializeField] private TextMeshPro scoreText;

    private Transform current;
    private Transform previous;
    
    private float lerpAmount = 0.0f;

    public int swordOrientation = 0;

    private float absorbIntensity;

    private ParticleSystem blockAffect;

    public uint score;
    
    void Awake()
    {
        current = idle;
        previous = idle;
        leftWheelr = leftWheelo.GetComponent<SpriteRenderer>();
        rightWheelr = rightWheelo.GetComponent<SpriteRenderer>();
        topWheelr = topWheelo.GetComponent<SpriteRenderer>();
        bottomWheelr = bottomWheelo.GetComponent<SpriteRenderer>();
        blockAffect = GetComponent<ParticleSystem>();
    }
    
    void Update()
    {
        lerpAmount += changeSpeed * Time.deltaTime;
        absorbIntensity -= 4f * Time.deltaTime * absorbIntensity;
        absorbIntensity = Mathf.Clamp(absorbIntensity, 0, 1);
        postProcessingMaterial.SetFloat("_AbsorbIntensity", absorbIntensity);
        scoreText.text = score.ToString();

        float mouseX = Input.mousePosition.x;
        float mouseY = Input.mousePosition.y;

        if (Vector2.Distance(new Vector2(Screen.width * 0.5f, Screen.height * 0.5f), new Vector2(mouseX, mouseY)) > sensitivity)
        {

            if (mouseY > Line1(mouseX) && mouseY > Line2(mouseX))
            {
                AnimateSword(top);
                leftWheelr.color = idleColor;
                rightWheelr.color = idleColor;
                topWheelr.color = selectedColor;
                bottomWheelr.color = idleColor;
                swordOrientation = 3;
            }

            else if (mouseY < Line1(mouseX) && mouseY < Line2(mouseX))
            {
                AnimateSword(bottom);
                leftWheelr.color = idleColor;
                rightWheelr.color = idleColor;
                topWheelr.color = idleColor;
                bottomWheelr.color = selectedColor;
                swordOrientation = 4;
            }

            if (mouseY > Line1(mouseX) && mouseY < Line2(mouseX))
            {
                AnimateSword(left);
                leftWheelr.color = selectedColor;
                rightWheelr.color = idleColor;
                topWheelr.color = idleColor;
                bottomWheelr.color = idleColor;
                swordOrientation = 1;
            }

            else if (mouseY < Line1(mouseX) && mouseY > Line2(mouseX))
            {
                AnimateSword(right);
                leftWheelr.color = idleColor;
                rightWheelr.color = selectedColor;
                topWheelr.color = idleColor;
                bottomWheelr.color = idleColor;
                swordOrientation = 2;
            }
        }

        else
        {
            AnimateSword(idle);
            leftWheelr.color = idleColor;
            rightWheelr.color = idleColor;
            topWheelr.color = idleColor;
            bottomWheelr.color = idleColor;
            swordOrientation = 0;
        }

        if (Input.GetMouseButton(1))
        {
            if (current == left)
            {
                leftWheelr.color = blockColor;
            }
            
            else if (current == right)
            {
                rightWheelr.color = blockColor;
            }
            
            else if (current == top)
            {
                topWheelr.color = blockColor;
            }
            
            else if (current == bottom)
            {
                bottomWheelr.color = blockColor;
            }
        }
    }

    //these are the lines that split up the screen into areas for the mouse
    float Line1(float x)
    {
        float y = 0;

        y = x - (0.5f * Screen.width - 0.5f * Screen.height);

        return y;
    }
    
    float Line2(float x)
    {
        float y = 0;

        y = -x + (Screen.width - (0.5f * Screen.width - 0.5f * Screen.height));

        return y;
    }
    
    //make the sword smooth
    void AnimateSword(Transform position)
    {
        if (current != position)
        {
            previous = current;
            lerpAmount = 0;
            current = position;
        }
                
        transform.position = Vector3.Lerp(previous.position, current.position + new Vector3(0, 0.01f * Mathf.Sin(Time.time * 5f * (0.1f * Mathf.PerlinNoise(0.05f * Time.time, 0f) + 0.9f)), 0), lerpAmount);
        transform.rotation = Quaternion.Slerp(previous.rotation, current.rotation, lerpAmount);
    }

    private void OnCollisionEnter(Collision other)
    {
        absorbIntensity = 1;
        blockAffect.Play();
        score += 1;
    }
}
