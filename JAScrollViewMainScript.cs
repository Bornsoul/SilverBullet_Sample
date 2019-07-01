using UnityEngine;
using System.Collections;

/// <summary>
/// 위치 : Panel_TitleMenuB /
/// 목적 : 스테이지 버튼 스크롤
/// </summary>
public class JAScrollViewMainScript : MonoBehaviour
{
    private Vector3 m_stMyPos = Vector3.zero;

    public GameObject m_pScrollTable_Obj = null;
    public JATitleBStageBtn m_pScrollTable_Src = null;

    void Start()
    {

        StartCoroutine(ScrollPosInit(0.1f));
        
    }

    /// <summary>
    /// 처음 시작시 스크롤 세팅
    /// </summary>
    /// <param name="fWaitTime"></param>
    /// <returns></returns>
    IEnumerator ScrollPosInit(float fWaitTime)
    {

// 140614jtj {
		transform.localPosition = GetComponent<UIPanel>().clipOffset * -1 ;
// } 140614jtj ;

		yield return new WaitForSeconds( fWaitTime );

// 140614jtj {
		//m_stMyPos = gameObject.transform.localPosition;
		//m_stMyPos.x = 0f;
		//m_stMyPos.y = 0f;
		//m_stMyPos.z = 0f;
		//gameObject.transform.localPosition = m_stMyPos;
// } 140614jtj ;

        SetTableSetting();
    }

    void Update()
    {
        
    }

    void OnDestroy()
    {

        Destroy(m_pScrollTable_Obj);

    }

    /// <summary>
    /// 스테이지 버튼을 생성합니다.
    /// </summary>
    public void SetTableSetting()
    {
        JAPrefabMng.I.CreateLoading();
        for (int i = 0; i < JAStruckMng.I.m_pStageInfo.Length; i++)
        {
            m_pScrollTable_Obj = JAPrefabMng.I.CreatePrefab("JAGridB", E_JA_RESOURCELOAD.E_JIAN, "prf_ButtonSet_stage", -1f, ("ButtonSet_stage" + (i + 1)));
            m_pScrollTable_Obj.transform.localScale = new Vector3(0.00055f, 0.00055f, 1f);
            m_pScrollTable_Src = m_pScrollTable_Obj.GetComponent<JATitleBStageBtn>();
            m_pScrollTable_Src.m_nIndex = i;
            m_pScrollTable_Src.Enter(i);
        }

        Invoke("SetJAScrollGridPositionOnceMore", 0.01F);
    }

    /// <summary>
    /// 스테이지 버튼들 정렬
    /// </summary>
    private void SetJAScrollGridPositionOnceMore()
    {
		Transform grid = transform.FindChild( "Stretch/JAGridB" ) ;

        grid.GetComponent<UIGrid>().Reposition();
        grid.localPosition = new Vector3(-0.24F, 0, 0);

        GetComponent<UIScrollView>().ResetPosition();

// 140622jtj {
		float height = Mathf.Clamp( JAManager.I.m_nTitleBStageIndex * 0.1F - 0.15F, 0, 5 ) ;
		grid.localPosition = new Vector3(-0.24F, height, 0);
		grid.GetChild( JAManager.I.m_nTitleBStageIndex ).GetComponent<JATitleBStageBtn>().OnClick() ;
// } 140622jtj ;

        JAPrefabMng.I.DestroyLoading();
    }
  
}
