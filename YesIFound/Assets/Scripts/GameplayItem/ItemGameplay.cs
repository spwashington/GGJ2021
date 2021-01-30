using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGameplay : MonoBehaviour
{
    [SerializeField] SpriteRenderer m_light;

    private void Awake()
    {
        Unhighlight();
    }

    public void Highlight()
    {
        m_light.gameObject.SetActive(true);
    }

    public void Unhighlight()
    {
        m_light.gameObject.SetActive(false);
    }
}
