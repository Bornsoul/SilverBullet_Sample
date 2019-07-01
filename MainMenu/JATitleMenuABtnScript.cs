using UnityEngine;
using System.Collections;

public class JATitleMenuABtnScript : MonoBehaviour
{
    enum eState
    {
        E_NONE,
        E_ALPHA_START,
        E_MAIN,
        E_END,
        E_DESTROY,
    };

    eState m_eState = eState.E_NONE;

    public UIButtonMessage[] m_pButtonMessage = null;
    public UISprite[] m_pButtonSprite = null;
    private float[] m_fBtnAlpha = new float[6];

    private bool m_bCheatOnOff = false;
    public GameObject m_pTitleBScroll_Obj = null;
      

    void Awake()
    {
        JAManager.I.m_bCamMove = true;
        for (int i = 0; i < m_pButtonSprite.Length; i++)
        {
            m_fBtnAlpha[i] = 0f; 
            m_pButtonSprite[i].alpha = 0f;
            m_pButtonSprite[i].enabled = false;
        }
    }

    void Start()
    {
        //StartCoroutine(StartDelay(0.1f));
        m_eState = eState.E_ALPHA_START;
    }
    
    IEnumerator StartDelay(float fWaitTime)
    {
        yield return new WaitForSeconds(fWaitTime);

        m_eState = eState.E_ALPHA_START;
    }

    void OnDestroy()
    {
        for (int i = 0; i < m_pButtonMessage.Length; i++)
        {
            Destroy(m_pButtonMessage[i]);
            m_pButtonMessage[i] = null; 
        }
        for (int i = 0; i < m_pButtonSprite.Length; i++)
        {
            Destroy(m_pButtonSprite[i]);
            m_pButtonSprite[i] = null;
        }

        for (int i = 0; i < m_fBtnAlpha.Length; i++)
        {
            m_fBtnAlpha[i] = 0f;
        }
    }

    void Update()
    {
        if (JAManager.I.m_bInvenState == false)
        {
            if (JAManager.I.m_pShooterRoot.panel_TitleMenuA.isInputable == true)
            {
                if (Input.GetKeyDown(KeyCode.Escape) == true)
                {
                    JAPrefabMng.I.CreatePrefab("Pop_Offset", E_JA_RESOURCELOAD.E_JIAN, "prf_QuitPop");
                }
            }
        }

        switch (m_eState)
        {
            case eState.E_NONE:
                {

                }
                break;
            case eState.E_ALPHA_START:
				{
					StartCoroutine(StartFadeIn(0.1f));
					if (m_pButtonSprite[m_pButtonSprite.Length-1].enabled == true)
					{	m_eState = eState.E_MAIN;	}
				}
                break;
            case eState.E_MAIN:
                {
                    //Debug.Log("MAIN");
                }
                break;
            case eState.E_END:
                {
                    m_bCheatOnOff = false;
                    StartCoroutine(StartFadeOut(0.1f));
                    if (m_pButtonSprite[m_pButtonSprite.Length-1].enabled == false)
                    {
                        m_eState = eState.E_DESTROY;
                    }
                }
                break;
            case eState.E_DESTROY:
                {

                }
                break;
        }
    }

    private void AlphaStart(int nIndex, bool bFadeIn )
    {
        if (bFadeIn == true)
        {
            m_fBtnAlpha[nIndex] += 4f * Time.deltaTime;
            m_pButtonSprite[nIndex].alpha = m_fBtnAlpha[nIndex];
            m_pButtonSprite[nIndex].enabled = true;

            if (m_fBtnAlpha[nIndex] > 1f)
            {
                m_fBtnAlpha[nIndex] = 1f;
            }
        }
        else
        {
            m_fBtnAlpha[nIndex] -= 4f * Time.deltaTime;
            m_pButtonSprite[nIndex].alpha = m_fBtnAlpha[nIndex];
            m_pButtonSprite[nIndex].enabled = false;

            if (m_fBtnAlpha[nIndex] < 0f)
            {
                m_fBtnAlpha[nIndex] = 0f;
            }
        }
    }
	
	private IEnumerator StartFadeIn(float fWaitTime)
    {
        for (int i = 0; i < m_pButtonSprite.Length; i++)
        {
            yield return new WaitForSeconds(fWaitTime);
            JAManager.I.m_pShooterRoot.panel_TitleMenuA.StartFadeNGUIPanel(1, 2f);
            AlphaStart(i, true);
        }      
    }

    private IEnumerator StartFadeOut(float fWaitTime)
    {
        for (int i = 0; i < m_pButtonSprite.Length; i++)
        {
            yield return new WaitForSeconds(fWaitTime);
            AlphaStart(i, false);
        }      
    }

    private void OnGUI()
    {
		// 140717jtj {
	//	if (m_bCheatOnOff == true)
	//	{	JAManager.I.m_pShooterRoot.SetCheatOnOff(m_bCheatOnOff);	}
	//z	else
		{	JAManager.I.m_pShooterRoot.SetCheatOnOff(m_bCheatOnOff);	}
		// } 140717jtj ;
    }

