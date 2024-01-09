using UnityEngine;
using UnityEngine.EventSystems;

public class ShootButtonHandler : MonoBehaviour, IPointerClickHandler
{

    [SerializeField] private GameObject phrase_missile = null;
    [SerializeField] private Transform phrase_missile_parent = null;
    [SerializeField] private Transform shooter_position = null;
    [SerializeField] private AudioSource[] shotAudio = null;
    //[SerializeField] private AudioSource collisionSound = null;


    private Vector2 force = new Vector2(20f, 0);
    /*  public void OnClickShooterButton()
      {

          Debug.Log("Shoot button pressed");
          GameObject phraseMissileInstance = Instantiate(phrase_missile, shooter_position.position - new Vector3(80, 0f, 0f),
                                               Quaternion.identity, phrase_missile_parent);

      }
  */
    public void OnPointerClick(PointerEventData eventData)
    {
        int randomInt = Random.Range(0, 1);
        shotAudio[randomInt].Play();

        Debug.Log(eventData.pointerClick.gameObject.name + "pressed");
        if (eventData.pointerClick.gameObject.name.StartsWith("L_Shoot"))
        {
            GameObject phraseMissileInstance = Instantiate(phrase_missile, shooter_position.position - new Vector3(10f, 0f, 0f),
                                                                    Quaternion.identity, phrase_missile_parent);
            phraseMissileInstance.GetComponent<Rigidbody2D>().AddForce(-force, ForceMode2D.Impulse);
        }
        else if (eventData.pointerClick.gameObject.name.StartsWith("R_Shoot"))
        {
            GameObject phraseMissileInstance = Instantiate(phrase_missile, shooter_position.position + new Vector3(10f, 0f, 0f),
                                                                   Quaternion.identity, phrase_missile_parent);
            phraseMissileInstance.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);

        }

    }
}
