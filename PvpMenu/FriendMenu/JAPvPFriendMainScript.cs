using UnityEngine;
using System.Collections;

public class JAPvPFriendMainScript : MonoBehaviour
{

    enum eState
    {
        E_STATE_NONE,
        E_STATE_PANEL_ALPHA_START,
        E_STATE_MENU,
        E_STATE_END,
        E_STATE_ENDANI,
        E_STATE_DESTROY,
    };

    private eState m_eState = eState.E_STATE_NONE;
    private JAPvPFriendScrollMainScript m_pPvPScrollScript = null;
    public JAPvPMenuMainScript m_pMainPvpScript = null;

    public UIPanel m_pMyPanel = null;
    private Color m_pPanelColor;
    private float m_fPanelAlpha = 0f;

    public Animation[] m_pRightAni = null;

    private static float m_nAlphaSpeed = 4f;

    void Start()
    {
        if ( JAManager.I != null )
            JAManager.I.m_nPvpFriendTableSelect = 0;

        m_pPvPScrollScript = GameObject.Find("Scroll_Offset").transform.FindChild("prf_PvPFriendScrollPop(Clone)").GetComponent<JAPvPFriendScrollMainScript>();
        m_pMainPvpScript = GameObject.Find("Panel_Popup/Pop_Offset/prf_PvPMenu(Clone)").GetComponent<JAPvPMenuMainScript>();

        m_pRightAni[2].Play("prf_PvP_FriendPopBoxStart");
        m_pRightAni[0].Play("prf_PvP_FriendPopBackStart");
        m_pRightAni[1].Play("prf_PvP_FriendPopOKStart");
        m_pRightAni[3].Play("prf_PvP_FriendPopTopStart");
        

        m_eState = eState.E_STATE_PANEL_ALPHA_START;
        m_pPvPScrollScript.Enter();
    }

    void OnDestroy()
    {
        m_pPvPScrollScript.Destroy();
        m_pPvPScrollScript = null;

        
        Destroy(m_pMainPvpScript);
        m_pMainPvpScript = null;

        Destroy(m_pMyPanel);
        m_pMyPanel = null;

        for (int i = 0; i < m_pRightAni.Length; i++)
        {
            Destroy(m_pRightAni[i]);
            m_pRightAni[i] = null;
        }
    }

    void Update()
    {
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
            case eState.E_STATE_PANEL_ALPHA_START:
                {
                    m_fPanelAlpha += m_nAlphaSpeed * Time.deltaTime;
                    m_pMyPanel.alpha = m_fPanelAlpha;

                    if (m_fPanelAlpha > 1f)
                    {
                        m_fPanelAlpha = 1f;
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

                    m_fPanelAlpha -= m_nAlphaSpeed * Time.deltaTime;
                    m_pMyPanel.alpha = m_fPanelAlpha;

                    if (m_fPanelAlpha < 0f)
                    {
                        m_fPanelAlpha = 0f;
                        m_eState = eState.E_STATE_DESTROY;
                    }
                }
                break;
            case eState.E_STATE_ENDANI:
                {
                    m_pRightAni[2].Stop("prf_PvP_FriendPopBoxOut");
                    m_pRightAni[0].Stop("prf_PvP_FriendPopBackOut");
                    m_pRightAni[1].Stop("prf_PvP_FriendPopOKOut");
                    m_pRightAni[3].Stop("prf_PvP_FriendPopTopOut");
                }
                break;
            case eState.E_STATE_DESTROY:
                {

                    JAPrefabMng.I.DestroyPrefab("prf_PvPFriendPop(Clone)");
                   
                }
                break;
        }
    }

    IEnumerator DestroyDelay(float fWaitTime)
    {
        yield return new WaitForSeconds(fWaitTime);

        m_eState = eState.E_STATE_DESTROY; 
    }

    public void SetBackButton()
    {
        if (m_eState == eState.E_STATE_MENU)
        {
            JAPrefabMng.I.CreatePrefab("Pop_Offset", E_JA_RESOURCELOAD.E_JIAN, "prf_PvPMenu");

            m_eState = eState.E_STATE_END;
            m_pPvPScrollScript.SetEndState();
        }
    }

    public void SetOKButton()
    {
        if (m_eState == eState.E_STATE_MENU)
        {
            JAPrefabMng.I.CreatePrefab("Pop_Offset", E_JA_RESOURCELOAD.E_JIAN, "prf_PvPMenu");

            m_eState = eState.E_STATE_END;
            m_pPvPScrollScript.SetEndState();

            JAManager.I.m_nPvpFriendSelectInfo = JAManager.I.m_nPvpFriendTableSelect;
            
        }
    }
}
