using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetGame : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    void Update()
    {
        scoreText.text = SwordScript.score.ToString();
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Lobby");
        }
    }
}
