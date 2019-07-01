using UnityEngine;
using System.Collections;

public class JAPopupExit : MonoBehaviour
{
    
    void Start()
    {

    }

    void Update()
    {

    }

    public void ExitPopupFun()
    {
        JAPrefabMng.I.DestroyPrefabs();
       
    }

	public void ItemPopExit()
	{
		JAPrefabMng.I.DestroyPrefab( "prf_ItemInvenPop(Clone)" );
        JAPrefabMng.I.DestroyPrefab("prf_MyInven(Clone)");
	}
}
