using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class LevelGenerator : MonoBehaviour
{
    public GameObject platform;
    public GameObject clock;
    public GameObject rainbow;

    public Transform genPoint;
    public BoxCollider2D boxCol;
    public Transform cam;
    public float distance;
    float prevDist;

    public int RainbowChance = 100;
    public int ClockChance = 100;

    float distMin = 3;
    float distMax = 6.5f;
    private float platformWidth;

    float currPlatform = 0;
    float prevPlatform = 0;
    float platMin = -4.0f;

    int ColorRandom = 0;
    int Size = 0;

    public Sprite Red;
    public Sprite Orange;
    public Sprite Yellow;
    public Sprite Green;
    public Sprite Blue;
    public Sprite Purple;
    public Sprite Rosa;

    public Sprite RedBig;
    public Sprite OrangeBig;
    public Sprite YellowBig;
    public Sprite GreenBig;
    public Sprite BlueBig;
    public Sprite PurpleBig;
    public Sprite RosaBig;

    public Sprite RedMedium;
    public Sprite OrangeMedium;
    public Sprite YellowMedium;
    public Sprite GreenMedium;
    public Sprite BlueMedium;
    public Sprite PurpleMedium;
    public Sprite RosaMedium;

    public GameObject Background;

    public Light2D light = new Light2D();

    void Start()
    {
        platformWidth = platform.GetComponent<BoxCollider2D>().size.x;
    }

    void Update()
    {
        if (transform.position.x < genPoint.position.x)
        {
            ColorRandom = Random.Range(1, 8);
            Size = Random.Range(1, 4);

            if (Size == 1)
            {
                boxCol.size = new Vector2(0.32f, 0.075f);
            }
            else if (Size == 2)
            {
                boxCol.size = new Vector2(0.48f, 0.075f);
            }
            else if (Size == 3)
            {
                boxCol.size = new Vector2(0.64f, 0.075f);
            }
            if (ColorRandom == 1)
            {
                if (Size == 1)
                {
                    platform.GetComponent<SpriteRenderer>().sprite = Red;
                }
                else if (Size == 2)
                {
                    platform.GetComponent<SpriteRenderer>().sprite = RedMedium;
                }
                else if (Size == 3)
                {
                    platform.GetComponent<SpriteRenderer>().sprite = RedBig;
                }
                light.intensity = 0.5f;
            }
            else if (ColorRandom == 2)
            {
                if (Size == 1)
                {
                    platform.GetComponent<SpriteRenderer>().sprite = Orange;
                }
                else if (Size == 2)
                {
                    platform.GetComponent<SpriteRenderer>().sprite = OrangeMedium;
                }
                else if (Size == 3)
                {
                    platform.GetComponent<SpriteRenderer>().sprite = OrangeBig;
                }
                light.intensity = 0.5f;
            }
            else if (ColorRandom == 3)
            {
                if (Size == 1)
                {
                    platform.GetComponent<SpriteRenderer>().sprite = Yellow;
                }
                else if (Size == 2)
                {
                    platform.GetComponent<SpriteRenderer>().sprite = YellowMedium;
                }
                else if (Size == 3)
                {
                    platform.GetComponent<SpriteRenderer>().sprite = YellowBig;
                }
                light.intensity = 0.5f;
            }
            else if (ColorRandom == 4)
            {
                if (Size == 1)
                {
                    platform.GetComponent<SpriteRenderer>().sprite = Green;
                }
                else if (Size == 2)
                {
                    platform.GetComponent<SpriteRenderer>().sprite = GreenMedium;
                }
                else if (Size == 3)
                {
                    platform.GetComponent<SpriteRenderer>().sprite = GreenBig;
                }
                light.intensity = 0.5f;
            }
            else if (ColorRandom == 5)
            {
                if (Size == 1)
                {
                    platform.GetComponent<SpriteRenderer>().sprite = Blue;
                }
                else if (Size == 2)
                {
                    platform.GetComponent<SpriteRenderer>().sprite = BlueMedium;
                }
                else if (Size == 3)
                {
                    platform.GetComponent<SpriteRenderer>().sprite = BlueBig;
                }
                light.intensity = 0.5f;
            }
            else if (ColorRandom == 6)
            {
                if (Size == 1)
                {
                    platform.GetComponent<SpriteRenderer>().sprite = Purple;
                }
                else if (Size == 2)
                {
                    platform.GetComponent<SpriteRenderer>().sprite = PurpleMedium;
                }
                else if (Size == 3)
                {
                    platform.GetComponent<SpriteRenderer>().sprite = PurpleBig;
                }
                light.intensity = 0.5f;
            }
            else if (ColorRandom == 7)
            {
                if (Size == 1)
                {
                    platform.GetComponent<SpriteRenderer>().sprite = Rosa;
                }
                else if (Size == 2)
                {
                    platform.GetComponent<SpriteRenderer>().sprite = RosaMedium;
                }
                else if (Size == 3)
                {
                    platform.GetComponent<SpriteRenderer>().sprite = RosaBig;
                }
                light.intensity = 0.5f;
            }


            currPlatform = Random.Range(platMin, prevPlatform + 2);
            if (currPlatform < prevPlatform && platMin < 2.5)
            {
                platMin += Random.Range(0.5f, 1.5f);
            }
            else if (currPlatform > prevPlatform && platMin > -3)
            {
                platMin -= Random.Range(0.5f, 1.5f);
            }

            distance = Random.Range(distMin, distMax);
            if (distance < prevDist && distMin < 7f)
            {
                distMin += Random.Range(0.5f, 1.5f);
            }
            else if (distance > prevDist && distMax > 3)
            {
                distMax -= Random.Range(0.5f, 1.5f);
            }

            transform.position = new Vector3(transform.position.x + platformWidth + distance, currPlatform, 0);

            prevPlatform = currPlatform;
            prevDist = distance;

            Instantiate(platform, transform.position, transform.rotation);

            if (Random.Range(1, RainbowChance) == 1)
            {
                Instantiate(rainbow, new Vector3(transform.position.x, transform.position.y + 0.75f, 0), transform.rotation);
            }
            else if (Random.Range(1, ClockChance) == 1)
            {
                Instantiate(clock, new Vector3(transform.position.x, transform.position.y + 0.75f, 0), transform.rotation);
            }
        }
    }
}
