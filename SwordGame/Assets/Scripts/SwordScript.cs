using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwordScript : MonoBehaviour
{
    private static float resolutionRatio = 0;
    
    [SerializeField] float sensitivity = 0;
    
    [SerializeField] private Transform idle;
    [SerializeField] private Transform left;
    [SerializeField] private Transform right;
    [SerializeField] private Transform top;
    [SerializeField] private Transform bottom;

    [SerializeField] private float changeSpeed;

    private Transform current;
    private Transform previous;
    
    private float lerpAmount = 0.0f;

    void Awake()
    {
        current = idle;
        previous = idle;
    }
    
    void Update()
    {
        lerpAmount += Mathf.Pow(changeSpeed * Time.deltaTime, 2f);
        
        float mouseX = Input.mousePosition.x;
        float mouseY = Input.mousePosition.y;

        if (Vector2.Distance(new Vector2(Screen.width * 0.5f, Screen.height * 0.5f), new Vector2(mouseX, mouseY)) > sensitivity)
        {

            if (mouseY > Line1(mouseX) && mouseY > Line2(mouseX))
            {
                AnimateSword(top);
            }

            else if (mouseY < Line1(mouseX) && mouseY < Line2(mouseX))
            {
                AnimateSword(bottom);
            }

            if (mouseY > Line1(mouseX) && mouseY < Line2(mouseX))
            {
                AnimateSword(left);
            }

            else if (mouseY < Line1(mouseX) && mouseY > Line2(mouseX))
            {
                AnimateSword(right);
            }
        }

        else
        {
            AnimateSword(idle);
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

    void AnimateSword(Transform position)
    {
        if (current != position)
        {
            previous = current;
            lerpAmount = 0;
            current = position;
        }
                
        transform.position = Vector3.Lerp(previous.position, current.position, lerpAmount);
        transform.rotation = Quaternion.Slerp(previous.rotation, current.rotation, lerpAmount);
    }
}
