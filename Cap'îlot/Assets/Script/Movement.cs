using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;


public class Movement : MonoBehaviour
{
    //public Animator animator;
    Vector3 target;
    NavMeshAgent agent;
    // Start is called before the first frame update
    SpriteRenderer spriteRenderer;
    void Start()
    {
        target = transform.position;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0)) {

            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = transform.position.z;
            if (target.x < transform.position.x )//deplacement  a gauche
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                if (target.x > transform.position.x )
                {
                    spriteRenderer.flipX=false;
                }
            }
            

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

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.GetComponent<Trigger>())
        {
            if (agent.remainingDistance < 0.2f)
            {
                
                SceneManager.LoadScene(collision.gameObject.GetComponent<Trigger>().SceneName);
            }
        }

    }
}
