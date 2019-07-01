using UnityEngine;
using System.Collections;

public class JAPopupNoticeCancelBtn : MonoBehaviour
{

    void Start()
    {

    }

    void Update()
    {

    }

    void OnClick()
    {
        if ( JAManager.I.m_pShooterRoot != null )
            JAManager.I.m_pShooterRoot.OutControl(false);
    
        JAPrefabMng.I.DestroyPrefab(transform.parent.transform.parent.transform.parent.name);
    }
}
