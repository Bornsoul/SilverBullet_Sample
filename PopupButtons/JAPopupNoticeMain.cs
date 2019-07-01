using UnityEngine;
using System.Collections;

/// <summary>
/// 팝업창 스크립트
/// JAPrefabMng 에서 만들고있습니다.
/// </summary>
public class JAPopupNoticeMain : MonoBehaviour
{

    public Animation m_pPopAnis = null;
    public UILabel m_pTitle_Label = null;
    public UILabel m_pMain_Label = null;

    public GameObject m_pButtonOK_Gam = null;
    public GameObject m_pButtonCancel_Gam = null;

    public void Enter(string sTitleName, string sMainName)
    {
        m_pPopAnis.Play("prf_FirstLoginStart");

        m_pTitle_Label.text = sTitleName;
        m_pMain_Label.text = sMainName;

    }

    void Update()
    {

    }

    //public void Button_OK()
    //{
    //    for (int i = 0; i < m_pPopAnis.Length; i++)
    //        m_pPopAnis[i].Play("prf_FirstLoginStart");


    //}

    //public void Button_Cancel()
    //{
    //    for (int i = 0; i < m_pPopAnis.Length; i++)
    //        m_pPopAnis[i].Play("prf_FirstLoginStart");


    //}
}
