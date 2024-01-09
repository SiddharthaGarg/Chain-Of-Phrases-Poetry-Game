using UnityEngine;
using UnityEngine.SceneManagement;

public class ProjectileHandler : MonoBehaviour
{

    public static int hit_count = 0;
    private TutorialElementsManager tutorialElementsManager;
    private LevelElementsManager levelElementsManager;
    //[SerializeField] private AudioSource collisionSound = null;
    //private Vector2 force = new Vector2(100f, 0f);

    private void Start()
    {
        //GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
        tutorialElementsManager = FindObjectOfType<TutorialElementsManager>();
        levelElementsManager = FindObjectOfType<LevelElementsManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Shooter"))
        {
            hit_count += 1;
            other.gameObject.GetComponent<AudioSource>().Play();
            if (SceneManager.GetActiveScene().name == "Tutorial")
            {
                tutorialElementsManager.ReduceHealth();
            }
            else
            {
                levelElementsManager.ReduceHealth();
            }
            Debug.Log(hit_count);
            Destroy(gameObject);
        }
    }
}
