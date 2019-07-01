using UnityEngine;
using System.Collections;

public class JAItemUpgScrollTable : MonoBehaviour
{
    public UISprite m_pBackSprite = null;
    public UISprite m_pItemSprite = null;

    [HideInInspector]
    public int m_nIndex = 0;

    void Start()
    {

    }

    void Update()
    {

    }

    public void SetItemSprite(string sSpriteName = "icon_Gun", bool bShow = true)
    {
        m_pItemSprite.spriteName = sSpriteName;
        m_pItemSprite.enabled = bShow;
    }

    void OnPress(bool isPress)
    {
        if (isPress == true)
        {

        }
        else
        {

        }
    }

    void OnClick()
    {
        JADBManager.I.m_nSelectUpgTableIndex = m_nIndex;
       
    }
}
