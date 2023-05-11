using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _quitButton;

    private void Start()
    {
        _continueButton.onClick.AddListener(Continue);
        _quitButton.onClick.AddListener(QuitToMenu);
    }

    private void Continue()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void QuitToMenu()
    {
        SceneManager.LoadScene("SCN_Menu");
    }
}
