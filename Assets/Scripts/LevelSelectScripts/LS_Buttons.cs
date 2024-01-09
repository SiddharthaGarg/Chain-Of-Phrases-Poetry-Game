using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LS_Buttons : MonoBehaviour
{
    [SerializeField] private GameObject b_easy;
    [SerializeField] private GameObject b_medium;
    [SerializeField] private GameObject b_hard;
    [SerializeField] private GameObject i_LevelSelect;
    [SerializeField] private GameObject b_Back;
    [SerializeField] private AudioSource[] soundsList;


    // private RectTransform objectTransform;
    private Vector2 scale = new Vector2(1, 1);
    private float yPos_text = 360f;
    private float yPos_back = 120f;
    private float tweenTime = 1f;
    public static string difficultyLevel;


    private void OnEnable()
    {
        i_LevelSelect.GetComponent<RectTransform>().localPosition = new Vector2(-600f, Screen.height);
        i_LevelSelect.GetComponent<RectTransform>().LeanMoveLocalY(yPos_text, tweenTime).setEaseOutQuad().delay = 0.1f;

        b_Back.GetComponent<RectTransform>().localPosition = new Vector2(680, -Screen.height);
        b_Back.GetComponent<RectTransform>().LeanMoveY(yPos_back, tweenTime).setEaseOutQuad().delay = 0.1f;

        b_easy.GetComponent<RectTransform>().localScale = new Vector2(0, 0);
        b_medium.GetComponent<RectTransform>().localScale = new Vector2(0, 0);
        b_hard.GetComponent<RectTransform>().localScale = new Vector2(0, 0);

        LeanTween.scale(b_easy, scale, tweenTime).setEaseInQuad().delay = 0.1f;
        LeanTween.scale(b_medium, scale, tweenTime).setEaseInQuad().delay = 0.1f;
        LeanTween.scale(b_hard, scale, tweenTime).setEaseInQuad().delay = 0.1f;

    }

    #region Clicking Different Buttons

    public void OnClickEasy()
    {
        soundsList[0].Play();
        difficultyLevel = "Easy";
        SceneManager.LoadScene("EasyLevel");
    }
    public void OnClickMedium()
    {
        soundsList[0].Play();
        difficultyLevel = "Medium";
        SceneManager.LoadScene("MediumLevel");
    }
    public void OnClickHard()
    {
        soundsList[0].Play();
        difficultyLevel = "Hard";
        SceneManager.LoadScene("HardLevel");
    }
    public void Back()
    {
        soundsList[1].Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    #endregion

}
