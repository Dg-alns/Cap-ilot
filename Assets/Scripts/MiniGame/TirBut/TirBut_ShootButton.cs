using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TirBut_ShootButton : MonoBehaviour
{

    [SerializeField] private TirBut_ButtonManager _targetManager;

    [SerializeField] private TirBut_Ball _Ball;

    [SerializeField] private TirBut_Diabete _Diabete;


    GameObject _ButtonInterface;
    // Start is called before the first frame update
    void Start()
    {
        _ButtonInterface = GameObject.Find("ButtonInterface");
    }

    public void TryShoot()
    {
        // If there is no position select
        if (_targetManager.GetActiveButton() == null)
        {
            _targetManager.SetErrorButtons();
            return;
        }
        // Else Shoot to the choice position 
        _Ball.Shoot(GetTargetPosition());

        _Diabete.PlayDiabete();

        _targetManager.SetDisableButton();
        _ButtonInterface.SetActive(false);
        return;
    }
    int GetTargetPosition()
    {
        return _targetManager.GetActiveButton().position;
    }
}
