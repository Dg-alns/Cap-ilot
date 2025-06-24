using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TirBut_ShootButton : MonoBehaviour
{

    [SerializeField] private TirBut_ButtonManager _targetManager;

    [SerializeField] private TirBut_Ball _Ball;

    GameObject _ButtonInterface;
    // Start is called before the first frame update
    void Start()
    {
        _ButtonInterface = GameObject.Find("ButtonInterface");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TryShoot()
    {
        if (_targetManager.GetActiveButton() == null)
        {
            _targetManager.SetErrorButtons();
            return;
        }

        if (_Ball.IsShooting()) return;
        // SHOOT
        Debug.Log("Boom !");
        _Ball.Shoot(GetTargetPosition());
        _targetManager.DisableButton();
        _ButtonInterface.SetActive(false);
        return;

    }
    int GetTargetPosition()
    {
        return _targetManager.GetActiveButton().position;
    }
}
