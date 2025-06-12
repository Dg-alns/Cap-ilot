using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using static UnityEngine.GridBrushBase;


public class Movement : MonoBehaviour
{
    //public Animator animator;
    Tools _tools;
    Vector3 _target;
    NavMeshAgent _agent;
    // Start is called before the first frame update
    SpriteRenderer _spriteRenderer;
    private int _clickedNpcId;
    private NPC _clickedNpc;
    private bool _dialogueStarted;
    [SerializeField] private GameObject _npcManager;
    [SerializeField] private GameObject _dialogue;

    //[SerializeField] private TouchManager _touchManager;

    void Start()
    {
        _tools = FindAnyObjectByType<Tools>();
        _target = transform.position;
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!_tools.IsPointerOverUIElement())
        {
            //_target = Camera.main.ScreenToWorldPoint(_touchManager.position);
            //_target.z = transform.position.z;



            /*if (_agent.destination != transform.position) {
                animator.SetBool("Walking", true);
            }
            else
            {
                animator.SetBool("Walking", false);
            }*/
            //Debug.Log(_target);
            //_agent.SetDestination(_target);
        }
        /*if (_target.x < transform.position.x)//deplacement  a gauche
        {
            _spriteRenderer.flipX = true;
        }
        else
        {
            if (_target.x > transform.position.x)
            {
                _spriteRenderer.flipX = false;
            }
        }*/

    }
    public void Move(Vector3 position)
    {
        //Debug.Log(position);
        position.z = transform.position.z;   
        _agent.SetDestination(position);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.GetComponent<Trigger>())
        {
            if (_agent.remainingDistance < 0.2f)
            {
                collision.gameObject.GetComponent<Trigger>().IsTrigger();
                //SceneManager.LoadScene(collision.gameObject.GetComponent<Trigger>().SceneName);
                if (collision.gameObject.GetComponent<Trigger>().Type == TriggerType.DIALOG)
                {
                    NPC clickedNpc = collision.gameObject.GetComponent<NPC>();
                    if (clickedNpc != null)
                    {
                        _clickedNpcId = clickedNpc.npcId;
                        _clickedNpc = _npcManager.GetComponent<NPCManager>().FindNpcById(_clickedNpcId);
                        Debug.Log("NPC: " + _clickedNpc.npcName);
                        _dialogue.GetComponentInChildren<DialogueBox>().GetDialogueLines();
                        _dialogue.GetComponentInChildren<DialogueBox>().StartDialogue();
                        _dialogueStarted = _dialogue.GetComponentInChildren<DialogueBox>().dialogStarted;
                    }
                }
            }
        }
    }
}
