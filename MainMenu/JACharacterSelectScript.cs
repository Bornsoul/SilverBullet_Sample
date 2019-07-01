using UnityEngine;
using System.Collections;

public class JACharacterSelectScript : MonoBehaviour 
{

	enum eState
	{
		E_STATE_NONE,
		E_STATE_LEFT,
		E_STATE_RIGHT,
		E_STATE_END,
	};
	
	public Transform m_stCamTransform = null; //!< 카메라 좌표
	public Transform m_stMoveTransform = null; //!< 캐릭터 좌표
	
	public GameObject[] m_pHeroObject = null;
	public Transform[] m_stHeroTransform = null; //!< 캐릭터들 간격 위치
	private string m_sName = string.Empty; //!< 캐릭터에 맞는 이름 넣어줌
	private eState m_eState = eState.E_STATE_NONE; //!< 캐릭터 방향 설정상태
	
	
	//private Vector3 m_stStartClick = Vector3.zero; //!< 제스쳐를 위한 터치 시작 지점
	

	void Start ()
	{
		
		//! 처음 캐릭터들의 위치 배정
		//! 좌표가 캐릭터 위치기준이라 일단 이렇게..
		for (int i = 0; i < m_stHeroTransform.Length; i++)
		{
			m_sName = "CharacterSelect/" + i + "_Char";
			m_pHeroObject[i] = GameObject.Find(m_sName).gameObject;
			m_stHeroTransform[i] = GameObject.Find(m_sName).GetComponent<Transform>();
		}
		
		m_stHeroTransform[1].transform.position = new Vector3(-3.278218f, 0f, -3.906826f);
		
		//-3.278218 , 0 , -3.906826
	}
	
	void Update()
	{
		//! 제스쳐 기능함수.. 방향에따라 캐릭터선택이 바뀝니다.
		if( m_eState == eState.E_STATE_NONE )
		{
		// 140625jtj {
		//	GestureEvent();
		//
		//	if( Input.GetKeyDown( KeyCode.LeftArrow ) )
		//	{	LeftBtn() ;	}
		//	else if ( Input.GetKeyDown(KeyCode.RightArrow))
		//!	{	RightBtn() ;	}
		// } 140625jtj ;
		}
	}

	public void SetState(bool bNone)
	{
		if( bNone == true )
		{
			m_eState = eState.E_STATE_NONE;
		}
		else
		{
			m_eState = eState.E_STATE_END;
		}
	}

	/// <summary>
	/// 제스쳐 이벤트 함수
	/// </summary>
	public void GestureEvent()
	{
		
//		if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
//		{
//			//m_stStartClick.x = Input.GetTouch(0).position.x;
//		}
		if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
		{
			Vector2 stTouchVec = Input.GetTouch(0).deltaPosition;

			//transform.Translate(-stTouchVec.x * 0.1f, -stTouchVec.y * 0.1f, 0);
			
			if (stTouchVec.x < -2f)
			{	LeftBtn();	}
			else if (stTouchVec.x > 2f)
			{	RightBtn();	}
		}
	}
	
	/// <summary>
	/// 왼쪽 이동
	/// </summary>
	public void LeftBtn()
	{
		if (m_eState == eState.E_STATE_NONE)
		{
			if (JAManager.I.m_nMyCharacter >= 1) return;
			JAManager.I.m_nMyCharacter++;
			theOne.oneThis.oneShooterRoot.currentSBPCNumber = JAManager.I.m_nMyCharacter ; // 140604jtj ;
			m_eState = eState.E_STATE_LEFT;

			// 140625jtj {
			TweenAlpha aTweenAlpha = theOne.oneThis.oneShooterRoot.panel_TitleMenuA.wigets[3].transform.Find( "Sprite" ).GetComponent<TweenAlpha>() ;
			TweenAlpha bTweenAlpha = theOne.oneThis.oneShooterRoot.panel_TitleMenuA.wigets[2].transform.Find( "Sprite" ).GetComponent<TweenAlpha>() ;
			theOne.oneThis.oneShooterRoot.SetTweenAlpha( aTweenAlpha, 0.5F, 1, 0.4F ) ;
			theOne.oneThis.oneShooterRoot.SetTweenAlpha( bTweenAlpha, 1, 0.5F, 0.4F ) ;
			// } 140625jtj ;
			
			NGUITools.SetActive(m_pHeroObject[JAManager.I.m_nMyCharacter], true);
			StartCoroutine(Test1(true));
			
			//StartCoroutine(Delay(eState.E_STATE_LEFT, 1f));
		}
	}
	
	/// <summary>
	/// 오른쪽 이동
	/// </summary>
	public void RightBtn()
	{
		if (m_eState == eState.E_STATE_NONE)
		{
			if (JAManager.I.m_nMyCharacter <= 0) return;
			JAManager.I.m_nMyCharacter--;
			theOne.oneThis.oneShooterRoot.currentSBPCNumber = JAManager.I.m_nMyCharacter ; // 140604jtj ;
			m_eState = eState.E_STATE_RIGHT;

			// 140625jtj {
			TweenAlpha aTweenAlpha = theOne.oneThis.oneShooterRoot.panel_TitleMenuA.wigets[2].transform.Find( "Sprite" ).GetComponent<TweenAlpha>() ;
			TweenAlpha bTweenAlpha = theOne.oneThis.oneShooterRoot.panel_TitleMenuA.wigets[3].transform.Find( "Sprite" ).GetComponent<TweenAlpha>() ;
			theOne.oneThis.oneShooterRoot.SetTweenAlpha( aTweenAlpha, 0.5F, 1, 0.4F ) ;
			theOne.oneThis.oneShooterRoot.SetTweenAlpha( bTweenAlpha, 1, 0.5F, 0.4F ) ;
			// } 140625jtj ;

			NGUITools.SetActive(m_pHeroObject[JAManager.I.m_nMyCharacter], true);
			StartCoroutine(Test1(false));
			
			//StartCoroutine(Delay(eState.E_STATE_RIGHT, 1f));
		}
	}
	
	/// <summary>
	/// 이동 지연 코루틴
	/// 1초 지정하면 1초움직이고 멈춤
	/// </summary>
	/// <param name="eState"></param>
	/// <param name="fWaitTime"></param>
	/// <returns></returns>
	IEnumerator Delay(eState eState, float fWaitTime)
	{
		NGUITools.SetActive(m_pHeroObject[JAManager.I.m_nMyCharacter], true);
		m_eState = eState;
		yield return new WaitForSeconds(fWaitTime);
		
		m_eState = eState.E_STATE_NONE;
		
	}
	
	
	/// <summary>
	/// 참고!!
	/// </summary>
	/// <returns></returns>
	private IEnumerator Test1(bool bLeft)
	{
		Vector3 destinationA = new Vector3( 0, 0, 0 ) ;
		Vector3 destinationB = new Vector3(3.36f, 0, 3.87f);
		
		Vector3 fromPosition = m_stMoveTransform.position ;
		Vector3 toPosition = Vector3.zero;
		if ( bLeft == true )
			toPosition = destinationA;
		else
			toPosition = destinationB;
		
		if (fromPosition == toPosition)
		{
			if (bLeft == true)
				toPosition = destinationB;
			else
				toPosition = destinationA;
		}
		
		float duration = 0.4F ;
		for( float f = 0 ; f < duration ; f += Time.deltaTime )
		{
			m_stMoveTransform.position = Vector3.Lerp(fromPosition, toPosition, f / duration ) ;
			yield return null ;
		}
		m_stMoveTransform.position = toPosition ;
		m_eState = eState.E_STATE_NONE;
	}

}
