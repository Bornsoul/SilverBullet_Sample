using UnityEngine;
using System.Collections;

public class JADBItemRandBox : MonoBehaviour
{
    float m_fItemRandom1 = 0;
    int m_nMBBigSelect = 0;
    int m_nMBSquareSelect = 0;


    //! 각 티어의 무기를 찾아 랜덤으로 하나 뽑습니다.
    public void GetWeaponRand(int nTier)
    {
        ArrayList nRandArr = new ArrayList();
        int nLastRand = 0;
        string sLastRand = string.Empty;
        for (int i = 0; i < JADBManager.I.m_stDBItem.Length; i++)
        {
            if (JADBManager.I.m_stDBItem[i].m_nBigName == 1)
            {
                if (JADBManager.I.m_stDBItem[i].m_nTier == nTier)
                {
                    Debug.Log(nTier + " Tier Weapon : " + i);
                    nRandArr.Add(i);
                    

                }
            }
        }
        nLastRand = (int)nRandArr[Random.Range(0, nRandArr.Count)];
        sLastRand = JADBManager.I.m_stDBItem[nLastRand].m_nID.ToString("000");
        JADBManager.I.SetAddInven_Item(1, 0, 00, 00, int.Parse(sLastRand), 1, 0, 1, true);
        Debug.Log("Tier = " + nLastRand + "Last Weapon = " + sLastRand);
        JAManager.I.SaveData();
    }

    //! 각 티어의 악마의 심장을 찾아 하나 뽑습니다.
    public void GetDemonSouls(int nTier)
    {
        int nLastRand = 0;
        string sLastRand = string.Empty;
        for (int i = 0; i < JADBManager.I.m_stDBItem.Length; i++)
        {
            if (JADBManager.I.m_stDBItem[i].m_nBigName == 3)
            {
                if (JADBManager.I.m_stDBItem[i].m_nID >= 100 && JADBManager.I.m_stDBItem[i].m_nID < 105)
                {
                    if (JADBManager.I.m_stDBItem[i].m_nTier == nTier)
                    {
                        nLastRand = i;
                        Debug.Log(nTier + " Tier DemonSoul : " + i);
                    }
                }
            }
        }
        sLastRand = JADBManager.I.m_stDBItem[nLastRand].m_nID.ToString("000");
        JADBManager.I.SetAddInven_UseItem(3, 0, 0001, int.Parse(sLastRand), 1, 0, true, false);
    }

    //! 각 티어의 총열을 찾아 하나 뽑습니다.
    public void GetGunLong(int nTier)
    {
        int nLastRand = 0;
        string sLastRand = string.Empty;
        for (int i = 0; i < JADBManager.I.m_stDBItem.Length; i++)
        {
            if (JADBManager.I.m_stDBItem[i].m_nBigName == 3)
            {
                if (JADBManager.I.m_stDBItem[i].m_nID >= 200 && JADBManager.I.m_stDBItem[i].m_nID < 205)
                {
                    if (JADBManager.I.m_stDBItem[i].m_nTier == nTier)
                    {
                        nLastRand = i;
                        Debug.Log(nTier + " Tier GunLong : " + i);
                    }
                }
            }
        }
        sLastRand = JADBManager.I.m_stDBItem[nLastRand].m_nID.ToString("000");
        JADBManager.I.SetAddInven_UseItem(3, 0, 0001, int.Parse(sLastRand), 1, 0, true, false);
    }

    //! 각 티어의 스프링을 찾아 하나 뽑습니다.
    public void GetGunSpring(int nTier)
    {
        int nLastRand = 0;
        string sLastRand = string.Empty;
        for (int i = 0; i < JADBManager.I.m_stDBItem.Length; i++)
        {
            if (JADBManager.I.m_stDBItem[i].m_nBigName == 3)
            {
                if (JADBManager.I.m_stDBItem[i].m_nID >= 300 && JADBManager.I.m_stDBItem[i].m_nID < 305)
                {
                    if (JADBManager.I.m_stDBItem[i].m_nTier == nTier)
                    {
                        nLastRand = i;
                        Debug.Log(nTier + " Tier Spring : " + i);
                    }
                }
            }
        }
        sLastRand = JADBManager.I.m_stDBItem[nLastRand].m_nID.ToString("000");
        JADBManager.I.SetAddInven_UseItem(3, 0, 0001, int.Parse(sLastRand), 1, 0, true, false);
    }

