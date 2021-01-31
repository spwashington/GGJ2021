using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    [SerializeField] Material m_MaterialSrc;
    [SerializeField] private float m_Speed;
    [SerializeField] private string[] m_SelectedItem;
    private WaveManager m_WaveManager;
    private GameObject m_RequestPopup;
    private bool m_Start;
    private bool m_Exit;
    private float m_WaitLimit;
    private bool m_StartCountDownToExit;
    private BoxCollider2D colliderNPC;

    [SerializeField]
    private List<Sprite> spriteList;

    [SerializeField]
    private List<Sprite> spriteBackList;
    private SpriteRenderer spriteNPC;
    private int index;
    private Animator animNPC;

    private void Start()
    {
        m_WaitLimit = 0f;
        m_StartCountDownToExit = false;
        spriteNPC = GetComponentInChildren<SpriteRenderer>();
        animNPC = GetComponentInChildren<Animator>();
        colliderNPC = GetComponent<BoxCollider2D>();
        m_WaveManager = GameObject.FindGameObjectWithTag("WaveManager").GetComponent<WaveManager>();
        m_Exit = false;
        m_Start = true;
        m_SelectedItem = new string[2];
        m_RequestPopup = transform.GetChild(0).gameObject;
        m_Speed = m_WaveManager.GetWaveSpeed();
        ChooseItem();
        RandomNPC();
    }

    private void Update()
    {
        MoveNpc();
        DestroyNPC();
        WaitRequest();
    }

    private void MoveNpc()
    {
        if (m_Start)
            transform.position = new Vector3(transform.position.x, transform.position.y + (m_Speed * Time.deltaTime), transform.position.z);

        if (m_Exit)
            transform.position = new Vector3(transform.position.x, transform.position.y + (m_Speed * Time.deltaTime), transform.position.z);
    }

    Dictionary<string, Color> PossibleColors = new Dictionary<string, Color> {
        {"Red", Color.red },
        {"Blue", Color.blue},
        {"Yellow", Color.yellow},
        {"Green", Color.green},
        {"White", Color.white }
    };
    private void ChooseItem()
    {
        m_SelectedItem = m_WaveManager.GetItem();

        Material itemMaterial = new Material(m_MaterialSrc);
        itemMaterial.color = PossibleColors[m_SelectedItem[1]];
        Sprite spr = Resources.Load<Sprite>("Sprites/Items/" + m_SelectedItem[0]);
        m_RequestPopup.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = spr;
        m_RequestPopup.transform.GetChild(0).GetComponent<SpriteRenderer>().material = itemMaterial;
    }

    //Logic to Npc recive your request
    public void TakeItem(string _ItemNameRecived, string _ItemRecivedColor)
    {
        bool wrongItem = false;

        if (m_SelectedItem[0] == _ItemNameRecived && m_SelectedItem[1] == _ItemRecivedColor)
            wrongItem = false;
        else
            wrongItem = true;

        m_RequestPopup.SetActive(false);
        m_Exit = true;
        m_Speed *= -1f;
        spriteNPC.sprite = spriteList[index];
        animNPC.speed = 1;
        colliderNPC.enabled = false;


    }

    //Change Npc speed
    public void SetSpeed(float _Value)
    {
        m_Speed = _Value;
    }

    private void WaitRequest()
    {
        if (m_StartCountDownToExit)
        {
            m_WaitLimit += 1f * Time.deltaTime;

            if (m_WaitLimit >= 15f)
            {
                m_RequestPopup.SetActive(false);
                m_Exit = true;
                m_Speed *= -1f;
                spriteNPC.sprite = spriteList[index];
                animNPC.speed = 1;
                colliderNPC.enabled = false;
                m_StartCountDownToExit = false;
            }
        }
    }

    //NPC stop in balcony
    private void OnCollisionEnter2D(Collision2D _Collision)
    {
        if (m_Start)
        {
            if (_Collision.gameObject.tag == "BalconyStop")
            {
                m_Start = false;
                m_RequestPopup.SetActive(true);
                animNPC.speed = 0;
                m_StartCountDownToExit = true;
            }
        }
    }

    private void RandomNPC()
    {
        index = Random.Range(0, spriteList.Capacity);
        spriteNPC.sprite = spriteBackList[index];
    }

    private void DestroyNPC()
    {
        if(gameObject.transform.position.y < -5)
        {
            Destroy(gameObject);
        }
    }
}