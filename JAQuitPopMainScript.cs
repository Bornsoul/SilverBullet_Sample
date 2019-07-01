using UnityEngine;
using System.Collections;

public class JAQuitPopMainScript : MonoBehaviour
{
    public Animation m_pPopAni = null;

    void Start()
    {
        //SetPopupStartAni(true);
    }

    void OnEnable()
    {
        Time.timeScale = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            NoButton();
        }
    }

    public void SetPopupStartAni(bool bStart)
    {
        if (bStart != false)
        {
            //m_pPopAni.Play("prf_QuitPopStart");
        }
        else
        {
            //m_pPopAni.Play("prf_QuitPopEnd");
        }
    }

    public void YesButton()
    {
        Application.Quit();
    }

    public void NoButton()
    {
        SetPopupStartAni(false);
        Time.timeScale = 1;
        JAPrefabMng.I.DestroyPrefab("prf_QuitPop(Clone)");
    }

}
