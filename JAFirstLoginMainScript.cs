using UnityEngine;
using System.Collections;

public class JAFirstLoginMainScript : MonoBehaviour
{

    public Animation m_pPopAni = null;
    public UIInput m_pInputText = null;
    private string m_sGetName = string.Empty;

    void Start()
    {
        m_pPopAni.Play("prf_FirstLoginStart");

        StartCoroutine(StartPop(0.5f));
    }

    void Update()
    {
#if UNITY_EDITOR
        if (m_pInputText.value != string.Empty)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Button_OK();
            }
        }
#endif
    }

    public void Button_OK()
    {
        if (m_pInputText.value == string.Empty)
        {
            //m_pInputText.value = "아이디를 입력하세요!";

            m_pInputText.value = "Developer"; //! 현재 임시로 빨리 넘어가기 위해 쓰고있습니다.

        }
        else
        {

            Time.timeScale = 1;

            m_sGetName = m_pInputText.value;

            m_pPopAni.Play("prf_FirstLoginEnd");
            StartCoroutine(DestroyPop(0.5f));
        }
    }

    public void Button_Cancel()
    {
        Application.Quit();
    }

    IEnumerator StartPop(float fWaitTime)
    {
        yield return new WaitForSeconds(fWaitTime);

        Time.timeScale = 0;
    }

    IEnumerator DestroyPop(float fWaitTime)
    {
        yield return new WaitForSeconds(fWaitTime);

        JAManager.I.MngSave();
        JAManager.I.XMLDataInit();
        JAManager.I.myData.manage.m_stMyInfo.m_sName = m_sGetName;
        JAManager.I.myData.manage.m_bDataFristCheck = true;

        JAManager.I.SaveData();

        JAPrefabMng.I.DestroyPrefab("prf_FirstLogin(Clone)");
    }
}
