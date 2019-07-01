using UnityEngine;
using System.Collections;

public class JAPvPMPlayerInfoBox : MonoBehaviour
{
    enum eText
    {
        E_NAME,
        E_LEVEL,
        E_RANK,
        E_BATTLEPOINT,
        E_WINLOSS,

        E_STAT1,
        E_STAT2,
        E_STAT3,
        E_STAT4,
        E_STAT5,
    };

    public UILabel[] sTextLabel = null;

    void Start()
    {

    }

    public void SetTextDataInfoSetting(int nIndex)
    {
        sTextLabel[(int)eText.E_NAME].text = JAStruckMng.I.m_pPvpFriendPlayerInfo[nIndex].m_sName;
        sTextLabel[(int)eText.E_LEVEL].text = "레벨 " + JAStruckMng.I.m_pPvpFriendPlayerInfo[nIndex].m_nLevel.ToString();
        sTextLabel[(int)eText.E_RANK].text = "랭킹 " + JAStruckMng.I.m_pPvpFriendPlayerInfo[nIndex].m_nRank.ToString();
        sTextLabel[(int)eText.E_BATTLEPOINT].text = "배틀포인트 " + JAStruckMng.I.m_pPvpFriendPlayerInfo[nIndex].m_nPoint.ToString();
        sTextLabel[(int)eText.E_WINLOSS].text = JAStruckMng.I.m_pPvpFriendPlayerInfo[nIndex].m_nWin.ToString() + "승 " +
                                                            JAStruckMng.I.m_pPvpFriendPlayerInfo[nIndex].m_nLoss.ToString() + "패";

        //sTextLabel[(int)eText.E_BATTLEPOINT].text = "최대체력: " + JAManager.I.m_pStruckMng.m_pPvpFriendPlayerInfo[nIndex].;
        //sTextLabel[(int)eText.E_BATTLEPOINT].text = "";
        //sTextLabel[(int)eText.E_BATTLEPOINT].text = "";
        //sTextLabel[(int)eText.E_BATTLEPOINT].text = "";
        //sTextLabel[(int)eText.E_BATTLEPOINT].text = "";
       
    }

    void Update()
    {
        if ( JAManager.I !=null)
        SetTextDataInfoSetting(JAManager.I.m_nPvpFriendTableSelect);
    }
}
