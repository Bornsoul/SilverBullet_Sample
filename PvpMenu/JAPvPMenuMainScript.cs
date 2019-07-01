using UnityEngine;
using System.Collections;

public class JAPvPMenuMainScript : MonoBehaviour
{
    enum eState
    {
        E_STATE_NONE,
        E_STATE_TITLE_ALPHA_START,        
        E_STATE_PVP_ANI,
        E_STATE_BUTTON_ANI,
        E_STATE_MENU,
        E_STATE_END,
        E_STATE_DESTROY,
        E_STATE_HIDE,
        E_STATE_SHOW,
    };

    private eState m_eState = eState.E_STATE_NONE;

    public GameObject m_pHideWindowObj = null;
    public UISprite m_pTopTitleSprite = null;
    private float m_fTopTitleAalpha = 0f;

    public GameObject m_pFightObj = null;
    public GameObject m_pBattleObj = null;
    public Animation[] m_pBattleAni = null;

    public UISprite[] m_pButtonSprite = null;
    private float[] m_fButtonAlpha = new float[4];

    public UIPanel m_pMyPanel = null;
    private Color m_pPanelColor;
    private float m_fPanelAlpha = 1f;

    void Awake()
    {
        m_fTopTitleAalpha = 0f;
        m_pTopTitleSprite.alpha = 0f;
        NGUITools.SetActive(m_pFightObj, false);
        NGUITools.SetActive(m_pBattleObj, false);

        for (int i = 0; i < m_pButtonSprite.Length; i++)
        {
            m_pButtonSprite[i].enabled = false;
        }
    }

    void OnDestroy()
    {
        Destroy(m_pHideWindowObj);
        m_pHideWindowObj = null;

        m_pTopTitleSprite = null;

        Destroy(m_pFightObj);
        m_pFightObj = null;

        Destroy(m_pBattleObj);
        m_pBattleObj = null;

        for (int i = 0; i < m_pBattleAni.Length; i++)
        {
            Destroy(m_pBattleAni[i]);
            m_pBattleAni[i] = null;
        }

        for (int i = 0; i < m_pButtonSprite.Length; i++)
        {
            m_pButtonSprite[i] = null;
        }

        Destroy(m_pMyPanel);
        m_pMyPanel = null;
    }

    void Start()
    {

        //if ( JAManager.I.myData.manage.m_stMyInfo.m_sName != string.Empty )
        //    JAStruckMng.I.m_pPvpFriendPlayerInfo[1].m_sName = JAManager.I.myData.manage.m_stMyInfo.m_sName; //"KALI";

        JAManager.I.myData.manage.m_stMyInfo.m_nLevel = JAManager.I.m_pShooterRoot.theLevel;
        //JAStruckMng.I.m_pPvpFriendPlayerInfo[1].m_nLevel = JAManager.I.m_pShooterRoot.theLevel;
	    //! 플레이어용으로 고정된 [1]번 친구의 이름과 레벨을 수동 지정, 140412해랑 ;

        StartCoroutine(Delay(0.1f));

    }

    IEnumerator Delay(float fWaitTime)
    {
        yield return new WaitForSeconds(fWaitTime);
        m_eState = eState.E_STATE_TITLE_ALPHA_START;    
       
    }

    void Update()
    {
        if (m_eState == eState.E_STATE_MENU || m_eState == eState.E_STATE_BUTTON_ANI)
        {
            if (Input.GetKeyDown(KeyCode.Escape) == true)
            {
                JAPrefabMng.I.SetMainUISetting();

                JAManager.I.m_pCharSelectScript.SetState(true);
                JAManager.I.m_pShooterRoot.panel_TitleMenuA.isInputable = true;
                JAManager.I.m_pShooterRoot.panel_TitleMenuA.StartFadeNGUIPanel(1, 2f);

                m_eState = eState.E_STATE_END;
            }
        }


        CurState();
    }

    public void CurState()
    {
        switch (m_eState)
        {
            case eState.E_STATE_NONE:
                {
          
                }
                break;
            case eState.E_STATE_TITLE_ALPHA_START:
                {
                    m_fTopTitleAalpha += 1.5f * Time.deltaTime;
                    m_pTopTitleSprite.alpha = m_fTopTitleAalpha;

                    if (m_fTopTitleAalpha > 1f)
                    {
                        m_fTopTitleAalpha = 1f;
                        m_eState = eState.E_STATE_PVP_ANI;
                    }
                }
                break;
            case eState.E_STATE_PVP_ANI:
                {
                    StartCoroutine(BattleAniDelay(0.01f));
                }
                break;
            case eState.E_STATE_BUTTON_ANI:
                {
                    for (int i = 0; i < m_pButtonSprite.Length; i++)
                    {
                        m_pButtonSprite[i].enabled = true;
                    }
                    m_fButtonAlpha[0] += 0.95f * Time.deltaTime;
                    m_fButtonAlpha[1] += 0.9f * Time.deltaTime;
                    m_fButtonAlpha[2] += 0.85f * Time.deltaTime;
                    m_fButtonAlpha[3] += 0.8f * Time.deltaTime;

                    for (int i = 0; i < m_pButtonSprite.Length; i++)
                        m_pButtonSprite[i].alpha = m_fButtonAlpha[i];

                    if (m_fButtonAlpha[3] > 1f)
                    {
                        for (int i = 0; i < m_fButtonAlpha.Length; i++)
                        {
                            m_fButtonAlpha[i] = 1f;
                        }
                        m_eState = eState.E_STATE_MENU;
                    }
                }
                break;
            case eState.E_STATE_MENU:
                {
                    
                }
                break;
            case eState.E_STATE_END:
                {
                    m_fPanelAlpha -= 2f * Time.deltaTime;
                    m_pMyPanel.alpha = m_fPanelAlpha;

                    if (m_fPanelAlpha < 0f)
                    {
                        m_fPanelAlpha = 0f;
                        m_eState = eState.E_STATE_DESTROY;
                    }
                }
                break;
            case eState.E_STATE_DESTROY:
                {
                    JAPrefabMng.I.DestroyPrefab("prf_PvPMenu(Clone)");
                }
                break;
            case eState.E_STATE_HIDE:
                {
                    NGUITools.SetActive(m_pHideWindowObj, false);
                }
                break;
            case eState.E_STATE_SHOW:
                {
                    NGUITools.SetActive(m_pHideWindowObj, true);

                    m_eState = eState.E_STATE_MENU;
                }
                break;
        }
    }

