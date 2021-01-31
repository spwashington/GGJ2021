using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    [SerializeField] private float m_Speed;
    private WaveManager m_WaveManager;
    private GameObject m_RequestPopup;
    [SerializeField] private string[] m_SelectedItem;
    private bool m_Start;
    private bool m_Exit;

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
        spriteNPC = GetComponentInChildren<SpriteRenderer>();
        animNPC = GetComponentInChildren<Animator>();
        colliderNPC = GetComponent<BoxCollider2D>();
        m_WaveManager = GameObject.FindGameObjectWithTag("WaveManager").GetComponent<WaveManager>();
        m_Speed = 1.5f;
        m_Exit = false;
        m_Start = true;
        m_SelectedItem = new string[2];
        m_RequestPopup = transform.GetChild(0).gameObject;
        ChooseItem();
        RandomNPC();
    }

    private void Update()
    {
        MoveNpc();
        DestroyNPC();
    }

    private void MoveNpc()
    {
        if (m_Start)
            transform.position = new Vector3(transform.position.x, transform.position.y + (m_Speed * Time.deltaTime), transform.position.z);

        if (m_Exit)
            transform.position = new Vector3(transform.position.x, transform.position.y + (m_Speed * Time.deltaTime), transform.position.z);
    }

    private void ChooseItem()
    {
        m_SelectedItem = m_WaveManager.GetItem();
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
        //pts
    }

    //Change Npc speed
    public void SetSpeed(float _Value)
    {
        m_Speed = _Value;
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