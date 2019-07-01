using UnityEngine;
using System.Collections;

/// <summary>
/// 무기 경험치 관리
/// </summary>
public class JAMyInvenLevelExpScript : MonoBehaviour
{
    public UIProgressBar m_pExpSlider = null;
    public UILabel m_pLevelExp_Label = null;


    public void Enter()
    {
        //JADBManager.I.m_fExpVirtualValue = JADBManager.I.GetLevelExpValue();
    }

    public void ItemExpUpdate(int nSelectNum)
    {
        if (JADBManager.I.m_fExpValue < 0)
        {
            JADBManager.I.m_fExpValue = 0;
            return;
        }
        if (JAManager.I.myData.manage.m_stInven.m_stDBInven[nSelectNum].m_nLevel < JADBManager.I.GetInvenItemMaxLevel(JAManager.I.myData.manage.m_stInven.m_stDBInven[nSelectNum].m_nItemName))
        {
            switch (JAManager.I.myData.manage.m_stInven.m_stDBInven[nSelectNum].m_nBigName)
            {
                case 1:
                    m_pLevelExp_Label.text = JAManager.I.GetStringLong("경험치 ",
                        JAManager.I.myData.manage.m_stInven.m_stDBInven[nSelectNum].m_fLevelExp.ToString(),
                        " / ", JADBManager.I.GetLevelExp(nSelectNum, JAManager.I.myData.manage.m_stInven.m_stDBInven[nSelectNum].m_nLevel).ToString());
                    break;
                case 3:
                    m_pLevelExp_Label.text = " ";
                    JADBManager.I.m_fExpValue = 0f;
                    m_pExpSlider.value = 0f;
                    break;
                default:
                    JADBManager.I.m_fExpValue = 0f;
                    m_pExpSlider.value = 0f;
                    break;
            }
        }
        else
        {
            m_pLevelExp_Label.text = "최고 레벨 입니다";
            JADBManager.I.m_fExpValue = 2f;
        }

        JADBManager.I.m_fExpValue = Mathf.SmoothStep(JADBManager.I.m_fExpValue, JADBManager.I.GetLevelExpValue(nSelectNum), 0.2f);
        m_pExpSlider.value = JADBManager.I.m_fExpValue;
    }

    public void MyExpUpdate(int nSelectNum)
    {
        if (JADBManager.I.m_fExpValue < 0)
        {
            JADBManager.I.m_fExpValue = 0;
            return;
        }
        if (JAManager.I.myData.manage.m_stInven.m_stDBInven[nSelectNum].m_nLevel < JADBManager.I.GetInvenItemMaxLevel(JAManager.I.myData.manage.m_stInven.m_stDBInven[nSelectNum].m_nItemName))
        {
            
            m_pLevelExp_Label.text = JAManager.I.GetStringLong("경험치 ",
                JAManager.I.myData.manage.m_stInven.m_stDBInven[nSelectNum].m_fLevelExp.ToString(),
                " / ", JADBManager.I.GetLevelExp(nSelectNum, JAManager.I.myData.manage.m_stInven.m_stDBInven[nSelectNum].m_nLevel).ToString());
        }
        else
        {
            m_pLevelExp_Label.text = "최고 레벨 입니다";
            JADBManager.I.m_fExpValue = 2f;
        }
        JADBManager.I.m_fExpValue = Mathf.SmoothStep(JADBManager.I.m_fExpValue, JADBManager.I.GetLevelExpValue(nSelectNum), 0.2f);
        m_pExpSlider.value = JADBManager.I.m_fExpValue;
    }



}
