using UnityEngine;
using System.Collections;

[System.Serializable]
public class JADBItemFirstSubName
{
    public string m_sName;
    public int m_nMaxLevel;//! 최대 레벨.
    public int m_nStat1;    //! 공격력.
    public int m_nStat2;    //! 공격 속도.
    public int m_nStat3;    //! 명중률.
    public int m_nStat4;    //! 사거리.
    public int m_nStat5;    //! 소음.
    public int m_nTier;     //! 티어.

    /// <summary>
    /// nMaxLevel : 최대 레벨
    /// nStat1 : 공격력,
    /// nStat2 : 공격속도,
    /// nStat3 : 명중률,
    /// nStat4 : 사거리,
    /// nStat5 : 소음
    /// </summary>
    /// <param name="sName"></param>
    /// <param name="nMaxLevel"></param>
    /// <param name="nStat1"></param>
    /// <param name="nStat2"></param>
    /// <param name="nStat3"></param>
    /// <param name="nStat4"></param>
    /// <param name="nStat5"></param>
    /// <param name="nTier"></param>
    public void SetFistSubName(string sName, int nMaxLevel, int nStat1, int nStat2, int nStat3, int nStat4, int nStat5, int nTier)
    {
        m_sName = sName;
        m_nMaxLevel = nMaxLevel;
        m_nStat1 = nStat1;
        m_nStat2 = nStat2;
        m_nStat3 = nStat3;
        m_nStat4 = nStat4;
        m_nStat5 = nStat5;
        m_nTier = nTier;
    }
};

[System.Serializable]
public class JADBItemSecondSubName
{
    public string m_sName;
    public int m_nStat1;    //! 체력 증가.
    public int m_nStat2;    //! 체력 리젠.
    public int m_nStat3;    //! 체력 흡혈.
    public int m_nStat4;    //! 근접 뎀증.
    public int m_nStat5;    //! 고뎀 감소.
    public int m_nStat6;    //! 슬로 저항.
    public int m_nStat7;    //! 잠입 속도.
    public int m_nStat8;    //! 쿨탐 감소.
    public int m_nStat9;    //! 치타 증가.
    public int m_nStat10;   //! 회피 증가.
    public int m_nStat11;   //! 발각 감소.
    public int m_nTier;     //!티어.

    /// <summary>
    /// m_nStat1;   체력 증가.
    /// m_nStat2;   체력 리젠.
    /// m_nStat3;   체력 흡혈.
    /// m_nStat4;   근접 뎀증.
    /// m_nStat5;   고뎀 감소.
    /// m_nStat6;   슬로 저항.
    /// m_nStat7;   잠입 속도.
    /// m_nStat8;   쿨탐 감소.
    /// m_nStat9;   치타 증가.
    /// m_nStat10; 회피 증가.
    /// m_nStat11; 발각 감소.
    /// 
    /// </summary>
    /// <param name="sName"></param>
    /// <param name="nStat1"></param>
    /// <param name="nStat2"></param>
    /// <param name="nStat3"></param>
    /// <param name="nStat4"></param>
    /// <param name="nStat5"></param>
    /// <param name="nStat6"></param>
    /// <param name="nStat7"></param>
    /// <param name="nStat8"></param>
    /// <param name="nStat9"></param>
    /// <param name="nStat10"></param>
    /// <param name="nStat11"></param>
    /// <param name="nTier"></param>
    public void SetSecondSubName(string sName, int nStat1, int nStat2, int nStat3, int nStat4,
                                            int nStat5, int nStat6, int nStat7, int nStat8, int nStat9, int nStat10, int nStat11, int nTier)
    {
        m_sName = sName;
        m_nStat1 = nStat1;
        m_nStat2 = nStat2;
        m_nStat3 = nStat3;
        m_nStat4 = nStat4;
        m_nStat5 = nStat5;
        m_nStat6 = nStat6;
        m_nStat7 = nStat7;
        m_nStat8 = nStat8;
        m_nStat9 = nStat9;
        m_nStat10 = nStat10;
        m_nStat11 = nStat11;
        m_nTier = nTier;
    }
};