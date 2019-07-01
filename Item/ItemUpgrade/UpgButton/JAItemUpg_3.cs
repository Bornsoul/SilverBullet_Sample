using UnityEngine;
using System.Collections;

public class JAItemUpg_3 : MonoBehaviour
{
    int m_nItemRandom1 = 0;
    int m_nItemRandom2 = 0;
    int m_nItemRandom3 = 0;
    int m_nCnt = 0;
    int m_nDemonCnt = 0;

    int m_nItemSel = 0;
    int m_nItemTierSel = 0;

    public UISprite m_pBtnSprite = null;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetButtonClick(bool bClick)
    {
        if (bClick == false)
            m_pBtnSprite.spriteName = "Enchant_Menu_Off";
        else
            m_pBtnSprite.spriteName = "Enchant_Menu_On";
    }

    public void DestroySystem(int nIndex)
    {
        m_nItemRandom1 = NGUITools.RandomRange(00, 100);
        m_nItemRandom2 = NGUITools.RandomRange(00, 100);
        m_nItemRandom3 = NGUITools.RandomRange(00, 100);

        int[] m_nResult;

        if (m_nItemRandom2 <= 50)
        {
            m_nItemSel = 1;
        }
        else
        {
            m_nItemSel = 2;
        }

        for (int i = 0; i < 4; i++)
        {
            switch (JADBManager.I.GetinvenItemTier(JAManager.I.myData.manage.m_stInven.m_stDBInven[nIndex].m_nItemName))
            {
                case 1:
                    CheckTier(nIndex, 90, 90, 90);
                    break;
                case 2:
                    CheckTier(nIndex, 90, 90, 65);
                    break;
                case 3:
                    CheckTier(nIndex, 90, 85, 45);
                    break;
                case 4:
                    CheckTier(nIndex, 90, 40, -1);
                    break;
                case 5:
                    CheckTier(nIndex, 70, -1, -1);
                    break;
                case 6:
                    CheckTier(nIndex, 40, -1, -1);
                    break;
            }
        }

        m_nResult = new int[4];

        m_nResult[0] = (JADBManager.I.GetinvenItemTier(JAManager.I.myData.manage.m_stInven.m_stDBInven[nIndex].m_nItemName)) * (JADBManager.I.GetInvenItemLevel(nIndex) / 60);
        m_nResult[1] = (JADBManager.I.GetinvenItemTier(JAManager.I.myData.manage.m_stInven.m_stDBInven[nIndex].m_nItemName)) * (JADBManager.I.GetInvenItemLevel(nIndex) / 40);
        m_nResult[2] = (JADBManager.I.GetinvenItemTier(JAManager.I.myData.manage.m_stInven.m_stDBInven[nIndex].m_nItemName)) * (JADBManager.I.GetInvenItemLevel(nIndex) / 20);
        m_nResult[3] = (JADBManager.I.GetinvenItemTier(JAManager.I.myData.manage.m_stInven.m_stDBInven[nIndex].m_nItemName)) * (JADBManager.I.GetInvenItemLevel(nIndex) / 10);

        if (m_nItemRandom3 <= m_nResult[0])
        {
            JADBManager.I.SetAddInven_UseItem(3, 0, 0001, 103, 1, 0, true, false);
            JAPrefabMng.I.CreatePopup("아이템 분해", "[임시문구]"+System.Environment.NewLine +"악마의 심장 4티어를 얻었습니다!", "", "", E_JA_POPUP_SETTING.E_POPUP_OK);
        }
        else if (m_nItemRandom3 >= m_nResult[0] && m_nItemRandom3 < m_nResult[1])
        {
            JADBManager.I.SetAddInven_UseItem(3, 0, 0001, 102, 1, 0, true, false);
            JAPrefabMng.I.CreatePopup("아이템 분해", "[임시문구]" + System.Environment.NewLine + "악마의 심장 3티어를 얻었습니다!", "", "", E_JA_POPUP_SETTING.E_POPUP_OK);
        }
        else if (m_nItemRandom3 >= m_nResult[1] && m_nItemRandom3 < m_nResult[2])
        {
            JADBManager.I.SetAddInven_UseItem(3, 0, 0001, 101, 1, 0, true, false);
            JAPrefabMng.I.CreatePopup("아이템 분해", "[임시문구]" + System.Environment.NewLine + "악마의 심장 2티어를 얻었습니다!", "", "", E_JA_POPUP_SETTING.E_POPUP_OK);
        }
        else if (m_nItemRandom3 >= m_nResult[2] && m_nItemRandom3 < m_nResult[3])
        {
            JADBManager.I.SetAddInven_UseItem(3, 0, 0001, 100, 1, 0, true, false);
            JAPrefabMng.I.CreatePopup("아이템 분해", "[임시문구]" + System.Environment.NewLine + "악마의 심장 1티어를 얻었습니다!", "", "", E_JA_POPUP_SETTING.E_POPUP_OK);
        }
        else
        {
            Debug.Log("악마 못받음");
        }


    }

