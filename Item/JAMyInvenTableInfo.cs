using UnityEngine;
using System.Collections;

public class JAMyInvenTableInfo : MonoBehaviour
{
    public BoxCollider m_pBtnCollider = null;
    public UISprite m_pBackSprite = null;
    public UISprite m_pItemSprite = null;

    public UILabel m_pItemCntLabel = null;

    [HideInInspector]
    public int m_nIndex = 0;

   


    public JADBInvenScript m_stDBInven = null;

    //public bool m_bItemClick = false;
    public int m_nItemCode = 0;
    //public int m_nLevel = 0;

    void Start()
    {
        if (m_pItemSprite == null) return;
        m_pBackSprite = transform.FindChild("Background").GetComponent<UISprite>();
        m_pBtnCollider = GetComponent<BoxCollider>();

    }

    void Update()
    {

    }
    
    public void SetItemSprite(string sSpriteName, bool bShow = true)
    {
        m_pItemSprite.spriteName = sSpriteName;
        m_pItemSprite.enabled = bShow;
    }

    void OnPress(bool isPress)
    {
        if (isPress == true)
        {

        }
        else
        {

        }
    }

    void OnClick()
    {
        if (JADBManager.I.m_fExpValue < 0) JADBManager.I.m_fExpValue = 0f;
        JADBManager.I.m_nInvenCurTableIndex = m_nIndex;
        JAManager.I.GetLogLong("선택: " + JADBManager.I.m_nInvenCurTableIndex.ToString(),
            ", 코드: ", JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex].m_nItemCode.ToString(),
            ", 레벨: ", JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex].m_nLevel.ToString(),
            ", 장착: ", JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex].m_bUseItem.ToString());

        //JADBManager.I.m_fExpValue = 0f;

        //! 아이템 강화 업그레이드 인벤에서 눌러졌을때 입니다.
        //! JAItemUseBoxBtn 과 관련있습니다.
        if (JAManager.I.m_bUpgZone == true)
        {
            switch (JADBManager.I.m_nSelectUpgIndex)
            {
                case 0:
                    //! 아이템 강화
                    switch (JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex].m_nBigName)
                    {
                        case 1:
                            JAPrefabMng.I.CreatePopup("아이템 강화", "아이템 강화에서는 무기를" + System.Environment.NewLine + "재료로 사용할수 없습니다!", "", "", E_JA_POPUP_SETTING.E_POPUP_OK);
                            break;
                        case 3:
                            if (JAManager.I.m_nUseItemBoxCnt > 109 || JADBManager.I.m_nCopyInvenNum > 4)
                            {
                                JAPrefabMng.I.CreatePopup("아이템 강화", "더이상 추가할수 없습니다!", "", "", E_JA_POPUP_SETTING.E_POPUP_OK);
                                return;
                            }
                            if (JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex].m_nItemValue < 1) return;
                            if (JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex].m_nItemName > 150)
                            {
                                JAPrefabMng.I.CreatePopup("아이템 강화", "강화는 악마의 심장만 사용가능합니다.", "", "", E_JA_POPUP_SETTING.E_POPUP_OK);
                                return;
                            }
                            if (JAManager.I.myData.manage.m_stInven.m_stDBInven[(int)JAManager.I.m_eMyItemSlot].m_nLevel >= JADBManager.I.GetInvenItemMaxLevel(JAManager.I.myData.manage.m_stInven.m_stDBInven[(int)JAManager.I.m_eMyItemSlot].m_nItemName))
                            {
                                JAPrefabMng.I.CreatePopup("아이템 강화", "더이상 아이템 업그레이드를" + System.Environment.NewLine + "하실수 없습니다!", "", "", E_JA_POPUP_SETTING.E_POPUP_OK);
                                return;
                            }

                           
                            switch (JAManager.I.m_nUseItemBoxCnt)
                            {
                                case 106:
                                    JAManager.I.m_bItemUse[0] = true;
                                    JADBManager.I.m_sItemUseSprite[0] = JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex].m_sIconName;
                                    break;
                                case 107:
                                    JAManager.I.m_bItemUse[1] = true;
                                    JADBManager.I.m_sItemUseSprite[1] = JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex].m_sIconName;
                                    break;
                                case 108:
                                    JAManager.I.m_bItemUse[2] = true;
                                    JADBManager.I.m_sItemUseSprite[2] = JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex].m_sIconName;
                                    break;
                                case 109:
                                    JAManager.I.m_bItemUse[3] = true;
                                    JADBManager.I.m_sItemUseSprite[3] = JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex].m_sIconName;
                                    break;
                            }
                            JAManager.I.myData.manage.m_stInven.m_stDBInven[JAManager.I.m_nUseItemBoxCnt].SetAddInven_UseItemData(JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex].m_nBigName,
                                                                                                                              JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex].m_nSmallName,
                                                                                                                              JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex].m_nItemValue,
                                                                                                                              JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex].m_nItemName,
                                                                                                                              JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex].m_nLevel,
                                                                                                                              JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex].m_fLevelExp,
                                                                                                                              JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex].m_bUseItem,
                                                                                                                              JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex].m_bUseName,
                                                                                                                              JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex].m_sIconName);

                            JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex].m_nItemValue--;
                            ++JAManager.I.m_nUseItemBoxCnt;
                            //if (JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex].m_nItemValue < 1)
                            //{
                            //    JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex].m_sIconName = "";
                            //}
                            JADBManager.I.SetAddExp(JADBManager.I.GetUseItemExp(JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex].m_nBigName,
                                JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex].m_nItemName));

                            JADBManager.I.m_pCopyInven[JADBManager.I.m_nCopyInvenNum] = JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex];
                            ++JADBManager.I.m_nCopyInvenNum; 
                            //Debug.Log("ItemLevel : " + JAManager.I.myData.manage.m_stInven.m_stDBInven[(int)JAManager.I.m_eMyItemSlot].m_nLevel);
                            //Debug.Log("ItemValue : " + JAManager.I.m_nUseItemSelCnt);
                            //Debug.Log("Price : " + JAManager.I.myData.manage.m_stInven.m_stDBInven[(int)JAManager.I.m_eMyItemSlot].m_nLevel * JAManager.I.m_nUseItemSelCnt * 1000);


                            break;
                    }
                    break;
                case 1:
                    //! 아이템 개조
                    switch (JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex].m_nBigName)
                    {
                        case 1:
                            JAPrefabMng.I.CreatePopup("아이템 강화", "아이템 강화에서는 무기를" + System.Environment.NewLine + "재료로 사용할수 없습니다!", "", "", E_JA_POPUP_SETTING.E_POPUP_OK);

                            break;
                        case 3:
                            if (JADBManager.I.m_fExpValue > 2f)
                            {
                                JAPrefabMng.I.CreatePopup("아이템 개조", "더이상 업그레이드 할수 없습니다!", "", "", E_JA_POPUP_SETTING.E_POPUP_OK);

                                return;
                            }
                            if (JAManager.I.m_nUseItemBoxCnt > 107 || JADBManager.I.m_nCopyInvenNum > 2)
                            {
                                JAPrefabMng.I.CreatePopup("아이템 개조", "더이상 추가할수 없습니다!", "", "", E_JA_POPUP_SETTING.E_POPUP_OK);
                                return;    
                            }
                            if (JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex].m_nItemValue < 1) return;

                            //if (JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex].m_nItemName < 104)
                            //{
                            //    JAPrefabMng.I.CreatePopup("아이템 개조", "개조는 총열과 스프링만 사용가능합니다.", "", "", E_JA_POPUP_SETTING.E_POPUP_OK);
                            //    return;
                            //}


                            switch (JAManager.I.m_nUseItemBoxCnt)
                            {
                                case 106:

                                    JAManager.I.m_bItemUse[0] = true;
                                    JADBManager.I.m_sItemUseSprite[0] = JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex].m_sIconName;

                                    break;
                                case 107:
   
                                    JAManager.I.m_bItemUse[1] = true;
                                    JADBManager.I.m_sItemUseSprite[1] = JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex].m_sIconName;

                                    break;
                            }
                            JAManager.I.myData.manage.m_stInven.m_stDBInven[JAManager.I.m_nUseItemBoxCnt].SetAddInven_UseItemData(JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex].m_nBigName,
                                                                                                                               JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex].m_nSmallName,
                                                                                                                               JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex].m_nItemValue,
                                                                                                                               JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex].m_nItemName,
                                                                                                                               JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex].m_nLevel,
                                                                                                                               JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex].m_fLevelExp,
                                                                                                                               JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex].m_bUseItem,
                                                                                                                               JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex].m_bUseName,
                                                                                                                               JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex].m_sIconName);

                            JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex].m_nItemValue--;
                            ++JAManager.I.m_nUseItemBoxCnt;
                            //if (JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex].m_nItemValue < 1)
                            //{
                            //    JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex].m_sIconName = "";

                            //}
                           
                            if (JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex].m_nItemName >= 200 &&
                               JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex].m_nItemName <= 210)
                            {
                                JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex].m_bUseName = true;
                            }
                            
                            if (JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex].m_nItemName >= 300 &&
                                JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex].m_nItemName <= 310)
                            {
                                JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex].m_bUseName = true;
                            }

                            JADBManager.I.m_nCopyCheckCnt++;
                            //JADBManager.I.m_pCopyInven[JADBManager.I.m_nCopyInvenNum++] = JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex];
                             JADBManager.I.m_pCopyInven[JADBManager.I.m_nCopyInvenNum] = JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex];
                            ++JADBManager.I.m_nCopyInvenNum; 
                            break;
                    }
                    break;
                case 2:
                    //! 아이템 삭제

                    switch (JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex].m_nBigName)
                    {
                        case 1:
                            Debug.Log("Item Destroy");
                          
                        
                            break;
                        case 3:
                            //JAPrefabMng.I.CreatePopup("아이템 분해", "아이템 분해에서는 재료를" + System.Environment.NewLine + "분해할수 없습니다!", "", "", E_JA_POPUP_SETTING.E_POPUP_OK);
                            
                            break;
                    }
                    
                    //m_pUpgBtn3_Src.DestroySystem((int)JAManager.I.m_eMyItemSlot);
                    //JAManager.I.myData.manage.m_stInven.m_stDBInven[(int)JAManager.I.m_eMyItemSlot].SetInven_DeleteData();
                    //JAManager.I.myData.manage.m_stInven.m_nDBInvenScrollIndex--;
                    //JAManager.I.myData.manage.m_stInven.m_nDBInvenScrollIndex++;
                    break;
            }

        }
        else
        {

        }
    }

    public UISprite GetItemSprite()
    {
        return m_pItemSprite;
    }

    public int GetTableIndex()
    {
        return m_nIndex;
    }

    //public bool GetTableClick()
    //{
    //    return m_bItemClick;
    //}

    //public int GetTableItemCode()
    //{
    //    return m_nItemCode;
    //}

    //public int GetLevel()
    //{
    //    return m_nLevel;
    //}
}
