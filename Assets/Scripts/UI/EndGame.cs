using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    [SerializeField] private TMP_Text _text1;
    [SerializeField] private TMP_Text _text2;

    private void Start()
    {
        StartCoroutine(ShowEndScreen());
    }

    private IEnumerator ShowEndScreen()
    {
        yield return new WaitForSeconds(4f);
        DOTween.To(() => _text1.color, (color) => _text1.color = color, new Color(1f, 1f, 1f, 0f), 2f);
        DOTween.To(() => _text2.color, (color) => _text2.color = color, new Color(1f, 1f, 1f, 0f), 2f).WaitForCompletion();
        SceneManager.LoadScene("SCN_Menu");
    }
}
