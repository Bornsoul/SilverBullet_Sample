using UnityEngine;
using System.Collections;

public class JAMyInvenScrollMainScript : MonoBehaviour
{
    private Vector3 m_stMyPos = Vector3.zero;

    public GameObject[] m_pInvenScroll_Obj = null;
    public JAMyInvenTableInfo[] m_pInvenScroll_Src = null;
    public UIScrollBar m_pScrollBar = null;
    public int m_nTableIndex = 0;

    float m_fAlphaDown = 0f;
    Color m_stBtnColor;

    Vector4 m_stBtnVec;

    public void Enter()
    {

        JAManager.I.LoadData();
        m_pScrollBar = GameObject.Find("prf_MyInven(Clone)/Window/ScrollBar").GetComponent<UIScrollBar>();
   

        SettingInvenTable();

        JADBManager.I.Swap();
       

        transform.GetComponent<UIScrollView>().verticalScrollBar = m_pScrollBar;
        StartCoroutine(ScrollPosInit(0.1f));

    }

    IEnumerator ScrollPosInit(float fWaitTime)
    {
        yield return new WaitForSeconds(fWaitTime);

        m_stMyPos = gameObject.transform.localPosition;
        m_stMyPos.x = 0f;
        m_stMyPos.y = 0f;
        m_stMyPos.z = 0f;
        gameObject.transform.localPosition = m_stMyPos;
    }



    public void SetItem(int nIndex, int nBig, int nSmall, int nFirst, int nSecond, int nValue, int nItem, int nLevel, float fLevelExp, bool bUse, bool bUseName, string sIconName)
    {
        JADBInvenScript pDBInven = new JADBInvenScript();

		switch ( nBig )
		{
		case 1:
        	pDBInven.SetAddInven_DataInfo(nBig, nSmall, nFirst, nSecond, nItem, bUse, sIconName);
        	pDBInven.m_nLevel = nLevel;
            pDBInven.m_fLevelExp = fLevelExp;
			break;
		case 3:
            pDBInven.SetAddInven_UseItemData(nBig, nSmall, nValue, nItem, nLevel, fLevelExp, bUse, bUseName, sIconName);
			break;
		}
        m_pInvenScroll_Src[nIndex].m_stDBInven = pDBInven;
		m_pInvenScroll_Src[nIndex].m_pItemSprite.spriteName = sIconName;
		m_pInvenScroll_Src[nIndex].m_pItemCntLabel.text = nValue.ToString();

        JAManager.I.myData.manage.m_stInven.m_stDBInven[nIndex] = m_pInvenScroll_Src[nIndex].m_stDBInven;
        
        //if (pDBInven != null)
        //{
        //    JAManager.I.myData.manage.m_stInven.m_stDBInven[nIndex].m_bUseItem = true;
        //    m_pInvenScroll_Src[nIndex].m_pItemSprite.enabled = true;
        //}
        //else
        //{
        //    JAManager.I.myData.manage.m_stInven.m_stDBInven[nIndex].m_bUseItem = false;
        //    m_pInvenScroll_Src[nIndex].m_pItemSprite.enabled = false;
        //}

    }



