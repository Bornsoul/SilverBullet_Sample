using UnityEngine;
using System.Collections;


public class JACreditBox : MonoBehaviour
{
    enum eState
    {
        E_STATE_START,
        E_STATE_END,
        E_STATE_DESTROY,
    }

    public GameObject m_pCreditBox_Gam = null;
    public UILabel m_pCreditLabel = null;

    private Vector3 m_stPos = Vector3.zero;
    private float m_fPosY = 0f;
    public float m_fSpeed = 5f;
    private float m_fAlpha = 0.0f;

    public float m_fStartPosY = 0;
    public float m_fEndPosY = 0;

    eState m_eState = eState.E_STATE_START;
    public Animator m_pKailAnimator = null;
   
    void Start()
    {
        if (m_pKailAnimator == null)
        {
            m_pKailAnimator = GameObject.Find("kali_noTwist_inGameAnimation_0725").GetComponent<Animator>();
            //m_pKailAnimator.applyRootMotion = false; //BBR 14.11.16
            m_pKailAnimator.SetInteger("codeNumber", 2001);
        }
       
        m_stPos = m_pCreditBox_Gam.transform.localPosition;
        m_stPos.y = m_fStartPosY;
        m_fPosY = m_fStartPosY;

        m_pCreditLabel.alpha = 0f;

    }

    void Update()
    {
        m_fPosY += m_fSpeed * Time.deltaTime;

        if (Input.GetMouseButtonUp(0) && m_fPosY > -260)
        {
            m_eState = eState.E_STATE_DESTROY;
        }
        
        switch (m_eState)
        {
            case eState.E_STATE_START:
                m_fAlpha = Mathf.SmoothStep(m_fAlpha, 1f, 0.02f);
                if (m_fPosY > m_fEndPosY-85)
                {
                    m_eState = eState.E_STATE_END;
                }
                break;
            case eState.E_STATE_END:
                m_fAlpha = Mathf.SmoothStep(m_fAlpha, 0f, 0.04f);
                if (m_fPosY > m_fEndPosY)
                {
                    m_stPos.y = m_fStartPosY;
                    m_fPosY = m_fStartPosY;

                    m_eState = eState.E_STATE_START;
                }
                break;
            case eState.E_STATE_DESTROY:
               
                    m_fAlpha = Mathf.SmoothStep(m_fAlpha, 0f, 0.08f);
                    JAManager.I.SetTitleFadeA(true, 1f, 2f);
                    m_pKailAnimator.SetInteger("codeNumber", 1);
                if (m_fAlpha < 0.02f)
                {
                    

                    JAPrefabMng.I.SetMainUISetting();
                    JAPrefabMng.I.DestroyPrefab("prf_CreditPop(Clone)");
                    
                }
                break;
        }
        m_stPos.y = Mathf.SmoothStep(m_stPos.y, m_fPosY, 0.03f);
        m_pCreditBox_Gam.transform.localPosition = m_stPos;
        
        m_pCreditLabel.alpha = m_fAlpha;
    }


}