    public void SetMissionButton()
    {
        if (m_eState == eState.E_MAIN)
        {
            JAManager.I.m_bCamMove = false;
			JAManager.I.m_pCharSelectScript.SetState(false);
            JAManager.I.m_pShooterRoot.panel_TitleMenuA.isInputable = true;
            JAManager.I.m_pShooterRoot.StartMenuButton();
            if ( m_pTitleBScroll_Obj == null )
                m_pTitleBScroll_Obj = JAPrefabMng.I.CreatePrefab("Panel_TitleMenuB", E_JA_RESOURCELOAD.E_JIAN, "prf_TitleBScrollView");
            m_eState = eState.E_END;
			JAStruckMng.I.SetStageInfos();
        }
    }

    public void SetPvpButton()
    {
        if (m_eState == eState.E_MAIN)
        {
            if (JAPrefabMng.I != null)
                JAPrefabMng.I.DestroyPrefabs();

			JAManager.I.m_pCharSelectScript.SetState(false);
            JAManager.I.m_pShooterRoot.panel_TitleMenuA.isInputable = false;
            JAManager.I.m_pShooterRoot.panel_TitleMenuA.StartFadeNGUIPanel(0, 2f);

            JAPrefabMng.I.CreatePrefab("Pop_Offset", E_JA_RESOURCELOAD.E_JIAN, "prf_PvPMenu");

// 140621jtj {
			JAManager.I.m_pShooterRoot.StartBGMFadeIn( JAManager.I.m_pShooterRoot.audioClips[38], 10, 1 ) ;
// } 140621jtj ;

            JAManager.I.m_bCamMove = false;

            m_eState = eState.E_END;
        }
    }

    public void SetDungeonButton()
    {
        if (m_eState == eState.E_MAIN)
        {
            JAManager.I.LoadData();
//            if (JAManager.I.myData.manage.m_stMyInfo.m_nSpawn <= 0)
//            {
//                JAManager.I.myData.manage.m_stMyInfo.m_nSpawn = 0;
//#if UNITY_EDITOR
//                Debug.Log("소환진 부족");
//#endif
//            }
//            else
//            {
				JAManager.I.m_pCharSelectScript.SetState(false);
                //JAManager.I.myData.manage.m_stMyInfo.m_nSpawn -= 1;
                if (JAPrefabMng.I != null)
                    JAPrefabMng.I.DestroyPrefabs();

                JAManager.I.m_pShooterRoot.panel_TitleMenuA.isInputable = false;
                JAManager.I.m_pShooterRoot.panel_TitleMenuA.StartFadeNGUIPanel(0, 2f);

                JAManager.I.m_pShooterRoot.DungeonStart();

                JAManager.I.SaveData();
                m_eState = eState.E_END;
            //}
            JAManager.I.m_bCamMove = false;
        }
		// } 슈터루트의 DungeonStart() 실행, 140412해랑 ;
    }

    public void SetCheatButton()
    {
        if (m_eState == eState.E_MAIN)
        {
            JAManager.I.m_bCamMove = false;
            JAPrefabMng.I.DestroyPrefab("prf_ItemInvenPop(Clone)");
            m_bCheatOnOff = !m_bCheatOnOff;
			JADBManager.I.m_pRandBox.BigWeaponBox();
            JAManager.I.SaveData();
        }
    }

    public void SetShopButton()
    {
        if (m_eState == eState.E_MAIN)
        {
			// 140805jtj {

			JAManager.I.LoadData();
			if (JAManager.I.myData.manage.m_stMyInfo.m_nSpawn <= 0)
			{
				JAManager.I.myData.manage.m_stMyInfo.m_nSpawn = 0;
#if UNITY_EDITOR
				Debug.Log("소환진 부족");
#endif
			}
		//!	else // 140812jtj ;
			{
				JAManager.I.m_pCharSelectScript.SetState(false);
				JAManager.I.myData.manage.m_stMyInfo.m_nSpawn -= 1;
				if (JAPrefabMng.I != null)
					JAPrefabMng.I.DestroyPrefabs();
				
				JAManager.I.m_pShooterRoot.panel_TitleMenuA.isInputable = false;
				JAManager.I.m_pShooterRoot.panel_TitleMenuA.StartFadeNGUIPanel(0, 2f);
				
				JAManager.I.m_pShooterRoot.TestSceneStart();
                //JADBManager.I.m_pRandBox.BigWeaponBox();
				JAManager.I.SaveData();
				m_eState = eState.E_END;
			}
			// } 140805jtj ;
        }
    }

    public void SetCreditButton()
    {
        if (m_eState == eState.E_MAIN)
        {
            if (JAPrefabMng.I != null)
                JAPrefabMng.I.DestroyPrefabs();

            JAManager.I.SetTitleFadeA(false, 0f, 2f);

            JAPrefabMng.I.CreatePrefab("Pop_Offset", E_JA_RESOURCELOAD.E_JIAN, "prf_CreditPop");

            JAManager.I.m_bCamMove = false;

        }
    }

    public void SetConfigButton()
    {
        if (m_eState == eState.E_MAIN)
        {
			//JAManager.I.m_pCharSelectScript.SetState(false);
            
        }
    }
}