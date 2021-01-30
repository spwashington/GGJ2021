using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGameplay : MonoBehaviour
{
    [SerializeField] SpriteRenderer m_light, m_objSprite;
    private string m_ItemName, m_Color;

    private void Awake()
    {
        Unhighlight();
    }

    public void Atributes(string _name, string _color, Sprite _sprite)
    {
        m_ItemName = _name;
        m_Color = _color;
        m_objSprite.sprite = _sprite;
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
