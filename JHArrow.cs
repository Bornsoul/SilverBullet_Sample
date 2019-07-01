using UnityEngine;
using System.Collections;
//BBR 14.11.19 Remake
public class JHArrow : MonoBehaviour {
	public JHWayPoint_Mng m_pMng = null;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(m_pMng.GetCurrPoint()==null) return;
		Vector3 direction = m_pMng.GetCurrPoint().position - transform.position ;
		//direction.y = 0.0f;
		direction.Normalize();
		Quaternion toRotation = Quaternion.LookRotation( direction ) ;
		transform.rotation = toRotation;//Quaternion.Lerp( transform.rotation, toRotation, Time.deltaTime * 10.0f ) ;
		float Distance = Vector3.Distance(transform.position, m_pMng.GetCurrPoint().position);
		//Vector3 moveV = new Vector3 (0.5F, 0.5F, Distance);
		//transform.localPosition.z = Distance/2;
		Vector3 sizeV = new Vector3 (1.0F, 1.0F, Distance);

		transform.localScale = sizeV;
		transform.position = theOne.oneThis.oneShooterRoot.transform.position;
	}
}
