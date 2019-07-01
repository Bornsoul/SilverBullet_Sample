using UnityEngine;
using System.Collections;

public class JAItemUpgButtonsScript : MonoBehaviour
{

    enum eState
    {
        E_STATE_NONE,
        E_STATE_NORMAL,
        E_STATE_INVEN,
        E_STATE_END,
    };

    private eState m_eState = eState.E_STATE_NONE;

    public JAItemUpgradMainScript m_pUpgMain_Src = null;
    public JAItemUpg_1 m_pUpgBtn1_Src = null;
    public JAItemUpg_2 m_pUpgBtn2_Src = null;
    public JAItemUpg_3 m_pUpgBtn3_Src = null;

    public void Enter()
    {
        m_eState = eState.E_STATE_NORMAL;

        JADBManager.I.m_nSelectUpgIndex = 0;
        m_pUpgBtn1_Src.SetButtonClick(true);
        m_pUpgBtn2_Src.SetButtonClick(false);
        m_pUpgBtn3_Src.SetButtonClick(false);
    }

    public void Button_1()
    {
        if (m_eState == eState.E_STATE_NORMAL)
        {
            m_pUpgBtn1_Src.SetButtonClick(true);
            m_pUpgBtn2_Src.SetButtonClick(false);
            m_pUpgBtn3_Src.SetButtonClick(false);
            JADBManager.I.m_nSelectUpgIndex = 0;
            m_pUpgMain_Src.SetSelectState(1);
        }

    }

    public void Button_2()
    {
        if (m_eState == eState.E_STATE_NORMAL)
        {
            m_pUpgBtn1_Src.SetButtonClick(false);
            m_pUpgBtn2_Src.SetButtonClick(true);
            m_pUpgBtn3_Src.SetButtonClick(false);
            JADBManager.I.m_nSelectUpgIndex = 1;
            m_pUpgMain_Src.SetSelectState(2);
        }
    }

    public void Button_3()
    {
        if (m_eState == eState.E_STATE_NORMAL)
        {
            m_pUpgBtn1_Src.SetButtonClick(false);
            m_pUpgBtn2_Src.SetButtonClick(false);
            m_pUpgBtn3_Src.SetButtonClick(true);
            JADBManager.I.m_nSelectUpgIndex = 2;
            m_pUpgMain_Src.SetSelectState(3);
        }
    }


    public void Button_Back()
    {
        if (m_eState == eState.E_STATE_NORMAL)
        {
            //! NORMAL 상태일때

            JAManager.I.SaveData();
            JAManager.I.SetTitleFadeA(true, 1, 3f);
            JAManager.I.m_bUpgZone = false;
         
         
            NGUITools.SetActive(m_pUpgMain_Src.m_pInvenMain_Src.m_pButtons_Obj, true);
           

            if (JAManager.I.m_bInvenGoUpg == true)
            {
                m_pUpgMain_Src.m_pInvenMain_Src.SetItemChange(m_pUpgMain_Src.m_pInvenMain_Src.m_nCurSaveIndex);
                m_pUpgMain_Src.m_pInvenMain_Src.SetInvenPrefab(true);
                m_pUpgMain_Src.m_pInvenMain_Src.SetState_INVEN();
                JAManager.I.m_bInvenGoUpg = false;
            }
            else
            {
                m_pUpgMain_Src.m_pInvenMain_Src.SetState_SHOW();
            }

            JAPrefabMng.I.DestroyPrefab("prf_ItemUpgradePop(Clone)");

        }
        else if (m_eState == eState.E_STATE_INVEN)
        {
            //! INVEN 상태일때

            NGUITools.SetActive(m_pUpgMain_Src.m_pUpgButtons_Gam, true);

            JAPrefabMng.I.DestroyPrefab(m_pUpgMain_Src.m_pPrfUpgInvenScroll_Gam);
            JAPrefabMng.I.DestroyPrefab(m_pUpgMain_Src.m_pPrfUpgInvenScrollPop_Gam);
            

            m_eState = eState.E_STATE_NORMAL;
        }

    }

