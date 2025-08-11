using Unity.Services.Vivox;
using UnityEngine;
using UnityEngine.UI;

public class ShowUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject _panelLoading;
    public Animator animator;
    bool open;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(ShowHideUI);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ShowHideUI()
    {
        if (open)
        {
            animator.Play("HideUI");
            open = false;
        }
        else {
            if (!VivoxService.Instance.IsLoggedIn )
            {
                _panelLoading.SetActive(true);
            }
            animator.Play("ShowUI");
            open = true;
        }

    }
}
