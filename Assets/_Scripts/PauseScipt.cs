using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScipt : MonoBehaviour
{
    private bool isPaused = false;
    [SerializeField] GameObject pauseUI;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                Time.timeScale = 0.0f;
                isPaused = true;
                pauseUI.SetActive(true);
            }
            else
            {
                Time.timeScale = 1.0f;
                isPaused = false;
                pauseUI.SetActive(false);
            }
        }
    }
}