    public void Button_Inventory()
    {
        if (m_eState == eState.E_STATE_NORMAL)
        {
            //NGUITools.SetActive(m_pUpgMain_Src.m_pInvenMain_Src.m_pButtons_Obj, false);
            //JAManager.I.m_bUpgZone = true;
            //JAManager.I.SetTitleFadeA(false, 0, 10f);
            //JAPrefabMng.I.CreatePrefab("Pop_Offset", E_JA_RESOURCELOAD.E_JIAN, "prf_ItemUpgradePop", -2f);
            if (JAManager.I.myData.manage.m_stInven.m_stDBInven[(int)JAManager.I.m_eMyItemSlot].m_bUseItem == false)
            {
                JAPrefabMng.I.CreatePopup("아이템 강화", "슬롯이 비어있습니다." + System.Environment.NewLine + "무기를 장착해주세요.");
            }
            else
            {
                JAManager.I.SetTitleFadeA(true, 1, 3f);
                JAManager.I.m_bInvenGoUpg = true;
                JAManager.I.m_bUpgZone = false;

                NGUITools.SetActive(m_pUpgMain_Src.m_pInvenMain_Src.m_pButtons_Obj, true);
                if (JAManager.I.m_bInvenGoUpg == true)
                {
                    m_pUpgMain_Src.m_pInvenMain_Src.SetInvenPrefab(true);
                    m_pUpgMain_Src.m_pInvenMain_Src.SetState_INVEN();
                    JAManager.I.m_bInvenGoUpg = false;
                }
                else
                {
                    m_pUpgMain_Src.m_pInvenMain_Src.SetState_SHOW();
                }
                JAPrefabMng.I.DestroyPrefab("prf_ItemUpgradePop(Clone)");
                m_eState = eState.E_STATE_INVEN;
            }
        }
    }

    public void Button_Buy()
    {
        if (m_eState == eState.E_STATE_NORMAL)
        {
            //! NORMAL 상태일때

            switch (JADBManager.I.m_nSelectUpgIndex)
            {
                case 0:
                    //m_pUpgMain_Src.SetWarningMessage();
                    Button_LeftUpgScroll();
                    break;
                case 1:
                    //JAPrefabMng.I.CreatePopup("알 림", "현재 버전에서 사용할수없습니다.", "", "", E_JA_POPUP_SETTING.E_POPUP_OK);
                    //m_pUpgBtn2_Src.Enter(true, JAManager.I.m_eMyItemSlot);
                    //Debug.Log("NameChange : " + JAManager.I.m_eMyItemSlot);
                    Button_LeftUpgScroll();
                    break;
                case 2:
                    //JAPrefabMng.I.CreatePopup("알 림", "현재 버전에서 사용할수없습니다.", "", "", E_JA_POPUP_SETTING.E_POPUP_OK);
                    Button_LeftUpgScroll();
                    //m_pUpgMain_Src.SetWarningMessage();
                    break;
            }

            JAManager.I.SaveData();
        }
        else if (m_eState == eState.E_STATE_INVEN)
        {
            //! INVEN 상태일때


        }
    }

