using UnityEngine;
using System.Collections;

/// <summary>
/// 스테이지를 클리어 한 적이 없다면 디폴트로 Dialog : On,
/// 스테이지를 클리어한 적이 있다면 디폴트로 Dialog : Off
/// 게임 내에 뜨는 모든 대사나 튜토리얼 전부 무시.
/// </summary>
/// 
public class JADialogOnOff : MonoBehaviour
{
    public UISprite m_pFrontSprite = null;
    public UILabel m_pFrontLabel = null;

    private bool m_bToggle = false;
    private Vector3 m_stMovePos = Vector3.zero;

	private shooterRoot shrt ;//BBR 2014.11.16

    void Start()
    {
		shrt = theOne.oneThis.oneShooterRoot ;//BBR 2014.11.16
    }

    void Update()
    {
        if (m_bToggle == true)
        {
            m_pFrontSprite.alpha = 1f;
			m_pFrontLabel.text = "OFF";//BBR 2014.11.18
            m_stMovePos.x = 33f;
        }
        else
        {
            m_pFrontSprite.alpha = 180f / 255f;
			m_pFrontLabel.text = "ON";//BBR 2014.11.18
            m_stMovePos.x = -33f;
        }

        m_pFrontSprite.transform.localPosition = Vector3.MoveTowards(m_pFrontSprite.transform.localPosition,
                m_stMovePos, 300f * Time.deltaTime);
		shrt.isAllSkipping = m_bToggle;//BBR 2014.11.18
    }

    void OnPress(bool bPress)
    {
        m_bToggle = !m_bToggle;
    }
   
    void OnClick()
    {
        m_bToggle = !m_bToggle;
    }
}
