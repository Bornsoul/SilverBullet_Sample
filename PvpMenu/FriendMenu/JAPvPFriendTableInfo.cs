using UnityEngine;
using System.Collections;
using System.Text;

public class JAPvPFriendTableInfo : MonoBehaviour
{

    public UISprite m_pBackSprite = null;
    public UILabel m_pName = null;
    public UILabel m_pLevelRank = null;
    public UILabel m_pBattlePoint = null;

    [HideInInspector]
    public int m_nIndex = 0;
    private bool m_bIndexCheck = false;

    void Start()
    {
        
    }

    public void Destroy()
    {
        Destroy(m_pBackSprite);
        m_pBackSprite = null;

        Destroy(m_pName);
        m_pName = null;

        Destroy(m_pLevelRank);
        m_pLevelRank = null;

        Destroy(m_pBattlePoint);
        m_pBattlePoint = null;
    }

    public void SetTextDataSetting(int nIndex)
    {
        m_pName.text = JAStruckMng.I.m_pPvpFriendPlayerInfo[nIndex].m_sName;

        StringBuilder sbLevelRank = new StringBuilder();
        sbLevelRank.Append("Lv ");
        sbLevelRank.Append(JAStruckMng.I.m_pPvpFriendPlayerInfo[nIndex].m_nLevel);
        sbLevelRank.Append(" / ");
        sbLevelRank.Append("랭킹 ");
        sbLevelRank.Append(JAStruckMng.I.m_pPvpFriendPlayerInfo[nIndex].m_nRank);
        sbLevelRank.Append("위");

        m_pLevelRank.text = sbLevelRank.ToString();
        m_pBattlePoint.text = "배틀포인트: " + JAStruckMng.I.m_pPvpFriendPlayerInfo[nIndex].m_nPoint.ToString();
    }

    void Update()
    {
     

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
        JAManager.I.m_nPvpFriendTableSelect = m_nIndex;

        //int nTotal = transform.name.Length - 19;
        //string sID = transform.name.Substring(0 , nTotal);
      
    }

}
