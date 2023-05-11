using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseScreen : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _quitButton;
    [SerializeField] private GameObject _screen;

    private void Start()
    {
        _screen.SetActive(false);
        _continueButton.onClick.AddListener(Continue);
        _quitButton.onClick.AddListener(QuitToMenu);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!_screen.activeInHierarchy)
            {
                Time.timeScale = 0f;
                _screen.SetActive(true);
            }
            else
            {
                Continue();
            }
        }
    }

    private void Continue()
    {
        Time.timeScale = 1f;
        _screen.SetActive(false);
    }

    private void QuitToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SCN_Menu");
    }
}
