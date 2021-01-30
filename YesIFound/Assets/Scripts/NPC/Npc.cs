using UnityEngine;

public class Npc : MonoBehaviour
{
    [SerializeField] private float m_Speed;
    private bool m_Start;
    private bool m_Exit;
    private GameObject m_RequestPopup;

    private void Start()
    {
        m_Speed = 1.5f;
        m_Exit = false;
        m_Start = true;
        m_RequestPopup = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        if(m_Start)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + (m_Speed * Time.deltaTime), transform.position.z);
        }

        if (m_Exit)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + (m_Speed * Time.deltaTime), transform.position.z);
        }
    }

    public void TakeItem()
    {
        m_RequestPopup.SetActive(false);
        m_Exit = true;
        m_Speed *= -1f;
    }

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