using UnityEngine;
using System.Collections;

/// <summary>
/// 2014_06_19
/// 헬프콜 이미지 게이지
/// </summary>
public class JAHelpCallBar : MonoBehaviour
{

    enum eState
    {
        E_STATE_NONE,
        E_STATE_UP,
        E_STATE_MARK,
        E_STATE_MARK_DOWN,
        E_STATE_END,
    };

    private eState m_eState = eState.E_STATE_NONE;
    public UISprite m_pHelpInSprite = null;
    public UISprite m_pHelpOutSprite = null;

    public GameObject m_pCallMarkObject = null;
    public UISprite m_pCallMarkSprite = null;

    public Animation m_pCallBarAni = null;
    public Animation m_pCallMarkAni = null;

    static float m_fCallTime = 0f;
    private float m_fGageValue = 0f;
    private float m_fGageValueTo = 0f;
    private float m_fMarkAlpha = 1f;

    void Start()
    {
        
    }

    public void Enter(float fCallTime)
    {
        m_eState = eState.E_STATE_UP;
        m_pCallBarAni.Play("prf_HelpCallBarStart");

        m_fCallTime = fCallTime;
        m_fGageValue = 0f;
        m_fGageValueTo = 0f;
        m_fMarkAlpha = 1f;
        m_pHelpInSprite.fillAmount = 0f;
    }

    void Update()
    {

        switch (m_eState)
        {
            case eState.E_STATE_NONE:
                break;
            case eState.E_STATE_UP:
                if (m_fGageValueTo > 1.3f) { m_fGageValueTo = 1f; m_eState = eState.E_STATE_MARK; }

                m_fGageValueTo += m_fCallTime * Time.deltaTime;
                break;
            case eState.E_STATE_MARK:
                 m_pCallBarAni.Play("prf_HelpCallBarEnd");
        NGUITools.SetActive(m_pCallMarkObject, true);
        m_pCallMarkAni.Play();
        m_eState = eState.E_STATE_MARK_DOWN;
                
                break;
            case eState.E_STATE_MARK_DOWN:
                if ( m_fMarkAlpha < 0f ) { m_fMarkAlpha = 0f; m_eState = eState.E_STATE_END; }
                m_fMarkAlpha -= 0.3f * Time.deltaTime;
                break;
            case eState.E_STATE_END:
                m_pHelpInSprite.alpha = 0f;
                m_pHelpOutSprite.alpha = 0f;
                m_fMarkAlpha = 0f;

                break;
        }
        m_fGageValue = Mathf.SmoothStep(m_fGageValue, m_fGageValueTo, 0.05f);
        m_pHelpInSprite.fillAmount = m_fGageValue;
        m_pCallMarkSprite.alpha = m_fMarkAlpha;

    }

    IEnumerator MarkDelay(float fWaitTime)
    {
        yield return new WaitForSeconds(fWaitTime);

        
    }
    IEnumerator SpriteAlpha(float fWaitTime, float fAlpha)
    {
        yield return new WaitForSeconds(fWaitTime);

        if (fAlpha < 0f) { m_eState = eState.E_STATE_END; yield return null; }
        fAlpha -= 0.5f;
        m_pHelpInSprite.alpha = fAlpha;
        m_pHelpOutSprite.alpha = fAlpha;
    }

}
