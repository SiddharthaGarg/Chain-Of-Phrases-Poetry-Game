
using UnityEngine;

public class SelectPoemLevel : MonoBehaviour
{
    [SerializeField] private AudioSource buttonClick;

    public void LoadTheme1()
    {
        buttonClick.Play();
        GameManager.Instance.poemlevel = 0;
        SetPoemLevel();
        if (!PlayerPrefs.HasKey(LS_Buttons.difficultyLevel) || PlayerPrefs.GetInt(LS_Buttons.difficultyLevel) >= GameManager.Instance.poemlevel)
        {
            GameManager.Instance.SwitchStates(GameManager.GameState.loadLevel, 0.5f);
        }
        else
        {
            Debug.Log("Complete previous levels first!");
        }
    }
    public void LoadTheme2()
    {
        buttonClick.Play();
        GameManager.Instance.poemlevel = 1;
        SetPoemLevel();
        if (PlayerPrefs.GetInt(LS_Buttons.difficultyLevel) >= GameManager.Instance.poemlevel)
        {
            GameManager.Instance.SwitchStates(GameManager.GameState.loadLevel, 0.5f);
        }
        else
        {
            Debug.Log("Complete previous levels first!");
        }
    }
    public void LoadTheme3()
    {
        buttonClick.Play();
        GameManager.Instance.poemlevel = 2;
        SetPoemLevel();
        if (PlayerPrefs.GetInt(LS_Buttons.difficultyLevel) >= GameManager.Instance.poemlevel)
        {
            GameManager.Instance.SwitchStates(GameManager.GameState.loadLevel, 0.5f);
        }
        else
        {
            Debug.Log("Complete previous levels first!");
        }
    }
    public void LoadTheme4()
    {
        buttonClick.Play();
        GameManager.Instance.poemlevel = 3;
        SetPoemLevel();
        if (PlayerPrefs.GetInt(LS_Buttons.difficultyLevel) >= GameManager.Instance.poemlevel)
        {
            GameManager.Instance.SwitchStates(GameManager.GameState.loadLevel, 0.5f);
        }
        else
        {
            Debug.Log("Complete previous levels first!");
        }
    }
    public void LoadTheme5()
    {
        buttonClick.Play();
        GameManager.Instance.poemlevel = 4;
        SetPoemLevel();
        if (PlayerPrefs.GetInt(LS_Buttons.difficultyLevel) >= GameManager.Instance.poemlevel)
        {
            GameManager.Instance.SwitchStates(GameManager.GameState.loadLevel, 0.5f);
        }
        else
        {
            Debug.Log("Complete previous levels first!");
        }
    }

    void SetPoemLevel()
    {

        if (LS_Buttons.difficultyLevel == "Easy") { LevelElementsManager.easyLevel = GameManager.Instance.poemlevel; }
        else if (LS_Buttons.difficultyLevel == "Medium") { LevelElementsManager.mediumLevel = GameManager.Instance.poemlevel; }
        else if (LS_Buttons.difficultyLevel == "Hard") { LevelElementsManager.hardLevel = GameManager.Instance.poemlevel; }
    }
}
