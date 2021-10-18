using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance { get { return instance; } }

    public GameObject playPanel;
    public Image feverBar;
    public Image levelBar;
    public Text currentLevelText;
    public Text nextLevelText;
    public Text levelCompleteText;
    public Text bestScoreText;
    PlayerController playerController;
    int level;
    [SerializeField]
    float totalObstacleCount;
    [SerializeField]
    float brokenObstacleCount;
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
        playerController = FindObjectOfType<PlayerController>();
        level = PlayerPrefs.GetInt("Level");
        currentLevelText.text = level.ToString();
        nextLevelText.text = (level + 1).ToString();
    }


    public void OnTab()
    {
        playPanel.SetActive(false);
        playerController.playerState = PlayerController.PlayerState.Playing;
        feverBar.transform.parent.gameObject.SetActive(true);
        totalObstacleCount = GameManagerSc.Instance.ObstacleCount();
    }

    public void SetFeverBar(float barValue)
    {
        feverBar.fillAmount = barValue;
        feverBar.color = Remap(0, 1, Color.yellow, Color.red, barValue);
    }

    public void SetLevelBar()
    {
        brokenObstacleCount++;
        float levelBarAmount = brokenObstacleCount / totalObstacleCount;
        levelBar.fillAmount = levelBarAmount;
    }

    public void LevelUpText() 
    {
        levelCompleteText.text = "Level " + level + "\nCOMPLETED!";
    }
    public void BestScoreText()
    {
        bestScoreText.gameObject.SetActive(true);
        bestScoreText.text = "Best " + PlayerPrefs.GetInt("HighScore").ToString();
    }
    Color Remap(float iMin, float iMax, Color oMin, Color oMax, float barValue)
    {
        float t = Mathf.InverseLerp(iMin, iMax, barValue);
        return Color.Lerp(oMin, oMax, t);
    }

    
}
