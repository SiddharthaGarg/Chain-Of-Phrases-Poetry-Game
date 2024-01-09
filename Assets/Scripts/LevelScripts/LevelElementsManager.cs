using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics;
using System.Collections.Generic;

public class LevelElementsManager : MonoBehaviour
{
    [SerializeField] private float startTime = 10000f;
    [SerializeField] private Text time;
    [SerializeField] private GameObject[] Lives;
    [SerializeField] private GameObject healthBar;
    [SerializeField] private GameObject[] phrases;
    //[SerializeField] private GameObject[] slots;
    [SerializeField] private Color defaultColor;
    [SerializeField] private AudioSource buttonClick;
    [SerializeField] private AudioSource lowTime;
    [SerializeField] private AudioSource lifeLost;
    [SerializeField] private GameObject BGM1;
    [SerializeField] private GameObject BGM2;
    [SerializeField] private GameObject Background;
    private int health;
    private int count = 0;
    private float bg_movingSpeed = 3f;
    private Vector3 startPos;
    public bool isPoemCorrect;
    private bool isGameOver = false;
    public static int easyLevel;
    public static int mediumLevel;
    public static int hardLevel;



    #region  Lives
    private int lives;
    public int _lives
    {
        get { return lives; }
        set
        {
            lives = value;


            if (value < 3 && value >= 0)
            {
                Lives[value].SetActive(false);
            }
            if (value == 0)
            {
                SendGameOverAnalytics();
            }

        }
    }
    #endregion

    #region Time
    private float timeLeft;

    public float _timeLeft
    {
        get { return timeLeft; }
        set
        {

            timeLeft = value;
            //int days = (int)(value / 84600) % 365;
            //int hours = (int)(value / 3600) % 24;
            int mins = (int)(value / 60) % 60;
            int secs = (int)(value % 60);
            time.text = mins + "m " + secs + "s";

        }

    }
    #endregion

