using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum PlayerState
    {
        Prepare,
        Playing,
        Died,
        Finish
    }
    public GameObject fireShieldParticle;
    public GameObject levelCompletePanel;
    public GameObject gameOverPanel;
    Rigidbody rb;

    bool dive;
    int diveVelocity = -700;
    int bounceVelocity = 250;

    float feverTime;
    bool feverMode;

    [SerializeField]
    AudioClip jump, normalBreakStack, immortalBreakStack, passedLevel, playerDied;

    [HideInInspector]
    public PlayerState playerState = PlayerState.Prepare;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        feverTime = 0f;
        feverMode = false;
    }

    private void Update()
    {
        if ( playerState == PlayerState.Playing )
        {
            if ( Input.GetMouseButtonDown(0) )
            {
                dive = true;
            }

            if ( Input.GetMouseButtonUp(0) )
            {
                dive = false;
            }

            if ( feverMode )
            {
                if ( !fireShieldParticle.activeInHierarchy )
                {
                    fireShieldParticle.SetActive(true);
                }

                feverTime -= Time.deltaTime * 0.1f;
            }
            else
            {
                if ( fireShieldParticle.activeInHierarchy )
                {
                    fireShieldParticle.SetActive(false);
                }

                if ( dive )
                {
                    feverTime += Time.deltaTime * 0.6f;
                }
                else
                {
                    feverTime -= Time.deltaTime * 0.3f;
                }
            }

            if ( feverTime >= 1f )
            {
                feverTime = 1f;
                feverMode = true;
            }
            else if ( feverTime <= 0f )
            {
                feverTime = 0f;
                feverMode = false;
            }

            UIManager.Instance.SetFeverBar(feverTime);
        }
    }

    private void FixedUpdate()
    {
        if ( dive )
        {
            rb.velocity = new Vector3(0, diveVelocity * Time.fixedDeltaTime, 0);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ( !dive )
        {
            rb.velocity = new Vector3(0, bounceVelocity * Time.deltaTime, 0);
        }
        else
        {
            if ( feverMode && collision.transform.parent.name != "win(Clone)" )
            {

                collision.transform.parent.GetComponent<ObstacleParent>().DisjointAll(5, transform.position);
                Destroy(collision.transform.parent.gameObject, 1f);

                //ScoreManager.Instance.AddScore(5);

                //Transform popup = Instantiate(popupText, transform.position + Vector3.left, Quaternion.identity);
                //popup.GetComponent<PopupText>().displayText = "+5";

                SoundManager.Instance.PlaySound(immortalBreakStack, .3f);
            }
            else
            {
                if ( collision.collider.CompareTag("Plane") )
                {

                    collision.transform.parent.GetComponent<ObstacleParent>().DisjointAll(1,transform.position);
                    Destroy(collision.transform.parent.gameObject, 1f);

                    //ScoreManager.Instance.AddScore(1);

                    //Transform popup = Instantiate(popupText, transform.position + Vector3.left, Quaternion.identity);
                    //popup.GetComponent<PopupText>().displayText = "+1";

                    SoundManager.Instance.PlaySound(normalBreakStack, .3f);
                }
                else if ( collision.collider.CompareTag("Unbreakable") )
                {
                    Debug.Log("Game Over");
                    playerState = PlayerState.Died;
                    dive = false;
                    SoundManager.Instance.PlaySound(playerDied, .3f);
                    UIManager.Instance.BestScoreText();
                    gameOverPanel.SetActive(true);

                }
            }
        }

        if ( collision.transform.parent.name == "win(Clone)" && playerState == PlayerState.Playing )
        {
            dive = false;
            playerState = PlayerState.Finish;
            PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
            SoundManager.Instance.PlaySound(passedLevel, .3f);
            UIManager.Instance.LevelUpText();
            UIManager.Instance.BestScoreText();
            levelCompletePanel.SetActive(true);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if ( !dive )
        {
            rb.velocity = new Vector3(0, bounceVelocity * Time.deltaTime, 0);
            SoundManager.Instance.PlaySound(jump, .3f);
        }
    }

}
