using UnityEngine;
using System.Collections;

public class JAItemInvenMainScript : MonoBehaviour
{
    enum eState
    {
        E_STATE_NONE,
        E_STATE_SHOW,
        E_STATE_INVEN,
        E_STATE_UPGRADE,
        E_STATE_END,
    };

    private eState m_eState = eState.E_STATE_NONE;
    public Animation m_pPopAni = null;

    public UISprite m_pItemSprite = null;

    public UILabel[] m_pInvenNameLabel = null;
    public UILabel[] m_pItemNameLabel = null;
    public UILabel[] m_pItemValueLabel = null;

    public UILabel m_pGunSmithLabel = null;
    public UILabel m_pInvenLabel = null;
    public UILabel m_pExitLabel = null;

    string m_sBtnString = string.Empty;

    public GameObject m_pButtons_Obj = null;
	public GameObject m_pItemLabels_Obj = null;
    public GameObject m_pFSNameDetail = null;
    public GameObject m_pExpSlider_Obj = null;

    public JAMyInvenLevelExpScript m_pLevelExp_Src = null;

	[HideInInspector]
	public string[] m_sStatString = null;
	[HideInInspector]
	public string[] m_sBtnLabel = null;

    [HideInInspector]
    public int m_nCurSaveIndex = 0;

    public bool m_bUpgShow = false;


    GameObject m_pGun = null;
    float m_fGunSpeed = 0f;
    void Start()
    {
        m_eState = eState.E_STATE_NONE;
        if (m_eState == eState.E_STATE_NONE)
        {
            JAManager.I.LoadData();

            m_pLevelExp_Src.Enter();

            NGUITools.SetActive(m_pFSNameDetail, false);

            m_pItemSprite.enabled = false;
            if (m_pGun == null)
            {
                m_pGun = JAPrefabMng.I.CreatePrefab("JATestModel", E_JA_RESOURCELOAD.E_JIAN, "USP46", -1);
                m_pGun.transform.localPosition = new Vector3(-1.34f, 0.47f, -1f);
                m_pGun.transform.localRotation = Quaternion.Euler(-15f, -90f, 0f);
                m_pGun.transform.localScale = new Vector3(0.085f, 0.085f, 0.085f);
                
            }
            m_eState = eState.E_STATE_SHOW;
        }

    }

