using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PoemTextManager : MonoBehaviour
{
    [SerializeField] private Text[] phrases;
    private void Awake()
    {
        //if(SceneManager.GetActiveScene().ToString()=="EasyLevel"
        SetText();
    }
    // Update is called once per frame
    void Update()
    {

    }
    void SetText()
    {

        phrases[0].text = "I'm dreaming of a new body";
        phrases[1].text = "with every chocolate\nI unwrap";
        phrases[2].text = "But I can't stop eating";
        phrases[3].text = "I can't stop cheating";
        phrases[4].text = "There's just too many\nChristmas snacks";

    }
    /*
    I'm dreaming of a new body
with every chocolate I unwrap.
But I can't stop eating, 
I can't stop cheating. 
There's just too many Christmas snacks.
*/
}