    public IEnumerator BattleAniDelay(float fWaitTime)
    {
        yield return new WaitForSeconds(fWaitTime);
        
        NGUITools.SetActive(m_pBattleObj, true);
        NGUITools.SetActive(m_pFightObj, true);

        m_pBattleAni[0].Play("prf_PvP_TopLeftStart");
        m_pBattleAni[1].Play("prf_PvP_TopRightStart");
        m_pBattleAni[2].Play("prf_PvP_BotRightStart");
        m_pBattleAni[3].Play("prf_PvP_BotLeftStart");
        m_pBattleAni[4].Play("prf_PvP_FightStart");

        yield return StartCoroutine(BattleAniEnd());
    }

    public IEnumerator BattleAniEnd()
    {
        yield return new WaitForEndOfFrame();

        m_eState = eState.E_STATE_BUTTON_ANI;
    }
    public void SetBackButton()
    {
        if (m_eState == eState.E_STATE_MENU || m_eState == eState.E_STATE_BUTTON_ANI)
        {
            JAPrefabMng.I.SetMainUISetting();

			JAManager.I.m_pCharSelectScript.SetState(true);
            JAManager.I.m_pShooterRoot.panel_TitleMenuA.isInputable = true;
            JAManager.I.m_pShooterRoot.panel_TitleMenuA.StartFadeNGUIPanel(1, 2f);

// 140621jtj {
			JAManager.I.m_pShooterRoot.StartBGMFadeIn( JAManager.I.m_pShooterRoot.audioClips[37], 10, 1 ) ;
// } 140621jtj ;

            JAManager.I.m_bCamMove = true;
            m_eState = eState.E_STATE_END;
        }
    }

    public void SetFriendsButton()
    {
        if (m_eState == eState.E_STATE_MENU || m_eState == eState.E_STATE_BUTTON_ANI)
        {
            JAPrefabMng.I.CreatePrefab("Pop_Offset", E_JA_RESOURCELOAD.E_JIAN, "prf_PvPFriendPop");
            JAPrefabMng.I.CreatePrefab("Scroll_Offset", E_JA_RESOURCELOAD.E_JIAN, "prf_PvPFriendScrollPop");

            //SetHide(true);
            m_eState = eState.E_STATE_END;
        }
    }

    public void SetRefreashButton()
    {
        if (m_eState == eState.E_STATE_MENU || m_eState == eState.E_STATE_BUTTON_ANI)
        {

            JAManager.I.m_nPvpEnemyRand1 = NGUITools.RandomRange(0, 25);
            JAManager.I.m_nPvpEnemyRand2 = NGUITools.RandomRange(0, 25);

        }
    }

    public void SetStartButton()
    {
		// {
		if (m_eState == eState.E_STATE_MENU || m_eState == eState.E_STATE_BUTTON_ANI)
		{
            string meName = JAManager.I.myData.manage.m_stMyInfo.m_sName;
            int meLevel = JAManager.I.myData.manage.m_stMyInfo.m_nLevel;
			string friendName = JAStruckMng.I.m_pPvpFriendPlayerInfo[JAManager.I.m_nPvpFriendSelectInfo].m_sName ;
			int friendLevel = JAStruckMng.I.m_pPvpFriendPlayerInfo[JAManager.I.m_nPvpFriendSelectInfo].m_nLevel ;
			string bot1Name = JAStruckMng.I.m_pPvpEnemyPlayerInfo[JAManager.I.m_nPvpEnemyRand1].m_sName ;
			int bot1Level = JAStruckMng.I.m_pPvpEnemyPlayerInfo[JAManager.I.m_nPvpEnemyRand1].m_nLevel ;
			string bot2Name = JAStruckMng.I.m_pPvpEnemyPlayerInfo[JAManager.I.m_nPvpEnemyRand2].m_sName ;
			int bot2Level = JAStruckMng.I.m_pPvpEnemyPlayerInfo[JAManager.I.m_nPvpEnemyRand2].m_nLevel ;

			string[] names = new string[4] ;
			names[0] = meName ;
			names[1] = friendName ;
			names[2] = bot1Name ;
			names[3] = bot2Name ;

			int[] levels = new int[4] ;
			levels[0] = meLevel ;
			levels[1] = friendLevel ;
			levels[2] = bot1Level ;
			levels[3] = bot2Level ;

// 140621jtj {
			JAManager.I.m_pShooterRoot.StartBGMFadeOut( 0.1F ) ;
// } 140621jtj ;

			JAManager.I.m_pShooterRoot.PvPStart( names, levels ) ;


			m_eState = eState.E_STATE_END;
		}
		// } 슈터루트의 PvPStart()를 실행, 선수4명의 이름과 레벨 정보를 넘겨준다 140412해랑 ;
    }

    public void SetHide(bool bHide = true)
    {
        if (bHide == true)
            m_eState = eState.E_STATE_HIDE;
        else
        {
            m_eState = eState.E_STATE_SHOW;
        
        }

        Debug.Log("STATE = " + m_eState);
    }

}
