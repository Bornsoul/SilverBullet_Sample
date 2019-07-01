using UnityEngine;
using System.Collections;

public class JATouchCamUse : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnPress(bool bPress)
    {
        if (bPress == true)
        {
            JAManager.I.m_bUseCam = false;
        }
        else
        {
            JAManager.I.m_bUseCam = true;
        }
    }
}
