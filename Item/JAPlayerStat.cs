using UnityEngine;
using System.Collections;

public class JAPlayerStat : MonoBehaviour
{
    const int m_nMaxPoint = 40;

    void Start()
    {

    }

    public void Update()
    {

    }

    public void SetPlayerAddPoint(bool bAdd)
    {
        if (GetPlayerPoint() <= 0)
        {
            JAPrefabMng.I.CreatePopup("능력치", "능력치 포인트가 부족합니다!");
            return;
        }
        if ( bAdd == false )
            JAManager.I.myData.manage.m_stPlayerStat.m_nPSPoint--;
        else
            JAManager.I.myData.manage.m_stPlayerStat.m_nPSPoint++;
    }

    public int GetPlayerPoint()
    {
        return JAManager.I.myData.manage.m_stPlayerStat.m_nPSPoint;
    }

    //! 최대체력 수정후 한번 호출해야함
    public void GetHealthParameters() { JAManager.I.m_pShooterRoot.SetParameters(); }

    /// <summary>
    /// 최대 체력
    /// 값 수정후 GetHealthParameters() 한번 호출해주어야합니다.
    /// </summary>
    public void SetHealth()
    {
        if (JAManager.I.myData.manage.m_stPlayerStat.m_fHitPointMax >= m_nMaxPoint)
        {
            JAPrefabMng.I.CreatePopup("능력치", "최대체력이 최대치에 도달했습니다.");
            JAManager.I.myData.manage.m_stPlayerStat.m_fHitPointMax = m_nMaxPoint;
            return;
        }
        JAManager.I.myData.manage.m_stPlayerStat.m_fHitPointMax ++;
        JAManager.I.m_pShooterRoot.fHitPointMax = ( JAManager.I.myData.manage.m_stPlayerStat.m_fHitPointMax * 50 );
        JAManager.I.SaveData();
    }

    public float GetHealth()
    {
        return JAManager.I.m_pShooterRoot.fHitPointMax = (JAManager.I.myData.manage.m_stPlayerStat.m_fHitPointMax * 50);
    }

    /// <summary>
    /// 명중률
    /// </summary>
    public void SetAccuracy()
    {
        if (JAManager.I.myData.manage.m_stPlayerStat.m_fShootAccuracyBase > m_nMaxPoint)
        {
            JAPrefabMng.I.CreatePopup("능력치", "명중률이 최대치에 도달했습니다.");
            JAManager.I.myData.manage.m_stPlayerStat.m_fShootAccuracyBase = m_nMaxPoint;
            return;
        }
        JAManager.I.myData.manage.m_stPlayerStat.m_fShootAccuracyBase++;
        // 기본값 = 캐릭터 레벨에 따른 명중률 + 아이템에 따른 명중률
        float fBase_Accuracy = 40.0f; //임시. 원래는 위의 레벨당 캐릭 명중률 + 아이템 명중률
        JAManager.I.m_pShooterRoot.fShootAccuracyBase = fBase_Accuracy
                                                                       + (JAManager.I.myData.manage.m_stPlayerStat.m_fShootAccuracyBase * 0.02f * fBase_Accuracy);
        JAManager.I.SaveData();
    }

    public float GetAccuracy()
    {
        float fBase_Accuracy = 40.0f; //임시. 원래는 위의 레벨당 캐릭 명중률 + 아이템 명중률
        return JAManager.I.m_pShooterRoot.fShootAccuracyBase = fBase_Accuracy +
                                                                            (JAManager.I.myData.manage.m_stPlayerStat.m_fShootAccuracyBase * 0.02f * fBase_Accuracy);
    }

