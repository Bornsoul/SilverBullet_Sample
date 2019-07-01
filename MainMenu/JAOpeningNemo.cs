using UnityEngine;
using System.Collections;

public class JAOpeningNemo : MonoBehaviour
{
    enum eState
    {
        E_STATE_NONE,
        E_STATE_SDOWN,
        E_STATE_SUP,
        E_STATE_STAT,

    }

    public UILabel m_pLevel = null;
    public UILabel m_pLevelClass = null;
    public UILabel m_pName = null;
    public UILabel m_pRank = null;

    private eState m_eState = eState.E_STATE_NONE;
    private bool m_bShow = false;
    private Vector3 m_stScale;

    void Start()
    {
        m_stScale = transform.localScale;

        m_eState = eState.E_STATE_NONE;
    }

    IEnumerator DelayCheck(float fWaitTime)
    {
        yield return new WaitForSeconds(fWaitTime);
           
    }

    void Update()
    {
        if (JAManager.I == null) return;

        m_pRank.text = JAManager.I.myData.manage.m_stMyInfo.m_sName;// +" 분대";
        m_pName.text = JAStruckMng.I.m_pCharacterInfo[JAManager.I.m_nMyCharacter].m_sCharName;
        m_pLevelClass.text = " Level"; // + JAStruckMng.I.m_pCharacterInfo[JAManager.I.m_nMyCharacter].m_sClass + "]";

        switch ( m_eState )
        {
            case eState.E_STATE_NONE:
                break;
            case eState.E_STATE_SDOWN:
                if (m_stScale.x < 0.5f)
                {
                    m_stScale.x = 0.5f;
                    m_eState = eState.E_STATE_SUP;
                }
                m_stScale.x = Mathf.SmoothStep(m_stScale.x, 0f, 0.01f);
                m_stScale.y = Mathf.SmoothStep(m_stScale.y, 0f, 0.01f);
                m_stScale.z = Mathf.SmoothStep(m_stScale.z, 0f, 0.01f);
                break;
            case eState.E_STATE_SUP:
                if (m_stScale.x > 0.9f)
                {
                    m_stScale.x = 0.9f;
                    m_eState = eState.E_STATE_SDOWN;
                }
                m_stScale.x = Mathf.SmoothStep(m_stScale.x, 1f, 0.01f);
                m_stScale.y = Mathf.SmoothStep(m_stScale.y, 1f, 0.01f);
                m_stScale.z = Mathf.SmoothStep(m_stScale.z, 1f, 0.01f);
                break;
            case eState.E_STATE_STAT:
                break;
        }

        transform.localScale = m_stScale;
    }

    public void Button_PlayerStat()
    {
        //JAPrefabMng.I.CreatePopup("디버그", "플레이어 스탯 확인","","",E_JA_POPUP_SETTING.E_POPUP_OK);
        JAManager.I.SetTitleFadeA(false, 0, 3f);
        JAPrefabMng.I.CreatePrefab("Pop_Offset", E_JA_RESOURCELOAD.E_JIAN, "prf_PlayerStatPop");
    }

	public void Button_Cheat() // 141117jtj {
	{
		JAManager.I.m_bCamMove = false;
		JAPrefabMng.I.DestroyPrefab("prf_ItemInvenPop(Clone)");
		m_bCheatOnOff = !m_bCheatOnOff;
	//	JADBManager.I.m_pRandBox.BigWeaponBox();
		JAManager.I.SaveData();
	}

	private bool m_bCheatOnOff ;

	private void OnGUI()
	{
		if( JAManager.I == null )
		{	return ;	}
		JAManager.I.m_pShooterRoot.SetCheatOnOff(m_bCheatOnOff) ;
	} // } 141117jtj ;
}
