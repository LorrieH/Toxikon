using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class VictoryPopup : Menu
{
    [SerializeField] private Text m_characterName;
    [SerializeField] private Image m_characterImage;

    private void OnEnable()
    {
        m_characterName.text = TileGrid.s_Instance.WinningPlayerData.PlayerData.Name + " Wins!";
        m_characterImage.sprite = AvatarImageLoader.s_CharacterAvatarSprite(TileGrid.s_Instance.WinningPlayerData.PlayerData.SkinName);
    }

    public override void Show()
    {
        base.Show();
    }

    public override void Hide()
    {
        base.Hide();
    }
}
