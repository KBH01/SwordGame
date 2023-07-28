using System.Collections;
using System.Collections.Generic;
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
    
    void Update()
    {
        resolutionRatio = (float)Screen.height / (float)Screen.width;
        float mouseX = Input.mousePosition.x;
        float mouseY = Input.mousePosition.y;
        
        Debug.Log(Screen.width + ", " + Screen.height + ", " + resolutionRatio);

        if (Vector2.Distance(new Vector2(Screen.width * 0.5f, Screen.height * 0.5f), new Vector2(mouseX, mouseY)) > sensitivity)
        {

            if (mouseY > Line1(mouseX) && mouseY > Line2(mouseX))
            {
                transform.SetPositionAndRotation(top.position, top.rotation);
            }

            else if (mouseY < Line1(mouseX) && mouseY < Line2(mouseX))
            {
                transform.SetPositionAndRotation(bottom.position, bottom.rotation);
            }

            if (mouseY > Line1(mouseX) && mouseY < Line2(mouseX))
            {
                transform.SetPositionAndRotation(left.position, left.rotation);
            }

            else if (mouseY < Line1(mouseX) && mouseY > Line2(mouseX))
            {
                transform.SetPositionAndRotation(right.position, right.rotation);
            }
        }

        else
        {
            transform.SetPositionAndRotation(idle.position, idle.rotation);
        }

    }

    //these are the lines that split up the screen into areas for the mouse
    float Line1(float x)
    {
        float y = 0;

        y = x * resolutionRatio;

        return y;
    }
    
    float Line2(float x)
    {
        float y = 0;

        y = x * -resolutionRatio + (float)Screen.height;

        return y;
    }
}