    public void SetShowItemInfo(int nIndex)
    {

        m_pItemSprite.spriteName = JAManager.I.myData.manage.m_stInven.m_stDBInven[nIndex].m_sIconName;
        m_pInvenNameLabel[0].text = JAManager.I.GetStringLong(
            JADBManager.I.m_stFirstSubName[JAManager.I.myData.manage.m_stInven.m_stDBInven[nIndex].m_nFirstName].m_sName,
            JADBManager.I.m_stSecondSubName[JAManager.I.myData.manage.m_stInven.m_stDBInven[nIndex].m_nSecondName].m_sName,
            JADBManager.I.GetInvenItemCodeName(JAManager.I.myData.manage.m_stInven.m_stDBInven[nIndex].m_nBigName,
            JAManager.I.myData.manage.m_stInven.m_stDBInven[nIndex].m_nItemName));

        switch (JAManager.I.myData.manage.m_stInven.m_stDBInven[nIndex].m_nBigName)
        {
            case 1:
                if (JAManager.I.m_bUpgZone == false)
                    NGUITools.SetActive(m_pExpSlider_Obj, true);
                    //m_pItemSprite.enabled = true;
                    m_pInvenNameLabel[2].text = "희귀무기";
                    m_pInvenNameLabel[4].text = JAManager.I.myData.manage.m_stInven.m_stDBInven[nIndex].m_nLevel.ToString();
                    m_pInvenNameLabel[1].text =
                        JAManager.I.GetStringLong(JAManager.I.myData.manage.m_stInven.m_stDBInven[nIndex].m_nLevel.ToString(), "레벨");

                    m_pItemValueLabel[0].text = JAManager.I.GetStringLong(
                        JAManager.I.myData.manage.m_stInven.m_stDBInven[nIndex].m_nLevel.ToString(), " / ",
                        JADBManager.I.GetInvenItemMaxLevel(JAManager.I.myData.manage.m_stInven.m_stDBInven[nIndex].m_nItemName).ToString());

                    m_pItemValueLabel[1].text = JADBManager.I.GetInvenItemStat(1, JAManager.I.myData.manage.m_stInven.m_stDBInven[nIndex].m_nLevel, JAManager.I.myData.manage.m_stInven.m_stDBInven[nIndex].m_nItemName).ToString();
                    m_pItemValueLabel[2].text = JADBManager.I.GetInvenItemStat(2, JAManager.I.myData.manage.m_stInven.m_stDBInven[nIndex].m_nLevel, JAManager.I.myData.manage.m_stInven.m_stDBInven[nIndex].m_nItemName).ToString();
                    m_pItemValueLabel[3].text = JADBManager.I.GetInvenItemStat(3, JAManager.I.myData.manage.m_stInven.m_stDBInven[nIndex].m_nLevel, JAManager.I.myData.manage.m_stInven.m_stDBInven[nIndex].m_nItemName).ToString();
                    m_pItemValueLabel[4].text = JADBManager.I.GetInvenItemStat(4, JAManager.I.myData.manage.m_stInven.m_stDBInven[nIndex].m_nLevel, JAManager.I.myData.manage.m_stInven.m_stDBInven[nIndex].m_nItemName).ToString();
                    m_pItemValueLabel[5].text = JADBManager.I.GetInvenItemStat(5, JAManager.I.myData.manage.m_stInven.m_stDBInven[nIndex].m_nLevel, JAManager.I.myData.manage.m_stInven.m_stDBInven[nIndex].m_nItemName).ToString();
                    NGUITools.SetActive(m_pItemLabels_Obj, true);
                
                break;
            case 3:
                JADBManager.I.m_fExpValue = 0f;
                if (JAManager.I.m_bUpgZone == false)
                    NGUITools.SetActive(m_pExpSlider_Obj, false);
                //m_pItemSprite.enabled = true;
                m_pInvenNameLabel[2].text = "강화재료";
                m_pInvenNameLabel[4].text = JAManager.I.myData.manage.m_stInven.m_stDBInven[nIndex].m_nItemValue.ToString();
                m_pInvenNameLabel[1].text =
                    JAManager.I.GetStringLong("재료 " + JAManager.I.myData.manage.m_stInven.m_stDBInven[nIndex].m_nItemValue.ToString(), "개");


                NGUITools.SetActive(m_pItemLabels_Obj, false);

                break;

            default:
                m_pItemSprite.enabled = false;
                m_pInvenNameLabel[2].text = "비어있음";
                m_pInvenNameLabel[0].text = "무기를 선택해주세요.";
                JADBManager.I.m_fExpValue = 0f;
                m_pInvenNameLabel[4].text = string.Empty;
                m_pInvenNameLabel[1].text = string.Empty;

                NGUITools.SetActive(m_pItemLabels_Obj, false);
                NGUITools.SetActive(m_pExpSlider_Obj, false);
                break;
        }

    }

    void FixedUpdate()
    {
        if (m_pGun == null) return;
        m_fGunSpeed -= 80f;
        m_pGun.transform.localRotation = Quaternion.Euler(-15f, m_fGunSpeed * Time.deltaTime, 0f);
    }

