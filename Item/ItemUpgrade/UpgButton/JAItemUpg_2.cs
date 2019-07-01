using UnityEngine;
using System.Collections;

public class JAItemUpg_2 : MonoBehaviour
{
    int m_nItemRandom1;
    int m_nItemRandom2;
    int m_nFirstTier;
    int m_nSecondTier;

    bool m_bFirstTier = false;

    public UISprite m_pBtnSprite = null;

    public void Enter(bool bNormal, bool bFirstTier, E_JA_MYITEM_SLOT eState)
    {
        Debug.Log("FIRST = " + JAManager.I.myData.manage.m_stInven.m_stDBInven[(int)eState].m_nFirstTier);
        Debug.Log("SECOND = " + JAManager.I.myData.manage.m_stInven.m_stDBInven[(int)eState].m_nSecondTier);
        Debug.Log("State = " + eState);

        m_bFirstTier = bFirstTier;
        if (bNormal != false)
        {
            Rand_Normal(eState);
        }
        else
        {
            Rand_Cash(eState);
        }
    }


    private void Rand_Normal(E_JA_MYITEM_SLOT eState)
    {
        m_nItemRandom1 = NGUITools.RandomRange(00, 100);
        m_nItemRandom2 = NGUITools.RandomRange(00, 100);

        #region ### 랜덤1 ###
        if (m_nItemRandom1 <= 50)
        {
            if (JAManager.I.myData.manage.m_stInven.m_stDBInven[(int)eState].m_nFirstTier < 0)
                m_nFirstTier = 0;
            else
                m_nItemRandom1 = NGUITools.RandomRange(00, 100);
        }
        else if (m_nItemRandom1 >= 50 && m_nItemRandom1 < 85)
        {
            m_nFirstTier = 1;
            if (JAManager.I.myData.manage.m_stInven.m_stDBInven[(int)eState].m_nFirstTier < 0)
            {
                Debug.Log(m_nFirstTier);
                return;
            }
        }
        else if (m_nItemRandom1 >= 85 && m_nItemRandom1 < 94)
        {
            m_nFirstTier = 2;
            if (JAManager.I.myData.manage.m_stInven.m_stDBInven[(int)eState].m_nFirstTier < 1)
            {
                Debug.Log(m_nFirstTier);
                return;
            }
        }
        else if (m_nItemRandom1 >= 94 && m_nItemRandom1 < 99)
        {
            m_nFirstTier = 3;
            if (JAManager.I.myData.manage.m_stInven.m_stDBInven[(int)eState].m_nFirstTier < 2)
            {
                Debug.Log(m_nFirstTier);
                return;
            }
        }
        else if (m_nItemRandom1 >= 99 && m_nItemRandom1 < 100)
        {
            m_nFirstTier = 4;
            if (JAManager.I.myData.manage.m_stInven.m_stDBInven[(int)eState].m_nFirstTier < 2)
            {
                Debug.Log(m_nFirstTier);
                return;
            }
        }
        #endregion

        #region ### 랜덤2 ###
        if (m_nItemRandom2 <= 50)
        {
            if (JAManager.I.myData.manage.m_stInven.m_stDBInven[(int)eState].m_nSecondTier < 0)
                m_nSecondTier = 0;
            else
                m_nItemRandom2 = NGUITools.RandomRange(00, 100);
            
        }
        else if (m_nItemRandom2 >= 50 && m_nItemRandom2 < 85)
        {
            m_nSecondTier = 1;
            if (JAManager.I.myData.manage.m_stInven.m_stDBInven[(int)eState].m_nSecondTier < 0)
            {
                Debug.Log(m_nFirstTier);
                return;
            }
        }
        else if (m_nItemRandom2 >= 85 && m_nItemRandom2 < 94)
        {
            m_nSecondTier = 2;
            if (JAManager.I.myData.manage.m_stInven.m_stDBInven[(int)eState].m_nSecondTier < 1)
            {
                Debug.Log(m_nFirstTier);
                return;
            }
        }
        else if (m_nItemRandom2 >= 94 && m_nItemRandom2 < 99)
        {
            m_nSecondTier = 3;
            if (JAManager.I.myData.manage.m_stInven.m_stDBInven[(int)eState].m_nSecondTier < 2)
            {
                Debug.Log(m_nFirstTier);
                return;
            }
        }
        else if (m_nItemRandom2 >= 99 && m_nItemRandom2 < 100)
        {
            m_nSecondTier = 4;
            if (JAManager.I.myData.manage.m_stInven.m_stDBInven[(int)eState].m_nSecondTier < 2)
            {
                Debug.Log(m_nFirstTier);
                return;
            }
        }
        #endregion

        int nFirstFinalTier = -1;
        switch (m_nFirstTier)
        {
            case 0:
                nFirstFinalTier = 0;
                break;
            case 1:
                nFirstFinalTier = Random.Range(01, 6);
                break;
            case 2:
                nFirstFinalTier = Random.Range(07, 14);
                break;
            case 3:
                nFirstFinalTier = Random.Range(15, 19);
                break;
            case 4:
                nFirstFinalTier = Random.Range(20, 23);
                break;
            default:
                break;
        }

        int nSecondFinalTier = -1;
        switch (m_nSecondTier)
        {
            case 0:
                nSecondFinalTier = 0;
                break;
            case 1:
                nSecondFinalTier = Random.Range(1, 9);
                break;
            case 2:
                nSecondFinalTier = Random.Range(10, 16);
                break;
            case 3:
                nSecondFinalTier = Random.Range(17, 22);
                break;
            case 4:
                nSecondFinalTier = Random.Range(23, 27);
                break;
            default:
                break;
        }

        if (m_bFirstTier == true)
        {
            JAManager.I.myData.manage.m_stInven.m_stDBInven[(int)eState].m_nFirstTier = m_nFirstTier;
            JAManager.I.myData.manage.m_stInven.m_stDBInven[(int)eState].m_nFirstName = nFirstFinalTier;
        }
        else
        {
            JAManager.I.myData.manage.m_stInven.m_stDBInven[(int)eState].m_nSecondTier = m_nSecondTier;
            JAManager.I.myData.manage.m_stInven.m_stDBInven[(int)eState].m_nSecondName = nSecondFinalTier;
        }
    }

