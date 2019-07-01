using UnityEngine;
using System.Collections;

public class JASelectPopOkBtn : MonoBehaviour
{
    public JASelectPopMainScript m_pMainScript = null; 

    void Start()
    {

    }

    void Update()
    {

    }

    public void OnClick()
    {
        m_pMainScript.SetPopupStartAni(false);

    }
}
