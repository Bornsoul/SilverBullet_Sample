using UnityEngine;
using System.Collections;

public class JAPopupOK_DelItem : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnClick()
    {
        Debug.Log("Del");
        DeleteItem();
       
        JAPrefabMng.I.DestroyPrefab(transform.parent.transform.parent.transform.parent.name);
    }

    public void DeleteItem()
    {
        JAMyInvenMainScript pInvenMainScrc = GameObject.Find("prf_MyInven(Clone)").GetComponent<JAMyInvenMainScript>();
        pInvenMainScrc.Button_DelItem();

        JAManager.I.SaveData();
    }
}
