using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] private AudioSource ballSound;

    private void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        ballSound.Play();
        if (other.CompareTag("PointZone"))
        {
           
            gameManager.Basket(transform.position);
        }
        else if (other.CompareTag("GameOverZone"))
        {
            gameManager.GameOver();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        ballSound.Play();
    }
}
