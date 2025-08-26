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


    private static readonly HashSet<char> nb = new HashSet<char>
    {
        '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'
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

    public string GetNameScript()
    {
        StringBuilder result = new StringBuilder();

        for (int i = name.Length - 1; i >= 0; i--)
        {
            if (nb.Contains(name[i]))
                result.Insert(0, name[i]);
            else
                break;
        }

        return result.ToString();
    }

}
