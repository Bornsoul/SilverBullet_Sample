using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class JAManager : MonoBehaviour
{
    private static JAManager m_pInstance = null;
    public static JAManager I
    {
        get
        {
            if (null == m_pInstance)
            {
                m_pInstance = FindObjectOfType(typeof(JAManager)) as JAManager;
                if (null == m_pInstance)
                {

                    return null;
                }
            }
            return m_pInstance;
        }
    }
   
    public ManagerData myData;
    private string _data;

    public shooterRoot m_pShooterRoot = null;
    public Mob m_pMob = null;    
    public NGUIPanel m_pItemRockOnPanel = null;
    public NGUIPanel m_pOpeningNemo = null;
    public StringStore[] m_pStringStore_Src = null;
    public JATitleMenuBMainScript m_pTitleBMenu = null;
	public JACharacterSelectScript m_pCharSelectScript = null;
    public E_JA_MYITEM_SLOT m_eMyItemSlot = E_JA_MYITEM_SLOT.E_WEAPON_ONE;


    //! 현재 사용 안함
    //public Vector3 m_stClipingPosVec = Vector2.zero;
    //public Vector3 m_stClipingSizeVec = Vector2.zero;

    #region ### 팝업에서 사용 ###
    
    public int m_nSelectPopTextIndex = 0;
    
    //! 타이틀 B
    public int m_nTitleBStageIndex = 0; //!< 스테이지 버튼 인덱스 
    public int m_nMyCharacter = 0; //!< 자신의 캐릭터 인덱스

    //! PVP메뉴
    public int m_nPvpFriendSelectInfo = 0;
    public int m_nPvpFriendTableSelect = 0;
    public int m_nPvpEnemyRand1 = 0;
    public int m_nPvpEnemyRand2 = 0;

    //! 인벤 강화창
    public bool m_bInvenGoUpg = false;
    public bool m_bUpgZone = false;
    public int m_nUseItemBoxCnt = 106;
    public int m_nUseItemSelCnt = 0;
    public bool[] m_bItemUse = new bool[4];
    
    #endregion

    public bool m_bUseCam = true;
    public bool m_bCamMove = true;
    public bool m_bInvenState = false;
    private string m_sXMLDataName = "2014_11_15_0.xml";

    void Awake()
    {
        //! XML 데이터 구조체
        myData = new ManagerData();
       
        LoadData();
       //! 새로운 사용자 체크
        StartCoroutine(XmlFirstTimeCheck(0.2f));
    }

    void Start()
    {
		m_pShooterRoot = GameObject.Find( "thePlayerRoot(body)" ).GetComponent<shooterRoot>();
		m_pItemRockOnPanel = GameObject.Find( "Panel_TitleMenuA/Left/ItemRockOn" ).GetComponent<NGUIPanel>();
		m_pOpeningNemo = GameObject.Find("Panel_TitleMenuA/Left/OpeningNemo").GetComponent<NGUIPanel>();
		m_pTitleBMenu = GameObject.Find( "Panel_TitleMenuB" ).GetComponent<JATitleMenuBMainScript>();
		m_pCharSelectScript = GameObject.Find( "CharacterSelect" ).GetComponent<JACharacterSelectScript>();


// 140615jtj {
		if( PlayerPrefs.HasKey( "lastStageIndex" ) == false )
		{	PlayerPrefs.SetInt( "lastStageIndex", 0 ) ;	}
        m_nTitleBStageIndex = PlayerPrefs.GetInt("lastStageIndex");
        // } 140615jtj ;

        for (int i = 0; i < m_bItemUse.Length; i++)
            m_bItemUse[i] = false;

        m_bInvenState = false;

        LoadData();
        //! 메뉴 UI생성
        StartCoroutine(DelayUI(5f));

        //! PVP 대전에서 친구이름과 적이름을 랜덤으로 뽑아줍니다. 
        m_nPvpFriendSelectInfo = NGUITools.RandomRange(2, 17);
        m_nPvpEnemyRand1 = NGUITools.RandomRange(0, 25);
        m_nPvpEnemyRand2 = NGUITools.RandomRange(0, 25);

        SaveData();

        //! 소환진 시간을위해.. 
        //SetManaTime();

        DontDestroyOnLoad(this);
    }

    void Update()
    {
        //! 게임중이 아닐때 ESC 누르면 게임종료 팝업창이 뜹니다.
       
    }

    /// <summary>
    /// 메뉴에 기본 배치 UI를 띄웁니다.
    /// </summary>
    /// <param name="fWaitTIme"></param>
    /// <returns></returns>
    IEnumerator DelayUI(float fWaitTIme)
    {
        yield return new WaitForSeconds(fWaitTIme);
        JAPrefabMng.I.SetMainUISetting();
    }

    /// <summary>
    /// 첫 게임 구동인지 아닌지 확인합니다.
    /// 첫구동이면 닉네임 설정을 요구합니다.
    /// </summary>
    /// <param name="fWaitTime"></param>
    /// <returns></returns>
    IEnumerator XmlFirstTimeCheck(float fWaitTime)
    {
        yield return new WaitForSeconds(fWaitTime);

		//myData.manage.m_bDataFristCheck = false ; // temp, 140717jtj ;

        if (myData.manage.m_bDataFristCheck == false)
        {
            //! 첫 구동일시
            //! 아이디 입력 팝업창 띄웁니다.
            JAPrefabMng.I.CreatePrefab("Pop_Offset", E_JA_RESOURCELOAD.E_JIAN, "prf_FirstLogin");
        }
        else
        {
            //! 한번이상 구동했을시
            myData.manage.m_bDataFristCheck = true;

            MngSave();
            SaveData();
           
        }
    }

    /// <summary>
    /// 소환진을 위한 시간 관련... 
    /// 이건 아직..
    /// </summary>
    public void SetManaTime()
    {
        System.DateTime stDateTime = System.DateTime.Now;

        LoadData();

        if (myData.manage.m_nDateTimeMinute == -1)
        {
            myData.manage.m_nDateTimeMinute = stDateTime.Minute;
        }

      
        int nLastMinute = myData.manage.m_nDateTimeMinute;

        int nSumOfMinute = myData.manage.m_nDateTimeMinute - nLastMinute;


        Debug.Log("NowTime = " + myData.manage.m_nDateTimeMinute);
        Debug.Log("LastTime = " + nLastMinute);
        Debug.Log("SumOfMinute = " + nSumOfMinute);

        SaveData();
    }

    /// <summary>
    /// TitleFadeA 오브젝트 페이드
    /// </summary>
    /// <param name="bInputable"></param>
    /// <param name="fAlpha"></param>
    /// <param name="fAlphaSpeed"></param>
    public void SetTitleFadeA(bool bInputable, float fAlpha, float fAlphaSpeed)
    {
        m_pShooterRoot.panel_TitleMenuA.isInputable = bInputable;
        m_pShooterRoot.panel_TitleMenuA.StartFadeNGUIPanel(fAlpha, fAlphaSpeed);
    }

    public bool GetInputTable()
    {
        return m_pShooterRoot.panel_TitleMenuA.isInputable;
    }

    public void SetLeftMenuFade(bool bShow, float fAlphaSpeed = 3f)
    {
        if (bShow == true)
        {
            m_pItemRockOnPanel.StartFadeNGUIPanel(1, fAlphaSpeed);
            m_pOpeningNemo.StartFadeNGUIPanel(1, fAlphaSpeed);
        }
        else
        {
            m_pItemRockOnPanel.StartFadeNGUIPanel(0, fAlphaSpeed);
            m_pOpeningNemo.StartFadeNGUIPanel(0, fAlphaSpeed);
        }
    }
   
    /// <summary>
    /// 숫자를 출력할때 콤바가 같이 출력되게 합니다.
    /// 예:) 1000 -> 1,000
    /// </summary>
    /// <param name="nStartNum"></param>
    /// <returns></returns>
    public string GetIntNumberString(int nStartNum)
    {
        StringBuilder sTotalString = new StringBuilder();
        int nTotal = 0;
        int nLength = 0;
        nTotal = nStartNum;
        nLength = nTotal.ToString().Length;
        for (int i = 0; i < nLength; i++)
        {
            sTotalString.Insert(0, "0");
            if (i % 3 == 0)
                sTotalString.Insert(0, ",");
        }

        return nTotal.ToString(sTotalString.ToString());
    }


    /// <summary>
    /// stringbuilder 로그 출력
    /// </summary>
    /// <param name="sString"></param>
    public void GetLogLong(params string[] sString)
    {
        StringBuilder sStringBD = new StringBuilder();
        for (int i = 0; i < sString.Length; i++)
            sStringBD.Append(sString[i]);

        Debug.Log(sStringBD.ToString());
    }

    /// <summary>
    /// stringbuilder 문장 반환
    /// </summary>
    /// <param name="sString"></param>
    /// <returns></returns>
    public string GetStringLong(params string[] sString)
    {
        StringBuilder sStringBD = new StringBuilder();
        for (int i = 0; i < sString.Length; i++)
            sStringBD.Append(sString[i]);

        return sStringBD.ToString();
    }


    #region ### XML 함수 ###
    /// <summary>
    /// XML 저장 함수
    /// </summary>
    public void SaveData()
    {
        _data = GameStateXML.SerializeObject(myData, "ManagerData");
        GameStateXML.CreateXML(m_sXMLDataName, _data);
    }

    /// <summary>
    ///  XML 불러오는 함수
    /// </summary>
    public void LoadData()
    {
        LoadDBData();

        _data = GameStateXML.LoadXML(m_sXMLDataName);

        if (_data.ToString() != "")
        {
            myData = (ManagerData)GameStateXML.DeserializeObject(_data, "ManagerData");
        }
    }

    public void LoadDBData()
    {
        myData.manage.m_stInven.m_stDBInven = new JADBInvenScript[120];
        for (int i = 0; i < myData.manage.m_stInven.m_stDBInven.Length; i++)
        {
            myData.manage.m_stInven.m_stDBInven[i] = new JADBInvenScript();
        }

    }

    /// <summary>
    /// XML 배열 초기화
    /// </summary>
    public void MngSave()
    {

        myData.manage.m_stGame.m_bSoliderChatCheck = new bool[m_pStringStore_Src[1].strings.Length];


    }

    /// <summary>
    /// XML 데이터 초기화
    /// </summary>
    public void XMLDataInit()
    {
        myData.manage.m_nDateTimeMinute = -1;
        myData.SetMyPvPInfo("", 1, 42521, 25, 4, 3);
        myData.SetMyInfo(999999, 512, 2014, 4, 10, 0);

        myData.SetPlayerStat(1, 0, 200, 0, 0, 500, 10, 0, 0, 5);

        for (int i = 0; i < m_pStringStore_Src[1].strings.Length; i++)
            myData.manage.m_stGame.m_bSoliderChatCheck[i] = false;

        myData.manage.m_stInven.m_nDBInvenCnt = 0;
        myData.manage.m_stInven.m_nDBInvenScrollIndex = 20;
        JADBManager.I.SetAddInven_Item(1, 0, 20, 00, 048, 1, 0, 1, true);
        JADBManager.I.SetAddInven_Item(1, 0, 00, 09, 022, 1, 0, 1, true);

        JADBManager.I.SetAddInven_Item(1, 0, 00, 00, 700, 1, 0, 1, true);
        JADBManager.I.SetAddInven_Item(1, 0, 00, 00, 800, 5, 0, 1, true);
        JADBManager.I.SetAddInven_Item(1, 0, 00, 00, 900, 10, 0, 1, true);

        JADBManager.I.SetAddInven_UseItem(3, 0, 0010, 101, 1, 0, true, false);
		//JADBManager.I.SetAddInven_UseItem(3, 0, 0050, 100, 1, 0, true, false);
        //JADBManager.I.SetAddInven_UseItem(3, 0, 0050, 101, 1, 0, true, false);
        //JADBManager.I.SetAddInven_UseItem(3, 0, 0050, 102, 1, 0, true, false);
        //JADBManager.I.SetAddInven_UseItem(3, 0, 0050, 103, 1, 0, true, false);

        //JADBManager.I.SetAddInven_UseItem(3, 0, 0050, 200, 1, 0, true, false);
        //JADBManager.I.SetAddInven_UseItem(3, 0, 0050, 201, 1, 0, true, false);
        //JADBManager.I.SetAddInven_UseItem(3, 0, 0050, 202, 1, 0, true, false);
        //JADBManager.I.SetAddInven_UseItem(3, 0, 0050, 203, 1, 0, true, false);
        
        //JADBManager.I.SetAddInven_UseItem(3, 0, 0050, 300, 1, 0, true, false);
        //JADBManager.I.SetAddInven_UseItem(3, 0, 0050, 301, 1, 0, true, false);
        //JADBManager.I.SetAddInven_UseItem(3, 0, 0050, 302, 1, 0, true, false);
        //JADBManager.I.SetAddInven_UseItem(3, 0, 0050, 303, 1, 0, true, false);


        JADBManager.I.SetMyItemSlot(E_JA_MYITEM_SLOT.E_WEAPON_ONE, 1, 0, 00, 00, 001, 1, 0, 1, true);
        JADBManager.I.SetMyItemSlot(E_JA_MYITEM_SLOT.E_WEAPON_TWO, 1, 0, 00, 00, 002, 1, 0, 1, true);
        JADBManager.I.SetMyItemSlot(E_JA_MYITEM_SLOT.E_DEFEND, 1, 0, 00, 24, 801, 1, 0, 1, true);
        JADBManager.I.SetMyItemSlot(E_JA_MYITEM_SLOT.E_SHOES, 1, 0, 00, 25, 801, 1, 0, 1, true);


    }

    #endregion

    

}