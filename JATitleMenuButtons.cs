using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JATitleMenuButtons : MonoBehaviour 
{

	public JACreditBox m_pCreditSrc;
   

	void Start() 
	{

	}

	void Update() 
	{
	
	}

	public void CreditButton()
	{

		
	}


	public void CreatePopupButton()
	{
        JAPrefabMng.I.CreatePrefab("Popup_I", E_JA_RESOURCELOAD.E_JIAN, "prf_SelectPop");        
	}
   
}
