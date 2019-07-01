using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JAStruckMng : MonoBehaviour
{
    private GameObject m_pCurrentObject = null;
    private static JAStruckMng m_pInstance = null;

	public int ActiveStageCount;
    public static JAStruckMng I
    {
        get
        {
            if (null == m_pInstance)
            {
                m_pInstance = FindObjectOfType(typeof(JAStruckMng)) as JAStruckMng;
                if (null == m_pInstance)
                {

                    return null;
                }
            }
            return m_pInstance;
        }
    }

    [System.Serializable]
    public class MyInfo
    {
        public int m_nPrice;
        public int m_nBullet;
        public int m_nAmmo;
        public int m_nKarma;
        public string m_sSpawn;
        public string m_sFriend;

        public MyInfo()
        {
            m_nPrice = 0;
            m_nBullet = 0;
            m_nAmmo = 0;
            m_nKarma = 0;
            m_sSpawn = string.Empty;
            m_sFriend = string.Empty;
        }

        public void SetMyInfo(int nPrice, int nBullet, int nAmmo, int nKarma, string sSpawn, string sFriend)
        {
            m_nPrice = nPrice;
            m_nBullet = nBullet;
            m_nAmmo = nAmmo;
            m_nKarma = nKarma;
            m_sSpawn = sSpawn;
            m_sFriend = sFriend;
        }
    };

    [System.Serializable]
    public class PvpPlayerInfo
    {

        public string m_sName;
        public int m_nLevel;
        public int m_nRank;
        public int m_nPoint;
        public int m_nWin;
        public int m_nLoss;

        public PvpPlayerInfo()
        {
            m_sName = string.Empty;
            m_nLevel = 0;
            m_nRank = 0;
            m_nPoint = 0;
            m_nWin = 0;
            m_nLoss = 0;
        }

        public void SetPlayerDataInfo(string sName, int nLevel, int nRank, int nPoint, int nWin, int nLoss)
        {
            m_sName = sName;
            m_nLevel = nLevel;
            m_nRank = nRank;
            m_nPoint = nPoint;
            m_nWin = nWin;
            m_nLoss = nLoss;
        }
    };

    [System.Serializable]
    public class CharacterInfo
    {
        public int m_nIndex;
        public string m_sRankName;
        public string m_sCharName;
        public string m_sClass;

        public CharacterInfo()
        {
            m_nIndex = 0;
            m_sRankName = string.Empty;
            m_sCharName = string.Empty;
            m_sClass = string.Empty;
        }

        public void SetCharacterInfo(int nIndex, string sMainName, string sSubName, string sClass)
        {
            m_nIndex = nIndex;
            m_sRankName = sMainName;
            m_sCharName = sSubName;
            m_sClass = sClass;
        }
    };

    [System.Serializable]
    public class StageInfo
    {
        public string m_sStageName;
        public string m_sStagePreview;
        public string m_sPlayScene;
        //public string m_sLastStage;
        public string m_sBossName;
        public string m_sRandDungeon;
        public int m_nStageLevel;
        public bool m_bOpen;

        public string m_sStageStory;

        public StageInfo()
        {
            m_sStageName = string.Empty;
            m_sStagePreview = string.Empty;
            m_sPlayScene = string.Empty;
           // m_sLastStage = string.Empty;
            m_sBossName = string.Empty;
            m_sRandDungeon = string.Empty;
            m_nStageLevel = 0;
            m_bOpen = false;
        }


		public void SetStageInfo(string sStageName, string sStagePreview, string sPlayScene, int nStageLevel, bool bOpen)
		{
			m_sStageName = sStageName;
			m_sStagePreview = sStagePreview;
			m_sPlayScene = sPlayScene;
			//m_sLastStage = sLastStage;
			m_nStageLevel = nStageLevel;
			m_bOpen = bOpen;
		}
/*
		public void SetStageInfo(string sStageName, string sStagePreview, string sPlayScene, string sLastStage, int nStageLevel, bool bOpen)
        {
            m_sStageName = sStageName;
            m_sStagePreview = sStagePreview;
            m_sPlayScene = sPlayScene;
            m_sLastStage = sLastStage;
            m_nStageLevel = nStageLevel;
            m_bOpen = bOpen;
        }
*/
        public void SetStageStory(string sStory)
        {
            m_sStageStory = sStory;
        }
/*
        public void SetStageInfo(string sStageName, string sStagePreview, string sPlayScene, string sLastStage, string sRandDungeon, int nStageLevel, bool bOpen)
        {
            m_sStageName = sStageName;
            m_sStagePreview = sStagePreview;
            m_sPlayScene = sPlayScene;
            m_sLastStage = sLastStage;
            m_sRandDungeon = sRandDungeon;
            m_nStageLevel = nStageLevel;
            m_bOpen = bOpen;
        }
*/
        public void SetStageBossInfo(string sStageName, string sStagePreview, string sPlayScene, string sBossName, int nStageLevel, bool bOpen)
        {
            m_sStageName = sStageName;
            m_sStagePreview = sStagePreview;
            m_sPlayScene = sPlayScene;
            m_sBossName = sBossName;
            m_nStageLevel = nStageLevel;
            m_bOpen = bOpen;
        }
    };

    [HideInInspector]
    public CharacterInfo[] m_pCharacterInfo = new CharacterInfo[2];

    [HideInInspector]
	public StageInfo[] m_pStageInfo;// = new StageInfo[59];

    public PvpPlayerInfo[] m_pPvpFriendPlayerInfo = new PvpPlayerInfo[18];
    public PvpPlayerInfo[] m_pPvpEnemyPlayerInfo = new PvpPlayerInfo[26];
    
    void Start()
    {
		//string[] Title = theOne.oneThis.transform.Find( "TextDatas/StageNames" ).GetComponent<StringStore_Google_Stages>().Title;
        if (JAManager.I == null) return;
		ActiveStageCount = 0;
	//!	m_pStageInfo = new StageInfo[51];

        SetCharacterInfos();
		//SetStageInfos();

        JAManager.I.LoadData();
    }

    void Update()
    {

    }

	public string GetStageScene(int num)
	{
		return m_pStageInfo[num].m_sPlayScene;
	}
	public int GetStageLevel(int num)
	{
		return m_pStageInfo[num].m_nStageLevel;
	}
	
	/// <summary>
    /// 캐릭터들 정보
    /// </summary>
    public void SetCharacterInfos()
    {
        for ( int i = 0; i<m_pCharacterInfo.Length; i ++ )
           m_pCharacterInfo[i] = new CharacterInfo();

        m_pCharacterInfo[0].SetCharacterInfo(0, "상급 심문관", "칼리", "듀얼거너");
        m_pCharacterInfo[1].SetCharacterInfo(1, "상급 저격수", "빅스", "레인져 ");
    }

    /// <summary>
    /// 스테이지 정보
    /// </summary>
    public void SetStageInfos()
    {
		Debug.Log("SetStageInfos");
		string[] Title = theOne.oneThis.transform.Find( "TextDatas/StageNames" ).GetComponent<StringStore_Google_Stages>().Title;
		string[] Description = theOne.oneThis.transform.Find( "TextDatas/StageNames" ).GetComponent<StringStore_Google_Stages>().Description;
		string[] Scene = theOne.oneThis.transform.Find( "TextDatas/StageNames" ).GetComponent<StringStore_Google_Stages>().Scene;
		string[] Preview = theOne.oneThis.transform.Find( "TextDatas/StageNames" ).GetComponent<StringStore_Google_Stages>().Preview;
		bool[] isOn = theOne.oneThis.transform.Find( "TextDatas/StageNames" ).GetComponent<StringStore_Google_Stages>().isOn;


		m_pStageInfo = new StageInfo[Title.Length];//new StageInfo[59]; // temp ;
		ActiveStageCount = 0;
		for (int i = 0; i < Title.Length; i++)
		{
            m_pStageInfo[i] = new StageInfo();
			m_pStageInfo[i].SetStageInfo(Title[i], Preview[i], Scene[i], i, isOn[i]);
			m_pStageInfo[i].SetStageStory(Description[i]);
			if (isOn[i] == true)
				ActiveStageCount++;
		}

        //# - 스테이지 구성141008 기준 -
        //# 보스전 총      22
        //# 마이너보스    9
        //# 진보스         13

        //! Axis-21; 

		/*

		m_pStageInfo[0].SetStageInfo("Axis-21 사건", "Sprite_preview01", "Stage1-1", "Stage1-1", 1, true);
        m_pStageInfo[0].SetStageStory("대서양 해상의 연구시추 플랫폼 Axis-21에서 괴기사건이 의심되는 의문의 사고가 발생하였다. 바티칸의 마물 대응 조직인 네버라이트 기관은 상황의 조사를 위해 요원 \"칼리\"를 파견한다.");

		m_pStageInfo[1].SetStageInfo("수상한 아티팩트", "Sprite_preview01", "Stage1-2", "Stage1-1", 2, true);
        m_pStageInfo[1].SetStageStory("Axis-21에서 발견된 수수께끼의 아티팩트는 뭔가 고대의 마물이 봉인된 고대 유물이었다. 아티팩트의 봉인된 존재는 스스로 풀려나기 위해 기괴한 파동을 내뿜으며 좀비들을 불러들인다.");

		m_pStageInfo[2].SetStageInfo("심연의 고대신", "Sprite_preview01", "Stage1-3", "Stage1-2", 3, true);
        m_pStageInfo[2].SetStageStory("불완전한 상태나마 마물은 봉인에서 풀려난다. 고대의 마물 리바이어선은 칼리를 공격해온다.");

        //! 부두;
		// BBR 14.11.16. Preview를 4,4,4,5,5,5 로 재배분
		m_pStageInfo[3].SetStageInfo("사라진 약품상자", "Sprite_preview04", "Harbor1", "Stage1-3", 4, true);
        m_pStageInfo[3].SetStageStory("Axis-21에서 뭔가 연구/수송된 흔적을 추적하여 에이전트 칼리는 해안가의 항구에 도착한다. 갱단 아지트가 된 버려진 컨테이너 야적장에에서 칼리는 운송 상자의 흔적을 추적하는데..");

		m_pStageInfo[4].SetStageInfo("야옹이의 부름", "Sprite_preview04", "Harbor2", "Harbor1", 5, true);
        m_pStageInfo[4].SetStageStory("목표 상자 안의 약품 앰플은 모두 비어있었다. 더 이상 추적할만한 단서가 부족한 상황에서 부서진 앰플을 물고 온 검은 고양이를 만난다. 양이는 자신을 따라오라는 듯 부르는데..");

		m_pStageInfo[5].SetStageInfo("불사의 영약", "Sprite_preview04", "Harbor3", "Harbor2", 6, true);
        m_pStageInfo[5].SetStageStory("넓은 야적장에 좀비브레이크가 번져나가고 상황이 급변하여 대형 사건으로 변해간다. 지원을 요청하며 좀비들을 막으러 가던 칼리는 거대한 마물이 으르렁대는 소리를 듣게 된다.");

		m_pStageInfo[6].SetStageInfo("고립된 생존자들", "Sprite_preview05", "Factory1", "Harbor3", 7, true);
        m_pStageInfo[6].SetStageStory("좀비 확산을 막기위해 전투를 계속하던 칼리는 창고에 생존자들이 모여있는 것을 발견한다. 창고 주변에 많은 좀비들이 몰려드는 상황에서 칼리는 모인 생존자들을 지키기로 결심한다.");

		m_pStageInfo[7].SetStageInfo("검을 든 소녀", "Sprite_preview05", "Factory2", "Factory1", 8, true);
        m_pStageInfo[7].SetStageStory("생존자들을 지키며 결사항전을 벌이던 창고에 어느 소녀가 뛰어들어 좀비와 생존자 구분없이 모두를 베어버리기 시작한다. 분노한 칼리는 그녀를 막아서는데..");

		m_pStageInfo[8].SetStageInfo("수수께끼의 기동병기", "Sprite_preview05", "Factory3", "Factory2", 9, true);
        m_pStageInfo[8].SetStageStory("악몽과도 같은 좀비들의 새벽이 끝나가던 때, 좀비들과 생존자들의 모두를 제거하기 위하여 누군가에 의해 자율형 결전병기가 투입된다. 이 사건을 일으킨 배후는 과연 누구인가?");

		//! 제 1연구소;
		m_pStageInfo[9].SetStageInfo("잊혀진 과거의 유산", "Sprite_preview02", "Lab_001", "Factory3", 10, true);
		m_pStageInfo[9].SetStageStory("50년전에 어떠한 사고로 해산되었다고 알려진 글리모어 재단의 흔적을 쫓아서 칼리는 폐허가 된 글리모어의 옛 연구소에 들어선다. 버려진 연구소에서 칼리가 만나게 된 사실은..");

		m_pStageInfo[10].SetStageInfo("금단의 지하시설", "Sprite_preview02", "Lab_002", "Lab_001", 11, true);
		m_pStageInfo[10].SetStageStory("글리모어 중앙 연구소가 살아있는 것을 확인한 칼리는 시설 깊숙히 잠입하지만 본부와의 통신이 두절된다. 본부와 통신을 위해 유선 터미널을 찾아나선 칼리는..");

		m_pStageInfo[11].SetStageInfo("솔로몬의 기둥", "Sprite_preview_MinorBoss", "Lab_003", "Lab_002", 12, true);
		m_pStageInfo[11].SetStageStory("B-5구역으로 이동하던 칼리는 수상한 구조의 방에 도달하게 된다. 이때 다시 한번 연구소 내에서 의문의 폭발이 일어나며 잠시 본부와의 통신이 재개되는데..");

		//m_pStageInfo[10].SetStageInfo("금단의 지하시설", "Sprite_preview_Kenny", "Stage1-1", "Stage1", 11, false);
		//m_pStageInfo[11].SetStageInfo("솔로몬의 기둥", "Sprite_preview_elimination", "Stage1-1", "Stage1", 12, false);
		m_pStageInfo[12].SetStageInfo("닥터 세스 P 무어", "Sprite_preview_Beast", "RandomDungeon", "Stage1", "Stage4-1", 13, false);
		m_pStageInfo[13].SetStageInfo("이계의 통로", "Sprite_preview_Kenny", "Stage1-1", "Stage1", 14, false);
		m_pStageInfo[14].SetStageInfo("전장의 망령", "Sprite_preview_elimination", "Stage1-1", "Stage1", 15, false);
		m_pStageInfo[15].SetStageInfo("최후의 방어선", "Sprite_preview_MinorBoss", "Stage1-1", "Stage1", 16, false);
		m_pStageInfo[16].SetStageInfo("벌레들의 소굴", "Sprite_preview_Kenny", "Stage1-1", "Stage1", 17, false);
		m_pStageInfo[17].SetStageInfo("광기의 소환실", "Sprite_preview_elimination", "Stage1-1", "Stage1", 18, false);
		m_pStageInfo[18].SetStageInfo("연구소의 설계도면", "Sprite_preview_Captain", "Stage1-1", "Stage1", 19, false);
		m_pStageInfo[19].SetStageInfo("제1동력로 셧다운", "Sprite_preview_Kenny", "Stage1-1", "Stage1", 20, false);
		m_pStageInfo[20].SetStageInfo("차원무한육면각체", "Sprite_preview_elimination", "Stage1-1", "Stage1", 21, false);

        //! 제 2연구소;
		m_pStageInfo[21].SetStageInfo("연구소 제2구역", "Sprite_preview_MinorBoss", "Stage1-1", "Stage1", 22, false);
		m_pStageInfo[22].SetStageInfo("손이 많이가는 남자", "Sprite_preview_Kenny", "Stage1-1", "Stage1", 23, false);
		m_pStageInfo[23].SetStageInfo("죽음의 그림자", "Sprite_preview_empty", "Stage1-1", "Stage1", 24, false);
		m_pStageInfo[24].SetStageInfo("인공 마물", "Sprite_preview01", "Stage1-1", "Stage1", 30, false);
		m_pStageInfo[25].SetStageInfo("풀려난 악몽", "Sprite_preview01", "Stage1-1", "Stage1", 31, false);
		m_pStageInfo[26].SetStageInfo("초토화 기동병기", "Sprite_preview01", "Stage1-1", "Stage1", 32, false);
		m_pStageInfo[27].SetStageInfo("장비를 정지합니다", "Sprite_preview01", "Stage1-1", "Stage1", 33, false);
		m_pStageInfo[28].SetStageInfo("어긋나는 인과", "Sprite_preview03", "BossRoom", "SetBossTeemoVayne", 34, false);
		m_pStageInfo[29].SetStageInfo("뒤틀린 주시자", "Sprite_preview01", "Stage1-1", "Stage1", 35, false);
		m_pStageInfo[30].SetStageInfo("동력로로 가는 길", "Sprite_preview01", "Stage1-1", "Stage1", 36, false);
		m_pStageInfo[31].SetStageInfo("제2동력로 셧다운", "Sprite_preview01", "Stage1-1", "Stage1", 37, false);
		m_pStageInfo[32].SetStageInfo("지옥불의 괴수", "Sprite_preview01", "Stage1-1", "Stage1", 38, false);

        //! 제 3연구소;
		m_pStageInfo[33].SetStageInfo("연구소 제3구역", "Sprite_preview01", "Stage1-1", "Stage1", 39, false);
		m_pStageInfo[34].SetStageInfo("죽음으로 가득찬 시설", "Sprite_preview01", "Stage1-1", "Stage1", 40, false);
		m_pStageInfo[35].SetStageInfo("미쳐버린 수호자", "Sprite_preview01", "Stage1-1", "Stage1", 41, false);
		m_pStageInfo[36].SetStageInfo("어째서 여기에?", "Sprite_preview01", "Stage1-1", "Stage1", 42, false);
		m_pStageInfo[37].SetStageInfo("좀비와 고양이", "Sprite_preview01", "Stage1-1", "Stage1", 43, false);
		m_pStageInfo[38].SetStageInfo("죽음을 먹는 자", "Sprite_preview01", "Stage1-1", "Stage1", 44, false);
		m_pStageInfo[39].SetStageInfo("구원요청", "Sprite_preview01", "Stage1-1", "Stage1", 45, false);
		m_pStageInfo[40].SetStageInfo("닥터 무어 구하기", "Sprite_preview01", "Stage1-1", "Stage1", 46, false);
		m_pStageInfo[41].SetStageInfo("프로젝트 님로드", "Sprite_preview01", "Stage1-1", "Stage1", 47, false);
		m_pStageInfo[42].SetStageInfo("당신은 누구지?", "Sprite_preview01", "Stage1-1", "Stage1", 48, false);
		m_pStageInfo[43].SetStageInfo("네피림", "Sprite_preview01", "Stage1-1", "Stage1", 49, false);

        //! 수직타출 통로;
		m_pStageInfo[44].SetStageBossInfo("프로메테우스", "Sprite_preview01", "Stage1-1", "Stage1", 50, false);

        //! 어사일럼 어비스;
        m_pStageInfo[45].SetStageInfo("지옥의 밑바닥", "Sprite_preview01", "Stage1-1", "Stage1", 51, false);
        m_pStageInfo[46].SetStageInfo("영원한 어둠", "Sprite_preview01", "Stage1-1", "Stage1", 52, false);
		m_pStageInfo[47].SetStageInfo("열리지 않는 문", "Sprite_preview01", "Stage1-1", "Stage1", 53, false);
		m_pStageInfo[48].SetStageInfo("빌어먹을 고양이", "Sprite_preview01", "Stage1-1", "Stage1", 54, false);
		m_pStageInfo[49].SetStageInfo("갇혀버린 영혼들", "Sprite_preview01", "Stage1-1", "Stage1", 55, false);
		m_pStageInfo[50].SetStageInfo("지옥의 사서", "Sprite_preview01", "Stage1-1", "Stage1", 56, false);
		m_pStageInfo[51].SetStageInfo("중첩되어 비틀린", "Sprite_preview01", "Stage1-1", "Stage1", 58, false);
		m_pStageInfo[52].SetStageInfo("윤회의 탈출구", "Sprite_preview01", "Stage1-1", "Stage1", 60, false);
		m_pStageInfo[53].SetStageInfo("심연의 문 앞에서", "Sprite_preview01", "Stage1-1", "Stage1", 70, false);
		m_pStageInfo[54].SetStageInfo("마도결전 기동병기", "Sprite_preview01", "Stage1-1", "Stage1", 70, false);

        //! 얼어붙은 심연;
		m_pStageInfo[55].SetStageInfo("죽은 자들의 바다", "Sprite_preview01", "Stage1-1", "Stage1", 70, false);
		m_pStageInfo[56].SetStageInfo("타천사 아사셀", "Sprite_preview01", "Stage1-1", "Stage1", 70, false);
		m_pStageInfo[57].SetStageInfo("타천사의 심장", "Sprite_preview01", "Stage1-1", "Stage1", 70, false);
		m_pStageInfo[58].SetStageInfo("악마포식자", "Sprite_preview01", "Stage1-1", "Stage1", 70, false);
		*/

    }


    

}