    void Update()
    {
        
        if (m_eState == eState.E_STATE_SHOW)
        {
            m_sBtnLabel[0] = "정보창 닫기";
            m_sBtnLabel[1] = "인벤토리";
            m_sBtnLabel[2] = "Gun Smith\n아이템 강화";
            //SetShowItemInfo((int)JAManager.I.m_eMyItemSlot);
            //m_pLevelExp_Src.MyExpUpdate((int)JAManager.I.m_eMyItemSlot);

            SetShowItemInfo((int)JAManager.I.m_eMyItemSlot);
            m_pLevelExp_Src.MyExpUpdate((int)JAManager.I.m_eMyItemSlot);

            
        }
        else if (m_eState == eState.E_STATE_INVEN)
        {

            m_sBtnLabel[0] = "강 화";
            m_sBtnLabel[1] = "판 매";
            m_sBtnLabel[2] = "장 착";
            if (JAManager.I.m_bInvenGoUpg == false)
            {
                SetShowItemInfo(JADBManager.I.m_nInvenCurTableIndex);
                m_pLevelExp_Src.ItemExpUpdate(JADBManager.I.m_nInvenCurTableIndex);
            }
            else
            {
                SetShowItemInfo((int)JAManager.I.m_eMyItemSlot);
                m_pLevelExp_Src.MyExpUpdate((int)JAManager.I.m_eMyItemSlot);
            }
            //SetShowItemInfo(JADBManager.I.m_nInvenCurTableIndex);
            
        }
        else if (m_eState == eState.E_STATE_UPGRADE)
        {
            m_sBtnLabel[0] = "정보창 닫기";
            m_sBtnLabel[1] = "인벤토리";
            m_sBtnLabel[2] = "Gun Smith\n아이템 강화";
            if (m_bUpgShow == false)
            {
                SetShowItemInfo((int)JAManager.I.m_eMyItemSlot);
            }
            else
            {
                SetShowItemInfo(JADBManager.I.m_nInvenCurTableIndex);
            }
            m_pLevelExp_Src.ItemExpUpdate((int)JAManager.I.m_eMyItemSlot);
        }


        m_pExitLabel.text = m_sBtnLabel[0];
        m_pInvenLabel.text = m_sBtnLabel[1];
        m_pGunSmithLabel.text = m_sBtnLabel[2];

        switch (JADBManager.I.m_nItemInfoIndex)
        {
            case 0:
                JAManager.I.m_eMyItemSlot = E_JA_MYITEM_SLOT.E_WEAPON_ONE;
                break;
            case 1:
                JAManager.I.m_eMyItemSlot = E_JA_MYITEM_SLOT.E_WEAPON_TWO;
                break;
            case 2:
                JAManager.I.m_eMyItemSlot = E_JA_MYITEM_SLOT.E_DEFEND;
                break;
            case 3:
                JAManager.I.m_eMyItemSlot = E_JA_MYITEM_SLOT.E_SHOES;

                break;
        }

        
    }

    #region ### 설정 함수 ###
    public void SetShowPop(bool bShow = true)
    {
        if (bShow == true)
        {
            NGUITools.SetActive(gameObject, true);
            m_eState = eState.E_STATE_SHOW;
        }
        else
        {
            NGUITools.SetActive(gameObject, false);
            m_eState = eState.E_STATE_INVEN;
        }

    }
    public void SetPopupStartAni(bool bStart)
    {
        if (bStart != false)
        {
            m_pPopAni.Play("prf_ItemInvenPopStart");
        }
        else
        {
            m_pPopAni.Play("prf_ItemInvenPopEnd");
        }
    }

    public void SetInvenPrefab(bool bShow)
    {
        if (bShow == true)
        {
            JAPrefabMng.I.CreatePrefab("Pop_Offset", E_JA_RESOURCELOAD.E_JIAN, "prf_MyInven", -2f);
            JAPrefabMng.I.CreatePrefab("Scroll_Offset", E_JA_RESOURCELOAD.E_JIAN, "prf_MyInvenScroll", -2f);

            JAManager.I.SetTitleFadeA(false, 0, 3f);
        }
        else
        {
            for (int i = 0; i < JAManager.I.myData.manage.m_stInven.m_nDBInvenScrollIndex; i++)
                JAPrefabMng.I.DestroyPrefab("prf_MyInvenTable" + i + "(Clone)");
            JAPrefabMng.I.DestroyPrefab("prf_MyInven(Clone)");
            JAPrefabMng.I.DestroyPrefab("prf_MyInvenScroll(Clone)");
            JAManager.I.SetTitleFadeA(true, 1, 3f);
        }
    }

