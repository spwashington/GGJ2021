using UnityEngine;

public class Npc : MonoBehaviour
{
    [SerializeField] private float m_Speed;
    private WaveManager m_WaveManager;
    private GameObject m_RequestPopup;
    private bool m_Start;
    private bool m_Exit;

    private void Start()
    {
        m_WaveManager = GameObject.FindGameObjectWithTag("WaveManager").GetComponent<WaveManager>();
        m_Speed = 1.5f;
        m_Exit = false;
        m_Start = true;
        m_RequestPopup = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        MoveNpc();
    }

    private void MoveNpc()
    {
        if (m_Start)
            transform.position = new Vector3(transform.position.x, transform.position.y + (m_Speed * Time.deltaTime), transform.position.z);

        if (m_Exit)
            transform.position = new Vector3(transform.position.x, transform.position.y + (m_Speed * Time.deltaTime), transform.position.z);
    }

    //Logic to Npc recive your request
    public void TakeItem(string _ItemRecived)
    {
        m_RequestPopup.SetActive(false);
        m_Exit = true;
        m_Speed *= -1f;
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
            }
        }
    }
}