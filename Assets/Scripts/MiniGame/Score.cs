using UnityEngine;


public class Score : MonoBehaviour
{
    public Timer timer;
    public GameObject win;
    public float MinBronze;
    public float MinArgent;
    public float MinOr;

    float MiniGamePoint = 0;

    public int mPoint = 200;
    public int mCurrentScore;
    public int mWinnigScore;

    bool animLoad = false;

    Animator animator;

    public void AddScore()
    {
        mCurrentScore += mPoint;
    }

    public void SetScore(int score)
    {
        mCurrentScore = score;
    }

    private void Start()
    {
        gameObject.SetActive(false);
        win.SetActive(false);

        if(timer != null)
        {
            if (mWinnigScore > 0)
            {
                MinBronze += mWinnigScore;
                MinArgent += mWinnigScore;
                MinOr += mWinnigScore;
            }
        }
    }


    public void LauchScore()
    {
        if(animLoad)
            return;

        gameObject.SetActive(true);

        if (timer != null)
        {
            timer.stop = true;
            MiniGamePoint = timer.GetTimeInSecondes();
        }

        MiniGamePoint += mCurrentScore;
        Debug.Log(MiniGamePoint);
        animator = GetComponent<Animator>();

        GetMedailles(animator);
        animLoad = true;
    }

    public void LauchWin()
    {
        win.SetActive(true);
        win.GetComponent<Animator>().SetBool("TEST", true);
    }


    void GetMedailles(Animator animator)
    {
        if (timer != null)
        {
            if (MiniGamePoint > MinBronze)
                animator.SetBool("0", true);

            else if (MiniGamePoint <= MinBronze && MiniGamePoint > MinArgent)
                animator.SetBool("1", true);

            else if (MiniGamePoint <= MinArgent && MiniGamePoint > MinOr)
                animator.SetBool("2", true);

            else if (MiniGamePoint <= MinOr)
                animator.SetBool("3", true);

            return;
        }


        if (MiniGamePoint < MinBronze)
            animator.SetBool("0", true);

        else if (MiniGamePoint >= MinBronze && MiniGamePoint < MinArgent)
            animator.SetBool("1", true);

        else if (MiniGamePoint >= MinArgent && MiniGamePoint < MinOr)
            animator.SetBool("2", true);

        else if (MiniGamePoint >= MinOr)
            animator.SetBool("3", true);


    }

}