    //! 탄약 추가
    public void GetAmmo(int nAmmo)
    {
        JAManager.I.m_pShooterRoot.bulletsCount += nAmmo;
    }

    //! 수류탄 추가
    public void GetGrenade(int nGrenade)
    {
        JAManager.I.m_pShooterRoot.grenadesCount += nGrenade;
    }




    //! 넓적한 무기 상자.
    public void BigWeaponBox()
    {
        m_fItemRandom1 = Random.Range(0.0f, 100f);
        if (m_fItemRandom1 <= 15f)
        {
            Debug.Log("1티어 무기 : " + m_fItemRandom1);
            GetWeaponRand(1);
        }
        else if (m_fItemRandom1 >= 15f && m_fItemRandom1 < 19f)
        {
            Debug.Log("2티어 무기 : " + m_fItemRandom1);
            GetWeaponRand(2);
        }
        else if (m_fItemRandom1 >= 19f && m_fItemRandom1 < 19.2f)
        {
            Debug.Log("3티어 무기 : " + m_fItemRandom1);
            GetWeaponRand(3);
        }
        else if (m_fItemRandom1 >= 19.2f && m_fItemRandom1 < 49.2f)
        {
            Debug.Log("1티어 스프링: " + m_fItemRandom1);
            GetGunSpring(1);
        }
        else if (m_fItemRandom1 >= 49.2f && m_fItemRandom1 < 59.2f)
        {
            Debug.Log("2티어 스프링: " + m_fItemRandom1);
            GetGunSpring(2);
        }
        else if (m_fItemRandom1 >= 59.2f && m_fItemRandom1 < 59.6f)
        {
            Debug.Log("3티어 스프링: " + m_fItemRandom1);
            GetGunSpring(3);
        }
        else if (m_fItemRandom1 >= 59.6f && m_fItemRandom1 < 89.6f)
        {
            Debug.Log("1티어 총열: " + m_fItemRandom1);
            GetGunLong(1);
        }
        else if (m_fItemRandom1 >= 89.6f && m_fItemRandom1 < 99.6f)
        {
            Debug.Log("2티어 총열: " + m_fItemRandom1);
            GetGunLong(2);
        }
        else if (m_fItemRandom1 >= 99.6f && m_fItemRandom1 < 100f)
        {
            Debug.Log("3티어 총열: " + m_fItemRandom1);
            GetGunLong(3);
        }
    }

    //! 정사각형 상자.
    public void SquareBox()
    {
        m_fItemRandom1 = Random.Range(0.0f, 100f);

        if (m_fItemRandom1 <= 40f)
        {
            Debug.Log("탄약 20발 : " + m_fItemRandom1);
            GetAmmo(20);
        }
        else if (m_fItemRandom1 >= 40f && m_fItemRandom1 < 60f)
        {
            Debug.Log("탄약 50발 : " + m_fItemRandom1);
            GetAmmo(50);
        }
        else if (m_fItemRandom1 >= 60f && m_fItemRandom1 < 64f)
        {
            Debug.Log("탄약 100발 : " + m_fItemRandom1);
            GetAmmo(100);
        }
        else if (m_fItemRandom1 >= 64f && m_fItemRandom1 < 84f)
        {
            Debug.Log("수류탄 1개 : " + m_fItemRandom1);
            GetGrenade(1);
        }
        else if (m_fItemRandom1 >= 84f && m_fItemRandom1 < 92f)
        {
            Debug.Log("수류탄 2개 : " + m_fItemRandom1);
            GetGrenade(2);
        }
        else if (m_fItemRandom1 >= 92f && m_fItemRandom1 < 94f)
        {
            Debug.Log("수류탄 3개 : " + m_fItemRandom1);
            GetGrenade(3);
        }
        else if (m_fItemRandom1 >= 94f && m_fItemRandom1 < 99f)
        {
            Debug.Log("1티어 악마의 심장 : " + m_fItemRandom1);
            GetDemonSouls(1);
        }
        else if (m_fItemRandom1 >= 99f && m_fItemRandom1 < 100f)
        {
            Debug.Log("2티어 악마의 심장 : " + m_fItemRandom1);
            GetDemonSouls(2);
        }
    }

