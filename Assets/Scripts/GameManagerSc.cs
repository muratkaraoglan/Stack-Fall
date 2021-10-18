using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManagerSc : MonoBehaviour
{

    public GameObject[] obstaclePrefab;
    public GameObject winPrefab;
    public static GameManagerSc Instance { get { return instance; } }

    private static GameManagerSc instance;

    GameObject[] selecttedObstacles = new GameObject[4];
    GameObject obstacleTemp, winTemp;

    float obstacleCount = 0f;

    [SerializeField]
    private int level = 1;
    private int baseObstacleCount = 7;

    private void Awake()
    {
        if ( instance != null && instance != this )
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    void Start()
    {
        if ( !PlayerPrefs.HasKey("Level") )
        {
            PlayerPrefs.SetInt("Level", 1);
        }

        level = PlayerPrefs.GetInt("Level");

        PickRandomObstacleModel();
        float t = Random.value;
        for ( obstacleCount = 0; obstacleCount > -level - baseObstacleCount; obstacleCount -= 0.5f )
        {
            if ( level <= 20 )
            {
                obstacleTemp = Instantiate(selecttedObstacles[Random.Range(0, 2)]);
            }
            else if ( level > 20 && level < 50 )
            {
                obstacleTemp = Instantiate(selecttedObstacles[Random.Range(1, 3)]);
            }
            else if ( level >= 50 && level < 100 )
            {
                obstacleTemp = Instantiate(selecttedObstacles[Random.Range(2, 4)]);
            }
            else
            {
                obstacleTemp = Instantiate(selecttedObstacles[Random.Range(0, 4)]);
            }

            obstacleTemp.transform.position = new Vector3(0, obstacleCount - 0.01f, 0);
            obstacleTemp.transform.eulerAngles = new Vector3(0, obstacleCount * 8, 0);

            float v = Mathf.Lerp(1, level + baseObstacleCount, t);

            if ( Mathf.Abs(obstacleCount) <= v )
            {
                obstacleTemp.transform.eulerAngles += Vector3.up * 180;
            }
            obstacleTemp.transform.parent = FindObjectOfType<ObstacleHolderController>().transform;
        }

        winTemp = Instantiate(winPrefab);
        winTemp.transform.position = new Vector3(0, obstacleCount - 0.01f, 0);

    }

    void PickRandomObstacleModel()
    {
        int rnd = Random.Range(0, 5);

        for ( int i = 0; i < 4; i++ )
        {
            selecttedObstacles[i] = obstaclePrefab[i + rnd * 4];
        }
    }

    public float ObstacleCount() => Mathf.Abs(-level-baseObstacleCount) * 2;

    public void LoadLevel()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartLevel() 
    {
        ScoreManager.Instance.ResetScore();
        LoadLevel();
    }

    public void Exit() 
    {
        Application.Quit();
    }
}
