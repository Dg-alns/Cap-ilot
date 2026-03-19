using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Anim_BodyPart
{
    None = -1,

    Arm_Left,
    Arm_Right,
    Stomach,
    Leg_Left,
    Leg_Right,

    Count
}

public class Injection_Body : MonoBehaviour
{

    private Anim_BodyPart _currentBodyPart;

    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _currentBodyPart = Anim_BodyPart.None;
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public bool IsFinish()
    {
        return _currentBodyPart >= Anim_BodyPart.Count;
    }

    public void ReturnToIdle()
    {
        if (_currentBodyPart != Anim_BodyPart.None)
            _animator.SetTrigger("FinishPart");
        _animator.SetInteger("BodyPart", (int)Anim_BodyPart.None);
    }

    public void NextBodyPart()
    {
        _currentBodyPart += 1;
        _animator.SetInteger("BodyPart", (int)_currentBodyPart);
    }

}
