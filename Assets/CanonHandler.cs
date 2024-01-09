using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonHandler : MonoBehaviour
{

    private float speed = 5f;
    [SerializeField] private GameObject fireball_prefab = null;
    [SerializeField] private GameObject pencil_prefab = null;
    [SerializeField] private GameObject projectie_parent = null;
    [SerializeField] private RectTransform[] fireballSpawnPoints;
    [SerializeField] private Vector3[] fireBallPositions;

    //[SerializeField] private AudioSource preShotAudio = null;
    [SerializeField] private AudioSource shotAudio = null;

    private RectTransform rectTransform;

    //private List<Vector3>

    private Vector2 force = new Vector2(20f, 0);

    private bool hasTarget = false;
    private float timer = 10f;                 //time after which cannon must change direction
    private Vector3 moveCannonPos = new Vector3();
    private int min_launch_delay = 10;
    private int max_launch_delay = 15;



    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        gameObject.GetComponent<RectTransform>().Translate(0f, speed * Time.deltaTime, 0f);

        if (LS_Buttons.difficultyLevel == "Easy")
        {
            min_launch_delay = 14;
            max_launch_delay = 18;
        }
        if (LS_Buttons.difficultyLevel == "Medium")
        {
            min_launch_delay = 12;
            max_launch_delay = 16;
        }
        if (LS_Buttons.difficultyLevel == "Hard")
        {
            min_launch_delay = 10;
            max_launch_delay = 15;
        }
        InvokeRepeating("LaunchProjectile", 2f, Random.Range(min_launch_delay, max_launch_delay));


        //Setting up the array of shooting positions
        for (int i = 0; i < fireballSpawnPoints.Length; i++)
        {
            fireBallPositions[i] = fireballSpawnPoints[i].position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasTarget)
        {
            gameObject.GetComponent<RectTransform>().Translate(0f, speed * Time.deltaTime, 0f);
        }


        //timer -= Time.deltaTime;

        /*  if (timer < 0)
          {
              speed = -speed;
              timer = 10f;
              Debug.Log("Direction Changed");
          }*/


        //gameObject.GetComponent<RectTransform>().Translate(0f, speed * Time.deltaTime, 0f);
        if (rectTransform.anchoredPosition.y >= 300f
                || rectTransform.anchoredPosition.y <= -350f)
        {
            speed = -speed;
        }
        /*if (rectTransform.position == fireballSpawnPoints[0].position ||
                    rectTransform.position == fireballSpawnPoints[1].position ||
                               rectTransform.position == fireballSpawnPoints[2].position)
        {
            LaunchProjectile();
        }*/
    }


    private void LaunchProjectile()
    {
        hasTarget = true;
        moveCannonPos = fireBallPositions[Random.Range(0, fireBallPositions.Length)];

        StartCoroutine("moveTimeDelay");

        /*if (gameObject.name == "L_Canon")
        {
            GameObject fireballPrefabInstance = Instantiate(fireball_prefab, gameObject.transform.position + new Vector3(100f, 0f, 0f),
                                                    Quaternion.identity, projectie_parent.transform);
            fireballPrefabInstance.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
        }
        else if (gameObject.name == "R_Canon")
        {
            GameObject fireballPrefabInstance = Instantiate(fireball_prefab, gameObject.transform.position - new Vector3(100f, 0f, 0f),
                                                   Quaternion.LookRotation(new Vector3(0f, 0f, 180f)), projectie_parent.transform);
            fireballPrefabInstance.GetComponent<Rigidbody2D>().AddForce(-force, ForceMode2D.Impulse);
        }*/


    }
    /* void LaunchPencil()
     {
         GameObject pencilPrefabInstance = Instantiate(pencil_prefab, gameObject.transform.position + new Vector3(100f, 0f, 0f),
                                                    pencil_prefab.GetComponent<RectTransform>().rotation, projectie_parent.transform);
         pencilPrefabInstance.GetComponent<RectTransform>().LeanMoveX(500f, 2f);
     }*/

    private IEnumerator moveTimeDelay()
    {
        LeanTween.move(gameObject, moveCannonPos, 2f).setEase(LeanTweenType.easeOutExpo);       //2 seconds to reach the required position

        yield return new WaitForSeconds(1f);
       // preShotAudio.Play();

        yield return new WaitForSeconds(2f);

        if (gameObject.name == "L_Canon")
        {
            GameObject fireballPrefabInstance = Instantiate(fireball_prefab, gameObject.transform.position,
                                                    Quaternion.identity, projectie_parent.transform);
            fireballPrefabInstance.GetComponent<Transform>().localScale = new Vector3(-1f, 1f, 1f);
            fireballPrefabInstance.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);

        }
        else if (gameObject.name == "R_Canon")
        {
            GameObject fireballPrefabInstance = Instantiate(fireball_prefab, gameObject.transform.position,
                                                   Quaternion.LookRotation(new Vector3(0f, 0f, 180f)), projectie_parent.transform);
            fireballPrefabInstance.GetComponent<Rigidbody2D>().AddForce(-force, ForceMode2D.Impulse);
        }
        shotAudio.Play();

        hasTarget = false;

    }
}
