using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    //  private bool has_time_left = true;
    [SerializeField] private GameObject pauseMenu;
    //[SerializeField] private float startTime = 10000f;
    [SerializeField] private GameObject levelSelectMenu;
    [SerializeField] private GameObject levelCompletedMenu;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject[] levelsPrefab;
    [SerializeField] private Transform parentCanvas;
    [SerializeField] private bool _isswitchingState;
    [SerializeField] private AudioSource buttonClick;
    [SerializeField] private AudioSource backgroundMusic;
    // [SerializeField] private SelectPoemLevel themesMenuManager;
    private LevelElementsManager levelElementsManager;
    private GameObject currentLevel;
    public static GameManager Instance;
    public bool is_gameover = false;

    public GameState State;



    private int _poemlevel;
    public int poemlevel
    {
        get { return _poemlevel; }
        set
        {
            _poemlevel = value;
        }
    }



    private void Start()
    {
        Instance = this;
        SwitchStates(GameState.levelSelect);
        backgroundMusic.Play();
    }
    public void SwitchStates(GameState newState, float delay = 0)
    {
        StartCoroutine(SwitchDelay(newState, delay));
    }
    IEnumerator SwitchDelay(GameState newState, float delay)
    {

        _isswitchingState = true;
        yield return new WaitForSeconds(delay);
        EndState();
        State = newState;
        BeginState();
        _isswitchingState = false;
    }
    private void BeginState()
    {

        switch (State)
        {
            case GameState.levelSelect:
                levelSelectMenu.SetActive(true);
                // Update the level select menu ( color change for themes that are completed)
                break;
            case GameState.loadLevel:
                backgroundMusic.Stop();
                currentLevel = Instantiate(levelsPrefab[poemlevel], parentCanvas);
                currentLevel.SetActive(true);
                SwitchStates(GameState.playMode);
                break;
            case GameState.playMode:
                levelElementsManager = GameObject.Find("UIElementsManager").GetComponent<LevelElementsManager>();
                levelElementsManager.StartTimeDisplay();
                break;
            case GameState.levelCompleted:
                Destroy(currentLevel);
                //themes[poemlevel].GetComponent<Image>().color = Color.red;
                // SelectPoemLevel.Instance.themes[poemlevel].color = Color.red;
                levelCompletedMenu.SetActive(true);
                break;
            case GameState.gameOver:
                Destroy(currentLevel);
                gameOverMenu.SetActive(true);
                break;
            default:
                break;
        }
    }
    private void Update()
    {
        switch (State)
        {
            case GameState.levelSelect:
                break;
            case GameState.loadLevel:
                break;
            case GameState.playMode:
                //  if(levelElementsManager._timeLeft == 0 || levelElementsManager._lives == 0){ SwitchStates(GameState.gameOver, 1f); }
                break;
            case GameState.levelCompleted:
                break;
            case GameState.gameOver:
                break;
            default:
                break;
        }

    }
    private void EndState()
    {

        switch (State)
        {
            case GameState.levelSelect:
                levelSelectMenu.SetActive(false);
                break;
            case GameState.loadLevel:
                break;
            case GameState.playMode:
                if (currentLevel != null) { currentLevel.SetActive(false); }
                break;
            case GameState.levelCompleted:
                levelCompletedMenu.SetActive(false);
                break;
            case GameState.gameOver:
                gameOverMenu.SetActive(false);
                break;
            default:
                break;
        }
    }
    public void OnPauseClicked()
    {
        buttonClick.Play();
        //currentLevel.SetActive(false);
        pauseMenu.SetActive(true);
        currentLevel.GetComponent<CanvasGroup>().alpha = 0;
        currentLevel.GetComponent<CanvasGroup>().blocksRaycasts = false;
        Time.timeScale = 0;
    }
    public void OnResumeClicked()
    {
        buttonClick.Play();
        pauseMenu.SetActive(false);
        currentLevel.GetComponent<CanvasGroup>().alpha = 1;
        currentLevel.GetComponent<CanvasGroup>().blocksRaycasts = true;
        //currentLevel.SetActive(true);
        Time.timeScale = 1;
    }
    public void LoadMainMenu()
    {
        buttonClick.Play();
        //  Destroy(currentLevel);
        SceneManager.LoadSceneAsync(0);
        //Invoke("LoadMenuScene", 1f);
    }
    private void LoadMenuScene()
    {
        SceneManager.LoadScene(0);
    }
    public void LoadDifficultyMenu()
    {
        buttonClick.Play();
        SceneManager.LoadScene("LevelSelect");
    }
    public void LoadThemeMenu()
    {
        Time.timeScale = 1;
        buttonClick.Play();
        if (currentLevel != null) { Destroy(currentLevel); }
        pauseMenu.SetActive(false);
        this.SwitchStates(GameState.levelSelect, 0.5f);
    }

    public void TryAgain()
    {
        buttonClick.Play();
        SwitchStates(GameState.loadLevel, 0.5f);
    }
    public enum GameState
    {
        levelSelect,
        loadLevel,
        playMode,
        levelCompleted,
        gameOver

    }
}
