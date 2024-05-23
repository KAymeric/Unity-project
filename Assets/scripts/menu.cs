using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    [SerializeField] public GameObject canva;
    private Custominputs inputActions;
    private bool isGamePaused = false;
    private bool isEscToggled = false;

    private void Awake()
    {
        inputActions = new Custominputs();
        Time.timeScale = 1.0f;
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    void Update()
    {
        pause();
    }

    private void pause()
    {
        if (inputActions.FPSController.pause.ReadValue<float>() > 0)
        {
            if (isGamePaused && !isEscToggled)
            {
                ContinueGame();
            }
            else if (!isEscToggled)
            {
                PauseGame();
            }
            isEscToggled = true;
        }
        else
        {
            isEscToggled = false;
        }
        canva.SetActive(isGamePaused);
        Cursor.visible = isGamePaused;
    }

    private void ContinueGame()
    {
        Time.timeScale = 1.0f;
        isGamePaused = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        isGamePaused = true;
        Cursor.lockState = CursorLockMode.None;

    }

    public void RestartGame()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}