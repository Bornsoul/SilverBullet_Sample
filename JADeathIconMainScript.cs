using UnityEngine;
using System.Collections;

public class JADeathIconMainScript : MonoBehaviour
{
    bool m_bActive = false;
    public Animation m_pAni = null;
    private Color m_stColor;
    private float m_fAlpha = 1f;

    void Start()
    {
        m_pAni.Play("prf_DeathIconStart");


        //m_stColor = renderer.material.color; BBR 2014.12.21 원래는 Transparent Diffuse 대응용
        StartCoroutine(DelayAlphaStart(1f));
    }

    private void OnEnable()
    {
       
    }


    void Update()
    {
        if (m_bActive == false) return;
        transform.localPosition = Vector3.up * (1f * Time.deltaTime);

        if (m_fAlpha < 0f)
        {
            m_bActive = false;
        }

		//m_fAlpha -= 1f * Time.deltaTime;BBR 2014.12.21 원래는 Transparent Diffuse 대응용
		//m_stColor.a = m_fAlpha; BBR 2014.12.21 원래는 Transparent Diffuse 대응용
		//renderer.material.color = m_stColor;BBR 2014.12.21 원래는 Transparent Diffuse 대응용
    }

    IEnumerator DelayAlphaStart(float fWaitTime)
    {
        yield return new WaitForSeconds(fWaitTime);

        m_bActive = true;
    }
}
