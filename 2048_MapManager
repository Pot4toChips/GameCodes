using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    public static int[,] Map;
    private GameObject[,] MapTiles;
    public GameObject rectPrefab;

    private int[,] saveMap = new int[4, 4];
    private int saveScore;
    private bool needUndo;

    public GameObject rects;
    public GameObject[] tiles;

    private bool nextMove = true;

    bool once = true;
    bool saveOnce;

    private Vector2 startTouchPos;
    private Vector2 endTouchPos;
    private float touchAngle;

    private int score;
    private int highScore;
    public Text scoreText;
    public Text highScoreText;

    public Animator gameOverAnimation;
    private bool freeSpace;
    private bool gameOver;

    void Start()
    {
        highScore = PlayerPrefs.GetInt("highscore");
        highScoreText.text = highScore.ToString();

        Map = new int[4, 4]
        {
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
        };
        MapTiles = new GameObject[4, 4];

        SpawnTile();
        SpawnTile();
    }
    void Update()
    {
        if (gameOver == false)
        {
            //PC Input
            //ButtonControl();
            
            //Android Input
            SwipeControl();

            /*for (int i = 0; i < 4; i++)
            {
                Debug.Log(Map[i, 0] + " " + Map[i, 1] + " " + Map[i, 2] + " " + Map[i, 3]);
            }*/
        }
    }

    IEnumerator waitNextMove(float time)
    {
        yield return new WaitForSeconds(time);
        nextMove = true;
    }

    void SaveMap()
    {
        if (saveOnce == false)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int q = 0; q < 4; q++)
                {
                    saveMap[i, q] = Map[i, q];
                    saveScore = score;
                }
            }
            saveOnce = true;
        }
    }
    void SpawnOnce()
    {
        if (once == true)
        {
            SpawnTile();
            once = false;
        }
    }
    void MoveTiles(int iMin, int iMax, int qMin, int qMax, bool mDir, bool iqDir)
    {
        nextMove = false;
        StartCoroutine(waitNextMove(0.1f));

        for (int i = 0; i < 4; i++)
        {
            for (int q = 0; q < 4; q++)
            {
                if (MapTiles[i, q] != null)
                {
                    MapTiles[i, q].GetComponent<Rectangle>().used = false;
                }
            }
        }
        for (int i = iMin; i < iMax; i++)
        {
            for (int q = qMin; q < qMax; q++)
            {
                if (mDir == false)
                {
                    for (int m = 0; m < 3; m++)
                    {
                        if (iqDir == true)
                        {
                            if (Map[i, 2 - m] != 0 && Map[i, 3 - m] == 0)
                            {
                                SaveMap();

                                MapTiles[i, 2 - m].GetComponent<Rectangle>().target = tiles[i * 4 + 3 - m];
                                MapTiles[i, 2 - m].GetComponent<Rectangle>().dir = Vector3.right;
                                MapTiles[i, 3 - m] = MapTiles[i, 2 - m];
                                MapTiles[i, 2 - m] = null;

                                Map[i, 3 - m] = Map[i, 2 - m];
                                Map[i, 2 - m] = 0;

                                once = true;
                            }
                            else if (Map[i, 2 - m] != 0 && Map[i, 3 - m] != 0 && Map[i, 2 - m] == Map[i, 3 - m] && MapTiles[i, 2 - m].GetComponent<Rectangle>().used == false && MapTiles[i, 3 - m].GetComponent<Rectangle>().used == false)
                            {
                                SaveMap();

                                MapTiles[i, 2 - m].GetComponent<Rectangle>().target = tiles[i * 4 + 3 - m];
                                MapTiles[i, 2 - m].GetComponent<Rectangle>().dir = Vector3.right;
                                MapTiles[i, 2 - m].GetComponent<Rectangle>().valueChange = MapTiles[i, 3 - m];

                                Map[i, 3 - m] *= 2;
                                Map[i, 2 - m] = 0;

                                MapTiles[i, 3 - m].GetComponent<Rectangle>().used = true;
                                MapTiles[i, 2 - m].GetComponent<Rectangle>().changeValue = Map[i, 3 - m];
                                MapTiles[i, 2 - m].GetComponent<Rectangle>().destroy = true;

                                once = true;
                                CalcScore(Map[i, 3 - m]);
                            }
                        }
                        else
                        {
                            if (Map[2 - m, q] != 0 && Map[3 - m, q] == 0)
                            {
                                SaveMap();

                                MapTiles[2 - m, q].GetComponent<Rectangle>().target = tiles[(3 - m) * 4 + q];
                                MapTiles[2 - m, q].GetComponent<Rectangle>().dir = Vector3.down;
                                MapTiles[3 - m, q] = MapTiles[2 - m, q];
                                MapTiles[2 - m, q] = null;

                                Map[3 - m, q] = Map[2 - m, q];
                                Map[2 - m, q] = 0;

                                once = true;
                            }
                            else if (Map[2 - m, q] != 0 && Map[3 - m, q] != 0 && Map[2 - m, q] == Map[3 - m, q] && MapTiles[2 - m, q].GetComponent<Rectangle>().used == false && MapTiles[3 - m, q].GetComponent<Rectangle>().used == false)
                            {
                                SaveMap();

                                MapTiles[2 - m, q].GetComponent<Rectangle>().target = tiles[(3 - m) * 4 + q];
                                MapTiles[2 - m, q].GetComponent<Rectangle>().dir = Vector3.down;
                                MapTiles[2 - m, q].GetComponent<Rectangle>().valueChange = MapTiles[3 - m, q];

                                Map[3 - m, q] *= 2;
                                Map[2 - m, q] = 0;

                                MapTiles[3 - m, q].GetComponent<Rectangle>().used = true;
                                MapTiles[2 - m, q].GetComponent<Rectangle>().changeValue = Map[3 - m, q];
                                MapTiles[2 - m, q].GetComponent<Rectangle>().destroy = true;

                                once = true;
                                CalcScore(Map[3 - m, q]);
                            }
                        }
                    }
                }
                else
                {
                    for (int m = 2; m >= 0; m--)
                    {
                        if (iqDir == true)
                        {
                            if (Map[i, 3 - m] != 0 && Map[i, 2 - m] == 0)
                            {
                                SaveMap();

                                MapTiles[i, 3 - m].GetComponent<Rectangle>().target = tiles[i * 4 + 2 - m];
                                MapTiles[i, 3 - m].GetComponent<Rectangle>().dir = Vector3.left;
                                MapTiles[i, 2 - m] = MapTiles[i, 3 - m];
                                MapTiles[i, 3 - m] = null;

                                Map[i, 2 - m] = Map[i, 3 - m];
                                Map[i, 3 - m] = 0;

                                once = true;
                            }
                            else if (Map[i, 2 - m] != 0 && Map[i, 3 - m] != 0 && Map[i, 3 - m] == Map[i, 2 - m] && MapTiles[i, 2 - m].GetComponent<Rectangle>().used == false && MapTiles[i, 3 - m].GetComponent<Rectangle>().used == false)
                            {
                                SaveMap();

                                MapTiles[i, 3 - m].GetComponent<Rectangle>().target = tiles[i * 4 + 2 - m];
                                MapTiles[i, 3 - m].GetComponent<Rectangle>().dir = Vector3.left;
                                MapTiles[i, 3 - m].GetComponent<Rectangle>().valueChange = MapTiles[i, 2 - m];

                                Map[i, 2 - m] *= 2;
                                Map[i, 3 - m] = 0;

                                MapTiles[i, 2 - m].GetComponent<Rectangle>().used = true;
                                MapTiles[i, 3 - m].GetComponent<Rectangle>().changeValue = Map[i, 2 - m];
                                MapTiles[i, 3 - m].GetComponent<Rectangle>().destroy = true;

                                once = true;
                                CalcScore(Map[i, 2 - m]);
                            }
                        }
                        else
                        {
                            if (Map[3 - m, q] != 0 && Map[2 - m, q] == 0)
                            {
                                SaveMap();

                                MapTiles[3 - m, q].GetComponent<Rectangle>().target = tiles[(2 - m) * 4 + q];
                                MapTiles[3 - m, q].GetComponent<Rectangle>().dir = Vector3.up;
                                MapTiles[2 - m, q] = MapTiles[3 - m, q];
                                MapTiles[3 - m, q] = null;

                                Map[2 - m, q] = Map[3 - m, q];
                                Map[3 - m, q] = 0;

                                once = true;
                            }
                            else if (Map[2 - m, q] != 0 && Map[3 - m, q] != 0 && Map[3 - m, q] == Map[2 - m, q] && MapTiles[2 - m, q].GetComponent<Rectangle>().used == false && MapTiles[3 - m, q].GetComponent<Rectangle>().used == false)
                            {
                                SaveMap();

                                MapTiles[3 - m, q].GetComponent<Rectangle>().target = tiles[(2 - m) * 4 + q];
                                MapTiles[3 - m, q].GetComponent<Rectangle>().dir = Vector3.up;
                                MapTiles[3 - m, q].GetComponent<Rectangle>().valueChange = MapTiles[2 - m, q];

                                Map[2 - m, q] *= 2;
                                Map[3 - m, q] = 0;

                                MapTiles[2 - m, q].GetComponent<Rectangle>().used = true;
                                MapTiles[3 - m, q].GetComponent<Rectangle>().changeValue = Map[2 - m, q];
                                MapTiles[3 - m, q].GetComponent<Rectangle>().destroy = true;

                                once = true;
                                CalcScore(Map[2 - m, q]);
                            }
                        }
                    }
                }
            }
        }
        needUndo = true;
        SpawnOnce();
        CheckGameOver();
        saveOnce = false;
    }
    void SpawnTile()
    {
        while (true)
        {
            int value = Random.Range(0, 10);
            int row = Random.Range(0, 4);
            int col = Random.Range(0, 4);

            if (Map[row, col] == 0)
            {
                if (value < 9)
                {
                    Map[row, col] = 2;
                }
                else
                {
                    Map[row, col] = 4;
                }
                MapTiles[row, col] = Instantiate(rectPrefab, tiles[row * 4 + col].transform.position, Quaternion.identity, rects.transform);
                MapTiles[row, col].GetComponent<Rectangle>().value = Map[row, col];

                break;
            }
            else
            {
                int count = 0;
                for (int i = 0; i < 4; i++)
                {
                    for (int q = 0; q < 4; q++)
                    {
                        if (Map[i, q] != 0)
                        {
                            count++;
                        }
                    }
                }

                if (count == 16)
                {
                    break;
                }
            }
        }
    }
    void SwipeControl()
    {
        if (nextMove == true)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                startTouchPos = Input.GetTouch(0).position;
            }
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                endTouchPos = Input.GetTouch(0).position;

                if ((startTouchPos - endTouchPos).magnitude >= 100)
                {
                    if (Mathf.Abs(endTouchPos.x - startTouchPos.x) > Mathf.Abs(endTouchPos.y - startTouchPos.y))
                    {
                        if (endTouchPos.x < startTouchPos.x)
                        {
                            MoveTiles(0, 4, 1, 4, true, true);
                        }
                        else
                        {
                            MoveTiles(0, 4, 0, 3, false, true);
                        }
                    }
                    else
                    {
                        if (endTouchPos.y < startTouchPos.y)
                        {
                            MoveTiles(0, 3, 0, 4, false, false);
                        }
                        else
                        {
                            MoveTiles(1, 4, 0, 4, true, false);
                        }
                    }
                }
            }
        }
    }
    void ButtonControl()
    {
        if (nextMove == true)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                MoveTiles(0, 4, 0, 3, false, true);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                MoveTiles(0, 3, 0, 4, false, false);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                MoveTiles(0, 4, 1, 4, true, true);
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                MoveTiles(1, 4, 0, 4, true, false);
            }
        }
    }
    void CalcScore(int bonus)
    {
        score += bonus;
        scoreText.text = score.ToString();

        if (score > highScore)
        {
            highScore = score;
            highScoreText.text = highScore.ToString();
            PlayerPrefs.SetInt("highscore", highScore);
        }
    }

    public void Restart()
    {
        needUndo = true;
        saveOnce = false;
        SaveMap();

        for (int i = 0; i < 4; i++)
        {
            for (int q = 0; q < 4; q++)
            {
                Map[i, q] = 0;
                MapTiles[i, q] = null;
            }
        }

        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Tile").Length; i++)
        {
            Destroy(GameObject.FindGameObjectsWithTag("Tile")[i]);
        }

        score = 0;
        scoreText.text = "0";

        nextMove = true;
        once = true;

        SpawnTile();
        SpawnTile();

        gameOverAnimation.gameObject.SetActive(false);
        gameOver = false;
        saveOnce = false;
    }
    public void Undo()
    {
        if (needUndo == true)
        {
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("Tile").Length; i++)
            {
                Destroy(GameObject.FindGameObjectsWithTag("Tile")[i]);
            }
            for (int i = 0; i < 4; i++)
            {
                for (int q = 0; q < 4; q++)
                {
                    Map[i, q] = saveMap[i, q];
                    if (Map[i, q] != 0)
                    {
                        MapTiles[i, q] = Instantiate(rectPrefab, tiles[i * 4 + q].transform.position, Quaternion.identity, rects.transform);
                        MapTiles[i, q].GetComponent<Rectangle>().value = Map[i, q];
                        MapTiles[i, q].GetComponent<Rectangle>().showSpeed = 0.3f;
                    }
                    score = saveScore;
                    scoreText.text = score.ToString();
                }
            }
            needUndo = false;
            gameOver = false;
            gameOverAnimation.gameObject.SetActive(false);
        }
    }

    void CheckGameOver()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int q = 0; q < 4; q++)
            {
                if (Map[i, q] == 0)
                {
                    freeSpace = true;
                    break;
                }
            }
        }
        if (freeSpace == false)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int q = 0; q < 3; q++)
                {
                    if (Map[i, q] == Map[i, q + 1])
                    {
                        freeSpace = true;
                        break;
                    }
                }
            }
            for (int i = 0; i < 4; i++)
            {
                for (int q = 0; q < 3; q++)
                {
                    if (Map[q, i] == Map[q + 1, i])
                    {
                        freeSpace = true;
                        break;
                    }
                }
            }
        }

        if (freeSpace == false)
        {
            gameOver = true;
            gameOverAnimation.gameObject.SetActive(true);
            gameOverAnimation.Play("GameOver");
        }
        else
        {
            freeSpace = false;
        }
    }
}