    public void SettingInvenTable()
    {
        JADBManager.I.m_nInvenCurTableIndex = 0;
        JADBManager.I.m_fExpValue = 0f;
        for (int i = 0; i < JAManager.I.myData.manage.m_stInven.m_nDBInvenScrollIndex; i++)
        {

            m_pInvenScroll_Obj[i] = JAPrefabMng.I.CreatePrefab("JAGrid", E_JA_RESOURCELOAD.E_JIAN, "prf_MyInvenTable", -1f, ("prf_MyInvenTable" + i));
            m_pInvenScroll_Src[i] = m_pInvenScroll_Obj[i].GetComponent<JAMyInvenTableInfo>();
            m_pInvenScroll_Src[i].m_nIndex = i;

            m_pInvenScroll_Src[i].m_nItemCode = JAManager.I.myData.manage.m_stInven.m_stDBInven[i].m_nItemCode;
            m_pInvenScroll_Src[i].m_pItemSprite.enabled = false;
        }

        transform.GetComponent<UIScrollView>().ResetPosition();



        for (int i = 0; i < JAManager.I.myData.manage.m_stInven.m_nDBInvenScrollIndex; i++)
        {
                SetItem(i, JAManager.I.myData.manage.m_stInven.m_stDBInven[i].m_nBigName,
                        JAManager.I.myData.manage.m_stInven.m_stDBInven[i].m_nSmallName,
                        JAManager.I.myData.manage.m_stInven.m_stDBInven[i].m_nFirstName,
                        JAManager.I.myData.manage.m_stInven.m_stDBInven[i].m_nSecondName,
                        JAManager.I.myData.manage.m_stInven.m_stDBInven[i].m_nItemValue,
                        JAManager.I.myData.manage.m_stInven.m_stDBInven[i].m_nItemName,
                        JAManager.I.myData.manage.m_stInven.m_stDBInven[i].m_nLevel,
                        JAManager.I.myData.manage.m_stInven.m_stDBInven[i].m_fLevelExp,
                        JAManager.I.myData.manage.m_stInven.m_stDBInven[i].m_bUseItem,
                        JAManager.I.myData.manage.m_stInven.m_stDBInven[i].m_bUseName,
                        JAManager.I.myData.manage.m_stInven.m_stDBInven[i].m_sIconName);
        }
      
        JAManager.I.SaveData();
    }

    public void Destroy()
    {
        for (int i = 0; i < JAManager.I.myData.manage.m_stInven.m_nDBInvenScrollIndex; i++)
        {
            m_pInvenScroll_Src[i].m_stDBInven = null;
            JAPrefabMng.I.DestroyPrefab("prf_MyInvenTable"+i+"(Clone)");
        }
    }

    void Update()
    {
        for (int i = 0; i < JADBManager.I.GetInvenUseCnt(); i++)
        {

            if (i == JADBManager.I.m_nInvenCurTableIndex)
            {

                m_stBtnVec.w = 255f / 255f;
                m_stBtnVec.x = 55f / 255f;
                m_stBtnVec.y = 55f / 255f;
                m_stBtnVec.z = 200f / 255f;
            }
            else
            {
                m_stBtnVec.w = 255f / 255f;
                m_stBtnVec.x = 255f / 255f;
                m_stBtnVec.y = 255f / 255f;
                m_stBtnVec.z = 255f / 255f;
            }

            m_stBtnColor.r = m_stBtnVec.w;
            m_stBtnColor.g = m_stBtnVec.x;
            m_stBtnColor.b = m_stBtnVec.y;
            m_stBtnColor.a = m_stBtnVec.z;
            m_pInvenScroll_Src[i].m_pBackSprite.color = m_stBtnColor;

            if (JAManager.I.myData.manage.m_stInven.m_stDBInven[i].m_bUseItem == false)
            {
                m_pInvenScroll_Src[i].SetItemSprite(JAManager.I.myData.manage.m_stInven.m_stDBInven[i].m_sIconName, false);
            }
            else
            {
                m_pInvenScroll_Src[i].SetItemSprite(JAManager.I.myData.manage.m_stInven.m_stDBInven[i].m_sIconName, true);
            }
            
        }

        //! 선택된 아이템 갯수 표시
        for (int i = 0; i < JAManager.I.myData.manage.m_stInven.m_nDBInvenScrollIndex; i++)
        {

            if (JAManager.I.myData.manage.m_stInven.m_stDBInven[i].m_nItemValue < 1)
            {
                m_pInvenScroll_Src[i].m_pItemCntLabel.enabled = false;
            }
            else
            {
                m_pInvenScroll_Src[i].m_pItemCntLabel.enabled = true;

            }

            m_pInvenScroll_Src[i].m_pItemCntLabel.text = JAManager.I.myData.manage.m_stInven.m_stDBInven[i].m_nItemValue.ToString();
        }

        //! 스크롤바 알파
        if (transform.GetComponent<UIScrollView>().isDragging == true)
        {
            m_fAlphaDown = 0f;
        }
        else
        {
            m_fAlphaDown += 1f * Time.deltaTime;
        }

        if (m_fAlphaDown > 1.4f)
            transform.GetComponent<UIScrollView>().showScrollBars = UIScrollView.ShowCondition.WhenDragging;
        else
            transform.GetComponent<UIScrollView>().showScrollBars = UIScrollView.ShowCondition.OnlyIfNeeded;
    }

}
