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
        //GetComponent<Button>().onClick.AddListener(ShowHideUI);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show()
    {
        
        if (!VivoxService.Instance.IsLoggedIn )
        {
            _panelLoading.SetActive(true);
        }
        animator.Play("ShowUI");
    
    }

    public void HideUI()
    {
        animator.Play("HideUI");
        //open = false;
    }

}
