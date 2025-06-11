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
    Vector3 target;
    NavMeshAgent agent;
    // Start is called before the first frame update
    SpriteRenderer spriteRenderer;
    public int clickedNpcId;
    void Start()
    {
        _tools = FindAnyObjectByType<Tools>();
        target = transform.position;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!_tools.IsPointerOverUIElement())
        {
            if (Input.GetMouseButton(0)) {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = transform.position.z;
            
            }

           
            /*if (agent.destination != transform.position) {
                animator.SetBool("Walking", true);
            }
            else
            {
                animator.SetBool("Walking", false);
            }*/
            agent.SetDestination(target);
        }
        if (target.x < transform.position.x)//deplacement  a gauche
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            if (target.x > transform.position.x)
            {
                spriteRenderer.flipX = false;
            }
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.GetComponent<Trigger>())
        {
            if (agent.remainingDistance < 0.2f)
            {
                collision.gameObject.GetComponent<Trigger>().IsTrigger();
                //SceneManager.LoadScene(collision.gameObject.GetComponent<Trigger>().SceneName);
                if (collision.gameObject.GetComponent<Trigger>().Type == Type.DIALOG)
                {
                    clickedNpcId = collision.gameObject.GetComponent<NPC>().npcId;
                }
            }
            
        }

    }
}
