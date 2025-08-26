using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Text;
using static UnityEngine.Rendering.DebugUI;


[CreateAssetMenu(fileName = "PersonalisationData", menuName = "PersonalisationData/Create Animation of Player")]
public class AnimationAvatarData : ScriptableObject
{
    [SerializeField] private List<Sprite> Animationlst;

    public List<Sprite> GetAnimation() => Animationlst; 

    private static readonly HashSet<char> letters = new HashSet<char>
    {
        'a','z','e','r','t','y','u','i','o','p',
        'q','s','d','f','g','h','j','k','l','m',
        'w','x','c','v','b','n'
    };

    public string GetCouleurBody()
    {
        StringBuilder result = new StringBuilder();

        for (int i = name.Length - 1; i >= 0; i--)
        {
            if (letters.Contains(name[i]))
                result.Insert(0, name[i]);
            else
                break;
        }
        
        return result.ToString();
    }

}
