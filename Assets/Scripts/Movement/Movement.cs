using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using static UnityEngine.GridBrushBase;


public class Movement : MonoBehaviour
{
    //public Animator animator;
    Tools _tools;
    NavMeshAgent _agent;
    // Start is called before the first frame update
    SpriteRenderer _spriteRenderer;
    private Vector3 _lastPosition;
    private bool _isRight = true;

    private int _clickedNpcId;
    private NPC _clickedNpc;
    private bool _dialogueStarted;
    [SerializeField] private GameObject _npcManager;
    [SerializeField] private GameObject _dialogue;

    [Header("Animation")]
    [SerializeField] private Animator _diabeteAnimator;

    public GameObject dialogueNpc;
    public GameObject activeDialogueUI;

    bool candebarque = false;

    //[SerializeField] private TouchManager _touchManager;

    void Start()
    {
        _tools = FindAnyObjectByType<Tools>();
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _lastPosition = transform.position;

        if (SceneManager.GetActiveScene().name != "Archipel")
            candebarque = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Animation Diabete
        if (_diabeteAnimator != null)
        {
            if (_diabeteAnimator.gameObject.activeSelf)
            {

                if (_diabeteAnimator != null)
                {
                    if (_agent.destination != transform.position)
                    {
                        _diabeteAnimator.SetBool("Walking", true);
                    }
                    else
                    {
                        _diabeteAnimator.SetBool("Walking", false);
                    }
                }
            }
        }



        //Debug.Log(_target);
        //_agent.SetDestination(_target);
        Vector3 movement = transform.position - _lastPosition;

        if (movement.x < 0 && _isRight)//deplacement  a gauche
        {
            transform.eulerAngles = new Vector3(0,180,0);  
            //transform.Rotate(new Vector3(0, 0, 0));
            _isRight = false;
            //_spriteRenderer.flipX = true;
        }
        else
        {
            if (movement.x > 0 && !_isRight)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                _isRight = true;
                //_spriteRenderer.flipX = false;
            }
        }
        
        _lastPosition = transform.position;

    }
    public void Move(Vector3 position)
    {
        //Debug.Log(position);
        if (!_tools.IsPointerOverUIElement())
        {
            position.z = transform.position.z;
            
            _agent.SetDestination(position);
        }
    }
/*    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.GetComponent<Trigger>())
        {
            if (_agent.remainingDistance < 0.2f)
            {
                collision.gameObject.GetComponent<Trigger>().IsTrigger();
            }
        }
    }*/

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (candebarque == false)
            return;

        if (collision.gameObject.GetComponent<Trigger>())
        {
            if (_agent.remainingDistance < 0.2f)
            { 
                if (collision.gameObject.GetComponent<Trigger>().Type == TriggerType.DIALOG || collision.gameObject.GetComponent<Trigger>().Type == TriggerType.MINIGAME)
                {
                    dialogueNpc = collision.gameObject;
                    NPC clickedNpc = dialogueNpc.GetComponent<NPC>();
                    if (clickedNpc != null)
                    {
                        _clickedNpcId = clickedNpc.npcId;
                        _clickedNpc = _npcManager.GetComponent<NPCManager>().FindNpcById(_clickedNpcId);

                        if (_clickedNpc != null)
                        {
                            dialogueNpc.GetComponent<Trigger>().IsTrigger();
                            activeDialogueUI = dialogueNpc.GetComponent<Trigger>().activeUI;
                            if (activeDialogueUI != null)
                            {
                                _dialogueStarted = activeDialogueUI.GetComponentInChildren<DialogueBox>().dialogStarted;
                            }
                        }
                        else
                        {
                            Debug.Log("ERROR : NPC not found");
                        }
                    }
                }
                else if (collision.gameObject.GetComponent<Trigger>().Type == TriggerType.PORT || collision.gameObject.GetComponent<Trigger>().Type == TriggerType.PANCARTE)
                {
                   collision.gameObject.GetComponent<Trigger>().IsTrigger();
                    
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        candebarque = true;
    }
}
