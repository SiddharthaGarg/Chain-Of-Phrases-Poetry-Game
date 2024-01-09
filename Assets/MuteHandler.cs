
using UnityEngine;
using UnityEngine.UI;
public class MuteHandler : MonoBehaviour
{
    // Start is called before the first frame update
    private Toggle myToggle;
    void Start()
    {
        myToggle = GetComponent<Toggle>();
        if (AudioListener.volume == 0)
        {
            myToggle.isOn = false;
        }
    }

    // Update is called once per frame
    public void OnToggleVolumeChanged(bool audioIn)
    {

        if (audioIn)
        {
            AudioListener.volume = 1;

        }
        else
        {
            AudioListener.volume = 0;
        }

    }
}
