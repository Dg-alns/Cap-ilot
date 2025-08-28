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
    

    List<AnimationAvatarData> Idle_Shoes_Animation = new List<AnimationAvatarData>();
    List<AnimationAvatarData> Walk_Shoes_Animation = new List<AnimationAvatarData>();
    

    List<AnimationAvatarData> Idle_Eyes_Animation = new List<AnimationAvatarData>();
    List<AnimationAvatarData> Walk_Eyes_Animation = new List<AnimationAvatarData>();

    SpriteRenderer Body;
    SpriteRenderer Bottom;
    SpriteRenderer Top;
    SpriteRenderer Shoes;
    SpriteRenderer Eyes;

    AnimationAvatarData CurrentAnimation_Idle_Body;
    AnimationAvatarData CurrentAnimation_Idle_Bottom;
    AnimationAvatarData CurrentAnimation_Idle_Top;
    AnimationAvatarData CurrentAnimation_Idle_Shoes;
    AnimationAvatarData CurrentAnimation_Idle_Eyes;

    AnimationAvatarData CurrentAnimation_Walk_Body;
    AnimationAvatarData CurrentAnimation_Walk_Bottom;
    AnimationAvatarData CurrentAnimation_Walk_Top;
    AnimationAvatarData CurrentAnimation_Walk_Shoes;
    AnimationAvatarData CurrentAnimation_Walk_Eyes;

    int idx_Body_Idle = 0;
    int idx_Body_Walk = 0;

    public void Init(SpriteRenderer body, SpriteRenderer bottom, SpriteRenderer top, SpriteRenderer shoes, SpriteRenderer eyes)
    {
        Body = body;
        Bottom = bottom;
        Top = top;
        Shoes = shoes;
        Eyes = eyes;


        SearchScriptObj.LoadCategory("ScriptObj/Animation/Body/Idle", Idle_Body_Animation);
        SearchScriptObj.LoadCategory("ScriptObj/Animation/Bottom/Idle", Idle_Bottom_Animation);
        SearchScriptObj.LoadCategory("ScriptObj/Animation/Top/Idle", Idle_Top_Animation);
        SearchScriptObj.LoadCategory("ScriptObj/Animation/Shoes/Idle", Idle_Shoes_Animation);
        SearchScriptObj.LoadCategory("ScriptObj/Animation/Eyes/Idle", Idle_Eyes_Animation);

        SearchScriptObj.LoadCategory("ScriptObj/Animation/Body/Walk", Walk_Body_Animation);
        SearchScriptObj.LoadCategory("ScriptObj/Animation/Bottom/Walk", Walk_Bottom_Animation);
        SearchScriptObj.LoadCategory("ScriptObj/Animation/Top/Walk", Walk_Top_Animation);
        SearchScriptObj.LoadCategory("ScriptObj/Animation/Shoes/Walk", Walk_Shoes_Animation);
        SearchScriptObj.LoadCategory("ScriptObj/Animation/Eyes/Walk", Walk_Eyes_Animation);



        CurrentAnimation_Idle_Body = DetectionAnimationBodyAndEyes(Idle_Body_Animation, body.sprite.name);
        CurrentAnimation_Idle_Bottom = DetectionAnimationIdle(Idle_Bottom_Animation, bottom.sprite.name);
        CurrentAnimation_Idle_Top = DetectionAnimationIdle(Idle_Top_Animation, top.sprite.name);
        CurrentAnimation_Idle_Shoes = DetectionAnimationIdle(Idle_Shoes_Animation, shoes.sprite.name);
        CurrentAnimation_Idle_Eyes = DetectionAnimationBodyAndEyes(Idle_Eyes_Animation, eyes.sprite.name);

        CurrentAnimation_Walk_Body = DetectionAnimationBodyAndEyes(Walk_Body_Animation, body.sprite.name);
        CurrentAnimation_Walk_Bottom = DetectionAnimationWalk(Walk_Bottom_Animation, CurrentAnimation_Idle_Bottom.name);
        CurrentAnimation_Walk_Top = DetectionAnimationWalk(Walk_Top_Animation, CurrentAnimation_Idle_Top.name);
        CurrentAnimation_Walk_Shoes = DetectionAnimationWalk(Walk_Shoes_Animation, CurrentAnimation_Idle_Shoes.name);
        CurrentAnimation_Walk_Eyes = DetectionAnimationBodyAndEyes(Walk_Eyes_Animation, eyes.sprite.name);

        animator.enabled = true;
    }

    AnimationAvatarData DetectionAnimationBodyAndEyes(List<AnimationAvatarData> lst, string nameSprite)
    {
        foreach (AnimationAvatarData anim in lst)
        {
            if(nameSprite.Contains(anim.GetCouleurBody()))
                return anim;
        }

        return null;
    }

    AnimationAvatarData DetectionAnimationIdle(List<AnimationAvatarData> lst, string nameSprite)
    {
        foreach (AnimationAvatarData anim in lst)
        {
            foreach (Sprite sprite in anim.GetAnimation())
            {
                if (nameSprite.Contains(sprite.name))
                    return anim;
            }
        }

        return null;
    }
    AnimationAvatarData DetectionAnimationWalk(List<AnimationAvatarData> lst, string IdleNameScript)
    {
        foreach (AnimationAvatarData anim in lst)
        {
            if (anim.name.Contains(IdleNameScript))
                return anim;
            
        }

        return null;
    }

    public void ChangeAllSpriteIdleAnimation()
    {
        idx_Body_Idle++;

        ChangeSpriteIdle(CurrentAnimation_Idle_Body, Body);
        ChangeSpriteIdle(CurrentAnimation_Idle_Bottom, Bottom);
        ChangeSpriteIdle(CurrentAnimation_Idle_Top, Top);
        ChangeSpriteIdle(CurrentAnimation_Idle_Shoes, Shoes);
        ChangeSpriteIdle(CurrentAnimation_Idle_Eyes, Eyes);
    }

    public void ChangeAllSpriteWalkAnimation()
    {
        idx_Body_Walk++;

        ChangeSpriteWalk(CurrentAnimation_Walk_Body, Body);
        ChangeSpriteWalk(CurrentAnimation_Walk_Bottom, Bottom);
        ChangeSpriteWalk(CurrentAnimation_Walk_Top, Top);
        ChangeSpriteWalk(CurrentAnimation_Walk_Shoes, Shoes);
        ChangeSpriteWalk(CurrentAnimation_Walk_Eyes, Eyes);
    }

    void ChangeSpriteIdle(AnimationAvatarData CurrentAnimation, SpriteRenderer PartAvatar)
    {
        if (idx_Body_Idle < CurrentAnimation.GetAnimation().Count)
            PartAvatar.sprite = CurrentAnimation.GetAnimation()[idx_Body_Idle];
        else
        {
            idx_Body_Idle = 0;
            PartAvatar.sprite = CurrentAnimation.GetAnimation()[idx_Body_Idle];
        }
    }

    void ChangeSpriteWalk(AnimationAvatarData CurrentAnimation, SpriteRenderer PartAvatar)
    {
        if (idx_Body_Walk < CurrentAnimation.GetAnimation().Count)
            PartAvatar.sprite = CurrentAnimation.GetAnimation()[idx_Body_Walk];
        else
        {
            idx_Body_Walk = 0;
            PartAvatar.sprite = CurrentAnimation.GetAnimation()[idx_Body_Walk];
        }
    }


    public void SwitchAnimation()
    {
        idx_Body_Walk = 0;
        idx_Body_Idle = 0;
    }
}
