using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsScript : MonoBehaviour
{
    public GameObject quitButton, restartButton, LevelButton, ResumeButton, ControlsButton;
    public static bool PauseActive = false;
    public void ButtonActive()
    {
        quitButton.SetActive(true);
        LevelButton.SetActive(true);
        restartButton.SetActive(true);
        ResumeButton.SetActive(true);
        ControlsButton.SetActive(true);
        PauseActive = true;
        CharController.playerSpeed = 0;
    }
    public void ButtonNoActive()
    {
        quitButton.SetActive(false);
        LevelButton.SetActive(false);
        restartButton.SetActive(false);
        ResumeButton.SetActive(false);
        ControlsButton.SetActive(false);
        PauseActive = false;
    }
    }
