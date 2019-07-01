using UnityEngine;
using System.Collections;

public class JAPvPFriendScrollMainScript : MonoBehaviour
{
    enum eState
    {
        E_STATE_NONE,
        E_STATE_PANEL_ALPHA_START,
        E_STATE_MENU,
        E_STATE_END,
        E_STATE_DESTROY,
    };

    private eState m_eState = eState.E_STATE_NONE;
    private Vector3 m_stMyPos = Vector3.zero;
    public UIScrollView m_pScrollView = null;

    public UIPanel m_pMyPanel = null;
    private Color m_pPanelColor;
    private float m_fPanelAlpha = 0f;

    private static float m_nAlphaSpeed = 4f;

    public GameObject m_pScrollTable_Obj = null;
    public JAPvPFriendTableInfo m_pScrollTable_Src = null;

    public Animation m_pLeftAni = null;

    

    public void Enter()
    {
        //m_pLeftAni.Play();
		// 140413jtj ;
        m_eState = eState.E_STATE_PANEL_ALPHA_START;


        SetFriendTableSetting();

        StartCoroutine(ScrollPosInit(0.1f));
    }

    public void Destroy()
    {
        Destroy(m_pScrollView);
        m_pScrollView = null;

        Destroy(m_pMyPanel);
        m_pMyPanel = null;

        Destroy(m_pScrollTable_Obj);
        m_pScrollTable_Obj = null;

        m_pScrollTable_Src.Destroy();
        m_pScrollTable_Src = null;
    }

    IEnumerator ScrollPosInit(float fWaitTime)
    {

// 140614jtj {
		transform.localPosition = GetComponent<UIPanel>().clipOffset * -1 ;
// } 140614jtj ;

        yield return new WaitForSeconds(fWaitTime);

// 140614jtj {
        //m_stMyPos = gameObject.transform.localPosition;
        //m_stMyPos.x = 0f;
        //m_stMyPos.y = 0f;
        //m_stMyPos.z = 0f;
        //gameObject.transform.localPosition = m_stMyPos;
// } 140614jtj ;

    }

    private void SetFriendTableSetting()
    {
        for (int i = 0; i < JAStruckMng.I.m_pPvpFriendPlayerInfo.Length; i++)
        {
            m_pScrollTable_Obj = JAPrefabMng.I.CreatePrefab("JAScrollGrid", E_JA_RESOURCELOAD.E_JIAN, "prf_FriendTable", -1f, ("prf_FriendTable"+i));
            m_pScrollTable_Obj.transform.localScale = new Vector3(0.00055f, 0.00055f, 1f);
            m_pScrollTable_Src = m_pScrollTable_Obj.GetComponent<JAPvPFriendTableInfo>();
            m_pScrollTable_Src.SetTextDataSetting(i);
            m_pScrollTable_Src.m_nIndex = i;
        }
        
		Invoke( "SetJAScrollGridPositionOnceMore", 0.1F ) ;
    }

	private void SetJAScrollGridPositionOnceMore()
	{
        transform.FindChild("Stretch/JAScrollGrid").GetComponent<UIGrid>().Reposition();
		transform.FindChild( "Stretch/JAScrollGrid" ).localPosition = new Vector3( -0.24F, 0, 0 ) ;
        
        transform.GetComponent<UIScrollView>().ResetPosition();
        
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
            case eState.E_STATE_DESTROY:
                {
                    JAPrefabMng.I.DestroyPrefab("prf_PvPFriendScrollPop(Clone)");

                    for (int i = 0; i < JAStruckMng.I.m_pPvpFriendPlayerInfo.Length; i++)
                        JAPrefabMng.I.DestroyPrefab("prf_FriendTable"+i+"(Clone)");
                }
                break;
        }
    }

    public void SetEndState()
    {
        if (m_eState == eState.E_STATE_MENU)
        {
            m_eState = eState.E_STATE_END;
        }
    }

}
