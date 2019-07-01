using UnityEngine;
using System.Collections;

public class JASelectPopItemDelBtn : MonoBehaviour
{
    JAMyInvenMainScript m_pMainSrc = null;

    // Use this for initialization
    void Start()
    {
        m_pMainSrc = new JAMyInvenMainScript(); 
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnClick()
    {
        Debug.Log("Destroy Item OK~");
        JAItemUpgButtonsScript pUpgButtons = new JAItemUpgButtonsScript();
        JAItemUpg_3 pUpgBtn3 = new JAItemUpg_3();

        pUpgBtn3.DestroySystem(JADBManager.I.m_nInvenCurTableIndex);
        JAManager.I.myData.manage.m_stInven.m_stDBInven[JADBManager.I.m_nInvenCurTableIndex].SetInven_DeleteData();
        JAManager.I.myData.manage.m_stInven.m_nDBInvenScrollIndex--;
        JAManager.I.myData.manage.m_stInven.m_nDBInvenScrollIndex++;
        pUpgButtons.Button_Back();

        JAManager.I.SaveData();
    }
}
