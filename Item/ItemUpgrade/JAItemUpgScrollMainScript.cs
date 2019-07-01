using UnityEngine;
using System.Collections;

public class JAItemUpgScrollMainScript : MonoBehaviour
{
    private Vector3 m_stMyPos = Vector3.zero;

    internal GameObject[] m_pItemScroll_Obj = null;
    internal JAItemUpgScrollTable[] m_pItemScroll_Src = null;
    internal UIScrollBar m_pScrollBar = null;

    internal int m_nTableIndex = 0;

    float m_fAlphaDown = 0f;
    Color m_stBtnColor;

    void Start()
    {       
        m_pItemScroll_Obj = new GameObject[JAManager.I.myData.manage.m_stInven.m_nDBInvenScrollIndex];
        m_pItemScroll_Src = new JAItemUpgScrollTable[JAManager.I.myData.manage.m_stInven.m_nDBInvenScrollIndex];
        m_pScrollBar = GameObject.Find("prf_ItemUpgInvenPop(Clone)/Window/ScrollBar").GetComponent<UIScrollBar>();

        
        SettingTable();


        transform.GetComponent<UIScrollView>().verticalScrollBar = m_pScrollBar;
        StartCoroutine(ScrollPosInit(0.3f));
    }

    public void Enter()
    {

    }

    void Update()
    {
      
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


    IEnumerator ScrollPosInit(float fWaitTime)
    {
        yield return new WaitForSeconds(fWaitTime);

        m_stMyPos = gameObject.transform.localPosition;
        m_stMyPos.x = 0f;
        m_stMyPos.y = 0f;
        m_stMyPos.z = 0f;
        gameObject.transform.localPosition = m_stMyPos;

    }

    public void SettingTable()
    {
        JAPrefabMng.I.CreateLoading();
        for (int i = 0; i < JAManager.I.myData.manage.m_stInven.m_nDBInvenScrollIndex; i++)
        {

            m_pItemScroll_Obj[i] = JAPrefabMng.I.CreatePrefab("JAGrid", E_JA_RESOURCELOAD.E_JIAN, "prf_ItemUpgInvenTable", -1f, ("prf_ItemUpgTable" + i));
            m_pItemScroll_Src[i] = m_pItemScroll_Obj[i].GetComponent<JAItemUpgScrollTable>();
            m_pItemScroll_Src[i].m_nIndex = i;

        }

        //transform.GetComponent<UIScrollView>().ResetPosition();
        transform.GetComponentInChildren<UIGrid>().Reposition();
        JAPrefabMng.I.DestroyLoading();

    }

    public void OnDestroy()
    {
        for (int i = 0; i < JAManager.I.myData.manage.m_stInven.m_nDBInvenScrollIndex; i++)
        {
            Destroy(m_pItemScroll_Obj[i]);
            Destroy(m_pItemScroll_Src[i]);
            JAPrefabMng.I.DestroyPrefab(m_pItemScroll_Obj[i]);
        }

    }

}
