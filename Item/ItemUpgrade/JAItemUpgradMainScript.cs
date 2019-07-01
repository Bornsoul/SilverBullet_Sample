using UnityEngine;
using System.Collections;

public class JAItemUpgradMainScript : MonoBehaviour
{

    enum eState
    {
        E_STATE_NONE,
        E_STATE_NORMAL,
        E_STATE_UPGINVEN,
        E_STATE_DESTROYITEM,
        E_STATE_END,
    };

    private eState m_eState = eState.E_STATE_NONE;

    internal JAItemInvenMainScript m_pInvenMain_Src = null;
    public JAItemUpgButtonsScript m_pUpgButtons_Src = null;
    public JAItemUseBox m_pItemUseBox_Src = null;
    
    internal GameObject m_pPrfUpgInvenScrollPop_Gam = null;
    internal GameObject m_pPrfUpgInvenScroll_Gam = null;
    public GameObject m_pUpgButtons_Gam = null;
    public GameObject m_pUseItemButtons_Gam = null;
    public GameObject m_pUseItemBoxMessage_Gam = null;
    public GameObject m_pUseItemBottomLabel_Gam = null;

    public GameObject[] m_pUseTwoItemBox_Gam = null;
    
    public UILabel m_pMessage_Label = null;
    public UILabel m_pPrice_Label = null;

    public UILabel[] m_pRightBtns_Label = null;
    public UISprite[] m_pRightBtns_Sprite = null;


    public int m_nSelectBtnNum = 0;

    void Start()
    {
        m_pInvenMain_Src = GameObject.Find("Panel_Popup/Pop_Offset/prf_ItemInvenPop(Clone)").GetComponent<JAItemInvenMainScript>();
        NGUITools.SetActive(m_pUseItemButtons_Gam, false);
        NGUITools.SetActive(m_pUseItemBottomLabel_Gam, false);

        m_pUpgButtons_Src.Enter();
        m_pItemUseBox_Src.Enter();

        SetSelectState(1);
        m_eState = eState.E_STATE_NORMAL;
    }

	void OnDestroy()
	{
        m_pItemUseBox_Src.Destroy();
	}

    void Update()
    {
        m_pUpgButtons_Src.BackKeyUdate();
        m_pItemUseBox_Src.BoxUpdate();
        SetStateChange(m_nSelectBtnNum);

        switch (JAManager.I.m_nUseItemBoxCnt)
        {
            case 106:
                JAManager.I.m_nUseItemSelCnt = 0;
                break;
            case 107:
                JAManager.I.m_nUseItemSelCnt = 1;
                break;
            case 108:
                JAManager.I.m_nUseItemSelCnt = 2;
                break;
            case 109:
                JAManager.I.m_nUseItemSelCnt = 3;
                break;
            case 110:
                JAManager.I.m_nUseItemSelCnt = 4;
                break;
            default:
                JAManager.I.m_nUseItemSelCnt = 0;
                break;
        }
        
        m_pPrice_Label.text = JAManager.I.GetStringLong("요구 비용 : ", JADBManager.I.GetUpgItemPrice_1().ToString(), "$");
    }

    /// <summary>
    /// 0 = NONE,
    /// 1 = NORMAL,
    /// 2 = UPGINVEN,
    /// 3 = END,
    /// </summary>
    /// <param name="nState"></param>
    public void SetStateChange(int nState)
    {
        m_pRightBtns_Label[0].text = "결정";
        m_pRightBtns_Label[1].text = "강화 완료";
        m_pRightBtns_Label[2].text = "인벤토리";
        switch (nState)
        {
            case 0:
                m_eState = eState.E_STATE_NONE;
                break;
            case 1:
                m_eState = eState.E_STATE_NORMAL;
                m_pPrice_Label.enabled = false;
                m_pMessage_Label.text = "악마의 심장을 사용하여 아이템의 경험치와 레벨을 올립니다." + 
                                                        " 악마의 심장과 골드가 필요합니다.";

                break;
            case 2:
                m_eState = eState.E_STATE_UPGINVEN;
                m_pPrice_Label.enabled = false;
                m_pMessage_Label.text = "총열과 스프링 부품을 사용하여 총기의 세부 능력을 변경합니다." +
                                                        " 두가지 재료와 작업을 위한 골드가 필요합니다.";
                break;
            case 3:
                m_eState = eState.E_STATE_DESTROYITEM;
                m_pPrice_Label.enabled = false;
                if (m_pInvenMain_Src.m_bUpgShow == false)
                {
                    m_pMessage_Label.text = "아이템을 분해하여 개조를 위한 부품으로 만듭니다." +
                                                            " 고급 총기일 수록 좋은 재료가 나올 확률이 높습니다.";
                }
                else
                {
                    m_pMessage_Label.text = "선택된 아이템은 분해후 소멸합니다.";

                }
                break;
            default:
                m_eState = eState.E_STATE_NORMAL;
                break;
        }
    }

    public void SetWarningMessage(float fSpeed = 0.1f)
    {
        StartCoroutine(WarningMessage(fSpeed));
    }

    IEnumerator WarningMessage(float fWaitTime)
    {
        yield return new WaitForSeconds(fWaitTime);
        m_pMessage_Label.color = Color.red;
        yield return new WaitForSeconds(fWaitTime);
        m_pMessage_Label.color = Color.white;
        yield return new WaitForSeconds(fWaitTime);
        m_pMessage_Label.color = Color.red;
        yield return new WaitForSeconds(fWaitTime);
        m_pMessage_Label.color = Color.white;
    }

    public int GetSelectState()
    {
        return m_nSelectBtnNum;
    }

    public void SetSelectState(int nSelState)
    {
        m_nSelectBtnNum = nSelState;
    }
}
