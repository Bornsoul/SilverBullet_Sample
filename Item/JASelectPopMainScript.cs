using UnityEngine;
using System.Collections;

public class JASelectPopMainScript : MonoBehaviour
{
    //public Animator m_pPopAni = null;
    public Animation m_pPopAni = null;
    public StringStore m_pStringStore_Src = null;

    public UILabel m_pTitleLabel = null;
    public UILabel m_pMainLabel = null;

    

    void Start()
    {

        SetPopupStartAni(true);
        m_pStringStore_Src = GameObject.Find("JAImshiMng").transform.FindChild("JATextData").transform.FindChild("JATextXML").GetComponent<StringStore>();
        
        m_pTitleLabel.text = m_pStringStore_Src.strings[0];
        m_pMainLabel.text = m_pStringStore_Src.strings[JAManager.I.m_nSelectPopTextIndex];

    }

    void Update()
    {

    }

    public void SetPopupStartAni(bool bStart)
    {
        if (bStart != false)
        {
            m_pPopAni.Play("prf_SelectPopStart");
        }
        else
        {
            m_pPopAni.Play("prf_SelectPopEnd");
        }
    }

}
