using UnityEngine;
using System.Collections;

public class JAItemUpg_1 : MonoBehaviour
{

    public UISprite m_pBtnSprite = null;

    public void Enter(E_JA_MYITEM_SLOT eState)
    {
        //JAManager.I.myData.manage.m_stInven.m_stDBInven[(int)eState].m_nLevel++;
    }

    public void SetButtonClick(bool bClick)
    {
        if (bClick == false)
            m_pBtnSprite.spriteName = "Enchant_Menu_Off";
        else
            m_pBtnSprite.spriteName = "Enchant_Menu_On";
    }


}
