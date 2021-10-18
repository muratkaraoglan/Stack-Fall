using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleParent : MonoBehaviour
{
    bool isRun = false;
    public void DisjointAll(int score, Vector3 popupPositon)
    {
        if ( !isRun )
        {
            if ( transform.parent != null )
            {
                transform.parent = null;
            }

            for ( int i = 0; i < transform.childCount; i++ )
            {
                transform.GetChild(i).GetComponent<Obstacle>().Disjoint();
            }
            ScoreManager.Instance.AddScore(score);
            ScoreManager.Instance.PopupText(popupPositon, "+" + score.ToString());
            UIManager.Instance.SetLevelBar();
            isRun = true;
        }

    }
}
