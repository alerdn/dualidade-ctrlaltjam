using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseScreen : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _quitButton;
    [SerializeField] private GameObject _screen;

    [Header("Nav")]
    [SerializeField] private List<ActionButton> _actions;
    [SerializeField] private List<GameObject> _screens;

    private float _previousTimeScale = 1f;

    private void OnEnable()
    {
        _continueButton.interactable = false;
        _restartButton.interactable = false;
        _quitButton.interactable = false;
        StartCoroutine(EnableButtons());
    }

    private void Start()
    {
        _screen.SetActive(false);
        _continueButton.onClick.AddListener(Continue);
        _restartButton.onClick.AddListener(RestartLevel);
        _quitButton.onClick.AddListener(QuitToMenu);

        for (int i = 0; i < _actions.Count; i++)
        {
            _actions[i].Init(i);
            _actions[i].OnClick += SelectScreen;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!_screen.activeInHierarchy)
            {
                OpenMenu();
            }
            else
            {
                Continue();
            }
        }
    }

    private IEnumerator EnableButtons()
    {
        yield return new WaitForSecondsRealtime(1f);
        _continueButton.interactable = true;
        _restartButton.interactable = true;
        _quitButton.interactable = true;
    }
    private void OpenMenu()
    {
        _previousTimeScale = Time.timeScale;
        Time.timeScale = 0f;
        _screen.SetActive(true);
        SelectScreen(0);
    }

    private void SelectScreen(int i)
    {
        for (int j = 0; j < _screens.Count; j++)
        {
            if (i == j)
            {
                _screens[j].SetActive(true);
                _actions[j].Select();
            }
            else
            {
                _screens[j].SetActive(false);
                _actions[j].Deselect();
            }
        }
    }

    private void Continue()
    {
        Time.timeScale = _previousTimeScale;
        _screen.SetActive(false);
    }

    private void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void QuitToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SCN_Menu");
    }
}
