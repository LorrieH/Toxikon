using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Spine.Unity;

public class SetCharacterSkin : MonoBehaviour {
    
    [SerializeField] private List<SkeletonAnimation> m_characterSkeletons = new List<SkeletonAnimation>();

    private void Start()
    {
        StartCoroutine(LateStart());
    }

    IEnumerator LateStart()
    {
        yield return new WaitForSeconds(0.2f);
        SetSkin();
    }

    void SetSkin()
    {
        for (int i = 0; i < PlayersManager.s_Instance.Players.Count; i++)
        {
            m_characterSkeletons[i].skeleton.SetSkin(PlayersManager.s_Instance.Players[i].PlayerData.SkinName);
        }
    }
}