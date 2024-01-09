using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;
public class MM_Buttons : MonoBehaviour
{
    [SerializeField] private GameObject b_Start;
    [SerializeField] private GameObject b_Tutorial;
    [SerializeField] private GameObject b_Credits;
    [SerializeField] private GameObject b_QuitGame;

    [SerializeField] private GameObject creditsPanel;
    //  [SerializeField] private GameObject howtoplayPanel;
    //  [SerializeField] private Sprite[] howToPlayPages;
    [SerializeField] private AudioSource[] soundsList;



    //private RectTransform buttonTransform;
    private float yPos_Start = 80f;
    private float yPos_HowToPlay = -30f;
    private float yPos_Credits = -140f;
    private float yPos_QuitGame = -250f;
    private float tweenTime = 2f;
    private int pageNumber = 0;        //0,1,2

    private void Awake()
    {
        //howtoplayPanel.GetComponent<RectTransform>().localPosition = new Vector2(-Screen.width, 0);
        b_Start.GetComponent<RectTransform>().localPosition = new Vector2(0, Screen.height);
        b_Tutorial.GetComponent<RectTransform>().localPosition = new Vector2(0, Screen.height);
        b_Credits.GetComponent<RectTransform>().localPosition = new Vector2(0, -Screen.height);
        b_QuitGame.GetComponent<RectTransform>().localPosition = new Vector2(0, -Screen.height);
        //  howtoplayPanel.SetActive(true);
        //howtoplayPanel.GetComponent<RectTransform>().localPosition = new Vector2(-Screen.width, 0);
        //creditsPanel.SetActive(true);
        creditsPanel.GetComponent<RectTransform>().localPosition = new Vector2(-Screen.width, 0);
    }

    void Start()
    {
        b_Start.GetComponent<RectTransform>().LeanMoveLocalY(yPos_Start, tweenTime).setEaseOutExpo().delay = 0.1f;
        b_Tutorial.GetComponent<RectTransform>().LeanMoveLocalY(yPos_HowToPlay, tweenTime).setEaseOutExpo().delay = 0.1f;
        b_Credits.GetComponent<RectTransform>().LeanMoveLocalY(yPos_Credits, tweenTime).setEaseOutExpo().delay = 0.1f;
        b_QuitGame.GetComponent<RectTransform>().LeanMoveLocalY(yPos_QuitGame, tweenTime).setEaseOutExpo().delay = 0.1f;
    }

    public void PlayGame()
    {
        soundsList[0].Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Tutorial()
    {
        soundsList[0].Play();
        SceneManager.LoadScene(5);
        // howtoplayPanel.SetActive(true);
        // howtoplayPanel.GetComponent<RectTransform>().LeanMoveLocalX(0, tweenTime).setEaseOutQuad();
    }

    public void Credits()
    {
        creditsPanel.SetActive(true);
        soundsList[0].Play();
        creditsPanel.GetComponent<RectTransform>().LeanMoveLocalX(0, tweenTime).setEaseOutQuad();
    }
    public void QuitGame()
    {
        soundsList[0].Play();
        //PlayerPrefs.DeleteAll();
        //if (Application.platform == RuntimePlatform.WindowsEditor) { UnityEditor.EditorApplication.isPlaying = false; }
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            Application.Quit();
            Application.OpenURL("https://aquafy.tech/");
        }
        else { Application.Quit(); }
    }
    public void LearnMoreAboutUs()
    {
        //Application.Quit();
        Application.OpenURL("https://aquafy.tech/");
        AnalyticsResult aa = Analytics.CustomEvent("gtw_conversion");
        Debug.Log(aa);

    }

    /*  public void BackH2P()
      {
          soundsList[0].Play();
         // howtoplayPanel.GetComponent<RectTransform>().LeanMoveLocalX(Screen.width, tweenTime).setEaseInQuad();
          pageNumber = 0;
          StartCoroutine(TweenTimeDelay());

      }*/

    public void BackCredits()
    {
        soundsList[0].Play();
        creditsPanel.GetComponent<RectTransform>().LeanMoveLocalX(Screen.width, tweenTime).setEaseInQuad();
        StartCoroutine(TweenTimeDelay());

    }

    /* public void MoveRight()
     {
         if (pageNumber == 0)
         {
             soundsList[1].Play();
             pageNumber = 1;
             howtoplayPanel.GetComponent<Image>().sprite = howToPlayPages[1];
             return;
         }

         if (pageNumber == 1)
         {
             soundsList[1].Play();
             pageNumber = 2;
             howtoplayPanel.GetComponent<Image>().sprite = howToPlayPages[2];
             return;
         }
         else
         {
             soundsList[2].Play();
             return;
         }
     }

     public void MoveLeft()
     {
         if (pageNumber == 1)
         {
             soundsList[1].Play();
             pageNumber = 0;
             howtoplayPanel.GetComponent<Image>().sprite = howToPlayPages[0];
             return;
         }

         if (pageNumber == 2)
         {
             soundsList[1].Play();
             pageNumber = 1;
             howtoplayPanel.GetComponent<Image>().sprite = howToPlayPages[1];
             return;
         }
         else
         {
             soundsList[2].Play();
             return;
         }
     }
 */
    IEnumerator TweenTimeDelay()        //Delay to let the previous animation finish
    {
        yield return new WaitForSeconds(tweenTime);
        /*   if (howtoplayPanel.activeInHierarchy)
           {
               howtoplayPanel.GetComponent<RectTransform>().localPosition = new Vector2(-Screen.width, 0);
               howtoplayPanel.SetActive(false);
           }*/
        if (creditsPanel.activeInHierarchy)
        {
            creditsPanel.GetComponent<RectTransform>().localPosition = new Vector2(-Screen.width, 0);
            creditsPanel.SetActive(false);
        }
    }
}
