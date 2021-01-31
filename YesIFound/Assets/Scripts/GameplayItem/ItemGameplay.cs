using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGameplay : MonoBehaviour
{
    [SerializeField] SpriteRenderer m_light, m_objSprite;
    [SerializeField] Material MaterialSrc;
    private bool m_NpcChooseThis;
    private string m_ItemName, m_Color;
    private Material ItemMaterial;
    public string ItemColor { get => m_Color; }
    public string ItemName { get => m_ItemName; }

    Dictionary<string, Color> PossibleColors = new Dictionary<string, Color> { 
        {"Red", Color.red },
        {"Blue", Color.blue},
        {"Yellow", Color.yellow},
        {"Green", Color.green},
        {"White", Color.white }
    };

    private void Awake()
    {
        m_NpcChooseThis = false;
        Unhighlight();
        ItemMaterial = new Material(MaterialSrc);
    }

    public void Atributes(string _name, string _color, Sprite _sprite)
    {
        m_ItemName = _name;
        m_Color = _color;
        ItemMaterial.color = PossibleColors[_color];
        m_objSprite.sprite = _sprite;
        m_objSprite.material = ItemMaterial;
    }

    public string GetName()
    {
        return m_ItemName;
    }
    public string GetColor()
    {
        return m_Color;
    }

    public bool IsNpcChooseThis()
    {
        return m_NpcChooseThis;
    }

    public void NpcChooseThis()
    {
        m_NpcChooseThis = true;
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
