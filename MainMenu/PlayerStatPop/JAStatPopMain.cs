using UnityEngine;
using System.Collections;

public class JAStatPopMain : MonoBehaviour
{
    public UILabel m_pUseStatCount = null;
    public UILabel[] m_pStatlabel = null;
    public UILabel[] m_pStatCountLabel = null;
    public UIProgressBar[] m_pStatCountBar = null;
    private JAPlayerStat m_pPlayerStat = new JAPlayerStat();

    void Start()
    {
       

        for (int i = 0; i < m_pStatCountBar.Length; i++)
        {
            m_pStatCountBar[i].value = 0f;
        }

    }

    void Update()
    {
        m_pUseStatCount.text = JAManager.I.GetStringLong("남은 스탯 : ", m_pPlayerStat.GetPlayerPoint().ToString());

        m_pStatlabel[0].text = JAManager.I.GetStringLong("최대체력 : ", m_pPlayerStat.GetHealth().ToString());
        m_pStatlabel[1].text = JAManager.I.GetStringLong("명중률 : ", m_pPlayerStat.GetAccuracy().ToString());
        m_pStatlabel[2].text = JAManager.I.GetStringLong("체력회복 : ", m_pPlayerStat.GetHealthRecovery().ToString());
        m_pStatlabel[3].text = JAManager.I.GetStringLong("이동속도 : ", m_pPlayerStat.GetMoveSpeed().ToString());
        m_pStatlabel[4].text = JAManager.I.GetStringLong("소음감소 : ", m_pPlayerStat.GetNoiseReduce().ToString());

        m_pStatCountLabel[0].text = JAManager.I.GetStringLong(JAManager.I.myData.manage.m_stPlayerStat.m_fHitPointMax.ToString(), "/40");
        m_pStatCountLabel[1].text = JAManager.I.GetStringLong(JAManager.I.myData.manage.m_stPlayerStat.m_fShootAccuracyBase.ToString(), "/40");
        m_pStatCountLabel[2].text = JAManager.I.GetStringLong(JAManager.I.myData.manage.m_stPlayerStat.m_fHealthRecovery.ToString(), "/40");
        m_pStatCountLabel[3].text = JAManager.I.GetStringLong(JAManager.I.myData.manage.m_stPlayerStat.m_fMoveSpeedBase.ToString(), "/40");
        m_pStatCountLabel[4].text = JAManager.I.GetStringLong(JAManager.I.myData.manage.m_stPlayerStat.m_fNoiseReduce.ToString(), "/40");

        m_pStatCountBar[0].value = (JAManager.I.myData.manage.m_stPlayerStat.m_fHitPointMax / 40) * 1f;
        m_pStatCountBar[1].value = (JAManager.I.myData.manage.m_stPlayerStat.m_fShootAccuracyBase / 40) * 1f;
        m_pStatCountBar[2].value = (JAManager.I.myData.manage.m_stPlayerStat.m_fHealthRecovery / 40) * 1f;
        m_pStatCountBar[3].value = (JAManager.I.myData.manage.m_stPlayerStat.m_fMoveSpeedBase / 40) * 1f;
        m_pStatCountBar[4].value = (JAManager.I.myData.manage.m_stPlayerStat.m_fNoiseReduce / 40) * 1f;
    }

    public void Buttton_MaxHealth()
    {
        m_pPlayerStat.SetPlayerAddPoint(false);
        m_pPlayerStat.SetHealth();
        m_pPlayerStat.GetHealthParameters();
    }

    public void Button_MaxAccuracy()
    {
        m_pPlayerStat.SetPlayerAddPoint(false);
        m_pPlayerStat.SetAccuracy();
    }

    public void Button_HealthRecovery()
    {
        m_pPlayerStat.SetPlayerAddPoint(false);
        m_pPlayerStat.SetHealthRecovery();
    }

    public void Button_MaxMoveSpeed()
    {
        m_pPlayerStat.SetPlayerAddPoint(false);
        m_pPlayerStat.SetMoveSpeed();
    }

    public void Button_MaxNoiseReduce()
    {
        m_pPlayerStat.SetPlayerAddPoint(false);
        m_pPlayerStat.SetNoiseReduce();
    }

    public void Button_BackButton()
    {
        JAManager.I.SetTitleFadeA(true, 1, 3f);
        JAPrefabMng.I.DestroyPrefab(gameObject);
    }

    public void Button_ReSetButton()
    {
        JAManager.I.myData.manage.m_stPlayerStat.m_nPSPoint = 200;
        m_pPlayerStat.SetAllReSet();
        JAManager.I.SaveData();
    }
}
