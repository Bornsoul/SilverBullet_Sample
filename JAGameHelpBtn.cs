using UnityEngine;
using System.Collections;

public class JAGameHelpBtn : MonoBehaviour
{

    void Start()
    {

    }

    void Update()
    {

    }

    void OnClick()
    {
        JAManager.I.m_pShooterRoot.OutControl(true);
        JAPrefabMng.I.CreatePopup("테스트", "작동 확인용");
    }
}
