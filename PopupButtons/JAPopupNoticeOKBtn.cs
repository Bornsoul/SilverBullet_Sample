using UnityEngine;
using System.Collections;

public class JAPopupNoticeOKBtn : MonoBehaviour
{

    void Start()
    {

    }

    void Update()
    {
     
    }

    void OnClick()
    {
        JAMyInvenMainScript pInvenMainScrc = GameObject.Find("prf_MyInven(Clone)").GetComponent<JAMyInvenMainScript>();
        pInvenMainScrc.Button_DelItem();
        JAPrefabMng.I.DestroyPrefab(transform.parent.transform.parent.transform.parent.name);
    }
}

