using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class JADBManager : MonoBehaviour
{
    private static JADBManager m_pInstance = null;
    public static JADBManager I
    {
        get
        {
            if (null == m_pInstance)
            {
                m_pInstance = FindObjectOfType(typeof(JADBManager)) as JADBManager;
                if (null == m_pInstance)
                {

                    return null;
                }
            }
            return m_pInstance;
        }
    }

    [System.Serializable]
    public class ItemInvenInfo
    {
        public string m_sItemName;
        public string m_sQualityName;
        public string m_sItemSpriteName;
        public string[] m_sItemStatName = new string[5];
        public int[] m_nItemStat = new int[5];

        public ItemInvenInfo()
        {
            m_sItemName = string.Empty;
            m_sQualityName = string.Empty;
            m_sItemSpriteName = string.Empty;

            for (int i = 0; i < m_sItemStatName.Length; i++)
                m_sItemStatName[i] = string.Empty;

            for (int i = 0; i < m_nItemStat.Length; i++)
                m_nItemStat[i] = 0;
        }

        public void SetItemStatsName(params string[] sItemStatName)
        {
            for (int i = 0; i < m_sItemStatName.Length; i++)
                m_sItemStatName[i] = sItemStatName[i];
        }

        public void SetItemStats(params int[] nItemStatIndex)
        {
            for (int i = 0; i < m_nItemStat.Length; i++)
                m_nItemStat[i] = nItemStatIndex[i];
        }


        public void SetItemSetting(string sName, string sQcName, string sItemSpriteName)
        {
            m_sItemName = sName;
            m_sQualityName = sQcName;
            m_sItemSpriteName = sItemSpriteName;
        }
    };

    [HideInInspector]
    public ItemInvenInfo[] m_pItemInvenInfo = new ItemInvenInfo[4];

    public JADBItemScript[] m_stDBItem = null;
    public JADBItemFirstSubName[] m_stFirstSubName = null;
    public JADBItemSecondSubName[] m_stSecondSubName = null;
    public JADBItemRandBox m_pRandBox = null;

    private int m_nItemRandom;
    private int m_nFirstTier;
    private int m_nSecondTier;
    private int m_nItemTier;

    //! 인벤토리에서 사용
    public int m_nInvenCurTableIndex = 0; //!< 해당 박스 인덱스
    public float m_fMaxExp = 0;

    public int m_nSelectUpgIndex = 0; //!< 인벤 강화 3가지 버튼 체크
    public int m_nSelectUpgTableIndex = 0;
    public int m_nSelectUpgBoxTableIndex = 0;
    public int m_nItemInfoIndex = 0;

    public string[] m_sItemUseSprite = null;

    //! 클릭할때마다 이곳에 저장시키고 되돌릴때도 사용
    public JADBInvenScript[] m_pCopyInven = new JADBInvenScript[4]; 
    public int m_nCopyInvenNum = 0;  //!< 위 배열 인덱스넘버
    
    public int m_nCopyCheckCnt = 0; //!< 아이템 개조에서 똑같은 무기 중복 체크

    void Start()
    {

        SetItemSubNames();
        SetItemList();
        m_pCopyInven = new JADBInvenScript[4];
    }

    /// <summary>
    /// 아이템 코드와 레벨을 입력하여 추가합니다.
    /// </summary>
    /// <param name="nBig"></param>
    /// <param name="nSmall"></param>
    /// <param name="nFirst"></param>
    /// <param name="nSecond"></param>
    /// <param name="nItem"></param>
    /// <param name="nLevel"></param>
    /// <param name="nExeed"></param>
    public void SetAddInven_Item(int nBig, int nSmall, int nFirst, int nSecond, int nItem, int nLevel, int nLevelExp, int nExeed, bool bUse)
    {
        JAManager.I.myData.manage.m_stInven.m_stDBInven[JAManager.I.myData.manage.m_stInven.m_nDBInvenCnt++].SetAddInven_DataInfo(nBig, nSmall, nFirst, nSecond, nItem, nLevel, nLevelExp, nExeed, bUse, JADBManager.I.GetInvenItemIconName(nBig, nItem));
    }

    public void SetAddInven_UseItem(int nBig, int nSmall, int nValue, int nCode, int nLevel, int nLevelExp, bool bUse, bool bUseName)
	{
        JAManager.I.myData.manage.m_stInven.m_stDBInven[JAManager.I.myData.manage.m_stInven.m_nDBInvenCnt++].SetAddInven_UseItemData(nBig, nSmall, nValue, nCode, nLevel, nLevelExp, bUse, bUseName, JADBManager.I.GetInvenItemIconName(nBig, nCode));
	}

    /// <summary>
    /// 자신 아이템슬롯 아이템 코드와 레벨을 입력 추가
    /// </summary>
    /// <param name="eState"></param>
    /// <param name="nBig"></param>
    /// <param name="nSmall"></param>
    /// <param name="nFirst"></param>
    /// <param name="nSecond"></param>
    /// <param name="nItem"></param>
    /// <param name="nLevel"></param>
    /// <param name="nExeed"></param>
    /// <param name="sIconName"></param>
    public void SetAddInvenSlot_Item(E_JA_MYITEM_SLOT eState, int nBig, int nSmall, int nFirst, int nSecond, int nItem, int nLevel, float fLevelExp, int nExeed, bool bUse, string sIconName = "icon_Gun")
    {
        JAManager.I.myData.manage.m_stInven.m_stDBInven[(int)eState].SetAddInven_DataInfo(nBig, nSmall, nFirst, nSecond, nItem, nLevel, fLevelExp, nExeed, bUse, sIconName);
    }

    /// <summary>
    /// 랜덤으로 아이템을 뽑습니다.
    /// </summary>
    public void SetAddRandItem()
    {
        int nItemCode = -1;
        m_nItemRandom = NGUITools.RandomRange(00, 100);

        if (m_nItemRandom <= 50)
        {
            m_nFirstTier = 0;
            m_nSecondTier = 0;
            m_nItemTier = 1;
        }
        else if (m_nItemRandom >= 50 && m_nItemRandom < 85)
        {
            m_nFirstTier = 1;
            m_nSecondTier = 1;
            m_nItemTier = 2;
        }
        else if (m_nItemRandom >= 85 && m_nItemRandom < 94)
        {
            m_nFirstTier = 2;
            m_nSecondTier = 2;
            m_nItemTier = 3;
        }
        else if (m_nItemRandom >= 94 && m_nItemRandom < 99)
        {
            m_nFirstTier = 3;
            m_nSecondTier = 3;
            m_nItemTier = 4;
        }
        else if (m_nItemRandom >= 99 && m_nItemRandom < 100)
        {
            m_nFirstTier = 4;
            m_nSecondTier = 4;
            m_nItemTier = 5;
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

        int[] nTierArr1 = new int[10] { 1, 6, 11, 16, 21, 26, 31, 36, 41, 46 };
        int[] nTierArr2 = new int[40] { 2, 3, 4, 5, 7, 8, 9, 10, 12, 13, 14, 15,
                                             17, 18, 19, 20, 22, 23, 24, 25, 27,
                                             28, 29, 30, 32, 33, 34, 35, 37, 38, 39,
                                             40, 42, 43, 44, 45, 47, 48, 49, 50 };
        int[] nTierArr3 = new int[4] { 700, 701, 702, 703 };
        int[] nTierArr4 = new int[4] { 800, 801, 802, 803 };
        int[] nTierArr5 = new int[2] { 900, 901 };
        int nItemFinalTier = -1;
        switch (m_nItemTier)
        {
            case 1:

                nItemFinalTier = nTierArr1[Random.Range(0, 10)];
                break;
            case 2:
                nItemFinalTier = nTierArr2[Random.Range(0, 40)];
                break;
            case 3:
                nItemFinalTier = nTierArr3[Random.Range(0, 4)];
                break;
            case 4:
                nItemFinalTier = nTierArr4[Random.Range(0, 4)];
                break;
            case 5:
                nItemFinalTier = nTierArr5[Random.Range(0, 2)];
                break;
            default:
                break;
        }

        nItemCode += 1 * 100000000;
        nItemCode += 0 * 10000000;
        nItemCode += nFirstFinalTier * 100000;
        nItemCode += nSecondFinalTier * 1000;
        nItemCode += nItemFinalTier;

        //JAManager.I.myData.manage.m_stInven.m_stDBInven[JAManager.I.myData.manage.m_stInven.m_nDBInvenCnt++].SetAddItemNames(1, 0, nFirstFinalTier, nSecondFinalTier, nItemFinalTier);
        //JAManager.I.SaveData();
    }

   
    public void SetMyItemSlot(E_JA_MYITEM_SLOT eState, int nIndex)
    {
        JADBManager.I.SetAddInvenSlot_Item(eState,
            JAManager.I.myData.manage.m_stInven.m_stDBInven[nIndex].m_nBigName,
            JAManager.I.myData.manage.m_stInven.m_stDBInven[nIndex].m_nSmallName,
            JAManager.I.myData.manage.m_stInven.m_stDBInven[nIndex].m_nFirstName,
            JAManager.I.myData.manage.m_stInven.m_stDBInven[nIndex].m_nSecondName,
            JAManager.I.myData.manage.m_stInven.m_stDBInven[nIndex].m_nItemName,
            JAManager.I.myData.manage.m_stInven.m_stDBInven[nIndex].m_nLevel,
            JAManager.I.myData.manage.m_stInven.m_stDBInven[nIndex].m_fLevelExp,
            JAManager.I.myData.manage.m_stInven.m_stDBInven[nIndex].m_nExceed,
            JAManager.I.myData.manage.m_stInven.m_stDBInven[nIndex].m_bUseItem);
    }

    public void SetMyItemSlot(E_JA_MYITEM_SLOT eState, int nBig, int nSmall, int nFirst, int nSecond, int nItem, int nLevel, float fLevelExp, int nExceed, bool bUse, string sIconName = "icon_Gun")
    {
        JADBManager.I.SetAddInvenSlot_Item(eState,
            nBig,
            nSmall,
            nFirst,
            nSecond,
            nItem,
            nLevel,
            fLevelExp,
            nExceed,
            bUse,
            sIconName);
    }

    /// <summary>
    /// 접두어 수식어 모음.
    /// </summary>
    public void SetItemSubNames()
    {
        m_stFirstSubName = new JADBItemFirstSubName[24];
        m_stSecondSubName = new JADBItemSecondSubName[29];

        for (int i = 0; i < m_stFirstSubName.Length; i++)
            m_stFirstSubName[i] = new JADBItemFirstSubName();
        for (int i = 0; i < m_stSecondSubName.Length; i++)
            m_stSecondSubName[i] = new JADBItemSecondSubName();

        m_stFirstSubName[0].SetFistSubName("", 0, 0, 0, 0, 0, 0, 0);
        m_stFirstSubName[1].SetFistSubName("손때묻은 ", 1, 0, 0, 0, 0, 0, 1);
        m_stFirstSubName[2].SetFistSubName("차가운 ", 0, 0, 0, 0, 0, -2, 1);
        m_stFirstSubName[3].SetFistSubName("피 묻은 ", 0, 1, 0, 0, 0, 1, 1);
        m_stFirstSubName[4].SetFistSubName("매끄러 ", 0, 0, 1, -2, 0, 0, 1);
        m_stFirstSubName[5].SetFistSubName("묵직한 ", 0, 0, 0, 1, 0, 0, 1);
        m_stFirstSubName[6].SetFistSubName("가벼운 ", 0, 0, 0, 0, 2, 0, 1);
        m_stFirstSubName[7].SetFistSubName("다듬어진 ", 0, 1, 0, 1, 0, 0, 2);
        m_stFirstSubName[8].SetFistSubName("반짝이는 ", 0, 1, 1, 0, 0, 2, 2);
        m_stFirstSubName[9].SetFistSubName("믿음직한 ", 2, 0, 0, 0, 0, 0, 2);
        m_stFirstSubName[10].SetFistSubName("싸늘한 ", 0, 0, 0, 0, 0, -4, 2);
        m_stFirstSubName[11].SetFistSubName("잔혹한 ", 0, 2, 0, 0, 0, 1, 2);
        m_stFirstSubName[12].SetFistSubName("신속한 ", 0, 0, 2, -5, 0, 0, 2);
        m_stFirstSubName[13].SetFistSubName("두터운 ", 0, 0, 0, 2, 0, 0, 2);
        m_stFirstSubName[14].SetFistSubName("경쾌한 ", 0, 0, 0, 0, 4, 0, 2);
        m_stFirstSubName[15].SetFistSubName("잊혀진 ", 0, 0, 2, 0, 0, 0, 3);
        m_stFirstSubName[16].SetFistSubName("범상찮은 ", 1, 1, 0, 1, 0, 1, 3);
        m_stFirstSubName[17].SetFistSubName("기묘한 ", 0, 0, 0, 1, 3, 0, 3);
        m_stFirstSubName[18].SetFistSubName("신비로운 ", 0, 0, 0, 3, 0, -5, 3);
        m_stFirstSubName[19].SetFistSubName("빛나는 ", 0, 2, 1, 1, 0, 2, 3);
        m_stFirstSubName[20].SetFistSubName("속삭이는 ", 1, 2, 0, 2, 5, 0, 4);
        m_stFirstSubName[21].SetFistSubName("성스러운 ", 2, 1, 0, 2, 1, 0, 4);
        m_stFirstSubName[22].SetFistSubName("저주받은 ", 5, -1, -1, -1, -1, 1, 4);
        m_stFirstSubName[23].SetFistSubName("축복받은 ", 2, 0, 1, 5, 0, -5, 4);

        m_stSecondSubName[0].SetSecondSubName("", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1);
        m_stSecondSubName[1].SetSecondSubName("용병의", 0, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1);
        m_stSecondSubName[2].SetSecondSubName("병사의", 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1);
        m_stSecondSubName[3].SetSecondSubName("말년병장의", 0, 0, 0, 0, 0, 0, 5, 0, 0, 0, 5, 1);
        m_stSecondSubName[4].SetSecondSubName("교관의", 0, 0, 0, 0, 0, 0, 0, 5, 0, 0, 0, 1);
        m_stSecondSubName[5].SetSecondSubName("해결사의", 0, 0, 5, 0, 0, 0, 0, 0, 0, 5, 0, 1);
        m_stSecondSubName[6].SetSecondSubName("경비병의", 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 1);
        m_stSecondSubName[7].SetSecondSubName("추적자의", 0, 0, 0, 0, 0, 5, 0, 0, 5, 0, 0, 1);
        m_stSecondSubName[8].SetSecondSubName("훈련병의", 5, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1);
        m_stSecondSubName[9].SetSecondSubName("사냥꾼의", 0, 0, 0, 0, 0, 0, 5, 0, 0, 0, 5, 1);
        m_stSecondSubName[10].SetSecondSubName("선봉대의", 10, 0, 0, 0, 0, 5, 0, 0, 0, 0, 5, 2);
        m_stSecondSubName[11].SetSecondSubName("저격병의", 0, 0, 5, 0, 0, 0, 0, 0, 10, 0, 0, 2);
        m_stSecondSubName[12].SetSecondSubName("돌격병의", 0, 0, 0, 5, 0, 0, 0, 5, 0, 5, 0, 2);
        m_stSecondSubName[13].SetSecondSubName("지휘관의", 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2);
        m_stSecondSubName[14].SetSecondSubName("이단심문관의", 0, 0, 0, 0, 0, 0, 0, 0, 20, 0, 0, 2);
        m_stSecondSubName[15].SetSecondSubName("고문관의", 0, 5, 10, 0, 0, 0, 0, 0, 0, 0, 0, 2);
        m_stSecondSubName[16].SetSecondSubName("철갑병의", 10, 0, 0, 0, 5, 0, 0, 0, 0, 0, 0, 2);
        m_stSecondSubName[17].SetSecondSubName("암살자의", 0, 0, 0, 0, 0, 0, 10, 0, 0, 5, 5, 2);
        m_stSecondSubName[18].SetSecondSubName("용사의", 20, 10, 0, 5, 5, 10, 0, 0, 0, 0, 0, 3);
        m_stSecondSubName[19].SetSecondSubName("체력의", 50, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3);
        m_stSecondSubName[20].SetSecondSubName("생존전문가의", 0, 20, 10, 0, 0, 0, 0, 10, 0, 10, 0, 3);
        m_stSecondSubName[21].SetSecondSubName("신념의", 0, 0, 0, 0, 10, 20, 10, 0, 0, 0, 0, 3);
        m_stSecondSubName[22].SetSecondSubName("예언자의", 0, 0, 0, 0, 0, 0, 0, 0, 20, 20, 10, 3);
        m_stSecondSubName[23].SetSecondSubName("은장식된", 0, 0, 0, 0, 10, 20, 10, 10, 0, 0, 0, 3);
        m_stSecondSubName[24].SetSecondSubName("악마의", 10, 0, 10, 0, 0, 0, 0, 0, 50, -5, -5, 4);
        m_stSecondSubName[25].SetSecondSubName("천사의", 30, 10, 0, 0, 10, 0, 0, 0, 0, 10, 0, 4);
        m_stSecondSubName[26].SetSecondSubName("격투가의", 20, 0, 0, 20, 0, 0, 0, 0, 0, 20, 0, 4);
        m_stSecondSubName[27].SetSecondSubName("영웅의", 10, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 4);
        m_stSecondSubName[28].SetSecondSubName("진리의", 40, 0, 0, 0, 0, 0, 0, 20, 0, 0, 0, 4);

    }

    /// <summary>
    /// 아이템 리스트
    /// </summary>
    public void SetItemList()
    {
        m_stDBItem = new JADBItemScript[72];
        for (int i = 0; i < m_stDBItem.Length; i++)
            m_stDBItem[i] = new JADBItemScript();

        m_stDBItem[0].SetItemInfo(1, 1, "Jex-40", "icon_Gun", 1, 50, 30, 42, 100, 100, 30);
        m_stDBItem[1].SetItemInfo(1, 2, "Jex-40s", "icon_Gun", 2, 49, 30, 42, 100, 80, 40);
        m_stDBItem[2].SetItemInfo(1, 3, "Jex-40x", "icon_Gun", 2, 50, 30, 44, 100, 100, 40);
        m_stDBItem[3].SetItemInfo(1, 4, "Jex-40c", "icon_Gun", 2, 50, 31, 41, 95, 100, 40);
        m_stDBItem[4].SetItemInfo(1, 5, "Jex-40g", "icon_Gun", 2, 52, 30, 40, 100, 100, 40);

        m_stDBItem[5].SetItemInfo(1, 6, "Jex-45", "icon_Gun", 1, 53, 30, 37, 100, 100, 30);
        m_stDBItem[6].SetItemInfo(1, 7, "Jex-45s", "icon_Gun", 2, 52, 30, 37, 100, 80, 40);
        m_stDBItem[7].SetItemInfo(1, 8, "Jex-45x", "icon_Gun", 2, 53, 30, 39, 100, 100, 40);
        m_stDBItem[8].SetItemInfo(1, 9, "Jex-45c", "icon_Gun", 2, 52, 31, 36, 95, 100, 40);
        m_stDBItem[9].SetItemInfo(1, 10, "Jex-45g", "icon_Gun", 2, 54, 30, 34, 100, 100, 40);

        m_stDBItem[10].SetItemInfo(1, 11, "SSP-1", "icon_Gun", 1, 42, 40, 32, 100, 100, 30);
        m_stDBItem[11].SetItemInfo(1, 12, "SSP-1 AC", "icon_Gun", 2, 42, 40, 34, 100, 100, 40);
        m_stDBItem[12].SetItemInfo(1, 13, "SSP-1 XP", "icon_Gun", 2, 43, 40, 31, 100, 100, 40);
        m_stDBItem[13].SetItemInfo(1, 14, "SSP-1 SL", "icon_Gun", 2, 42, 40, 31, 100, 90, 40);
        m_stDBItem[14].SetItemInfo(1, 15, "SSP-1 RP", "icon_Gun", 2, 41, 42, 30, 90, 100, 40);
                   
        m_stDBItem[15].SetItemInfo(1, 16, "SSP-2", "icon_Gun", 1, 40, 40, 36, 100, 100, 30);
        m_stDBItem[16].SetItemInfo(1, 17, "SSP-2 AC", "icon_Gun", 2, 40, 40, 37, 100, 100, 40);
        m_stDBItem[17].SetItemInfo(1, 18, "SSP-2 XP", "icon_Gun", 2, 41, 40, 35, 100, 100, 40);
        m_stDBItem[18].SetItemInfo(1, 19, "SSP-2 SL", "icon_Gun", 2, 40, 40, 35, 100, 90, 40);
        m_stDBItem[19].SetItemInfo(1, 20, "SSP-2 RP", "icon_Gun", 2, 39, 41, 34, 90, 100, 40);
                           
        m_stDBItem[20].SetItemInfo(1, 21, "DZ-P4", "icon_Gun", 1, 85, 20, 27, 100, 120, 30);
        m_stDBItem[21].SetItemInfo(1, 22, "DZ-P4-A", "icon_Gun", 2, 83, 20, 29, 100, 120, 40);
        m_stDBItem[22].SetItemInfo(1, 23, "DZ-P4-M", "icon_Gun", 2, 88, 20, 24, 100, 130, 40);
        m_stDBItem[23].SetItemInfo(1, 24, "DZ-P4-F", "icon_Gun", 2, 86, 20, 26, 100, 125, 40);
        m_stDBItem[24].SetItemInfo(1, 25, "DZ-P4-Z", "icon_Gun", 2, 95, 19, 20, 90, 150, 40);
                           
        m_stDBItem[25].SetItemInfo(1, 26, "PN23", "icon_Gun", 1, 45, 35, 35, 110, 90, 30);
        m_stDBItem[26].SetItemInfo(1, 27, "PN23-A1", "icon_Gun", 2, 47, 34, 34, 110, 90, 40);
        m_stDBItem[27].SetItemInfo(1, 28, "PN23-Ez", "icon_Gun", 2, 45, 35, 37, 110, 90, 40);
        m_stDBItem[28].SetItemInfo(1, 29, "PN23-TT", "icon_Gun", 2, 44, 35, 35, 110, 80, 40);
        m_stDBItem[29].SetItemInfo(1, 30, "PN23-X45", "icon_Gun", 2, 45, 35, 40, 110, 100, 40);
                          
        m_stDBItem[30].SetItemInfo(1, 31, "X-2000", "icon_Gun", 1, 43, 33, 50, 120, 100, 30);
        m_stDBItem[31].SetItemInfo(1, 32, "X-2000 AP1", "icon_Gun", 2, 43, 33, 52, 120, 100, 40);
        m_stDBItem[32].SetItemInfo(1, 33, "X-2000 AP2", "icon_Gun", 2, 44, 33, 45, 120, 100, 40);
        m_stDBItem[33].SetItemInfo(1, 34, "X-2000 CR", "icon_Gun", 2, 42, 33, 60, 130, 100, 40);
        m_stDBItem[34].SetItemInfo(1, 35, "X-2000 GT", "icon_Gun", 2, 43, 34, 48, 120, 100, 40);
                                           
        m_stDBItem[35].SetItemInfo(1, 36, "IRL-1a", "icon_Gun", 1, 60, 27, 35, 100, 110, 30);
        m_stDBItem[36].SetItemInfo(1, 37, "IRL-1tt", "icon_Gun", 2, 60, 27, 36, 100, 100, 40);
        m_stDBItem[37].SetItemInfo(1, 38, "IRL-1sp", "icon_Gun", 2, 59, 27, 40, 100, 110, 40);
        m_stDBItem[38].SetItemInfo(1, 39, "IRL-1zx", "icon_Gun", 2, 59, 28, 33, 100, 110, 40);
        m_stDBItem[39].SetItemInfo(1, 40, "IRL-1rs", "icon_Gun", 2, 61, 27, 35, 100, 110, 40);
                                           
        m_stDBItem[40].SetItemInfo(1, 41, "PN25", "icon_Gun", 1, 43, 37, 35, 105, 100, 30);
        m_stDBItem[41].SetItemInfo(1, 42, "PN25-A2", "icon_Gun", 2, 43, 37, 38, 105, 100, 40);
        m_stDBItem[42].SetItemInfo(1, 43, "PN25-B1", "icon_Gun", 2, 43, 37, 34, 110, 100, 40);
        m_stDBItem[43].SetItemInfo(1, 44, "PN25-RF", "icon_Gun", 2, 42, 38, 32, 105, 100, 40);
        m_stDBItem[44].SetItemInfo(1, 45, "PN25-Z0", "icon_Gun", 2, 45, 37, 30, 100, 100, 40);
                                         
        m_stDBItem[45].SetItemInfo(1, 46, "Asn_D", "icon_Gun", 1, 50, 34, 31, 90, 90, 30);
        m_stDBItem[46].SetItemInfo(1, 47, "Asn_D1", "icon_Gun", 2, 51, 34, 30, 90, 90, 40);
        m_stDBItem[47].SetItemInfo(1, 48, "Asn_D2", "icon_Gun", 2, 50, 34, 34, 90, 100, 40);
        m_stDBItem[48].SetItemInfo(1, 49, "Asn_DS", "icon_Gun", 2, 49, 34, 32, 90, 60, 40);
        m_stDBItem[49].SetItemInfo(1, 50, "Asn_DL", "icon_Gun", 2, 49, 34, 30, 100, 90, 40);

        ///////////////////////* 네임드 아이템 */////////////////////////////////////////
        m_stDBItem[50].SetItemInfo(1, 700, "Kerberos", "icon_Gun", 3, 50, 40, 40, 100, 100, 50);
        m_stDBItem[51].SetItemInfo(1, 701, "type-XIII", "icon_Gun", 3, 55, 35, 50, 110, 100, 50);
        m_stDBItem[52].SetItemInfo(1, 702, "BFG-P", "icon_Gun", 3, 100, 21, 35, 100, 100, 50);
        m_stDBItem[53].SetItemInfo(1, 703, "Asura-X", "icon_Gun", 3, 40, 50, 38, 105, 100, 50);
                          
        m_stDBItem[54].SetItemInfo(1, 800, "One1000", "icon_Gun", 4, 45, 37, 100, 120, 90, 60);
        m_stDBItem[55].SetItemInfo(1, 801, "Ninja-SL", "icon_Gun", 4, 60, 31, 50, 100, 50, 60);
        m_stDBItem[56].SetItemInfo(1, 802, "Paladin", "icon_Gun", 4, 53, 38, 60, 100, 100, 60);
        m_stDBItem[57].SetItemInfo(1, 803, "Thanatos", "icon_Gun", 4, 70, 29, 55, 110, 80, 60);
                           
        m_stDBItem[58].SetItemInfo(1, 900, "Dogma", "icon_Gun", 5, 52, 44, 50, 100, 100, 70);
        m_stDBItem[59].SetItemInfo(1, 901, "LightBringer", "icon_Gun", 5, 63, 35, 50, 110, 90, 70);
        ///////////////////////////////////////////////////////////////////////////////

        ///////////////////////* 재료 아이템 */////////////////////////////////////////
        m_stDBItem[60].SetItemInfo(3, 100, "하급 악마의 심장", "Item_Heart_01", 1, 100, 100, 150);
        m_stDBItem[61].SetItemInfo(3, 101, "중급 악마의 심장", "Item_Heart_02", 2, 200, 500, 750);
        m_stDBItem[62].SetItemInfo(3, 102, "상급 악마의 심장", "Item_Heart_03", 3, 500, 3000, 4500);
        m_stDBItem[63].SetItemInfo(3, 103, "전설급 악마의 심장", "Item_Heart_03", 4, 2000, 10000, 15000);
                              
        m_stDBItem[64].SetItemInfo(3, 200, "하급 총열", "icon_Grenade", 1, 2000, 0, 0);
        m_stDBItem[65].SetItemInfo(3, 201, "중급 총열", "icon_Grenade", 2, 2000, 0, 0);
        m_stDBItem[66].SetItemInfo(3, 202, "상급 총열", "icon_Grenade", 3, 2000, 0, 0);
        m_stDBItem[67].SetItemInfo(3, 203, "전설급 총열", "icon_Grenade", 4, 2000, 0, 0);
                                         
        m_stDBItem[68].SetItemInfo(3, 300, "하급 스프링", "icon_Bullet", 1, 2000, 0, 0);
        m_stDBItem[69].SetItemInfo(3, 301, "중급 스프링", "icon_Bullet", 2, 2000, 0, 0);
        m_stDBItem[70].SetItemInfo(3, 302, "상급 스프링", "icon_Bullet", 3, 2000, 0, 0);
        m_stDBItem[71].SetItemInfo(3, 303, "전설급 스프링", "icon_Bullet", 4, 2000, 0, 0);
        ///////////////////////////////////////////////////////////////////////////////


    }

    public int GetMyItemSlot()
    {
        switch (m_nItemInfoIndex)
        {
            case 0:
                //무기1
                return (int)E_JA_MYITEM_SLOT.E_WEAPON_ONE;
            case 1:
                //무기2
                return (int)E_JA_MYITEM_SLOT.E_WEAPON_TWO;
            case 2:
                //방어구
                return (int)E_JA_MYITEM_SLOT.E_DEFEND;
            case 3:
                //신발
                return (int)E_JA_MYITEM_SLOT.E_SHOES;
            default:
                return 0;
        }
    }

    public int GetinvenItemTier(int nItemCode)
    {
        for (int i = 0; i < m_stDBItem.Length; i++)
        {
            if (nItemCode == m_stDBItem[i].m_nID)
            {
                return m_stDBItem[i].m_nTier;
            }
        }

        return 0;
    }

    public string GetInvenItemCodeName(int nBig, int nItemCode)
    {
        for (int i = 0; i < m_stDBItem.Length; i++)
        {
            switch (nBig)
            {
                case 1:
                    if (nItemCode == m_stDBItem[i].m_nID)
                    {
                        return m_stDBItem[i].m_sName;
                    }
                    break;
                case 3:
                    if (nItemCode == m_stDBItem[i].m_nID)
                    {
                        return m_stDBItem[i].m_sName;
                    }
                    break;
            }
           
        }

        return string.Empty;
    }

    public int GetInvenItemMaxLevel(int nItemCode)
    {
        for (int i = 0; i < m_stDBItem.Length; i++)
        {
            if (nItemCode == m_stDBItem[i].m_nID)
            {
                return m_stDBItem[i].m_nMaxLevel;
            }
        }

        return 0;
    }

	public string GetInvenItemIconName(int nBig, int nItemCode)
	{
		for ( int i = 0 ; i< m_stDBItem.Length; i++ )
		{
            switch (nBig)
            {
                case 1:
                    if (nItemCode == m_stDBItem[i].m_nID)
                    {
                        return m_stDBItem[i].m_sIcon;
                    }
                    break;
                case 3:
                    if (nItemCode == m_stDBItem[i].m_nID)
                    {
                        return m_stDBItem[i].m_sIcon;
                    }
                    break;
            }
			
		}

		return string.Empty;
	}

    public int GetInvenUseCnt()
    {
        int nCnt = 0;

        for (int i = 0; i < JAManager.I.myData.manage.m_stInven.m_nDBInvenScrollIndex; i++)
        {
            if (JAManager.I.myData.manage.m_stInven.m_stDBInven[i].m_bUseItem == true)
            {
                nCnt ++;
            }
        }

        return nCnt;
    }

    public int GetInvenItemStart(int nBigNum, int nItemCode)
    {        
        for (int i = 0; i < JAManager.I.myData.manage.m_stInven.m_nDBInvenScrollIndex; i++)
        {
            switch (JAManager.I.myData.manage.m_stInven.m_stDBInven[i].m_nBigName)
            {
                case 1:
                    JADBManager.I.m_nInvenCurTableIndex = i;
                    break;
                case 3:
                    if ( nItemCode <= 400 ) 
                    JADBManager.I.m_nInvenCurTableIndex = i;

                    break;
            }
        }
        return JADBManager.I.m_nInvenCurTableIndex;
    }

    public int GetUseItemPrice(int nBigCode, int nItemCode)
    {
        for (int i = 0; i < m_stDBItem.Length; i++)
        {
            switch (nBigCode)
            {
                case 3:
                    if (nItemCode == m_stDBItem[i].m_nID)
                    {
                        return m_stDBItem[i].m_nStat1;
                    }
                    break;
            }
        }

        return 0;
    }

    public int GetUseItemExp(int nBigCode, int nItemCode)
    {
        for (int i = 0; i < m_stDBItem.Length; i++)
        {
            switch (nBigCode)
            {
                case 3:
                    if (nItemCode == m_stDBItem[i].m_nID)
                    {
                        return m_stDBItem[i].m_nStat2;
                    }
                    break;
            }
        }

        return 0;
    }
  
    #region ### 임시 경험치 ###
    //internal float m_fExpVirtualValue = 0f;
    internal float m_fExpValue = 0f;

    float m_fExpCnt = 0;

    /// <summary>
    /// 플레이어 경험치 / 20 +1 값을 양쪽 무기 경험치에 더해줍니다.
    /// </summary>
    public void SetAddExpToBoss()
    {
        int nExp = (JAManager.I.m_pShooterRoot.GetExperinecePointInt() / 20) + 1;
        JAManager.I.myData.manage.m_stInven.m_stDBInven[(int)E_JA_MYITEM_SLOT.E_WEAPON_ONE].m_fLevelExp += nExp;
        JAManager.I.myData.manage.m_stInven.m_stDBInven[(int)E_JA_MYITEM_SLOT.E_WEAPON_TWO].m_fLevelExp += nExp;
    }

    /// <summary>
    /// 플레이어 경험치 / 20 + 1 값을 가져옵니다.
    /// </summary>
    /// <returns></returns>
    public int GetAddExpToBossInt()
    {
        return (JAManager.I.m_pShooterRoot.GetExperinecePointInt() / 20) + 1;
    }

    /// <summary>
    /// 선택된 (장착된) 현재 무기의 경험치를 fExp 만큼 올립니다.
    /// </summary>
    /// <param name="fExp"></param>
    public void SetAddExp(float fExp)
    {
        m_fExpCnt += fExp;
        //JAManager.I.myData.manage.m_stInven.m_stDBInven[(int)JAManager.I.m_eMyItemSlot].m_fLevelExpVir += JADBManager.I.GetLevelExpValue(fExp);
        JAManager.I.myData.manage.m_stInven.m_stDBInven[(int)JAManager.I.m_eMyItemSlot].m_fLevelExp += fExp;

        Debug.Log("+Exp : " + JAManager.I.myData.manage.m_stInven.m_stDBInven[(int)JAManager.I.m_eMyItemSlot].m_fLevelExp);
    }

    /// <summary>
    /// 선택된 (장착된) 현재 무기의 경험치를 fExp 만큼 내립니다.
    /// </summary>
    /// <param name="fExp"></param>
    public void SetBackExp(float fExp)
    {
        m_fExpCnt -= fExp;
        //JAManager.I.myData.manage.m_stInven.m_stDBInven[(int)JAManager.I.m_eMyItemSlot].m_fLevelExpVir -= JADBManager.I.GetLevelExpValue(fExp);
        JAManager.I.myData.manage.m_stInven.m_stDBInven[(int)JAManager.I.m_eMyItemSlot].m_fLevelExp -= fExp;

        //Debug.Log("-Exp : " + JAManager.I.myData.manage.m_stInven.m_stDBInven[(int)JAManager.I.m_eMyItemSlot].m_fLevelExp);
    }

    /// <summary>
    /// 현재 무기의 경험치값
    /// nSelectIndex에 (int)JAManager.I.m_eMyItemSlot 같은 장착무기 기입
    /// </summary>
    /// <param name="nSelectIndex"></param>
    /// <returns></returns>
    public float GetLevelExpValue(int nSelectIndex)
    {
        if (JADBManager.I.GetLevelExp(nSelectIndex, JAManager.I.myData.manage.m_stInven.m_stDBInven[nSelectIndex].m_nLevel) > 0)
        {
            return (JAManager.I.myData.manage.m_stInven.m_stDBInven[nSelectIndex].m_fLevelExp / JADBManager.I.GetLevelExp(nSelectIndex, JAManager.I.myData.manage.m_stInven.m_stDBInven[nSelectIndex].m_nLevel) * 1.0f);
        }

        return 0f;
    }
    
    /// <summary>
    /// 레벨업을 하기위해 필요한 경험치값
    /// </summary>
    /// <param name="nIndex"></param>
    /// <param name="nLevel"></param>
    /// <returns></returns>
    public float GetLevelExp(int nIndex, int nLevel)
    {
        //int nExp = 0;

        //return nExp += nLevel  * 5000;

        return (300 + (nLevel * 100)) * GetinvenItemTier(JAManager.I.myData.manage.m_stInven.m_stDBInven[nIndex].m_nItemName) / 2;
    }

    #endregion

    /// <summary>
    /// 아이템 강화에서 필요한 금액
    /// </summary>
    /// <returns></returns>
    public int GetUpgItemPrice_1()
    {
        return (JAManager.I.myData.manage.m_stInven.m_stDBInven[(int)JAManager.I.m_eMyItemSlot].m_nLevel * JAManager.I.m_nUseItemSelCnt) * 1000;
    }

    public int GetInvenItemStat(int nIndex, int nLevel, int nItemCode)
    {
        int nValue = -1;
        int nStatUp = -1;

        for (int i = 0; i < m_stDBItem.Length; i++)
        {
            if (nItemCode == m_stDBItem[i].m_nID)
            {
                switch (nIndex)
                {
                    case 1:
                        nValue = m_stDBItem[i].m_nStat1;
                        nStatUp = (nLevel * m_stDBItem[i].m_nStat1 * 5 / 100);
                        nValue += nStatUp;
                        break;
                    case 2:
                        nValue = m_stDBItem[i].m_nStat2;
                        break;
                    case 3:
                        nValue = m_stDBItem[i].m_nStat3;
                        nStatUp = nLevel;
                        nValue += nStatUp;
                        break;
                    case 4:
                        nValue = m_stDBItem[i].m_nStat4;
                        break;
                    case 5:
                        nValue = m_stDBItem[i].m_nStat5;
                        break;
                }

                return nValue;
            }
        }

        return 0;
    }


    public void Swap()
    {
        JADBInvenScript stTemp;

        for (int i = 0; i < GetInvenUseCnt(); i++)
        {
            for (int j = i; j < i + 1; j++)
            {
                if (JAManager.I.myData.manage.m_stInven.m_stDBInven[i].m_nBigName == 0)
                {
                    stTemp = JAManager.I.myData.manage.m_stInven.m_stDBInven[i];
                    JAManager.I.myData.manage.m_stInven.m_stDBInven[i] = JAManager.I.myData.manage.m_stInven.m_stDBInven[j + 1];
                    JAManager.I.myData.manage.m_stInven.m_stDBInven[j + 1] = stTemp;
                }
            }
        }

    }

    public int GetInvenItemName(E_JA_MYITEM_SLOT eState)
    {
        return JAManager.I.myData.manage.m_stInven.m_stDBInven[(int)eState].m_nItemName;
    }

    public void SetUseInvenItemLevel(E_JA_MYITEM_SLOT eState, int nLevel)
    {
        JAManager.I.myData.manage.m_stInven.m_stDBInven[(int)eState].m_nLevel = nLevel;
    }

    public int GetInvenItemLevel(int nIndex)
    {
        return JAManager.I.myData.manage.m_stInven.m_stDBInven[nIndex].m_nLevel;
    }

    public int GetInvenFirstName(E_JA_MYITEM_SLOT eState)
    {
        return JAManager.I.myData.manage.m_stInven.m_stDBInven[(int)eState].m_nFirstName;
    }

    public int GetInvenSecondName(E_JA_MYITEM_SLOT eState)
    {
        return JAManager.I.myData.manage.m_stInven.m_stDBInven[(int)eState].m_nSecondName;
    }

}
