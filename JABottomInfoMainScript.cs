using UnityEngine;
using System.Collections;

public class JABottomInfoMainScript : MonoBehaviour
{
    
    public UILabel[] m_pInfoLabels = null;
    public Animation m_pAni = null;


    void Start()
    {
        JAManager.I.LoadData();
        m_pAni.Play();

    }

    void Update()
    {
       
        SetSettingLabel();


    }

	public void SetSettingLabel()
	{
		m_pInfoLabels[(int)E_JA_BOTTOMINFO.E_PRICE].text = JAManager.I.m_pStringStore_Src[0].strings[19] + JAManager.I.myData.manage.m_stMyInfo.m_nPrice;
		m_pInfoLabels[(int)E_JA_BOTTOMINFO.E_BULLET].text = JAManager.I.m_pStringStore_Src[0].strings[20] + JAManager.I.myData.manage.m_stMyInfo.m_nBullet+
			JAManager.I.m_pStringStore_Src[0].strings[21];
		m_pInfoLabels[(int)E_JA_BOTTOMINFO.E_AMMO].text = JAManager.I.m_pStringStore_Src[0].strings[22] + JAManager.I.myData.manage.m_stMyInfo.m_nAmmo;
		m_pInfoLabels[(int)E_JA_BOTTOMINFO.E_KARMA].text = JAManager.I.m_pStringStore_Src[0].strings[23] + JAManager.I.myData.manage.m_stMyInfo.m_nKarma;
		m_pInfoLabels[(int)E_JA_BOTTOMINFO.E_SPAWN].text = JAManager.I.m_pStringStore_Src[0].strings[24] + JAManager.I.myData.manage.m_stMyInfo.m_nSpawn+"/10";
		m_pInfoLabels[(int)E_JA_BOTTOMINFO.E_FRIEND].text = JAManager.I.m_pStringStore_Src[0].strings[25] + JAManager.I.myData.manage.m_stMyInfo.m_nFriend+"/30";
	}
	
}