    //! 메이져 보스 보상 넓적한 상자
    public void MajorBossBigWeaponBox()
    {
        m_fItemRandom1 = Random.Range(0.0f, 100f);
        RandomStage pStage = new RandomStage();

        Debug.Log("StageLevel : " + pStage.GetStageLevel());
        switch (m_nMBBigSelect)
        {
            case 0:
                if (m_fItemRandom1 <= (pStage.GetStageLevel() * 0.2f))
                {
                    Debug.Log("5티어 무기 : " + m_fItemRandom1);
                    GetWeaponRand(5);
                }
                else
                {
                    m_nMBBigSelect++;
                    MajorBossBigWeaponBox();
                }
                break;
            case 1:
                if ( m_fItemRandom1 >= (pStage.GetStageLevel() * 0.2f) && m_fItemRandom1 < (pStage.GetStageLevel() * 0.5f))
                {
                   Debug.Log("4티어 무기 : " + m_fItemRandom1);
                   GetWeaponRand(4);
                }
                else
                {
                    m_nMBBigSelect++;
                    MajorBossBigWeaponBox();
                }
                break;
            case 2:
                if ( m_fItemRandom1 >= (pStage.GetStageLevel() * 0.5f) && m_fItemRandom1 < (pStage.GetStageLevel() * 1f))
                {
                   Debug.Log("3티어 무기 : " + m_fItemRandom1);
                   GetWeaponRand(3);
                }
                else
                {
                    Debug.Log("2티어 무기 획득 : " + m_fItemRandom1);
                    GetWeaponRand(2);
                    m_nMBBigSelect = 0;
                }
                break;
        }
    }

    //! 메이져 보스 보상 정사각형 상자
    public void MajorBossSquareBox()
    {
        m_fItemRandom1 = Random.Range(0.0f, 100f);
        RandomStage pStage = new RandomStage();
        
        Debug.Log("StageLevel : " + pStage.GetStageLevel());

        switch (m_nMBSquareSelect)
        {
            case 0:
                if (m_fItemRandom1 <= (pStage.GetStageLevel() * 0.2f))
                {
                    Debug.Log("4티어 악마의 심장 : " + m_fItemRandom1);
                    GetDemonSouls(4);
                }
                else
                {
                    m_nMBSquareSelect++;
                    MajorBossSquareBox();
                }
                break;
            case 1:
                if (m_fItemRandom1 >= (pStage.GetStageLevel() * 0.2f) && m_fItemRandom1 < (pStage.GetStageLevel() * 0.5f))
                {
                    Debug.Log("3티어 악마의 심장 : " + m_fItemRandom1);
                    GetDemonSouls(3);
                }
                else
                {
                    m_nMBSquareSelect++;
                    MajorBossSquareBox();
                }
                break;
            case 2:
                if (m_fItemRandom1 >= (pStage.GetStageLevel() * 0.5f) && m_fItemRandom1 < (pStage.GetStageLevel() * 1f))
                {
                    Debug.Log("2티어 악마의 심장  : " + m_fItemRandom1);
                    GetDemonSouls(2);
                }
                else
                {
                    Debug.Log("1티어 악마의 획득 : " + m_fItemRandom1);
                    GetDemonSouls(1);
                    m_nMBSquareSelect = 0;
                }
                break;
        }
    }

    //! 마이너 보스 / 필드보스 보상 넓적한 상자
    public void MinorBossBigWeaponBox()
    {
        m_fItemRandom1 = Random.Range(0.0f, 100f);
        RandomStage pStage = new RandomStage();

        Debug.Log("StageLevel : " + pStage.GetStageLevel());
        switch (m_nMBBigSelect)
        {
            case 0:
                if (m_fItemRandom1 <= (pStage.GetStageLevel() * 0.2f))
                {
                    Debug.Log("4티어 무기 : " + m_fItemRandom1);
                    GetWeaponRand(4);
                }
                else
                {
                    m_nMBBigSelect++;
                    MinorBossBigWeaponBox();
                }
                break;
            case 1:
                if (m_fItemRandom1 >= (pStage.GetStageLevel() * 0.2f) && m_fItemRandom1 < (pStage.GetStageLevel() * 0.5f))
                {
                    Debug.Log("3티어 무기 : " + m_fItemRandom1);
                    GetWeaponRand(3);
                }
                else
                {
                    m_nMBBigSelect++;
                    MinorBossBigWeaponBox();
                }
                break;
            case 2:
                if (m_fItemRandom1 >= (pStage.GetStageLevel() * 0.5f) && m_fItemRandom1 < (pStage.GetStageLevel() * 1f))
                {
                    Debug.Log("2티어 무기 : " + m_fItemRandom1);
                    GetWeaponRand(2);
                }
                else
                {
                    Debug.Log("1티어 무기 획득 : " + m_fItemRandom1);
                    GetWeaponRand(1);
                    m_nMBBigSelect = 0;
                }
                break;
        }
    }

