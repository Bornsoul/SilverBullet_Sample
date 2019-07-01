using UnityEngine;
using System.Collections;

public class JAInGameTextSkipBtnScript : MonoBehaviour
{

    void Start()
    {

    }

    void Update()
    {

    }

    void OnClick()
    {

// 140613jtj {
		if( JAManager.I == null )
		{	return ;	}
// } 140613jtj ;

        JAManager.I.m_pShooterRoot.isAutoPlaySkipping = true;
    }
}
