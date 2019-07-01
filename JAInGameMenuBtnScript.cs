using UnityEngine;
using System.Collections;

public class JAInGameMenuBtnScript : MonoBehaviour
{
    enum eState
    {
        E_STATE_NONE,
        E_STATE_BTNOFF,
        E_STATE_BTNON,
        E_STATE_PAUSE,
        E_STATE_END,
    };

    private eState m_eState = eState.E_STATE_NONE;
    public UISprite m_pMenuOnSprite = null;
    public UISprite m_pArrowSprite = null;
    public Animation m_pMenuOnStartAni = null;
    public Animation m_pArrowAni = null;

    private float m_fAlpha = 0f;

    void Start()
    {
        m_pMenuOnSprite.enabled = false;
        m_pArrowSprite.enabled = false;
    }

    void Update()
    {
        switch (m_eState)
        {
            case eState.E_STATE_NONE:
                {
                    if (m_fAlpha < 0f) return;
                    m_fAlpha -= 5f * Time.deltaTime;
                    m_pMenuOnSprite.alpha = m_fAlpha;
                    m_pArrowSprite.alpha = m_fAlpha;                    
                }
                break;
            case eState.E_STATE_BTNOFF:
                {                   
                    m_fAlpha += 1.5f * Time.deltaTime;
                    m_pMenuOnSprite.alpha = m_fAlpha;
                    m_pArrowSprite.alpha = m_fAlpha;

                    if (m_fAlpha > 1f)
                    {
                        m_fAlpha = 1f;
                        m_eState = eState.E_STATE_BTNON;
                    }
                }
                break;
            case eState.E_STATE_BTNON:
                {
                   
                }
                break;
            case eState.E_STATE_PAUSE:
                {
          
                }
                break;
            case eState.E_STATE_END:
                {

                }
                break;
        }
    }

    public void SetMenuBtn_Off()
    {
        if (m_eState == eState.E_STATE_NONE)
        {
            m_pMenuOnSprite.enabled = true;
            m_pArrowSprite.enabled = true;
            m_pArrowAni.Play();
            m_pMenuOnStartAni.Play("InGameMenu_MenuOnBtnStart");
            m_eState = eState.E_STATE_BTNOFF;
        }
        else if (m_eState == eState.E_STATE_BTNON || m_eState == eState.E_STATE_BTNOFF)
        {

            m_pMenuOnStartAni.Play("InGameMenu_MenuOnBtnEnd");
            m_eState = eState.E_STATE_NONE;
        }
    }

    public void SetMenuBtn_On()
    {
        if (m_eState == eState.E_STATE_BTNON || m_eState == eState.E_STATE_BTNOFF)
        {
            m_pMenuOnSprite.enabled = false;
            m_pArrowSprite.enabled = false;
            JAManager.I.m_pShooterRoot.SetPauseMenuOnOff(true, true);
            m_eState = eState.E_STATE_NONE;
        }
    }

    
}
