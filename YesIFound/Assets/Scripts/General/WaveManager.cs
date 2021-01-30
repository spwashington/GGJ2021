using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private GameObject[] m_ActiveNpcPlace;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if(m_ActiveNpcPlace[0].transform.childCount > 0)
                m_ActiveNpcPlace[0].transform.GetChild(0).GetComponent<Npc>().TakeItem();
        }
    }
}