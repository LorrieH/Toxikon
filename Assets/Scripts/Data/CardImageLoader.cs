using UnityEngine;

public class CardImageLoader : MonoBehaviour {

    public static Sprite s_CardSprite(string type,string cardName)
    {
        Sprite cardSprite = Resources.Load<Sprite>("Cards/" + type+ "/" + cardName);        
        return cardSprite;
    }
}