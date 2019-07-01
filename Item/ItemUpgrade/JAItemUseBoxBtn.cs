using UnityEngine;
using System.Collections;

public class JAItemUseBoxBtn : MonoBehaviour 
{
    internal UISprite m_pItemSprite = null;
	public int m_nItemNum = 0;
    

    public void Enter()
	{
        m_pItemSprite = transform.FindChild("ItemSprite").GetComponent<UISprite>();


        JAManager.I.myData.manage.m_stInven.m_stDBInven[106].SetInven_DeleteData();
        JAManager.I.myData.manage.m_stInven.m_stDBInven[107].SetInven_DeleteData();
        JAManager.I.myData.manage.m_stInven.m_stDBInven[108].SetInven_DeleteData();
        JAManager.I.myData.manage.m_stInven.m_stDBInven[109].SetInven_DeleteData();
	}

    void Update()
    {
        switch (JADBManager.I.m_nSelectUpgIndex)
        {
            case 0:
                //m_pItemSprite.spriteName = "Item_Heart_02";
                break;
            case 1:
                              
                break;
        }
    }

    void OnClick()
    {
        if (JAManager.I.myData.manage.m_stInven.m_stDBInven[m_nItemNum] == null)
        {
            Debug.Log("ERROR");
            return;
        }
        else
        {
            if (JAManager.I.m_nUseItemBoxCnt <= 106 || JADBManager.I.m_nCopyInvenNum <= 0) return;
            if (JADBManager.I.m_nCopyCheckCnt < 0) JADBManager.I.m_nCopyCheckCnt = 0;

            if (JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex].m_nItemName >= 200 &&
                               JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex].m_nItemName <= 210)
            {
                JADBManager.I.m_nCopyCheckCnt--;
                JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex].m_bUseName = false;
            }
            else if (JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex].m_nItemName >= 300 &&
                JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex].m_nItemName <= 310)
            {
                JADBManager.I.m_nCopyCheckCnt--;
                JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex].m_bUseName = false;
            }

            JAManager.I.m_nUseItemBoxCnt--;
            switch (JAManager.I.m_nUseItemBoxCnt)
            {
                case 106:
                    JAManager.I.m_bItemUse[0] = false;
                    break;
                case 107:
                    JAManager.I.m_bItemUse[1] = false;
                    break;
                case 108:
                    JAManager.I.m_bItemUse[2] = false;
                    break;
                case 109:
                    JAManager.I.m_bItemUse[3] = false;
                    break;
            }
            switch (JADBManager.I.m_nSelectUpgIndex)
            {
                case 0:
                    JADBManager.I.m_nCopyInvenNum--;
                    Debug.Log(JADBManager.I.m_pCopyInven[JADBManager.I.m_nCopyInvenNum].m_nItemValue++);

                    //JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex].m_nItemValue++;
                    //JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex].SetAddInven_UseItemData(JAManager.I.myData.manage.m_stInven.m_stDBInven[JAManager.I.m_nUseItemBoxCnt].m_nBigName,
                    //                                                                                                      JAManager.I.myData.manage.m_stInven.m_stDBInven[JAManager.I.m_nUseItemBoxCnt].m_nSmallName,
                    //                                                                                                      JAManager.I.myData.manage.m_stInven.m_stDBInven[JAManager.I.m_nUseItemBoxCnt].m_nItemValue,
                    //                                                                                                      JAManager.I.myData.manage.m_stInven.m_stDBInven[JAManager.I.m_nUseItemBoxCnt].m_nItemName,
                    //                                                                                                      JAManager.I.myData.manage.m_stInven.m_stDBInven[JAManager.I.m_nUseItemBoxCnt].m_nLevel,
                    //                                                                                                      JAManager.I.myData.manage.m_stInven.m_stDBInven[JAManager.I.m_nUseItemBoxCnt].m_fLevelExp,
                    //                                                                                                      JAManager.I.myData.manage.m_stInven.m_stDBInven[JAManager.I.m_nUseItemBoxCnt].m_bUseItem,
                    //                                                                                                      JAManager.I.myData.manage.m_stInven.m_stDBInven[JAManager.I.m_nUseItemBoxCnt].m_sIconName);

                    JADBManager.I.SetBackExp(JADBManager.I.GetUseItemExp(JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex].m_nBigName,
                                JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex].m_nItemName));
                    break;
                case 1:
                    //JADBManager.I.m_pCopyInvenList[JADBManager.I.m_nInvenCurTableIndex].m_nItemValue++;
                    JADBManager.I.m_nCopyInvenNum--;
                    Debug.Log(JADBManager.I.m_pCopyInven[JADBManager.I.m_nCopyInvenNum].m_nItemValue++);


                    //JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex].m_nItemValue++;
                    //JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex].SetAddInven_UseItemData(JAManager.I.myData.manage.m_stInven.m_stDBInven[JAManager.I.m_nUseItemBoxCnt].m_nBigName,
                    //                                                                                                      JAManager.I.myData.manage.m_stInven.m_stDBInven[JAManager.I.m_nUseItemBoxCnt].m_nSmallName,
                    //                                                                                                      JAManager.I.myData.manage.m_stInven.m_stDBInven[JAManager.I.m_nUseItemBoxCnt].m_nItemValue,
                    //                                                                                                      JAManager.I.myData.manage.m_stInven.m_stDBInven[JAManager.I.m_nUseItemBoxCnt].m_nItemName,
                    //                                                                                                      JAManager.I.myData.manage.m_stInven.m_stDBInven[JAManager.I.m_nUseItemBoxCnt].m_nLevel,
                    //                                                                                                      JAManager.I.myData.manage.m_stInven.m_stDBInven[JAManager.I.m_nUseItemBoxCnt].m_fLevelExp,
                    //                                                                                                      JAManager.I.myData.manage.m_stInven.m_stDBInven[JAManager.I.m_nUseItemBoxCnt].m_bUseItem,
                    //                                                                                                      JAManager.I.myData.manage.m_stInven.m_stDBInven[JAManager.I.m_nUseItemBoxCnt].m_sIconName);

                  
                    //for (int i = 0; i <= JAManager.I.myData.manage.m_stInven.m_nDBInvenCnt; i++)
                    //{
                    //    if (JAManager.I.myData.manage.m_stInven.m_stDBInven[i].m_bUseItem == false)
                    //    {
                    //        JAManager.I.myData.manage.m_stInven.m_stDBInven[i].SetAddInven_DataInfo(
                    //        JAManager.I.myData.manage.m_stInven.m_stDBInven[JAManager.I.m_nUseItemBoxCnt].m_nBigName,
                    //        JAManager.I.myData.manage.m_stInven.m_stDBInven[JAManager.I.m_nUseItemBoxCnt].m_nSmallName,
                    //        JAManager.I.myData.manage.m_stInven.m_stDBInven[JAManager.I.m_nUseItemBoxCnt].m_nFirstName,
                    //        JAManager.I.myData.manage.m_stInven.m_stDBInven[JAManager.I.m_nUseItemBoxCnt].m_nSecondName,
                    //        JAManager.I.myData.manage.m_stInven.m_stDBInven[JAManager.I.m_nUseItemBoxCnt].m_nItemName,
                    //        JAManager.I.myData.manage.m_stInven.m_stDBInven[JAManager.I.m_nUseItemBoxCnt].m_nLevel,
                    //        JAManager.I.myData.manage.m_stInven.m_stDBInven[JAManager.I.m_nUseItemBoxCnt].m_fLevelExp,
                    //        JAManager.I.myData.manage.m_stInven.m_stDBInven[JAManager.I.m_nUseItemBoxCnt].m_nExceed,
                    //        JAManager.I.myData.manage.m_stInven.m_stDBInven[JAManager.I.m_nUseItemBoxCnt].m_bUseItem);
                    //        break;
                    //    }
                    //}
                    ////JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex].SetAddInven_DataInfo(
                    ////JAManager.I.myData.manage.m_stInven.m_stDBInven[JAManager.I.m_nUseItemBoxCnt].m_nBigName,
                    ////JAManager.I.myData.manage.m_stInven.m_stDBInven[JAManager.I.m_nUseItemBoxCnt].m_nSmallName,
                    ////JAManager.I.myData.manage.m_stInven.m_stDBInven[JAManager.I.m_nUseItemBoxCnt].m_nFirstName,
                    ////JAManager.I.myData.manage.m_stInven.m_stDBInven[JAManager.I.m_nUseItemBoxCnt].m_nSecondName,
                    ////JAManager.I.myData.manage.m_stInven.m_stDBInven[JAManager.I.m_nUseItemBoxCnt].m_nItemName,
                    ////JAManager.I.myData.manage.m_stInven.m_stDBInven[JAManager.I.m_nUseItemBoxCnt].m_nLevel,
                    ////JAManager.I.myData.manage.m_stInven.m_stDBInven[JAManager.I.m_nUseItemBoxCnt].m_fLevelExp,
                    ////JAManager.I.myData.manage.m_stInven.m_stDBInven[JAManager.I.m_nUseItemBoxCnt].m_nExceed,
                    ////JAManager.I.myData.manage.m_stInven.m_stDBInven[JAManager.I.m_nUseItemBoxCnt].m_bUseItem);
                    break;
            }

        }
        JAManager.I.SaveData();
    }


}
