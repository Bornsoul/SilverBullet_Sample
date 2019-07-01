using UnityEngine;
using System.Collections;

[System.Serializable]
public class JADBItemScript
{
    public int m_nBigName;
    public int m_nID;
    public string m_sName;
    public int m_nPrice;
    public int m_nTier;
    public int m_nChara;
    public int m_nMax_Level;
    public int m_nMax_Exceed;

    public string m_sModeling;
    public float m_fPitch_X;
    public float m_fPitch_Y;
    public float m_fPitch_Z;

    public string m_sIcon;

    public int m_nStat1;
    public int m_nStat2;
    public int m_nStat3;
    public int m_nStat4;
    public int m_nStat5;

    public int m_nStat1_inc;
    public int m_nStat2_inc;
    public int m_nStat3_inc;
    public int m_nStat4_inc;
    public int m_nStat5_inc;

    public int m_nXstat1_ID;
    public int m_nXstat2_ID;
    public int m_nXstat3_ID;

    public int m_nXstat1_val;
    public int m_nXstat2_val;
    public int m_nXstat3_val;
    public int m_nMaxLevel;


	public void SetItemInfo(int nBigName, int nItemID, string sName, string sIcon, int nTier, int nPrice, int nMinExp, int nMaxExp)
	{
        m_nBigName = nBigName;
		m_nID = nItemID;
		m_sName = sName;
		m_sIcon = sIcon;
        m_nTier = nTier;
        m_nStat1 = nPrice;
        m_nStat2 = nMinExp;
        m_nStat3 = nMaxExp;
	}

    public void SetItemInfo(int nBigName, int nItemID, string sName, string sIcon, int nTier, int nStat1, int nStat2, int nStat3, int nStat4, int nStat5, int nMaxLevel)
    {
        m_nBigName = nBigName;
        m_nID = nItemID;
        m_sName = sName;
        m_sIcon = sIcon;
        m_nTier = nTier;
        m_nStat1 = nStat1;
        m_nStat2 = nStat2;
        m_nStat3 = nStat3;
        m_nStat4 = nStat4;
        m_nStat5 = nStat5;
        m_nMaxLevel = nMaxLevel;
    }

    public string GetItemName()
    {
        return m_sName;
    }

    public int GetItemPrice()
    {
        return m_nPrice;
    }

    public int GetItemTier()
    {
        return m_nTier;
    }

    public int GetItemChara()
    {
        return m_nChara;
    }

    public string GetIconName()
    {
        return m_sIcon;
    }

    public int GetStat1()
    {
        return m_nStat1;
    }

    public int GetStat2()
    {
        return m_nStat2;
    }

    public int GetStat3()
    {
        return m_nStat3;
    }

    public int GetStat4()
    {
        return m_nStat4;
    }

    public int GetStat5()
    {
        return m_nStat5;
    }
}
