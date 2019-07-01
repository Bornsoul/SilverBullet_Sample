using UnityEngine;
using System.Collections;

public class JALoadingScript : MonoBehaviour
{
    Vector3 m_stRotVec = Vector3.zero;

    void Start()
    {
        //m_stRotVec = transform.localRotation;
    }

    void Update()
    {
        transform.Rotate(new Vector3(0f, 0f, -1f));
    }
}
