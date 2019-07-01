using UnityEngine;
using System.Collections;
using System.Text;

public class JAPvPMenuPlayerInfoBox : MonoBehaviour
{
    public UILabel[] sMyInfo = null;
    public UILabel[] sFriendInfo = null;
    public UILabel[] sEnemyInfo1 = null;
    public UILabel[] sEnemyInfo2 = null;

    void Start()
    {

    }

    public void SetTextDataSetting(int nFriendIndex, int nEnemyIndex1, int nEnemyIndex2)
    {
        ////// 자기 정보 //////
        sMyInfo[0].text = JAManager.I.myData.manage.m_stMyInfo.m_sName;

        StringBuilder sbLevelRank = new StringBuilder();
        sbLevelRank.Append("Lv ");
        sbLevelRank.Append(JAManager.I.myData.manage.m_stMyInfo.m_nLevel);
        sbLevelRank.Append(" / 랭킹 ");
        sbLevelRank.Append(JAManager.I.myData.manage.m_stMyInfo.m_nRank);
        sMyInfo[1].text = sbLevelRank.ToString();

        sMyInfo[2].text = "배틀 포인트: " + JAManager.I.myData.manage.m_stMyInfo.m_nBattlePoint;
           
        StringBuilder sbWinLoss = new StringBuilder();
        sbWinLoss.Append("전적 : ");
        sbWinLoss.Append(JAManager.I.myData.manage.m_stMyInfo.m_nWin);
        sbWinLoss.Append("승 ");
        sbWinLoss.Append(JAManager.I.myData.manage.m_stMyInfo.m_nLose);
        sbWinLoss.Append("패");
        sMyInfo[3].text = sbWinLoss.ToString();
        //////////////////////////

        ////// 친구 정보 //////
        sFriendInfo[0].text = JAStruckMng.I.m_pPvpFriendPlayerInfo[nFriendIndex].m_sName;

        StringBuilder sbLevelRankFriend = new StringBuilder();
        sbLevelRankFriend.Append("Lv ");
        sbLevelRankFriend.Append(JAStruckMng.I.m_pPvpFriendPlayerInfo[nFriendIndex].m_nLevel);
        sbLevelRankFriend.Append(" / 랭킹 ");
        sbLevelRankFriend.Append(JAStruckMng.I.m_pPvpFriendPlayerInfo[nFriendIndex].m_nRank);
        sFriendInfo[1].text = sbLevelRankFriend.ToString();

        sFriendInfo[2].text = "배틀 포인트: " + JAStruckMng.I.m_pPvpFriendPlayerInfo[nFriendIndex].m_nPoint;

        StringBuilder sbWinLossFriend = new StringBuilder();
        sbWinLossFriend.Append(JAStruckMng.I.m_pPvpFriendPlayerInfo[nFriendIndex].m_nWin);
        sbWinLossFriend.Append("승 ");
        sbWinLossFriend.Append(JAStruckMng.I.m_pPvpFriendPlayerInfo[nFriendIndex].m_nLoss);
        sbWinLossFriend.Append("패");
        sFriendInfo[3].text = sbWinLossFriend.ToString();
        ////////////////////////////

        ////// 적군1 정보 //////
        sEnemyInfo1[0].text = JAStruckMng.I.m_pPvpEnemyPlayerInfo[nEnemyIndex1].m_sName;

        StringBuilder sbLevelRankEnemy1 = new StringBuilder();
        sbLevelRankEnemy1.Append("Lv ");
        sbLevelRankEnemy1.Append(JAStruckMng.I.m_pPvpEnemyPlayerInfo[nEnemyIndex1].m_nLevel);
        sbLevelRankEnemy1.Append(" / 랭킹 ");
        sbLevelRankEnemy1.Append(JAStruckMng.I.m_pPvpEnemyPlayerInfo[nEnemyIndex1].m_nRank);
        sEnemyInfo1[1].text = sbLevelRankEnemy1.ToString();

        sEnemyInfo1[2].text = "배틀 포인트: " + JAStruckMng.I.m_pPvpEnemyPlayerInfo[nEnemyIndex1].m_nPoint;

        StringBuilder sbWinLossEnemy1 = new StringBuilder();
        sbWinLossEnemy1.Append("전적 : ");
        sbWinLossEnemy1.Append(JAStruckMng.I.m_pPvpEnemyPlayerInfo[nEnemyIndex1].m_nWin);
        sbWinLossEnemy1.Append("승 ");
        sbWinLossEnemy1.Append(JAStruckMng.I.m_pPvpEnemyPlayerInfo[nEnemyIndex1].m_nLoss);
        sbWinLossEnemy1.Append("패");
        sEnemyInfo1[3].text = sbWinLoss.ToString();
        //////////////////////////////

        ////// 적군2 정보 //////
        sEnemyInfo2[0].text = JAStruckMng.I.m_pPvpEnemyPlayerInfo[nEnemyIndex2].m_sName;

        StringBuilder sbLevelRankEnemy2 = new StringBuilder();
        sbLevelRankEnemy2.Append("Lv ");
        sbLevelRankEnemy2.Append(JAStruckMng.I.m_pPvpEnemyPlayerInfo[nEnemyIndex2].m_nLevel);
        sbLevelRankEnemy2.Append(" / 랭킹 ");
        sbLevelRankEnemy2.Append(JAStruckMng.I.m_pPvpEnemyPlayerInfo[nEnemyIndex2].m_nRank);
        sEnemyInfo2[1].text = sbLevelRankEnemy2.ToString();

        sEnemyInfo2[2].text = "배틀 포인트: " + JAStruckMng.I.m_pPvpEnemyPlayerInfo[nEnemyIndex2].m_nPoint;

        StringBuilder sbWinLossEnemy2 = new StringBuilder();
        sbWinLossEnemy2.Append(JAStruckMng.I.m_pPvpEnemyPlayerInfo[nEnemyIndex2].m_nWin);
        sbWinLossEnemy2.Append("승 ");
        sbWinLossEnemy2.Append(JAStruckMng.I.m_pPvpEnemyPlayerInfo[nEnemyIndex2].m_nLoss);
        sbWinLossEnemy2.Append("패");
        sEnemyInfo2[3].text = sbWinLossEnemy2.ToString();
        /////////////////////////
    }


    void Update()
    {
        SetTextDataSetting(JAManager.I.m_nPvpFriendSelectInfo, JAManager.I.m_nPvpEnemyRand1, JAManager.I.m_nPvpEnemyRand2);
        //Debug.Log("Rand1 = " + JAManager.I.m_nPvpEnemyRand1 + ", Rand2 = " + JAManager.I.m_nPvpEnemyRand2);
    }

    
}
