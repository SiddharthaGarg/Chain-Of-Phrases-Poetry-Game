using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class TutorialElementsManager : MonoBehaviour
{
    [SerializeField] private float startTime = 60f;
    [SerializeField] private Text time;
    [SerializeField] private GameObject[] Lives;
    [SerializeField] private GameObject healthBar;
    [SerializeField] private GameObject[] phrases;
    [SerializeField] private Color defaultColor;
    [SerializeField] private AudioSource buttonClick;
    [SerializeField] private AudioSource lowTime;
    [SerializeField] private AudioSource lifeLost;
    [SerializeField] private GameObject BGM1;
    [SerializeField] private GameObject BGM2;
    [SerializeField] private GameObject levelCompletedMenu;
    [SerializeField] private GameObject tryAgainScreen;
    [SerializeField] private GameObject currentLevelCanvas;
    [SerializeField] private Tutorial tutorial;
    private IEnumerator countTime;



    private int health;
    private int count = 0;
    private float bg_movingSpeed = 3f;
    private Vector3 startPos;
    private bool isGameOver = false;

    public bool poemChecked = false;
    public bool isPoemCorrect;

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
        InitializeVariables();
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
    private void Update()
    {

        if (_timeLeft == 0)
        {
            tryAgainScreen.SetActive(true);
            StartCoroutine(TryAgainCoroutine());
        }
    }

    public void ReduceHealth()
    {
        healthBar.GetComponent<Image>().fillAmount -= (1f / health);

        Debug.Log(healthBar.GetComponent<Image>().fillAmount);

        if (healthBar.GetComponent<Image>().fillAmount <= 0)
        {
            tryAgainScreen.SetActive(true);
            StartCoroutine(TryAgainCoroutine());
        }
    }
    public void StartTimeDisplay()
    {
        if (countTime != null)
        {
            StopCoroutine(DisplayCountdown());


        }
        countTime = DisplayCountdown();
        StartCoroutine(countTime);

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
        while (timeLeft > 0f && _lives > 0);

    }


    public void LoadMainMenu()
    {
        buttonClick.Play();
        SceneManager.LoadSceneAsync(0);
    }

    public void OnClickCheckPoem()
    {
        poemChecked = true;
        buttonClick.Play();
        count = 0;
        Checked();
        if (!isPoemCorrect)
        {
            lifeLost.Play();
            Debug.Log(count);
            _lives -= 1;
        }
        if (_lives == 0)
        {
            tryAgainScreen.SetActive(true);
            //StopCoroutine("DisplayCountdown");
            StartCoroutine(TryAgainCoroutine());
        }

    }

    private IEnumerator TryAgainCoroutine()
    {
        yield return new WaitForSeconds(3f);
        tryAgainScreen.SetActive(false);
        ResetHealthAndLives();
        tutorial.ChangeState();
    }

    void InitializeVariables()
    {
        startTime = 60f;
        health = 10;
        isPoemCorrect = false;
        _lives = 3;
    }
    #region CheckPoem
    void Checked()
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
    IEnumerator ChnageWrongPhraseColor(int index, float highlight_time)
    {
        phrases[index].GetComponentInChildren<Text>().color = Color.red;
        yield return new WaitForSeconds(highlight_time);
        phrases[index].GetComponentInChildren<Text>().color = defaultColor;
        Debug.Log("Colour set to default");

    }
    #endregion

    public void ResetHealthAndLives()
    {
        healthBar.GetComponent<Image>().fillAmount = 1;
        _lives = 3;
        for (int i = 0; i < 3; i++)
        {
            Lives[i].SetActive(true);
        }
    }
}
