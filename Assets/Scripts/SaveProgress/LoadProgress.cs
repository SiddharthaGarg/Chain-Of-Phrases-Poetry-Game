using UnityEngine;

public class LoadProgress : MonoBehaviour
{

    [SerializeField] GameObject[] Lock = null;
    private void OnEnable()
    {


        Lock[0].SetActive(false);
        if (LS_Buttons.difficultyLevel == "Easy")
        {
            if (PlayerPrefs.HasKey("Easy"))
            {
                for (int i = 1; i <= PlayerPrefs.GetInt("Easy") && i < Lock.Length; i++)
                {
                    Lock[i].SetActive(false);
                }
            }
        }
        else if (LS_Buttons.difficultyLevel == "Medium")
        {
            if (PlayerPrefs.HasKey("Medium"))
            {
                for (int i = 1; i <= PlayerPrefs.GetInt("Medium") && i < Lock.Length; i++)
                {
                    Lock[i].SetActive(false);
                }
            }
        }
        else if (LS_Buttons.difficultyLevel == "Hard")
        {
            if (PlayerPrefs.HasKey("Hard"))
            {
                for (int i = 1; i <= PlayerPrefs.GetInt("Hard") && i < Lock.Length; i++)
                {
                    Lock[i].SetActive(false);
                }
            }
        }

    }

}
