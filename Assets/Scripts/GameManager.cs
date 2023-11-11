using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject platform;

    [SerializeField] private Image[] missionImages;
    [SerializeField] private Sprite missionCompleteSprite;
    [SerializeField] private AudioSource[] sounds;
    [SerializeField] private ParticleSystem[] effects;
    [SerializeField] private GameObject[] panels;

    [SerializeField] private int needToWin;

    private int basketCount;

    private float horizontalInput;
    private float xRange = 1.45f;
    [SerializeField] private float speed;
    void Start()
    {
        basketCount = 0;
        for (int i = 0; i < needToWin; i++)
        {
            missionImages[i].gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale!=0)
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            platform.transform.Translate(Vector3.down * horizontalInput * speed);
            var tempVec = platform.transform.position;
            tempVec.x = Mathf.Clamp(tempVec.x, -xRange, xRange);
            platform.transform.position = tempVec;
        }
        
    }
    public void Basket(Vector3 ballPos)
    {
        effects[0].transform.position = ballPos;
        effects[0].Play();
        sounds[0].Play();
        missionImages[basketCount].sprite = missionCompleteSprite;
        basketCount++;
        
        if (basketCount == needToWin)
        {
            Win();
        }
    }
    public void GameOver()
    {
        sounds[2].Play();
        panels[2].SetActive(true);
        Time.timeScale = 0;
    }
    public void Win()
    {
        sounds[1].Play();
        panels[1].SetActive(true);
        Time.timeScale = 0;
    }
    public void Buttons(string value)
    {
        switch (value)
        {
            case "Stop":
                Time.timeScale = 0;
                panels[0].SetActive(true);
                break;

            case "Resume":
                Time.timeScale = 1;
                panels[0].SetActive(false);
                break;
            

            case "Restart":
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                Time.timeScale = 1;
                panels[0].SetActive(false);
                break;

            case "NextLevel":
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                Time.timeScale = 1;
                panels[1].SetActive(false);
                break;
            
            case "Quit":
                Application.Quit();
                break;
        }

    }
}