    //! 마이너 보스 / 필드보스 보상 정사각형 상자
    public void MinorBossSquareBox()
    {
        m_fItemRandom1 = Random.Range(0.0f, 100f);
        RandomStage pStage = new RandomStage();

        Debug.Log("StageLevel : " + pStage.GetStageLevel());

        switch (m_nMBSquareSelect)
        {
            case 0:
                if (m_fItemRandom1 <= (pStage.GetStageLevel() * 0.2f))
                {
                    Debug.Log("3티어 악마의 심장 : " + m_fItemRandom1);
                    GetDemonSouls(3);
                }
                else
                {
                    m_nMBSquareSelect++;
                    MinorBossSquareBox();
                }
                break;
            case 1:
                if (m_fItemRandom1 >= (pStage.GetStageLevel() * 0.2f) && m_fItemRandom1 < (pStage.GetStageLevel() * 0.5f))
                {
                    Debug.Log("2티어 악마의 심장 : " + m_fItemRandom1);
                    GetDemonSouls(2);
                }
                else
                {
                    m_nMBSquareSelect++;
                    MinorBossSquareBox();
                }
                break;
            case 2:
                if (m_fItemRandom1 >= (pStage.GetStageLevel() * 0.5f) && m_fItemRandom1 < (pStage.GetStageLevel() * 1f))
                {
                    Debug.Log("1티어 악마의 심장  : " + m_fItemRandom1);
                    GetDemonSouls(1);
                }
                else
                {
                    Debug.Log("탄약 50발 획득 : " + m_fItemRandom1);
                    GetAmmo(50);
                    m_nMBSquareSelect = 0;
                }
                break;
        }
    }

    //! 섬멸전 보상 넓적한 상자
    public void DestroyBigWeaponBox()
    {
        m_fItemRandom1 = Random.Range(0.0f, 100f);
        RandomStage pStage = new RandomStage();

        Debug.Log("StageLevel : " + pStage.GetStageLevel());

        switch (m_nMBBigSelect)
        {
            case 0:
                if (m_fItemRandom1 <= (pStage.GetStageLevel() * 0.3f))
                {
                    Debug.Log("3티어 무기 : " + m_fItemRandom1);
                    GetWeaponRand(3);
                }
                else
                {
                    m_nMBBigSelect++;
                    DestroyBigWeaponBox();
                }
                break;
            case 1:
                if (m_fItemRandom1 >= (pStage.GetStageLevel() * 0.3f) && m_fItemRandom1 < (pStage.GetStageLevel() * 0.7f))
                {
                    Debug.Log("2티어 악마의 심장 : " + m_fItemRandom1);
                    GetDemonSouls(2);
                }
                else
                {
                    Debug.Log("1티어 무기획득 : " + m_fItemRandom1);
                    GetWeaponRand(1);
                    m_nMBBigSelect = 0;
                }
                break;
        }
    }

    //! 섬멸전 보상 정사각형 상자
    public void DestroySquareBox()
    {
        m_fItemRandom1 = Random.Range(0.0f, 100f);
        RandomStage pStage = new RandomStage();

        Debug.Log("StageLevel : " + pStage.GetStageLevel());

        switch (m_nMBSquareSelect)
        {
            case 0:
                if (m_fItemRandom1 <= (pStage.GetStageLevel() * 0.3f))
                {
                    Debug.Log("2티어 악마의 심장 : " + m_fItemRandom1);
                    GetDemonSouls(2);
                }
                else
                {
                    m_nMBSquareSelect++;
                    DestroySquareBox();
                }
                break;
            case 1:
                if (m_fItemRandom1 >= (pStage.GetStageLevel() * 0.3f) && m_fItemRandom1 < (pStage.GetStageLevel() * 0.7f))
                {
                    Debug.Log("1티어 악마의 심장 : " + m_fItemRandom1);
                    GetDemonSouls(1);
                }
                else
                {
                    Debug.Log("탄약 50발 획득 : " + m_fItemRandom1);
                    GetAmmo(50);
                    m_nMBSquareSelect = 0;
                }
                break;
        }
    }
}
