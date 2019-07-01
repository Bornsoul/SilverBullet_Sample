using UnityEngine;
using System.Collections;

public class JADBInvenScript
{
    public int m_nItemCode; //! 아이템코드 전체 길이
    public int m_nLevel;    //! 아이템 레벨
    public float m_fLevelExp; //! 아이템 레벨 경험치
    public int m_nExceed;   //! 한계도달 레벨
    public string m_sIconName; //! 아이템 아이콘
    

    public bool m_bUseItem; //! 장착중인지 구분
    public bool m_bUseName; //! 개조하는지 

    public int m_nFirstTier;    //! 개조 접두어
    public int m_nSecondTier; //! 개조 중간

    public int m_nBigName; //! 1 0 00 00 000
    public int m_nSmallName; //! 0 1 00 00 000
    public int m_nFirstName; //! 0 0 11 00 000
    public int m_nSecondName; //! 0 0 00 11 000
    public int m_nItemName; //! 0 0 00 00 111

	public int m_nItemValue; //! 0 0 1111 000

    #region ### 나중에 치트사용자들을 알아내기위해 쓰입니다. ###
    public long m_nSerialNo;
    public int m_nLogNo;
    public int m_nCheckNo;
    #endregion


    public void SetInven_DeleteData()
    {
        m_nBigName = 0;
        m_nSmallName = 0;
        m_nFirstName = 0;
        m_nSecondName = 0;
        m_nItemName = 0;
        m_nItemCode = 0;
        m_nLevel = 0;
        m_fLevelExp = 0;
        m_nExceed = 0;
        m_nFirstTier = 0;
        m_nSecondTier = 0;
        m_bUseItem = false;
        m_bUseName = false;
		m_nItemValue = 0;
        m_sIconName = "";
    }

    public void SetAddInven_UseItemData(int nBig, int nSmall, int nItemCnt, int nCode, int nLevel, float fLevelExp, bool bUse, bool bUseName, string sIconName)
	{
		m_nBigName = nBig;
		m_nSmallName = nSmall;
		m_nItemValue = nItemCnt;
		m_nItemName = nCode;

		m_nItemCode = 0;

		m_nItemCode += m_nBigName * 100000000;
		m_nItemCode += m_nSmallName * 10000000;
		m_nItemCode += m_nItemValue * 1000;
		m_nItemCode += m_nItemName;

        m_nLevel = nLevel;
        m_fLevelExp = fLevelExp;
		m_bUseItem = bUse;
        m_bUseName = bUseName;
		m_sIconName = sIconName;
	}

    //public void SetAddInven_DataInfo(int nBig, int nSmall, int nFirst, int nSecond, int nItem, bool bUse)
    //{
    //    m_nBigName = nBig;
    //    m_nSmallName = nSmall;
    //    m_nFirstName = nFirst;
    //    m_nSecondName = nSecond;
    //    m_nItemName = nItem;

    //    m_nItemCode = 0;

    //    m_nItemCode += m_nBigName * 100000000;
    //    m_nItemCode += m_nSmallName * 10000000;
    //    m_nItemCode += m_nFirstName * 100000;
    //    m_nItemCode += m_nSecondName * 1000;
    //    m_nItemCode += m_nItemName;

    //    m_bUseItem = bUse;
    //}

    public void SetAddInven_DataInfo(int nBig, int nSmall, int nFirst, int nSecond, int nItem, int nLevel, float fLevelExp, int nExceed, bool bUse, string sIconName)
    {
        m_nBigName = nBig;
        m_nSmallName = nSmall;
        m_nFirstName = nFirst;
        m_nSecondName = nSecond;
        m_nItemName = nItem;

        m_nItemCode = 0;

        m_nItemCode += m_nBigName * 100000000;
        m_nItemCode += m_nSmallName * 10000000;
        m_nItemCode += m_nFirstName * 100000;
        m_nItemCode += m_nSecondName * 1000;
        m_nItemCode += m_nItemName;

        m_bUseItem = bUse;
        m_nLevel = nLevel;
        m_fLevelExp = fLevelExp;
        m_nExceed = nExceed;
        m_sIconName = sIconName;
    }

