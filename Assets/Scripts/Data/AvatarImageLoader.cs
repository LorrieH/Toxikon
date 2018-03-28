using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarImageLoader : MonoBehaviour {

    public static Sprite s_CharacterAvatarSprite(string characterColor)
    {
        Sprite backgroundSprite = Resources.Load<Sprite>("Characters/CHARACTER_" + characterColor+ "/CHARACTER_" + characterColor);
        return backgroundSprite;
    }
}