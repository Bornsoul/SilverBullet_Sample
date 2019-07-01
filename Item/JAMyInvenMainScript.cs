using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JAMyInvenMainScript : MonoBehaviour
{
    enum eState
    {
        E_STATE_NONE,
        E_STATE_START,
        E_STATE_END,
    };

    private eState m_eState = eState.E_STATE_NONE;

    internal JAItemInvenMainScript m_pItemInven_Src = null;
    internal JAMyInvenScrollMainScript m_pItemScroll_Src = null;
    internal JAItemUpgradMainScript m_pUpgButton_Src = null;

    public GameObject m_pNormalInvenBtn_Gam = null;
    public GameObject m_pUpgInveBtn_Gam = null;
    public UILabel m_pUpgBtnLabel = null;

    void Start()
    {
        JAManager.I.LoadData();
        m_pItemInven_Src = GameObject.Find("prf_ItemInvenPop(Clone)").GetComponent<JAItemInvenMainScript>();
        m_pItemScroll_Src = GameObject.Find("prf_MyInvenScroll(Clone)").GetComponent<JAMyInvenScrollMainScript>();
        m_pItemScroll_Src.Enter();

        if (JAManager.I.m_bUpgZone == true)
        {
            NGUITools.SetActive(m_pNormalInvenBtn_Gam, false);
            NGUITools.SetActive(m_pUpgInveBtn_Gam, true);
            switch (JADBManager.I.m_nSelectUpgIndex)
            {
                case 2:
                    m_pUpgBtnLabel.text = "분해";
                    break;
                default:
                    m_pUpgBtnLabel.text = "결정";
                    break;
            }
            m_pUpgButton_Src = GameObject.Find("prf_ItemUpgradePop(Clone)").GetComponent<JAItemUpgradMainScript>();
        }
        else
        {
            NGUITools.SetActive(m_pNormalInvenBtn_Gam, true);
            NGUITools.SetActive(m_pUpgInveBtn_Gam, false);
        }

        

        m_eState = eState.E_STATE_START;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) == true)
        {
            Button_Exit();
        }
    }

    void OnDestroy()
    {


    }

    public void Button_LogBtn()
    {
        //JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex].m_nLevel++;

        JAPlayerStat pPlayerSet = new JAPlayerStat();

        pPlayerSet.SetMoveSpeed();
    }

    public void Button_AddItem()
    {
        //JAPlayerStat pPlayerSet = new JAPlayerStat();

        //pPlayerSet.SetNoiseReduce();
    }

    public void Button_Undo()
    {

        
        //JAManager.I.myData.manage.m_stInven.m_stDBInven[m_nUndoCnt--] = JAManager.I.myData.manage.m_stInven.m_stDBInven[m_nUndoList];
    }

    public void Button_DelItem()
    {
		if ( JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex].m_nItemValue <= 1 )
		{
            //JAManager.I.myData.manage.m_stInven.m_stDBInven[m_nUndoCnt++] = JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex];            
            JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex].SetInven_DeleteData();
            JAManager.I.myData.manage.m_stInven.m_nDBInvenScrollIndex--;
            JAManager.I.myData.manage.m_stInven.m_nDBInvenScrollIndex++;
			return;
		}
		else
		{
			JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex].m_nItemValue--;
			
		}

		//JADBManager.I.Swap();
    }

    public void Button_AddTable()
    {
        if (m_eState == eState.E_STATE_START)
        {
            m_pItemScroll_Src.m_pScrollBar.enabled = false;

            if (JAManager.I.myData.manage.m_stInven.m_nDBInvenScrollIndex > 99)
            {
                Debug.Log("인벤토리 추가 불가");
                return;
            }
            else
            {
                m_pItemScroll_Src.Destroy();

                JAPrefabMng.I.CreateLoading();
                StartCoroutine(Delay(0.1f));

                Debug.Log("현재 테이블 = " + (JAManager.I.myData.manage.m_stInven.m_nDBInvenScrollIndex + 5));
            }
        }
    }

    IEnumerator Delay(float fWaitTime)
    {
        yield return new WaitForSeconds(fWaitTime);


        JAManager.I.myData.manage.m_stInven.m_nDBInvenScrollIndex += 5;
        m_pItemScroll_Src.SettingInvenTable();
        m_pItemScroll_Src.transform.GetComponentInChildren<UIGrid>().Reposition();

        JAPrefabMng.I.DestroyLoading();
    }

    public void Button_Buy()
    {
        if (m_eState == eState.E_STATE_START)
        {
            if (JAManager.I.m_bUpgZone == false) return;

            switch (JADBManager.I.m_nSelectUpgIndex)
            {
                case 0:
                    Debug.Log("강화");
                    if (JAManager.I.m_nUseItemBoxCnt <= 106)
                    {
                        JAPrefabMng.I.CreatePopup("아이템 강화", "선택된 재료가 없습니다." + System.Environment.NewLine + "재료를 선택해주세요.", "", "", E_JA_POPUP_SETTING.E_POPUP_OK);
                        return;
                    }

                    LevelExpFun();

                    JAManager.I.myData.manage.m_stMyInfo.m_nPrice -= JADBManager.I.GetUpgItemPrice_1();

                    break;
                case 1:
                    Debug.Log("개조");
                    if (JAManager.I.m_nUseItemBoxCnt <= 106)
                    {
                        JAPrefabMng.I.CreatePopup("아이템 개조", "두개의 재료가 필요합니다." + System.Environment.NewLine + "총열과 스프링을 선택해주세요.", "", "", E_JA_POPUP_SETTING.E_POPUP_OK);
                        return;
                    }

                    JAItemUpg_2 pItemUpg2 = new JAItemUpg_2();
                    

                    for (int i = 0; i < JADBManager.I.GetInvenUseCnt(); i++)
                    {
                        if (JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex].m_nItemName >= 200 &&
                              JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex].m_nItemName <= 210)
                        {
                            if (JADBManager.I.m_nCopyCheckCnt < 2)
                            {

                                JAPrefabMng.I.CreatePopup("아이템 강화", "스프링이 필요합니다.", "", "", E_JA_POPUP_SETTING.E_POPUP_OK);
                                return;
                            }

                            if (JAManager.I.myData.manage.m_stInven.m_stDBInven[i].m_bUseName == true)
                            {

                                pItemUpg2.Enter(true, true, JAManager.I.m_eMyItemSlot);
                                Debug.Log("접두");
                                JAManager.I.myData.manage.m_stInven.m_stDBInven[i].m_bUseName = false;
                            }
                        }
                        if (JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex].m_nItemName >= 300 &&
                                JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex].m_nItemName <= 310)
                        {
                            if (JADBManager.I.m_nCopyCheckCnt < 2)
                            {

                                JAPrefabMng.I.CreatePopup("아이템 강화", "총열이 필요합니다.", "", "", E_JA_POPUP_SETTING.E_POPUP_OK);
                                return;
                            }

                            if (JAManager.I.myData.manage.m_stInven.m_stDBInven[i].m_bUseName == true)
                            {
                                pItemUpg2.Enter(true, false, JAManager.I.m_eMyItemSlot);
                                JAManager.I.myData.manage.m_stInven.m_stDBInven[i].m_bUseName = false;
                                Debug.Log("중간");
                            }
                        }
                    }
                    
                    JAManager.I.myData.manage.m_stMyInfo.m_nPrice -= JADBManager.I.GetUpgItemPrice_1();
                    break;
                case 2:
                    Debug.Log("분해 클릭");

                    //switch (JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex].m_nBigName)
                    //{
                    //    case 1:
                    //        JAPrefabMng.I.CreatePopup("아이템 분해",
                    //       JADBManager.I.m_stFirstSubName[JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex].m_nFirstName].m_sName +
                    //       JADBManager.I.m_stSecondSubName[JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex].m_nSecondName].m_sName +
                    //       JADBManager.I.GetInvenItemCodeName(1, JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex].m_nItemName)
                    //       + " 를(을) 정말 분해 하시겠습니까?", "JASelectPopItemDelBtn", "", E_JA_POPUP_SETTING.E_POPUP_OK_CANCEL);
                    //        return;
                    //    default:
                    //        JAPrefabMng.I.CreatePopup("아이템 분해", "재료는 분해할수 없습니다.");
                    //        break;
                            
                    //}
                       
                        
                    
                    //else
                    //{
                    //    JAPrefabMng.I.CreatePopup("아이템 분해", "재료는 분해할수 없습니다.");
                    //    return;
                    //}
                    if (JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex].m_nBigName == 1)
                    {
                        JAItemUpg_3 pUpg3 = new JAItemUpg_3();
                        pUpg3.DestroySystem(JADBManager.I.m_nInvenCurTableIndex);
                        //JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex].SetInven_DeleteData();
                      
                    }
                    else
                    {
                        JAPrefabMng.I.CreatePopup("아이템 분해", "재료는 분해할수 없습니다.");
                        return;
                    }
                    
                    break;
            }

            m_pItemInven_Src.SetInvenPrefab(false);
            m_pItemInven_Src.SetDestroyInvenTable(1);

            ////////////////

            NGUITools.SetActive(m_pUpgButton_Src.m_pUpgButtons_Gam, true);
            NGUITools.SetActive(m_pUpgButton_Src.m_pUseItemBoxMessage_Gam, true);
            NGUITools.SetActive(m_pUpgButton_Src.m_pUseItemButtons_Gam, false);
            NGUITools.SetActive(m_pUpgButton_Src.m_pUseItemBottomLabel_Gam, false);
            m_pItemInven_Src.m_bUpgShow = false;

            JAManager.I.m_nUseItemBoxCnt = 106;
            JADBManager.I.m_nCopyInvenNum = 0;
            JADBManager.I.m_nCopyCheckCnt = 0;

            JAManager.I.myData.manage.m_stInven.m_stDBInven[106].SetInven_DeleteData();
            JAManager.I.myData.manage.m_stInven.m_stDBInven[107].SetInven_DeleteData();
            JAManager.I.myData.manage.m_stInven.m_stDBInven[108].SetInven_DeleteData();
            JAManager.I.myData.manage.m_stInven.m_stDBInven[109].SetInven_DeleteData();

            for (int i = 0; i < JADBManager.I.m_pCopyInven.Length; i++)
            {
                JADBManager.I.m_pCopyInven[i] = null;
                JADBManager.I.m_sItemUseSprite[i] = string.Empty;
            }

            for (int i = 0; i < JAManager.I.m_bItemUse.Length; i++)
                JAManager.I.m_bItemUse[i] = false;

            

            m_pItemInven_Src.Button_Upgrade();
           

            //////////////////////
            for (int i = 0; i < JAManager.I.myData.manage.m_stInven.m_nDBInvenCnt; i++)
            {
                switch (JAManager.I.myData.manage.m_stInven.m_stDBInven[i].m_nBigName)
                {
                    case 3:
                        if (JAManager.I.myData.manage.m_stInven.m_stDBInven[i].m_nItemValue <= 0)
                        {
                            JAManager.I.myData.manage.m_stInven.m_stDBInven[i].SetInven_DeleteData();
                        }
                        break;
                }
                
            }

            //JAPrefabMng.I.CreatePopup("아이템 강화", "- 디버그 -" +System.Environment.NewLine + "강화 완료!", "", "", E_JA_POPUP_SETTING.E_POPUP_OK);

            JADBManager.I.Swap();
            JAManager.I.SaveData();
            m_eState = eState.E_STATE_END;
        }
    }

    public void Button_Exit()
    {
        if (m_eState == eState.E_STATE_START)
        {
            m_pItemInven_Src.SetInvenPrefab(false);
            m_pItemInven_Src.SetDestroyInvenTable(1);

            if (JAManager.I.m_bUpgZone == true)
            {
                NGUITools.SetActive(m_pUpgButton_Src.m_pUpgButtons_Gam, true);
                NGUITools.SetActive(m_pUpgButton_Src.m_pUseItemBoxMessage_Gam, true);
                NGUITools.SetActive(m_pUpgButton_Src.m_pUseItemButtons_Gam, false);
                NGUITools.SetActive(m_pUpgButton_Src.m_pUseItemBottomLabel_Gam, false);
                m_pItemInven_Src.m_bUpgShow = false;

                JAManager.I.m_nUseItemBoxCnt = 106;
                JADBManager.I.m_nCopyInvenNum = 0;
                JADBManager.I.m_nCopyCheckCnt = 0;

                JAManager.I.myData.manage.m_stInven.m_stDBInven[106].SetInven_DeleteData();
                JAManager.I.myData.manage.m_stInven.m_stDBInven[107].SetInven_DeleteData();
                JAManager.I.myData.manage.m_stInven.m_stDBInven[108].SetInven_DeleteData();
                JAManager.I.myData.manage.m_stInven.m_stDBInven[109].SetInven_DeleteData();

                int[] nArr = new int[8] { 64, 65, 66, 67, 68, 69, 70, 71 };

                for (int i = 0; i < JAManager.I.myData.manage.m_stInven.m_nDBInvenCnt; i++)
                {
                    switch (JAManager.I.myData.manage.m_stInven.m_stDBInven[i].m_nBigName)
                    {
                        case 3:
                            if (JAManager.I.myData.manage.m_stInven.m_stDBInven[i].m_nItemName >= 200)
                            {
                                if (JAManager.I.myData.manage.m_stInven.m_stDBInven[i].m_nFirstTier == 1)
                                    Debug.Log(JAManager.I.myData.manage.m_stInven.m_stDBInven[i].m_nItemName);
                            }
                            break;
                    }

                }

                for (int i = 0; i < JADBManager.I.m_pCopyInven.Length; i++)
                {
                    JADBManager.I.m_pCopyInven[i] = null;
                    JADBManager.I.m_sItemUseSprite[i] = string.Empty;
                }
                for (int i = 0; i < JAManager.I.m_bItemUse.Length; i++)
                    JAManager.I.m_bItemUse[i] = false;

                m_pItemInven_Src.Button_Upgrade();
            }

            for ( int i = 0; i<JAManager.I.myData.manage.m_stInven. m_nDBInvenCnt; i++ )
            {
                switch ( JAManager.I.myData.manage.m_stInven.m_stDBInven[i].m_nBigName )
                {
                    case 3:
                        if (JAManager.I.myData.manage.m_stInven.m_stDBInven[i].m_nItemValue <= 0)
                        {
                            JAManager.I.myData.manage.m_stInven.m_stDBInven[i].SetInven_DeleteData();
                        }
                        break;
                }

            }

            JADBManager.I.Swap();
            JAManager.I.LoadData();
            m_eState = eState.E_STATE_END;
        }
    }

    public void LevelExpFun()
    {
        //! 경험치 부분 
        if (JADBManager.I.GetLevelExpValue((int)JAManager.I.m_eMyItemSlot) >= 1f)
        {
            if (JAManager.I.myData.manage.m_stInven.m_stDBInven[(int)JAManager.I.m_eMyItemSlot].m_fLevelExp > JADBManager.I.GetLevelExp((int)JAManager.I.m_eMyItemSlot,
                JAManager.I.myData.manage.m_stInven.m_stDBInven[(int)JAManager.I.m_eMyItemSlot].m_nLevel))
            {
                JADBManager.I.m_fMaxExp =
                        (JAManager.I.myData.manage.m_stInven.m_stDBInven[(int)JAManager.I.m_eMyItemSlot].m_fLevelExp -
                        JADBManager.I.GetLevelExp((int)JAManager.I.m_eMyItemSlot,
                        JAManager.I.myData.manage.m_stInven.m_stDBInven[(int)JAManager.I.m_eMyItemSlot].m_nLevel));
                Debug.Log("MyExp : " + (int)JAManager.I.myData.manage.m_stInven.m_stDBInven[(int)JAManager.I.m_eMyItemSlot].m_fLevelExp);
                Debug.Log("MaxExp : " + (int)JADBManager.I.m_fMaxExp);

                JAManager.I.myData.manage.m_stInven.m_stDBInven[(int)JAManager.I.m_eMyItemSlot].m_fLevelExp = JADBManager.I.m_fMaxExp;
                LevelExpFun();
            }
            else
            {
                JAManager.I.myData.manage.m_stInven.m_stDBInven[(int)JAManager.I.m_eMyItemSlot].m_fLevelExp = 0f;
            }

            if ( JAManager.I.myData.manage.m_stInven.m_stDBInven[(int)JAManager.I.m_eMyItemSlot].m_nLevel < JADBManager.I.GetInvenItemMaxLevel(JAManager.I.myData.manage.m_stInven.m_stDBInven[(int)JAManager.I.m_eMyItemSlot].m_nItemName) )
                JAManager.I.myData.manage.m_stInven.m_stDBInven[(int)JAManager.I.m_eMyItemSlot].m_nLevel++;
        }
    }
}
