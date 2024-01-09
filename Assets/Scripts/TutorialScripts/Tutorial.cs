using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class Tutorial : MonoBehaviour
{
    private int[] tutorialState = null;
    [SerializeField] private GameObject[] gameObjectArray = null;
    [SerializeField] private GameObject[] stateObjectArray = null;

    [SerializeField] private TutorialElementsManager tutorialElementsManager = null;


    /*
     *0 = timer
     *1 = left cannon
     *2 = right cannon
     *3 = phrase 1
     *4= Life And Colour Instructions
     *5 = Level Complete
     *6 = Next Button
     *7 = Arrow
     */

    private int currentState = 0;

    private void Start()
    {
        for (int i = 0; i < gameObjectArray.Length; i++)
        {
            gameObjectArray[i].SetActive(false);
        }

        currentState = 0;
        ChangeState();
    }

    public void Next()
    {
        currentState++;
        ChangeState();
        //StopCoroutine(NextPrompt(0.1f));
        gameObjectArray[7].SetActive(false);
    }

    public void ChangeState()
    {
        switch (currentState)
        {
            case 0:
                ClearStates();
                stateObjectArray[0].SetActive(true);
                gameObjectArray[3].SetActive(true);     //enabling phrase 1
                tutorialElementsManager.ResetHealthAndLives();
                gameObjectArray[6].SetActive(true);
                StartCoroutine(NextPrompt(8f));
                break;

            case 1:
                ClearStates();
                stateObjectArray[1].SetActive(true);
                gameObjectArray[1].SetActive(true);             //cannon 1 active
                gameObjectArray[2].SetActive(true);             //cannon 2 active
                gameObjectArray[3].GetComponent<Animator>().enabled = false;
                gameObjectArray[3].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                tutorialElementsManager.ResetHealthAndLives();
                StartCoroutine(NextPrompt(10f));
                break;

            case 2:
                ClearStates();
                stateObjectArray[2].SetActive(true);
                gameObjectArray[2].SetActive(false);
                tutorialElementsManager.ResetHealthAndLives();
                StartCoroutine(NextPrompt(10f));
                break;

            case 3:
                ClearStates();
                stateObjectArray[3].SetActive(true);
                gameObjectArray[1].SetActive(false);
                gameObjectArray[2].SetActive(false);
                tutorialElementsManager.ResetHealthAndLives();
                StartCoroutine(NextPrompt(20f));
                break;

            case 4:
                ClearStates();
                stateObjectArray[4].SetActive(true);
                tutorialElementsManager.poemChecked = false;
                gameObjectArray[1].SetActive(false);             //cannon 1 deactive
                gameObjectArray[2].SetActive(false);             //cannon 2 deactive
                tutorialElementsManager.ResetHealthAndLives();
                StartCoroutine(NextPrompt(3f));
                break;

            case 5:
                ClearStates();
                stateObjectArray[5].SetActive(true);
                gameObjectArray[0].SetActive(true);             //setting time active
                gameObjectArray[1].SetActive(true);             //cannon 1 active
                gameObjectArray[2].SetActive(true);             //cannon 2 active
                tutorialElementsManager._timeLeft = 60f;
                tutorialElementsManager.StartTimeDisplay();
                tutorialElementsManager.ResetHealthAndLives();
                gameObjectArray[6].SetActive(false);
                break;
        }
    }

    private void Update()
    {
        if (currentState == 3)
        {
            if (tutorialElementsManager.poemChecked)
            {
                gameObjectArray[4].SetActive(true);
            }
        }

        if (currentState == 5)
        {
            if (tutorialElementsManager.isPoemCorrect)
            {
                gameObjectArray[5].SetActive(true);             //Tutorial Complete screen
            }

        }
    }
    private void ClearStates()
    {
        for (int n = 0; n < stateObjectArray.Length; n++)
        {
            stateObjectArray[n].SetActive(false);
        }
    }

    private IEnumerator NextPrompt(float time)
    {
        yield return new WaitForSeconds(time);
        gameObjectArray[7].SetActive(true);
    }

}
