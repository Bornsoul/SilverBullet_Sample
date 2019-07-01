using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JAItemUseBox : MonoBehaviour
{
    public JAItemUpgradMainScript m_pUpgMain_Src = null;
    public JAItemUseBoxBtn[] m_pUseBtn = null;

    int m_nSelectNum = -1;

    public void Enter()
    {
        JAManager.I.LoadData();

        for (int i = 0; i < m_pUseBtn.Length; i++)
        {
            m_pUseBtn[i].Enter();
            m_pUseBtn[i].m_pItemSprite.alpha = 0.2f;
        }

    }


    public void BoxUpdate()
    {
        for (int i = 0; i < m_pUseBtn.Length; i++)
        {
            if (JAManager.I.m_bItemUse[i] == false)
            {
                m_pUseBtn[i].m_pItemSprite.alpha = 0.2f;
            }
            else
            {
                m_pUseBtn[i].m_pItemSprite.alpha = 1f;

            }
        }

        for (int i = 0; i < m_pUseBtn.Length; i++)
        {
            if (JADBManager.I.m_sItemUseSprite[i] == string.Empty)
                m_pUseBtn[i].m_pItemSprite.enabled = false;
            else
                m_pUseBtn[i].m_pItemSprite.enabled = true;
        }

        switch (JADBManager.I.m_nSelectUpgIndex)
        {
            case 0:

                for (int i = 0; i < m_pUseBtn.Length; i++)
                {
                    m_pUseBtn[i].m_pItemSprite.spriteName = JADBManager.I.m_sItemUseSprite[i];
                }
                break;
            case 1:
                for ( int i = 0; i<2; i++ )
                   m_pUseBtn[i].m_pItemSprite.spriteName = JADBManager.I.m_sItemUseSprite[i];
                //m_pUseBtn[1].m_pItemSprite.spriteName = JADBManager.I.m_sItemUseSprite[1];
                break;
        }
        
    }

    public void Destroy()
    {
        for (int i = 0; i < m_pUseBtn.Length; i++)
        {
            Destroy(m_pUseBtn[i]);
            m_pUseBtn[i] = null;
        }
    }

}
