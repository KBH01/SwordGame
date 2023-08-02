using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public static void Play()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public static void Quit()
    {
        Application.Quit();
    }
}
