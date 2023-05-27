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

    private void OnEnable()
    {
        _continueButton.interactable = false;
        _quitButton.interactable = false;
        StartCoroutine(EnableButtons());
    }

    private void Start()
    {
        _continueButton.onClick.AddListener(Continue);
        _quitButton.onClick.AddListener(QuitToMenu);
    }

    private IEnumerator EnableButtons()
    {
        yield return new WaitForSecondsRealtime(1f);
        _continueButton.interactable = true;
        _quitButton.interactable = true;
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
