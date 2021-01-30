using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private GameObject m_NpcPrefab;
    [SerializeField] private Transform[] m_SpawnNpc;
    [SerializeField] private GameObject[] m_ActiveNpcPlace;
    private bool m_StartGame;
    private bool m_SinglePlayer;

    private void Start()
    {
        m_SinglePlayer = true;
        m_StartGame = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            m_StartGame = true;

        if (m_StartGame)
        {
            SpawnNpc();
        }
    }

    //SPAWN NPCs Logic
    private void SpawnNpc()
    {
        if (m_SinglePlayer)
        {
            for (int i = 0; i < 5; i++)
            {
                if (i % 2 == 0)
                {
                    if (m_ActiveNpcPlace[i].transform.childCount == 0)
                    {
                        GameObject temp = Instantiate(m_NpcPrefab, m_SpawnNpc[i].position, Quaternion.identity);
                        temp.transform.parent = m_ActiveNpcPlace[i].transform;
                    }
                }
            }
        }
    }

    //Drop Item Logic to accept npc request in Balcony
    public void DragRequest(int _NpcIndex, string _Item)
    {
        if (m_ActiveNpcPlace[_NpcIndex].transform.childCount > 0)
            m_ActiveNpcPlace[_NpcIndex].transform.GetChild(0).GetComponent<Npc>().TakeItem();
    }
}