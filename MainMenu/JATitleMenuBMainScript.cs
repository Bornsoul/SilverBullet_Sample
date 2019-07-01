using UnityEngine;
using System.Collections;

public class JATitleMenuBMainScript : MonoBehaviour
{
    enum eState
    {
        E_STATE_NONE,
        E_STATE_RIGHT,
        E_STATE_E_STATE_RIGHTNO,
        E_STATE_END,
    };

    public UIPanel m_pRightPanel = null;
    public UIPanel m_pRightNoPanel = null;

    float m_fRightAlpha = 1f;
    float m_fRightNoAlpha = 0f;

    const float m_nSpeed = 0.15f;

    eState m_eState = eState.E_STATE_NONE;

    void Start()
    {
        m_eState = eState.E_STATE_RIGHT;    
    }

    void Update()
    {
        if (JAManager.I == null) return;
        if (JAManager.I.m_pShooterRoot.panel_TitleMenuB.isInputable == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape) == true)
            {
                JAManager.I.m_pShooterRoot.SubMenuButton_Cancel();
            }
        }

        switch (m_eState)
        {
            case eState.E_STATE_NONE:
                break;
            case eState.E_STATE_RIGHT:
               
                m_fRightAlpha = Mathf.SmoothStep(m_fRightAlpha, 1f, m_nSpeed);
                m_fRightNoAlpha = Mathf.SmoothStep(m_fRightNoAlpha, 0f, m_nSpeed);
                break;
            case eState.E_STATE_E_STATE_RIGHTNO:
                m_fRightAlpha = Mathf.SmoothStep(m_fRightAlpha, 0f, m_nSpeed);
                m_fRightNoAlpha = Mathf.SmoothStep(m_fRightNoAlpha, 1f, m_nSpeed);
                break;
            case eState.E_STATE_END:
                break;
        }
        m_pRightPanel.alpha = m_fRightAlpha;
        m_pRightNoPanel.alpha = m_fRightNoAlpha;
    }

    public void SetAlpha(bool bRight)
    {
        if (bRight == true)
            m_eState = eState.E_STATE_RIGHT;
        else
            m_eState = eState.E_STATE_E_STATE_RIGHTNO;
    }
}