    public void SetDestroyInvenTable(int nState)
    {
        JAPrefabMng.I.DestroyPrefab("prf_MyInvenScroll(Clone)");

        for (int i = 0; i < JAManager.I.myData.manage.m_stInven.m_stDBInven.Length; i++)
            JAPrefabMng.I.DestroyPrefab("prf_MyInvenTable" + i + "(Clone)");

        m_eState = (eState)nState;
    }


    public void SetState_SHOW()
    {
        m_eState = eState.E_STATE_SHOW;
    }

    public void SetState_INVEN()
    {
        m_eState = eState.E_STATE_INVEN;
    }

    public void SetState_UPGRADE()
    {
        m_eState = eState.E_STATE_UPGRADE;
    }

    public void SetItemChange(int nIndex)
    {
        JAManager.I.myData.manage.m_stInven.m_stDBInven[106].SetAddInven_DataInfo(
                    JAManager.I.myData.manage.m_stInven.m_stDBInven[(int)JAManager.I.m_eMyItemSlot].m_nBigName,
                    JAManager.I.myData.manage.m_stInven.m_stDBInven[(int)JAManager.I.m_eMyItemSlot].m_nSmallName,
                    JAManager.I.myData.manage.m_stInven.m_stDBInven[(int)JAManager.I.m_eMyItemSlot].m_nFirstName,
                    JAManager.I.myData.manage.m_stInven.m_stDBInven[(int)JAManager.I.m_eMyItemSlot].m_nSecondName,
                    JAManager.I.myData.manage.m_stInven.m_stDBInven[(int)JAManager.I.m_eMyItemSlot].m_nItemName,
                    JAManager.I.myData.manage.m_stInven.m_stDBInven[(int)JAManager.I.m_eMyItemSlot].m_nLevel,
                    JAManager.I.myData.manage.m_stInven.m_stDBInven[(int)JAManager.I.m_eMyItemSlot].m_fLevelExp,
                    JAManager.I.myData.manage.m_stInven.m_stDBInven[(int)JAManager.I.m_eMyItemSlot].m_nExceed,
                    JAManager.I.myData.manage.m_stInven.m_stDBInven[(int)JAManager.I.m_eMyItemSlot].m_bUseItem);

        JADBManager.I.SetMyItemSlot(JAManager.I.m_eMyItemSlot, nIndex);

        JAManager.I.myData.manage.m_stInven.m_stDBInven[nIndex].SetAddInven_DataInfo(
            JAManager.I.myData.manage.m_stInven.m_stDBInven[106].m_nBigName,
            JAManager.I.myData.manage.m_stInven.m_stDBInven[106].m_nSmallName,
            JAManager.I.myData.manage.m_stInven.m_stDBInven[106].m_nFirstName,
            JAManager.I.myData.manage.m_stInven.m_stDBInven[106].m_nSecondName,
            JAManager.I.myData.manage.m_stInven.m_stDBInven[106].m_nItemName,
            JAManager.I.myData.manage.m_stInven.m_stDBInven[106].m_nLevel,
            JAManager.I.myData.manage.m_stInven.m_stDBInven[106].m_fLevelExp,
            JAManager.I.myData.manage.m_stInven.m_stDBInven[106].m_nExceed,
            JAManager.I.myData.manage.m_stInven.m_stDBInven[106].m_bUseItem);

        JAManager.I.myData.manage.m_stInven.m_stDBInven[106].SetInven_DeleteData();

        JAManager.I.SaveData();
    }
    #endregion


    #region ### 버튼 이벤트 함수 ###