    public void CheckTier(int nIndex, int nRand0, int nRand1, int nRand2)
    {
        switch (m_nCnt)
        {
            case 0:
                if (JAManager.I.myData.manage.m_stInven.m_stDBInven[nIndex].m_nFirstName > 0 ||
                   JAManager.I.myData.manage.m_stInven.m_stDBInven[nIndex].m_nSecondName > 0)
                {
                    if (m_nItemRandom1 <= nRand0)
                    {
                        m_nCnt++;
                        
                    }
                    else if (m_nItemRandom1 >= nRand0 && m_nItemRandom1 < 100)
                    {
                        m_nItemTierSel = 4;
                    }
                }
                else
                {
                    if (nRand0 < 0)
                    {
                        m_nItemTierSel = 4;
                        m_nCnt = 0;
                    }
                    else
                    {
                        m_nCnt++;
                    }
                }
                break;
            case 1:
                if (JAManager.I.myData.manage.m_stInven.m_stDBInven[nIndex].m_nFirstName > 0 ||
                   JAManager.I.myData.manage.m_stInven.m_stDBInven[nIndex].m_nSecondName > 0)
                {
                    if (m_nItemRandom1 <= nRand1)
                    {
                        m_nCnt++;
                    }
                    else if (m_nItemRandom1 >= nRand1 && m_nItemRandom1 < 100)
                    {
                        m_nItemTierSel = 3;
                    }
                }
                else
                {
                    if (nRand1 < 0)
                    {
                        m_nItemTierSel = 3;
                        m_nCnt = 0;
                        return; 
                    }
                    {
                        m_nCnt++;
                    }
                }
                break;
            case 2:
                if (JAManager.I.myData.manage.m_stInven.m_stDBInven[nIndex].m_nFirstName > 0 ||
                   JAManager.I.myData.manage.m_stInven.m_stDBInven[nIndex].m_nSecondName > 0)
                {
                    if (m_nItemRandom1 <= nRand2)
                    {
                        m_nCnt++;
                    }
                    else if (m_nItemRandom1 >= nRand2 && m_nItemRandom1 < 100)
                    {
                        m_nItemTierSel = 2;
                    }
                }
                else
                {
                    if (nRand2 < 0)
                    {
                        m_nItemTierSel = 2;
                        m_nCnt = 0;
                        return; 
                    }
                    else
                    {
                        m_nCnt++;
                    }
                }
                break;
            case 3:
                m_nItemTierSel = 1;
                m_nCnt = 0;
                break;
        }

        SetAddItem(m_nItemSel, m_nItemTierSel);
        string sStr = string.Empty;

        switch ( m_nItemSel)
        {
            case 1:
                sStr = JAManager.I.GetStringLong("[임시문구]분해 완료!"+System.Environment.NewLine, "총열 ", m_nItemTierSel.ToString(), "티어를 얻었습니다!");
                break;
            case 2:
                sStr = JAManager.I.GetStringLong("[임시문구]분해 완료!" + System.Environment.NewLine, "스프링 ", m_nItemTierSel.ToString(), "티어를 얻었습니다!");
                break;
        }

        JAPrefabMng.I.CreatePopup("아이템 분해", sStr, "", "", E_JA_POPUP_SETTING.E_POPUP_OK);
        return;
    }

    public void SetAddItem(int nItemSel, int nNum)
    {
        switch (nItemSel)
        {
            case 1:
                switch (nNum)
                {
                    case 1:
                        JADBManager.I.SetAddInven_UseItem(3, 0, 0001, 200, 1, 0, true, false);

                        break;
                    case 2:
                        JADBManager.I.SetAddInven_UseItem(3, 0, 0001, 201, 1, 0, true, false);
                        break;
                    case 3:
                        JADBManager.I.SetAddInven_UseItem(3, 0, 0001, 202, 1, 0, true, false);
                        break;
                    case 4:
                        JADBManager.I.SetAddInven_UseItem(3, 0, 0001, 203, 1, 0, true, false);

                        break;
                }
                break;
            case 2:
                switch (nNum)
                {
                    case 1:
                        JADBManager.I.SetAddInven_UseItem(3, 0, 0001, 300, 1, 0, true, false);
                        break;
                    case 2:
                        JADBManager.I.SetAddInven_UseItem(3, 0, 0001, 301, 1, 0, true, false);
                        break;
                    case 3:
                        JADBManager.I.SetAddInven_UseItem(3, 0, 0001, 302, 1, 0, true, false);
                        break;
                    case 4:
                        JADBManager.I.SetAddInven_UseItem(3, 0, 0001, 303, 1, 0, true, false);
                        break;
                }
                break;
        }
    }
}