    public void Button_LeftUpgScroll()
    {
        if (m_eState == eState.E_STATE_NORMAL)
        {
            //! NORMAL 상태일때

            //for (int i = 0; i < JADBManager.I.GetInvenUseCnt(); i++)
            //    JADBManager.I.m_nInvenCurTableIndex = JADBManager.I.GetInvenItemStart(JAManager.I.myData.manage.m_stInven.m_stDBInven[i].m_nBigName, JAManager.I.myData.manage.m_stInven.m_stDBInven[i].m_nItemName);
            switch (JADBManager.I.m_nSelectUpgIndex)
            {
                case 1:
                    
                    NGUITools.SetActive(m_pUpgMain_Src.m_pUpgButtons_Gam, false);
                    NGUITools.SetActive(m_pUpgMain_Src.m_pUseItemButtons_Gam, true);
                    NGUITools.SetActive(m_pUpgMain_Src.m_pUseItemBottomLabel_Gam, true);
                    NGUITools.SetActive(m_pUpgMain_Src.m_pUseItemBoxMessage_Gam, false);
                    for ( int i = 0; i<m_pUpgMain_Src.m_pUseTwoItemBox_Gam.Length; i++ )
                        NGUITools.SetActive(m_pUpgMain_Src.m_pUseTwoItemBox_Gam[i], false);

                    m_pUpgMain_Src.m_pPrfUpgInvenScrollPop_Gam = JAPrefabMng.I.CreatePrefab("Pop_Offset", E_JA_RESOURCELOAD.E_JIAN, "prf_MyInven", -2f);
                    m_pUpgMain_Src.m_pPrfUpgInvenScroll_Gam = JAPrefabMng.I.CreatePrefab("Scroll_Offset", E_JA_RESOURCELOAD.E_JIAN, "prf_MyInvenScroll", -2f);

                    m_pUpgMain_Src.SetSelectState(2);
                    m_pUpgMain_Src.m_pInvenMain_Src.m_bUpgShow = true;
                    //JAPrefabMng.I.CreatePopup("알 림", "현재 버전에서 사용할수없습니다.", "", "", E_JA_POPUP_SETTING.E_POPUP_OK);
                    

                    break;
                case 2:
                    //! 인벤토리들어가서 삭제
                    NGUITools.SetActive(m_pUpgMain_Src.m_pUpgButtons_Gam, false);
                    NGUITools.SetActive(m_pUpgMain_Src.m_pUseItemButtons_Gam, false);
                    NGUITools.SetActive(m_pUpgMain_Src.m_pUseItemBottomLabel_Gam, false);
                    NGUITools.SetActive(m_pUpgMain_Src.m_pUseItemBoxMessage_Gam, true); 
                    m_pUpgMain_Src.m_pPrfUpgInvenScrollPop_Gam = JAPrefabMng.I.CreatePrefab("Pop_Offset", E_JA_RESOURCELOAD.E_JIAN, "prf_MyInven", -2f);
                    m_pUpgMain_Src.m_pPrfUpgInvenScroll_Gam = JAPrefabMng.I.CreatePrefab("Scroll_Offset", E_JA_RESOURCELOAD.E_JIAN, "prf_MyInvenScroll", -2f);

                    m_pUpgMain_Src.SetSelectState(3);
                    m_pUpgMain_Src.m_pInvenMain_Src.m_bUpgShow = true;

                    //! 들고있는 무기 삭제
                    //m_pUpgBtn3_Src.DestroySystem((int)JAManager.I.m_eMyItemSlot);
                    //JAManager.I.myData.manage.m_stInven.m_stDBInven[(int)JAManager.I.m_eMyItemSlot].SetInven_DeleteData();
                    //JAManager.I.myData.manage.m_stInven.m_nDBInvenScrollIndex--;
                    //JAManager.I.myData.manage.m_stInven.m_nDBInvenScrollIndex++;
                    //Button_Back();
                    break;
                default:
                    
                    NGUITools.SetActive(m_pUpgMain_Src.m_pUpgButtons_Gam, false);
                    NGUITools.SetActive(m_pUpgMain_Src.m_pUseItemButtons_Gam, true);
                    NGUITools.SetActive(m_pUpgMain_Src.m_pUseItemBottomLabel_Gam, true);
                    NGUITools.SetActive(m_pUpgMain_Src.m_pUseItemBoxMessage_Gam, false);
                    for (int i = 0; i < m_pUpgMain_Src.m_pUseTwoItemBox_Gam.Length; i++)
                        NGUITools.SetActive(m_pUpgMain_Src.m_pUseTwoItemBox_Gam[i], true);

                    //m_pUpgMain_Src.m_pPrfUpgInvenScrollPop_Gam = JAPrefabMng.I.CreatePrefab("Pop_Offset", E_JA_RESOURCELOAD.E_JIAN, "prf_ItemUpgInvenPop", -2f);
                    //m_pUpgMain_Src.m_pPrfUpgInvenScroll_Gam = JAPrefabMng.I.CreatePrefab("Scroll_Offset", E_JA_RESOURCELOAD.E_JIAN, "prf_ItemUpgInvenScroll", -2f);
                    m_pUpgMain_Src.m_pPrfUpgInvenScrollPop_Gam = JAPrefabMng.I.CreatePrefab("Pop_Offset", E_JA_RESOURCELOAD.E_JIAN, "prf_MyInven", -2f);
                    m_pUpgMain_Src.m_pPrfUpgInvenScroll_Gam = JAPrefabMng.I.CreatePrefab("Scroll_Offset", E_JA_RESOURCELOAD.E_JIAN, "prf_MyInvenScroll", -2f);

                    m_pUpgMain_Src.SetSelectState(1);
                    m_pUpgMain_Src.m_pInvenMain_Src.m_bUpgShow = true;
                    break;
            }

            //m_eState = eState.E_STATE_INVEN;
        }
        else if (m_eState == eState.E_STATE_INVEN)
        {
            //! INVEN 상태일때


        }
    }


    public void BackKeyUdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape) == true)
        {
            if (m_eState == eState.E_STATE_NORMAL)
            {
                Button_Back();
                
            }
            else if (m_eState == eState.E_STATE_INVEN)
            {
                Button_LeftUpgScroll();
                Button_Back();
            }
        }
    }
}
