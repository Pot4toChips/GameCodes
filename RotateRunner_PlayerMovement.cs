using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float rotateSpeed;
    public float speed;
    float rotateDir;
    public GameObject center;
    public static int chanceToBarrier = 50;
    public ParticleSystem particle;
    public Text pointText;
    float speedHolder;

    public GameObject posIn;
    public GameObject posOut;
    public GameObject menu;
    public GameObject spawnPos;
    public Text highscoreText;
    bool isMenu = true;
    bool start = false;
    public GameObject cube;
    int highscore = 0;

    int point;
    float nextPoint;

    bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 120;

        highscore = PlayerPrefs.GetInt("Highscore");
        highscoreText.text = "Highscore: " + highscore.ToString();
        speedHolder = speed;
        speed = 0;
        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (highscore < point)
        {
            highscore = point;
            highscoreText.text = "Highscore: "+highscore.ToString();
            PlayerPrefs.SetInt("Highscore", highscore);
        }
        if (isMenu == true)
        {
            menu.transform.position = Vector2.Lerp(menu.transform.position, posIn.transform.position, 0.3f);
        }
        else
        {
            menu.transform.position = Vector2.Lerp(menu.transform.position, posOut.transform.position, 0.3f);
        }
        if (isMenu == false)
        {
            if (speed != speedHolder && point < 4)
            {
                speed = Mathf.Lerp(speed, speedHolder, 0.03f);
                if (speed >= speedHolder - 0.01f)
                {
                    speed = speedHolder;
                }
            }
            pointText.text = point.ToString();

            nextPoint += Time.deltaTime;
            if (nextPoint > 1 && gameOver == false)
            {
                point++;
                nextPoint = 0;
                speed += 0.02f;
            }
            if (gameOver == false)
            {
                //Android Input
                if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);
                    if (touch.position.x < Screen.width / 2)
                    {
                        rotateDir = Mathf.Lerp(rotateDir, 1, 0.25f);
                        if (rotateDir > 0.999f)
                        {
                            rotateDir = 1;
                        }
                    }
                    else
                    {
                        rotateDir = Mathf.Lerp(rotateDir, -1, 0.25f);
                        if (rotateDir < -0.999f)
                        {
                            rotateDir = -1;
                        }
                    }
                }
                //PC Input
                /*
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    rotateDir = Mathf.Lerp(rotateDir, 1, 0.05f);
                    if (rotateDir > 0.999f)
                    {
                        rotateDir = 1;
                    }
                }
                else if (Input.GetKey(KeyCode.RightArrow))
                {
                    rotateDir = Mathf.Lerp(rotateDir, -1, 0.05f);
                    if (rotateDir < -0.999f)
                    {
                        rotateDir = -1;
                    }
                }*/
                else
                {
                    rotateDir = Mathf.Lerp(rotateDir, 0, 0.25f);
                    if (rotateDir < 0.001f && rotateDir > -0.001f)
                    {
                        rotateDir = 0;
                    }
                }
                transform.RotateAround(center.transform.position, Vector3.forward, rotateDir * rotateSpeed);
            }
            else
            {
                speed = Mathf.Lerp(speed, 0, 0.25f);
                if (speed < 0.001f)
                {
                    speed = 0;
                }
            }
            transform.Translate(0, 0, speed);
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Barrier")
        {
            gameOver = true;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<TrailRenderer>().enabled = false;
            particle.Play();
            isMenu = true;
        }
    }
    public void Retry()
    {
        if (start == true)
        {
            GameObject[] barriers = GameObject.FindGameObjectsWithTag("Barrier");
            for (int i = 0; i < barriers.Length; i++)
            {
                Destroy(barriers[i]);
            }
            gameOver = false;
            point = 0;
            pointText.text = "0";
            speed = speedHolder;
            gameObject.GetComponent<MeshRenderer>().enabled = true;
            gameObject.GetComponent<TrailRenderer>().enabled = true;
            spawnPos.transform.position = new Vector3(0, 0, 60);
            transform.position = new Vector3(0, 3, 0);
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }
    }
    public void ChangeFlow(bool inOrOut)
    {
        isMenu = inOrOut;
        Retry();
        start = true;
    }
}
