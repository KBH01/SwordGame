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

    private uint swordState;
    
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
                swordState = 3;
            }

            else if (mouseY < Line1(mouseX) && mouseY < Line2(mouseX))
            {
                transform.SetPositionAndRotation(bottom.position, bottom.rotation);
                swordState = 4;
            }

            if (mouseY > Line1(mouseX) && mouseY < Line2(mouseX))
            {
                transform.SetPositionAndRotation(left.position, left.rotation);
                swordState = 1;
            }

            else if (mouseY < Line1(mouseX) && mouseY > Line2(mouseX))
            {
                transform.SetPositionAndRotation(right.position, right.rotation);
                swordState = 2;
            }
        }

        else
        {
            transform.SetPositionAndRotation(idle.position, idle.rotation);
            swordState = 0;
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
}