    void Awake()
    {
        InitializeVariables(LS_Buttons.difficultyLevel);
        _timeLeft = startTime;

        //randomly choosing int for background music
        int randInt = Random.Range(1, 3);
        if (randInt == 1)
        {
            BGM1.SetActive(true);
        }
        else
        {
            BGM2.SetActive(true);
        }
    }
    private void Start()
    {

        startPos = Background.transform.position;

    }
    void Update()
    {
        if (_lives == 0 || _timeLeft == 0 || isGameOver)
        {
            GameManager.Instance.SwitchStates(GameManager.GameState.gameOver, 0.5f);

        }

        Background.transform.Translate(-bg_movingSpeed * Time.deltaTime, 0f, 0f);
        if (Background.transform.position.x < startPos.x - 7680f)
        {
            Background.transform.position = startPos;
        }

    }
    public void ReduceHealth()
    {
        healthBar.GetComponent<Image>().fillAmount -= (1f / health);

        Debug.Log(healthBar.GetComponent<Image>().fillAmount);

        if (healthBar.GetComponent<Image>().fillAmount <= 0) { isGameOver = true; }
    }
    public void StartTimeDisplay()
    {
        StartCoroutine(DisplayCountdown());

    }
    public IEnumerator DisplayCountdown()
    {
        do
        {
            _timeLeft -= 1f;
            if (_timeLeft == 60f) { lowTime.Play(); }
            if (_timeLeft == 56f) { lowTime.Stop(); }
            yield return new WaitForSeconds(1f);

        }
        while (timeLeft > 0f);

    }
    public void OnClickPause()
    {
        GameManager.Instance.OnPauseClicked();
    }
    public void OnClickCheckPoem()
    {
        buttonClick.Play();
        count = 0;
        Checked(LS_Buttons.difficultyLevel);
        if (!isPoemCorrect)
        {
            lifeLost.Play();
            Debug.Log(count);
            _lives -= 1;

        }
        else
        {
            #region Save Progress and GameAnalytics
            // Save Progress
            if (LS_Buttons.difficultyLevel == "Easy")
            {
                if (!PlayerPrefs.HasKey(LS_Buttons.difficultyLevel) || easyLevel >= PlayerPrefs.GetInt(LS_Buttons.difficultyLevel))
                {
                    PlayerPrefs.SetInt(LS_Buttons.difficultyLevel, easyLevel + 1);

                }
                AnalyticsResult aa = Analytics.CustomEvent("LevelProgress", new Dictionary<string, object>(){
                    { "Easy", easyLevel }
                    }
                );
                Debug.Log("AnalyticsResult: " + aa);


            }
            else if (LS_Buttons.difficultyLevel == "Medium")
            {
                if (!PlayerPrefs.HasKey(LS_Buttons.difficultyLevel) || mediumLevel >= PlayerPrefs.GetInt(LS_Buttons.difficultyLevel))
                {
                    PlayerPrefs.SetInt(LS_Buttons.difficultyLevel, mediumLevel + 1);

                }
                AnalyticsResult aa = Analytics.CustomEvent("LevelProgress", new Dictionary<string, object>() {
                    { "Medium", mediumLevel }
                    }
                );
                Debug.Log("AnalyticsResult: " + aa);
            }
            else if (LS_Buttons.difficultyLevel == "Hard")
            {
                if (!PlayerPrefs.HasKey(LS_Buttons.difficultyLevel) || hardLevel >= PlayerPrefs.GetInt(LS_Buttons.difficultyLevel))
                {
                    PlayerPrefs.SetInt(LS_Buttons.difficultyLevel, hardLevel + 1);

                }
                AnalyticsResult aa = Analytics.CustomEvent("LevelProgress", new Dictionary<string, object>(){
                    { "Hard", hardLevel }
                    }
                );
                Debug.Log("AnalyticsResult: " + aa);
            }
            PlayerPrefs.Save();
            GameManager.Instance.SwitchStates(GameManager.GameState.levelCompleted, 0.5f);
            #endregion

        }

    }
    void InitializeVariables(string difficultyLevel)
    {
        if (difficultyLevel == "Easy")
        {
            startTime = 180f;
            health = 8;
            Debug.Log(health);
        }
        if (difficultyLevel == "Medium")
        {
            startTime = 180f;
            health = 6;
        }
        if (difficultyLevel == "Hard")
        {
            startTime = 240f;
            health = 4;
        }
        isPoemCorrect = false;
        _lives = 3;
    }
    void SendGameOverAnalytics()
    {

        if (LS_Buttons.difficultyLevel == "Easy")
        {

            AnalyticsResult aa = Analytics.CustomEvent("GameOver", new Dictionary<string, object>() {
                    { "Easy", easyLevel }
                    }
            );
            Debug.Log("AnalyticsResult: " + aa);


        }
        else if (LS_Buttons.difficultyLevel == "Medium")
        {

            AnalyticsResult aa = Analytics.CustomEvent("GameOver", new Dictionary<string, object>() {
                    { "Medium", mediumLevel }
                    }
            );
            Debug.Log("AnalyticsResult: " + aa);
        }
        else if (LS_Buttons.difficultyLevel == "Hard")
        {

            AnalyticsResult aa = Analytics.CustomEvent("GameOver", new Dictionary<string, object>() {
                    { "Hard", hardLevel }
                    }
            );
            Debug.Log("AnalyticsResult: " + aa);
        }

    }
    #region CheckPoem
    void Checked(string difficultyLevel)
    {

        if (difficultyLevel == "Easy")
        {
            int i = 0;
            for (i = 0; i < phrases.Length; i++)
            {
                if (!phrases[i].GetComponent<DragDrop>().IsPositionCorrect())
                {
                    count++;
                    StartCoroutine(ChnageWrongPhraseColor(i, 5f));
                }

            }
            if (count == 0)
            {
                isPoemCorrect = true;
            }
        }
        else if (difficultyLevel == "Medium")
        {

            int i = 0;
            for (i = 0; i < phrases.Length; i++)
            {
                if (!phrases[i].GetComponent<DragDrop>().IsPositionCorrect())
                {
                    count++;
                    StartCoroutine(ChnageWrongPhraseColor(i, 5f));
                }

            }
            if (count == 0)
            {
                isPoemCorrect = true;
            }
        }
        else if (difficultyLevel == "Hard")
        {

            int i = 0;
            for (i = 0; i < phrases.Length; i++)
            {
                if (!phrases[i].GetComponent<DragDrop>().IsPositionCorrect())
                {
                    count++;
                    StartCoroutine(ChnageWrongPhraseColor(i, 10f));

                }

            }
            if (count == 0)
            {
                isPoemCorrect = true;
            }
        }

    }
    IEnumerator ChnageWrongPhraseColor(int index, float highlight_time)
    {
        phrases[index].GetComponentInChildren<Text>().color = Color.red;
        yield return new WaitForSeconds(highlight_time);
        phrases[index].GetComponentInChildren<Text>().color = defaultColor;

    }
    #endregion
}
