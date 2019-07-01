using UnityEngine;
using System.Collections;

public class JAFollowChatBox : MonoBehaviour
{

    enum eState
    {
        E_STATE_NONE,
        E_STATE_SHOWCHAT,
        E_STATE_HIDE,
        E_STATE_DESTROY,
    };

    private eState m_eState = eState.E_STATE_NONE;
    public UISprite m_pBackSprite = null;
    public UILabel m_pTextLabel = null;

    float m_fAlpha = 0f;
    int m_nRandChat = 0;

    void Start()
    {

// 140604jtj {
		if( JAManager.I == null )
		{
			Destroy( this ) ;
			return ;
		}
// } 140604jtj ;

        JAManager.I.LoadData();
        m_pTextLabel.alpha = 0f;
        m_pTextLabel.enabled = false;
    }
    
    void Update()
    {
        switch (m_eState)
        {
            case eState.E_STATE_NONE:
                {

                }
                break;
            case eState.E_STATE_SHOWCHAT:
                {
                    m_pBackSprite.spriteName = "wordbaloon_big";
                    m_pBackSprite.SetDimensions(760, 127);

                    m_fAlpha += 1.2f * Time.deltaTime;
                    m_pTextLabel.alpha = m_fAlpha;

                    if (m_fAlpha > 1f) m_fAlpha = 1f;
                    StartCoroutine(DelayHide(8f));
                }
                break;
            case eState.E_STATE_HIDE:
                {
                    m_pBackSprite.spriteName = "wordbaloon_big";
                    m_pBackSprite.SetDimensions(760, 127);

                    m_fAlpha -= 0.9f * Time.deltaTime;
                    m_pBackSprite.alpha = m_fAlpha;
                    m_pTextLabel.alpha = m_fAlpha;
                    if (m_fAlpha < 0f)
                    {
                        m_fAlpha = 0f;
                        m_eState = eState.E_STATE_DESTROY;
                    }
                }
                break;
            case eState.E_STATE_DESTROY:
                {
                    m_pBackSprite.enabled = false;
                    m_pTextLabel.enabled = false;
                }
                break;
        }

    }

	private bool isDelayHideDid ; // 140806jtj ;

    IEnumerator DelayHide(float fWaitTime)
    {
		// 140806jtj {
		if( isDelayHideDid == true )
		{	return false ;	}
		isDelayHideDid = true ;
		// } 140806jtj ;

        yield return new WaitForSeconds(fWaitTime);

		Debug.Log( Time.time ) ; // 140806jtj ;
        JAManager.I.SaveData();
        m_eState = eState.E_STATE_HIDE;
    }

    public void SetClick()
    {
        if (m_eState == eState.E_STATE_NONE)
        {
            JAManager.I.LoadData();

            m_nRandChat = NGUITools.RandomRange(0, 78);

            if (JAManager.I.myData.manage.m_stGame.m_bSoliderChatCheck[m_nRandChat] == true)
            {
                m_nRandChat = NGUITools.RandomRange(0, 78);
                //Debug.Log("중복 인덱스 = " + m_nRandChat);
            }
            else
            {
                m_pTextLabel.text = JAManager.I.m_pStringStore_Src[1].strings[m_nRandChat];
                JAManager.I.myData.manage.m_stGame.m_bSoliderChatCheck[m_nRandChat] = true;
                m_pTextLabel.enabled = true;
                //Debug.Log("대화 인덱스 = " + m_nRandChat);
            }

            JAManager.I.SaveData();
            
            m_eState = eState.E_STATE_SHOWCHAT;
        }

// 140612jtj {
		theOne.oneThis.oneShooterRoot.EnableShoot() ;
		theOne.oneThis.oneShooterRoot.DisableShootAWhile() ;
// } 140612jtj ;

    }
}
