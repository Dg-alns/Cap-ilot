using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationAvatarManager : MonoBehaviour
{
    public Animator animator;

    List<AnimationAvatarData> Idle_Body_Animation = new List<AnimationAvatarData>();
    List<AnimationAvatarData> Walk_Body_Animation = new List<AnimationAvatarData>();

    List<AnimationAvatarData> Idle_Top_Animation = new List<AnimationAvatarData>();
    List<AnimationAvatarData> Walk_Top_Animation = new List<AnimationAvatarData>();

    List<AnimationAvatarData> Idle_Bottom_Animation = new List<AnimationAvatarData>();
    List<AnimationAvatarData> Walk_Bottom_Animation = new List<AnimationAvatarData>();

    SpriteRenderer Body;

    AnimationAvatarData CurrentAnimation_Idle_Body;
    int idx_Body_Idle = 0;

    public void Init(string nameBody, string nameTop, string nameBot)
    {
        Body = GetComponent<PlayerSpriteManager>().Corps.GetComponent<SpriteRenderer>();


        SearchScriptObj.LoadCategory("ScriptObj/Animation/Body/Idle", Idle_Body_Animation);

        CurrentAnimation_Idle_Body = DetectionAnimation(Idle_Body_Animation, nameBody);

        animator.enabled = true;


    }

    AnimationAvatarData DetectionAnimation(List<AnimationAvatarData> lst, string nameSprite)
    {
        foreach (AnimationAvatarData anim in lst)
        {
            if(nameSprite.Contains(anim.GetCouleurBody()))
                return anim;
        }

        return null;
    }

    public void ChangeSpriteIdle_Body()
    {
        Debug.Log("Change");
        idx_Body_Idle++;

        Debug.Log(CurrentAnimation_Idle_Body.GetAnimation().Count);

        if (idx_Body_Idle < CurrentAnimation_Idle_Body.GetAnimation().Count)
            Body.sprite = CurrentAnimation_Idle_Body.GetAnimation()[idx_Body_Idle];
        else
        {
            idx_Body_Idle = 0;
            Body.sprite = CurrentAnimation_Idle_Body.GetAnimation()[idx_Body_Idle];
        }
    }
}