    private void Rand_Cash(E_JA_MYITEM_SLOT eState)
    {
        m_nItemRandom1 = NGUITools.RandomRange(00, 100);
        m_nItemRandom2 = NGUITools.RandomRange(00, 100);

        if (m_nItemRandom1 <= 60)
        {
            m_nFirstTier = 2;
        }
        else if (m_nItemRandom1 >= 60 && m_nItemRandom1 < 90)
        {
            m_nFirstTier = 3;
        }
        else if (m_nItemRandom1 >= 90 && m_nItemRandom1 < 100)
        {
            m_nFirstTier = 4;
        }

        if (m_nItemRandom2 <= 60)
        {
            m_nSecondTier = 2;
        }
        else if (m_nItemRandom2 >= 60 && m_nItemRandom2 < 90)
        {
            m_nSecondTier = 3;
        }
        else if (m_nItemRandom2 >= 90 && m_nItemRandom2 < 100)
        {
            m_nSecondTier = 4;
        }

        int nFirstFinalTier = -1;
        switch (m_nFirstTier)
        {
            case 0:
                nFirstFinalTier = 0;
                break;
            case 1:
                nFirstFinalTier = Random.Range(01, 6);
                break;
            case 2:
                nFirstFinalTier = Random.Range(07, 14);
                break;
            case 3:
                nFirstFinalTier = Random.Range(15, 19);
                break;
            case 4:
                nFirstFinalTier = Random.Range(20, 23);
                break;
            default:
                break;
        }

        int nSecondFinalTier = -1;
        switch (m_nSecondTier)
        {
            case 0:
                nSecondFinalTier = 0;
                break;
            case 1:
                nSecondFinalTier = Random.Range(1, 9);
                break;
            case 2:
                nSecondFinalTier = Random.Range(10, 16);
                break;
            case 3:
                nSecondFinalTier = Random.Range(17, 22);
                break;
            case 4:
                nSecondFinalTier = Random.Range(23, 27);
                break;
            default:
                break;
        }

        JAManager.I.myData.manage.m_stInven.m_stDBInven[(int)eState].m_nFirstName = nFirstFinalTier;
        JAManager.I.myData.manage.m_stInven.m_stDBInven[(int)eState].m_nSecondName = nSecondFinalTier;
    }

    public void SetButtonClick(bool bClick)
    {
        if (bClick == false)
            m_pBtnSprite.spriteName = "Enchant_Menu_Off";
        else
            m_pBtnSprite.spriteName = "Enchant_Menu_On";
    }
}
