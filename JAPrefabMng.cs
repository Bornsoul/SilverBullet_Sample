using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JAPrefabMng : MonoBehaviour
{
    private GameObject m_pCurrentObject = null;
    private static JAPrefabMng m_pInstance = null;
    public static JAPrefabMng I
    {
        get
        {
            if (null == m_pInstance)
            {
                m_pInstance = FindObjectOfType(typeof(JAPrefabMng)) as JAPrefabMng;
                if (null == m_pInstance)
                {
					Debug.Log("Totally Crap!!");
                    return null;
                }
            }
            return m_pInstance;
        }
    }



    public List<GameObject> ResourceList = null;
    public Dictionary<string, GameObject> CreatePrefabList = null;

    public int m_nPopBtnState = 0;

    void Start()
    {
        ResourceList = new List<GameObject>();
        CreatePrefabList = new Dictionary<string, GameObject>();

        DontDestroyOnLoad(this);
    }

    public GameObject CreatePrefab(string sParentName, E_JA_RESOURCELOAD eResourceLoadPos, string sPrefabName, float fZ = -1f, string sTitleName = "", string sAddComponent = "")
    {
        GameObject ParentGame = GameObject.Find(sParentName);
        GameObject CreatePrefabGam = null;

        if (ParentGame)
        {
            if (bOverLabGameObject(sPrefabName, sTitleName))
            {
                return null;
            }

            List<GameObject> Resource = GetReousrceList(sPrefabName, eResourceLoadPos);

            if (Resource != null)
            {
                foreach (GameObject obj in Resource)
                {
                    if (obj.transform.name == sPrefabName)
                    {
                        CreatePrefabGam = Instantiate(obj) as GameObject;

                        if (sAddComponent != "")
                            CreatePrefabGam.AddComponent(sAddComponent);

                        if (sTitleName != "")
                            CreatePrefabGam.transform.name = sTitleName + "(Clone)";

                        CreatePrefabGam.transform.parent = ParentGame.transform;
                        CreatePrefabGam.transform.localPosition = new Vector3(0f, 0f, fZ);
                        CreatePrefabGam.transform.localScale = Vector3.one;

                        CreatePrefabList.Add(CreatePrefabGam.transform.name, CreatePrefabGam);

                        return CreatePrefabGam;
                    }
                }
            }
        }
        return null;
    }

    public GameObject CreatePrefab(string sParentName, E_JA_RESOURCELOAD eResourceLoadPos, string sPrefabName, Vector3 fPosVec, string sTitleName = "", string sAddComponent = "")
    {
        GameObject ParentGame = GameObject.Find(sParentName);
        GameObject CreatePrefabGam = null;

        if (ParentGame)
        {
            if (bOverLabGameObject(sPrefabName, sTitleName))
            {
                return null;
            }

            List<GameObject> Resource = GetReousrceList(sPrefabName, eResourceLoadPos);

            if (Resource != null)
            {
                foreach (GameObject obj in Resource)
                {
                    if (obj.transform.name == sPrefabName)
                    {
                        CreatePrefabGam = Instantiate(obj) as GameObject;

                        if (sAddComponent != "")
                            CreatePrefabGam.AddComponent(sAddComponent);

                        if (sTitleName != "")
                            CreatePrefabGam.transform.name = sTitleName + "(Clone)";

                        CreatePrefabGam.transform.parent = ParentGame.transform;
                        CreatePrefabGam.transform.localPosition = fPosVec;
                        CreatePrefabGam.transform.localScale = Vector3.one;

                        CreatePrefabList.Add(CreatePrefabGam.transform.name, CreatePrefabGam);

                        return CreatePrefabGam;
                    }
                }
            }
        }
        return null;
    }

    List<GameObject> GetReousrceList(string sPrefabName, E_JA_RESOURCELOAD eResourceLoadPos)
    {
        List<GameObject> Resource = null;

        switch (eResourceLoadPos)
        {
            case E_JA_RESOURCELOAD.E_COMMON:
                {
                    Resource = JAPrefabMng.I.ResourceList;
                    ResourceLoad("Common", sPrefabName, Resource);
                    return Resource;
                }
            case E_JA_RESOURCELOAD.E_JIAN:
                {
                    Resource = JAPrefabMng.I.ResourceList;
                    ResourceLoad("/UI/", sPrefabName, Resource);
                    return Resource;
                }
            default:
                {
                    return null;
                }
        }
    }

    void ResourceLoad(string sCurParentName, string sPrefabName, List<GameObject> Resource)
    {
        int nCnt = 0;

        foreach (GameObject obj in Resource)
        {

            if (obj.transform.name == sPrefabName)
                nCnt++;
        }

        if (nCnt == 0)
        {

            string sPath = "Prefabs" + sCurParentName + sPrefabName;

			GameObject ResourcePrefabGame = (GameObject)Resources.Load(sPath, typeof(GameObject));

            if (ResourcePrefabGame == null)
            {
				Debug.Log("ResourcePrefabGame is NULL " + sPath + " [ResourceLoad(..)/HPrefabMng.cs]");

                return;
            }

            ResourcePrefabGame.name = sPrefabName;

            if (ResourcePrefabGame != null)
                Resource.Add(ResourcePrefabGame);
        }
    }

    //List<GameObject> GetResourceList()
    //{
    //    switch (Application.loadedLevelName)
    //    {
    //        case "
    //    }
    //}

    public void SetMainUISetting()
    {
        //JAPrefabMng.I.CreatePrefab("Pop_Offset", E_JA_RESOURCELOAD.E_JIAN, "prf_BottomInfoPop");
        JAPrefabMng.I.CreatePrefab("Panel_TitleMenuA/Right", E_JA_RESOURCELOAD.E_JIAN, "prf_TitleMenuA");
//
//#if UNITY_EDITOR
//        JAPrefabMng.I.CreatePrefab("Pop_Offset", E_JA_RESOURCELOAD.E_JIAN, "prf_CharSelectBtn");
//#endif
    }

    public void CreateLoading(float fX = 0f, float fY =0f)
    {
        GameObject LoadingObj = null;
        GameObject LoadingImgObj = null;

        LoadingObj = CreatePrefab("Pop_Offset", E_JA_RESOURCELOAD.E_JIAN, "prf_Loading");

        LoadingImgObj= LoadingObj.transform.FindChild("Window").transform.FindChild("LoadImg").gameObject;
        LoadingImgObj.transform.localPosition = new Vector3(fX, fY, 0);
    }

    public void CreatePopup(string sTitleName, string sMainName, string sAddOkCoponent = "", string sAddCancelComponet = "", E_JA_POPUP_SETTING ePopupSetting = E_JA_POPUP_SETTING.E_POPUP_OK)
    {
        GameObject pPopupNoticeObj = null;
        GameObject pPopupOKBtnObj = null;
        GameObject pPopupCancelBtnObj = null;
        JAPopupNoticeMain pPopupNoticeSrc = null;

        string sPrefabName = "prfM_PopupNotice";
        JAPrefabMng.I.DestroyPrefab(sPrefabName + "(Clone)");

        pPopupNoticeObj = CreatePrefab("Pop_Message_Offset", E_JA_RESOURCELOAD.E_JIAN, "prfM_PopupNotice", -2);
        pPopupNoticeSrc = pPopupNoticeObj.GetComponent<JAPopupNoticeMain>();
        pPopupNoticeSrc.Enter(sTitleName, sMainName);

        pPopupOKBtnObj = pPopupNoticeSrc.m_pButtonOK_Gam;
        pPopupCancelBtnObj = pPopupNoticeSrc.m_pButtonCancel_Gam;

        switch (ePopupSetting)
        {
            case E_JA_POPUP_SETTING.E_POPUP_OK:
                 if (sAddOkCoponent != "")
                     pPopupOKBtnObj.AddComponent(sAddOkCoponent);
                 else
                     pPopupOKBtnObj.AddComponent("JAPopupNoticeCancelBtn");
                pPopupOKBtnObj.transform.localPosition = new Vector3(0f, 0f, -1f);
                NGUITools.SetActive(pPopupCancelBtnObj, false);                
                break;
            case E_JA_POPUP_SETTING.E_POPUP_CANCEL:
                 if (sAddCancelComponet != "")
                    pPopupCancelBtnObj.AddComponent(sAddCancelComponet);
                 else
                     pPopupCancelBtnObj.AddComponent("JAPopupNoticeCancelBtn");
                pPopupCancelBtnObj.transform.localPosition = new Vector3(0f, 0f, -1f);
                NGUITools.SetActive(pPopupOKBtnObj, false);   
                break;
            case E_JA_POPUP_SETTING.E_POPUP_OK_CANCEL:
                 if (sAddOkCoponent != "")
                     pPopupOKBtnObj.AddComponent(sAddOkCoponent);
                 else
                     pPopupOKBtnObj.AddComponent("JAPopupNoticeCancelBtn");
                pPopupOKBtnObj.transform.localPosition = new Vector3(pPopupOKBtnObj.transform.localPosition.x, 0f, -1f);
                if (sAddCancelComponet != "")
                    pPopupCancelBtnObj.AddComponent(sAddCancelComponet);
                else
                    pPopupCancelBtnObj.AddComponent("JAPopupNoticeCancelBtn");
                pPopupCancelBtnObj.transform.localPosition = new Vector3(pPopupCancelBtnObj.transform.localPosition.x, 0f, -1f);
                break;
        }
       
    }

    public void DestroyLoading()
    {
        DestroyPrefab("prf_Loading(Clone)");
    }

    public void DestroyPopupMessage()
    {
        DestroyPrefab("prfM_PopupNotice(Clone)");    
    }
    public void DestroyPrefab(string sName)
    {
        if (CreatePrefabList == null)
            return;

        if (CreatePrefabList.Count == 0)
            return;


        if (CreatePrefabList.ContainsKey(sName))
        {
            Destroy(CreatePrefabList[sName]);
            CreatePrefabList[sName] = null;
            CreatePrefabList.Remove(sName);
        }
    }

    public void DestroyPrefab(GameObject PrefabGame)
    {
        string sDeleteName = PrefabGame.name;

        if (CreatePrefabList == null)
            return;

        if (CreatePrefabList.Count == 0)
        {
            return;
        }

        if (CreatePrefabList.ContainsKey(sDeleteName))
        {
            Destroy(CreatePrefabList[sDeleteName]);
            CreatePrefabList[sDeleteName] = null;
            CreatePrefabList.Remove(sDeleteName);
        }
    }

    public void DestroyPrefabs()
    {
        if (CreatePrefabList == null)
            return;

        if (CreatePrefabList.Count == 0)
        {
            return;
        }

        foreach (GameObject obj in CreatePrefabList.Values)
        {
            if (obj != null)
            {

                Destroy(obj);

                continue;
            }
          
        }

        CreatePrefabList.Clear();

        System.GC.Collect();
        Resources.UnloadUnusedAssets();
    }

    bool bOverLabGameObject(string sPrefabName, string sTitleName)
    {
        string sTempStr = null;

        if (sTitleName == "")
            sTempStr = sPrefabName + "(Clone)";
        else
            sTempStr = sTitleName + "(Clone)";

        bool bTempGameFlag = false;

        bTempGameFlag = CreatePrefabList.ContainsKey(sTempStr);

        if (bTempGameFlag)
        {
            Debug.Log("이미 생성된 프리팹 ->" + sTempStr);
            return true;
        }
        else
            return false;
    }
}