    /// <summary>
    /// 체력회복
    /// </summary>
    public void SetHealthRecovery()
    {
        if (JAManager.I.myData.manage.m_stPlayerStat.m_fHealthRecovery > m_nMaxPoint)
        {
            JAPrefabMng.I.CreatePopup("능력치", "체력회복이 최대치에 도달했습니다.");
            JAManager.I.myData.manage.m_stPlayerStat.m_fHealthRecovery = m_nMaxPoint;
            return;
        }
        JAManager.I.myData.manage.m_stPlayerStat.m_fHealthRecovery++;
        float fBase_HealthRecovery = 20.0f;
        JAManager.I.m_pShooterRoot.fHealthRecovery = fBase_HealthRecovery + (JAManager.I.myData.manage.m_stPlayerStat.m_fHealthRecovery * 0.02f * fBase_HealthRecovery);
        Debug.Log(JAManager.I.m_pShooterRoot.fHealthRecovery);
        JAManager.I.SaveData();
    }

    public float GetHealthRecovery()
    {
        float fBase_HealthRecovery = 20.0f;
        return JAManager.I.m_pShooterRoot.fHealthRecovery = fBase_HealthRecovery + (JAManager.I.myData.manage.m_stPlayerStat.m_fHealthRecovery * 0.02f * fBase_HealthRecovery);
    }

    /// <summary>
    /// 이동속도
    /// </summary>
    public void SetMoveSpeed()
    {
        if (JAManager.I.myData.manage.m_stPlayerStat.m_fMoveSpeedBase > m_nMaxPoint)
        {
            JAPrefabMng.I.CreatePopup("능력치", "이동속도가 최대치에 도달했습니다.");
            JAManager.I.myData.manage.m_stPlayerStat.m_fMoveSpeedBase = m_nMaxPoint;
            return;
        }
        JAManager.I.myData.manage.m_stPlayerStat.m_fMoveSpeedBase++;
        JAManager.I.m_pShooterRoot.fMoveSpeedBase = (JAManager.I.myData.manage.m_stPlayerStat.m_fMoveSpeedBase * 0.1f);
        Debug.Log(JAManager.I.m_pShooterRoot.fMoveSpeedBase);
    }

    public float GetMoveSpeed()
    {
        return JAManager.I.m_pShooterRoot.fMoveSpeedBase = (JAManager.I.myData.manage.m_stPlayerStat.m_fMoveSpeedBase * 0.1f);
    } 

    /// <summary>
    /// 소음 감소
    /// </summary>
    public void SetNoiseReduce()
    {
        if (JAManager.I.myData.manage.m_stPlayerStat.m_fNoiseReduce > m_nMaxPoint)
        {
            JAPrefabMng.I.CreatePopup("능력치", "소음감소가 최대치에 도달했습니다.");
            JAManager.I.myData.manage.m_stPlayerStat.m_fNoiseReduce = m_nMaxPoint;
            return;
        }
        JAManager.I.myData.manage.m_stPlayerStat.m_fNoiseReduce++;
        JAManager.I.m_pShooterRoot.fNoiseReduce = (-JAManager.I.myData.manage.m_stPlayerStat.m_fNoiseReduce * 0.1f);
        Debug.Log(JAManager.I.m_pShooterRoot.fNoiseReduce);
        JAManager.I.SaveData();
    }

    public float GetNoiseReduce()
    {
        return JAManager.I.m_pShooterRoot.fNoiseReduce = (-JAManager.I.myData.manage.m_stPlayerStat.m_fNoiseReduce * 0.1f);
    }

    public void SetAllReSet()
    {
        JAManager.I.myData.manage.m_stPlayerStat.m_fHitPointMax = 0;
        JAManager.I.myData.manage.m_stPlayerStat.m_fShootAccuracyBase = 0;
        JAManager.I.myData.manage.m_stPlayerStat.m_fHealthRecovery = 0;
        JAManager.I.myData.manage.m_stPlayerStat.m_fMoveSpeedBase = 0;
        JAManager.I.myData.manage.m_stPlayerStat.m_fNoiseReduce = 0;

        JAManager.I.SaveData();
    }
}
