using UnityEngine;
using System.Collections;

/// <summary>
/// 2014_06_19
/// 헬프콜 메인 스크립트
/// </summary>
public class JAHelpCallMainScript : MonoBehaviour
{
    public JAHelpCallBar m_pHelpCallBar_Src;

    void Start()
    {
        m_pHelpCallBar_Src.Enter(0.3f);

    }

    void Update()
    {

    }
}
