using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileManager : MonoBehaviour
{
    private Vector2 force = new Vector2(-100f, 0);
    [SerializeField] private ParticleSystem collisionEffect;
    [SerializeField] private GameObject collisionSound = null;


    private void Start()
    {
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            other.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            collisionSound.TryGetComponent<AudioSource>(out AudioSource collisionAudio);
            collisionAudio.Play();
            collisionSound.transform.SetParent(null);
            //  collisionEffect.Play();
            Destroy(gameObject);
            Destroy(other.gameObject);
            Destroy(collisionSound, 1f);
        }
        else if (other.CompareTag("Shooter") || other.gameObject.name == "Phrasemissile")
        {
            return;
        }
        else
        {
            collisionSound.TryGetComponent<AudioSource>(out AudioSource collisionAudio);
            collisionAudio.Play();
            collisionSound.transform.SetParent(null);
            Destroy(collisionSound, 1f);
            Destroy(gameObject);
        }

        


    }
}