    public void Button_Inventory()
    {
        if (m_eState == eState.E_STATE_SHOW)
        {
            JAManager.I.m_bUpgZone = false;
            SetInvenPrefab(true);

            m_eState = eState.E_STATE_INVEN;
        }
        else if (m_eState == eState.E_STATE_INVEN)
        {

            //APrefabMng.I.CreatePopup("알 림", "정말로 판매 하시겠습니까?", "", "", E_JA_POPUP_SETTING.E_POPUP_OK_CANCEL);

            JAPrefabMng.I.CreatePopup("알 림", "정말로 판매 하시겠습니까?", "JAPopupOK_DelItem", "", E_JA_POPUP_SETTING.E_POPUP_OK_CANCEL);
           
           
            //JAMyInvenMainScript pInvenMainScrc = GameObject.Find("prf_MyInven(Clone)").GetComponent<JAMyInvenMainScript>();
            //pInvenMainScrc.Button_DelItem();
        }
        else if (m_eState == eState.E_STATE_UPGRADE)
        {
            JAManager.I.SetTitleFadeA(true, 1, 3f);
            JAPrefabMng.I.DestroyPrefab("prf_ItemUpgradePop(Clone)");

            SetInvenPrefab(true);
            m_eState = eState.E_STATE_INVEN;
        }
        JAManager.I.SaveData();
    }

    public void Button_Upgrade()
    {
        if (m_eState == eState.E_STATE_SHOW)
        {
            if (JAManager.I.myData.manage.m_stInven.m_stDBInven[(int)JAManager.I.m_eMyItemSlot].m_bUseItem == false)
            {
                JAPrefabMng.I.CreatePopup("아이템 강화", "슬롯이 비어있습니다." + System.Environment.NewLine + "무기를 장착해주세요.");
            }
            else
            {
                NGUITools.SetActive(m_pButtons_Obj, false);
                JAManager.I.m_bUpgZone = true;
                JAManager.I.SetTitleFadeA(false, 0, 10f);
                JAPrefabMng.I.CreatePrefab("Pop_Offset", E_JA_RESOURCELOAD.E_JIAN, "prf_ItemUpgradePop", -2f);

                m_eState = eState.E_STATE_UPGRADE;
            } 
        }
        else if (m_eState == eState.E_STATE_INVEN)
        {
			switch ( JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex].m_nBigName )
			{
			case 1:
                    m_nCurSaveIndex = JADBManager.I.m_nInvenCurTableIndex;
                    SetItemChange(m_nCurSaveIndex);
				break;

			case 3:
				Debug.Log ("Popup: 재료는 장착할수 없습니다!");
				break;

			default:
				break;
			}
            

            JAManager.I.SaveData();
        }
        else if (m_eState == eState.E_STATE_UPGRADE)
        {

            m_eState = eState.E_STATE_SHOW;
        }

    }

    public void Button_Exit()
    {
        if (m_eState == eState.E_STATE_SHOW)
        {
            JAManager.I.m_bUpgZone = false;
            JAManager.I.m_bInvenState = false;
            SetPopupStartAni(false);
            JAManager.I.SetLeftMenuFade(true);
            JAManager.I.m_bCamMove = true;
            JAPrefabMng.I.DestroyPrefab(m_pGun);
            m_eState = eState.E_STATE_END;
        }
        else if (m_eState == eState.E_STATE_INVEN)
        {
            switch (JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex].m_nBigName)
            {
                case 1:
                    
                    SetInvenPrefab(false);
                    SetDestroyInvenTable((int)eState.E_STATE_INVEN);
                    NGUITools.SetActive(m_pButtons_Obj, false);
                    JAManager.I.m_bUpgZone = true;
                    JAManager.I.m_bInvenGoUpg = true;

                    JAManager.I.SetTitleFadeA(false, 0, 10f);
                    JAPrefabMng.I.CreatePrefab("Pop_Offset", E_JA_RESOURCELOAD.E_JIAN, "prf_ItemUpgradePop", -2f);
                    m_nCurSaveIndex = JADBManager.I.m_nInvenCurTableIndex;
                    SetItemChange(m_nCurSaveIndex);

                    
                    m_eState = eState.E_STATE_INVEN;
                    break;

                case 3:
                    Debug.Log("Popup: 재료는 장착할수 없습니다!");
                    break;

                default:
                    break;
            }

        
        }
        else if (m_eState == eState.E_STATE_UPGRADE)
        {

            JAManager.I.SetTitleFadeA(true, 1, 3f);
            JAPrefabMng.I.DestroyPrefab("prf_ItemUpgradePop(Clone)");

            m_eState = eState.E_STATE_SHOW;
        }
        JAManager.I.SaveData();
    }

    
    #endregion
}
