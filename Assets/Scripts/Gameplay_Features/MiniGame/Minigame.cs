using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Minigame : MonoBehaviour
{
    public abstract void PauseMinigame();
    public abstract void ResumeMinigame();
}
