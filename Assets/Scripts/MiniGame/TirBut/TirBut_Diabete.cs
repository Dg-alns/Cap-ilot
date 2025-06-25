using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TirBut_Diabete : MonoBehaviour
{
    [SerializeField] private TirBut_Ball _Ball;

    [SerializeField] private Animator _Animator;

    [SerializeField] private int _AnimationIndex = -1;

    [SerializeField] private bool _isSave = false;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (_isSave) return;

        if (collision.gameObject.tag == "TirBut_Ball")
        {
            if (_Ball.CheckSavable())
            {
                _isSave = true;
                Debug.Log(collision.gameObject.transform.localScale);
            }
        }
    }

    // Play a random keeper animation of the diabete 
    public void PlayDiabete()
    {
        _AnimationIndex = Random.Range(0, 6);
        Debug.Log(_AnimationIndex);
        _Animator.SetInteger("SaveGrid", _AnimationIndex);
    }

    // Reset the diabete out of the goal
    public void ResetAnimation()
    {
        _isSave = false;
        _AnimationIndex = -1;
        _Animator.SetInteger("SaveGrid", _AnimationIndex);
    }
}
