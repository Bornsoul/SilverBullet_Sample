using UnityEngine;
using System.Collections;

/// <summary>
/// 위치 : Panel_TitleMenuB / prf_TitleBScrollView / 
/// 목적 : 스테이지 버튼
/// </summary>
public class JATitleBStageBtn : MonoBehaviour
{
    public UISprite m_pStageBtnBoxSprite;
    public UILabel m_pStageNumLabel;
    public UILabel m_pStageNameLabel;
    public UILabel m_pStageStoryLabel = null;

    private Color m_stColor;

    public int m_nIndex = 0;

    void Start()
    {
        if ( m_pStageStoryLabel == null )
            m_pStageStoryLabel = GameObject.Find("TextLabelZone/Label").GetComponent<UILabel>();
         
    }

    public void Enter(int nIndex)
    {
        m_pStageNumLabel.text = "Stage " + (nIndex+1);
        m_pStageNameLabel.text = JAStruckMng.I.m_pStageInfo[nIndex].m_sStageName;
        //JAManager.I.m_pShooterRoot.SubMenuChangeButton(transform.gameObject);

        if (JAStruckMng.I.m_pStageInfo[nIndex].m_bOpen == false)
        {
            m_stColor.r = 0.8f;
            m_stColor.g = 0f;
            m_stColor.b = 0f;
            m_stColor.a = 0.4f;
            m_pStageBtnBoxSprite.color = m_stColor;
            m_pStageNameLabel.color = m_stColor;
            m_pStageNumLabel.color = m_stColor;
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    void Update()
    {

    }

    public void TypingLabel(int nTime)
    {
        if (m_pStageStoryLabel.gameObject.GetComponent<TypewriterEffect>() == null)
        {
            m_pStageStoryLabel.gameObject.AddComponent<TypewriterEffect>().charsPerSecond = nTime;
        }
        else
        {
            Destroy(m_pStageStoryLabel.gameObject.GetComponent<TypewriterEffect>());
            m_pStageStoryLabel.gameObject.AddComponent<TypewriterEffect>().charsPerSecond = nTime;
        }
    }

//z	void OnClick() // 140619jtj ;
	public void OnClick() // 140619jtj ;
    {
        JAManager.I.m_nTitleBStageIndex = m_nIndex;

        //! 스테이지 설명라벨
        m_pStageStoryLabel.text = JAStruckMng.I.m_pStageInfo[JAManager.I.m_nTitleBStageIndex].m_sStageStory;

        //! 타이핑 라벨
        TypingLabel(60);
        
        SpotSprite startButton = JAManager.I.m_pShooterRoot.panel_TitleMenuB.wigets[1].transform.Find("SpriteA").GetComponent<SpotSprite>();

        if (JAStruckMng.I.m_pStageInfo[JAManager.I.m_nTitleBStageIndex].m_bOpen == true)
        {
            JAManager.I.m_pTitleBMenu.SetAlpha(true);
            JAManager.I.m_pShooterRoot.SubMenuChangeButton(transform.gameObject);

// 140625jtj {
			JAManager.I.m_pShooterRoot.panel_TitleMenuB.wigets[2].transform.Find( "Label_stageNumber" ).GetComponent<UILabel>().text = "Stage " + ( m_nIndex + 1 ).ToString() ;
			JAManager.I.m_pShooterRoot.panel_TitleMenuB.wigets[2].transform.Find( "Label_stageTitle" ).GetComponent<UILabel>().text = JAStruckMng.I.m_pStageInfo[m_nIndex].m_sStageName ;
// } 140625jtj ;

            JAManager.I.m_pShooterRoot.SubMenuChangePreview(JAStruckMng.I.m_pStageInfo[JAManager.I.m_nTitleBStageIndex].m_sStagePreview);
			startButton.StartChangeFunction("ChangeColor", 1, 1, 1, 1, 0.4F, false);
            startButton.transform.parent.Find("Button").GetComponent<UIButtonMessage>().enabled = true;
        }
        else
        {
            JAManager.I.m_pTitleBMenu.SetAlpha(false);
            JAManager.I.m_pShooterRoot.SubMenuChangePreview("Sprite_preview_empty");
            
            startButton.StartChangeFunction("ChangeColor", 1, 1, 1, 0.4f, 0.4F, false);
            startButton.transform.parent.Find("Button").GetComponent<UIButtonMessage>().enabled = false;
        }

// 140619jtj {
/*      JAManager.I.m_pShooterRoot.SetStageLevel(JAStruckMng.I.m_pStageInfo[JAManager.I.m_nTitleBStageIndex].m_nStageLevel);

        if (JAStruckMng.I.m_pStageInfo[JAManager.I.m_nTitleBStageIndex].m_sPlayScene != string.Empty)
        JAManager.I.m_pShooterRoot.playSceneName = JAStruckMng.I.m_pStageInfo[JAManager.I.m_nTitleBStageIndex].m_sPlayScene;

        if (JAStruckMng.I.m_pStageInfo[JAManager.I.m_nTitleBStageIndex].m_sRandDungeon != string.Empty)
        RandomDungeon.setName = JAStruckMng.I.m_pStageInfo[JAManager.I.m_nTitleBStageIndex].m_sRandDungeon;

        if (JAStruckMng.I.m_pStageInfo[JAManager.I.m_nTitleBStageIndex].m_sLastStage != string.Empty)
        JAManager.I.m_pShooterRoot.lastStageName = JAStruckMng.I.m_pStageInfo[JAManager.I.m_nTitleBStageIndex].m_sLastStage;

        if (JAStruckMng.I.m_pStageInfo[JAManager.I.m_nTitleBStageIndex].m_sBossName != string.Empty)
*///z   BossRoom.setName = JAStruckMng.I.m_pStageInfo[JAManager.I.m_nTitleBStageIndex].m_sBossName;

		JAManager.I.m_pShooterRoot.SetMission( m_nIndex ) ;
// } 140619jtj ;
    }
}
