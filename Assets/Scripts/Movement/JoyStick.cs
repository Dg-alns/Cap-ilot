using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JoyStick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public RectTransform backGround; //fond invisible prend tout lecran
    public RectTransform internJoy; //image du centre du joystick
    public float radius = 75; //porté max du joystick


    private Vector2 input = Vector2.zero; //direction du déplacement
    private CanvasGroup canvasGroup; //permet de gérer les alphas des images si on veut faire apparaitre/disparaitre le joystick 
    private Vector3 bgPositionStart; //position du background à sa création 
    private Boolean isMoving; //true qunad le joystick est utilisé, false sinon, permet de pas faire le vector move quand le joystick ne fait rien

    public Vector2 dir => input; //variable permet de récupérer les input dans le script
    public Boolean isM => isMoving; //permet de recuper isMoving dans le script
    
    public Boolean usingJoystick = true; //option

    

    public void OnPointerDown(PointerEventData eventData) //fait apparaitre le joystick là où le doigt press lecran tactile
    {
        if (usingJoystick){
            backGround.GetComponent<Image>().enabled = true;

            isMoving = true;

            backGround.position = eventData.position;
            internJoy.localPosition = Vector2.zero;

            if (canvasGroup != null)
            {
                canvasGroup.alpha = 1f;
            }

            OnDrag(eventData);
        }
        else{
            backGround.GetComponent<Image>().enabled = false;
        }
    }
    public void OnDrag(PointerEventData eventData) //déplace le joystick 
    {
        Vector2 pos = eventData.position - (Vector2)backGround.position;
        
        if(pos.magnitude > radius){
            input = pos.normalized; //n'avance pas plus vite en diagonale
        }
        else{
            input = pos / radius;
        }

        internJoy.localPosition = input * radius;
    }

    public void OnPointerUp(PointerEventData eventData) //reset le joystick
    {
        input = Vector2.zero;
        backGround.position = bgPositionStart;
        internJoy.localPosition = Vector2.zero;
        isMoving = false;

        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0f; 
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canvasGroup = backGround.GetComponent<CanvasGroup>();

        bgPositionStart = backGround.position;

        if (canvasGroup != null){
            canvasGroup.alpha = 0f; 
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
