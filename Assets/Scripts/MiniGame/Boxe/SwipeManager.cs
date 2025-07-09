using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DIRECTION
{
    None,
    X,
    Y
}

public class SwipeManager : MonoBehaviour //TODO Diego a adapter pour mobile
{
    Vector2 InitialposMous = Vector2.zero;
    Vector2 LastposMous = Vector2.zero;

    public int directionOfSwipe = 0;
    //bool moveValidate = false;

    public void Swip(DIRECTION directionOfSwipeExpect)
    {
        if (Input.GetMouseButtonDown(0))
        {
            InitialposMous = Input.mousePosition;
        }


        if (Input.GetMouseButtonUp(0))
        {
            LastposMous = Input.mousePosition;

            DetectionDirectionOfSwipe(InitialposMous, LastposMous, directionOfSwipeExpect);
        }
    }

    void DetectionDirectionOfSwipe(Vector2 InitialposMous, Vector2 LastposMous, DIRECTION directionOfSwipeExpect)
    {
        if (InitialposMous == LastposMous)
            return;
        

        if(directionOfSwipeExpect == DIRECTION.X)
        {
            if (LastposMous.x < InitialposMous.x)
            {
                Debug.Log("Left");
                directionOfSwipe = 1;
                return;
            }
            Debug.Log("Right");
            directionOfSwipe = -1;
            return;
        }

        if(directionOfSwipeExpect == DIRECTION.Y)
        {
            if (LastposMous.y > InitialposMous.y)
            {
                Debug.Log("Top");
                directionOfSwipe = 1;
                return;
            }
            Debug.Log("Bot");
            directionOfSwipe = -1;
            return;
        }

        //    case DIRECTION.Top:
        //        if (LastposMous.y > InitialposMous.y)
        //        {
        //            Debug.Log("Top");
        //            directionOfSwipe = DIRECTION.Top;
        //            //moveValidate = true;
        //            return true;
        //        }
        //        return false;

        //    case DIRECTION.Bottom:
        //        if (LastposMous.y < InitialposMous.y)
        //        {
        //            Debug.Log("Bot");
        //            directionOfSwipe = DIRECTION.Bottom;
        //            //moveValidate = true;
        //            return true;
        //        }
        //        return false;
        //    default:
        //        return false;
        //}
    }


    void Update()
    {
        Swip(DIRECTION.X);
    }
}