    public void SetAddInven_DataInfo(int nBig, int nSmall, int nFirst, int nSecond, int nItem, bool bUse, string sIconName)
    {
        m_nBigName = nBig;
        m_nSmallName = nSmall;
        m_nFirstName = nFirst;
        m_nSecondName = nSecond;
        m_nItemName = nItem;

        m_nItemCode = 0;

        m_nItemCode += m_nBigName * 100000000;
        m_nItemCode += m_nSmallName * 10000000;
        m_nItemCode += m_nFirstName * 100000;
        m_nItemCode += m_nSecondName * 1000;
        m_nItemCode += m_nItemName;

        m_bUseItem = bUse;
        m_sIconName = sIconName;
    }

    public void SetAddInven_DataInfo(int nBig, int nSmall, int nFirst, int nSecond, int nItem, int nLevel, float fLevelExp, int nExceed, bool  bUse)
    {
        m_nBigName = nBig;
        m_nSmallName = nSmall;
        m_nFirstName = nFirst;
        m_nSecondName = nSecond;
        m_nItemName = nItem;

        m_nItemCode = 0;
        
        m_nItemCode += m_nBigName * 100000000;
        m_nItemCode += m_nSmallName * 10000000;
        m_nItemCode += m_nFirstName * 100000;
        m_nItemCode += m_nSecondName * 1000;
        m_nItemCode += m_nItemName;

        m_bUseItem = bUse;
        m_nLevel = nLevel;
        m_fLevelExp = fLevelExp;
        m_nExceed = nExceed;
    }


    public void SetAddInven_BigNameInfo(int nID)
    {
        m_nItemCode -= m_nBigName * 100000000;

        m_nBigName = nID;
        m_nItemCode += m_nBigName * 100000000;
    }

    public void SetAddItemSmallName(int nID)
    {
        m_nItemCode -= m_nBigName * 100000000;
        m_nItemCode -= m_nSmallName * 10000000;


        m_nSmallName = nID;
        m_nItemCode += m_nSmallName * 10000000;
        m_nItemCode += m_nBigName * 100000000;
    }

    public void SetAddItemFirstName(int nID)
    {
        m_nItemCode -= m_nBigName * 100000000;
        m_nItemCode -= m_nSmallName * 10000000;
        m_nItemCode -= m_nFirstName * 100000;

        m_nFirstName = nID;
        m_nItemCode += m_nFirstName * 100000;
        m_nItemCode += m_nSmallName * 10000000;
        m_nItemCode += m_nBigName * 100000000;

    }

    public void SetAddSecondName(int nID)
    {
        m_nItemCode -= m_nBigName * 100000000;
        m_nItemCode -= m_nSmallName * 10000000;
        m_nItemCode -= m_nFirstName * 100000;
        m_nItemCode -= m_nSecondName * 1000;

        m_nSecondName = nID;
        m_nItemCode += m_nSecondName * 1000;
        m_nItemCode += m_nFirstName * 100000;
        m_nItemCode += m_nSmallName * 10000000;
        m_nItemCode += m_nBigName * 100000000;
    }

    public void SetAddItemName(int nID)
    {
        m_nItemCode -= m_nBigName * 100000000;
        m_nItemCode -= m_nSmallName * 10000000;
        m_nItemCode -= m_nFirstName * 100000;
        m_nItemCode -= m_nSecondName * 1000;
        m_nItemCode -= m_nItemName;

        m_nItemName = nID;
        m_nItemCode += m_nItemName;
        m_nItemCode += m_nSecondName * 1000;
        m_nItemCode += m_nFirstName * 100000;
        m_nItemCode += m_nSmallName * 10000000;
        m_nItemCode += m_nBigName * 100000000;

    }


    public void SetItemLevelExceed(int nLevel, int nExceed)
    {
        m_nLevel = nLevel;
        m_nExceed = nExceed;
    }

    public void SetItemIcon(string sName)
    {
        m_sIconName = sName;
    }

    public bool GetItemUse()
    {
        return m_bUseItem;
    }

    public int GetItemCode()
    {
        return m_nItemCode;
    }

    public int GetItemLevel()
    {
        return m_nLevel;
    }

    public int GetItemExceed()
    {
        return m_nExceed;
    }

    public string GetItemIconName()
    {
        return m_sIconName;
    }


}
