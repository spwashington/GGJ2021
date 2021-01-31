using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private GameObject m_NpcPrefab;
    [SerializeField] private GameObject m_ItemPrefab;
    [SerializeField] private Transform m_Itens;
    [SerializeField] private Transform m_ItensChoosed;
    [SerializeField] private Transform[] m_ItensspawnPositions;
    [SerializeField] private string[] m_ItensName;
    [SerializeField] private string[] m_ItensColor;
    [SerializeField] private Transform[] m_SpawnNpc;
    [SerializeField] private GameObject[] m_ActiveNpcPlace;
    [SerializeField] private Transform m_InactiveNpc;
    private int m_WaveCount;
    private float m_SpawnDelay;
    private bool m_StartGame;
    private bool m_SinglePlayer;

    private void Start()
    {
        m_SpawnDelay = 1.5f;
        m_WaveCount = 1;
        m_SinglePlayer = true;
        m_StartGame = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            m_StartGame = true;

        if (Input.GetKeyDown(KeyCode.V))
            SpawnObjectsInWave(38);

        if (m_StartGame)
        {
            SpawnNpc();
        }
    }

    //SPAWN NPCs Logic
    private void SpawnNpc()
    {
        m_SpawnDelay += 1f * Time.deltaTime;

        if (m_SpawnDelay > Random.Range(1f, 2f))
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
                            m_SpawnDelay = 0f;
                            break;
                        }
                    }
                }
            }
        }
    }

    //Drop Item Logic to accept npc request in Balcony
    public void DropRequest(GameObject _Item, int _NpcIndex, string _ItemName, string _Color)
    {
        if (m_ActiveNpcPlace[_NpcIndex].transform.childCount > 0)
        {
            m_ActiveNpcPlace[_NpcIndex].transform.GetChild(0).GetComponent<Npc>().TakeItem(_ItemName, _Color);
            m_ActiveNpcPlace[_NpcIndex].transform.GetChild(0).transform.parent = m_InactiveNpc; //ADICIONEI ISSO, TESTAR SE Ñ BUGA 
            Destroy(_Item.gameObject);
        }
    }

    public string[] GetItem()
    {
        string[] temp = new string[2];
        int rnd = Random.Range(0, m_Itens.childCount - 1);
        m_Itens.GetChild(rnd).GetComponent<ItemGameplay>().NpcChooseThis();
        temp[0] = m_Itens.GetChild(rnd).GetComponent<ItemGameplay>().GetName();
        temp[1] = m_Itens.GetChild(rnd).GetComponent<ItemGameplay>().GetColor();
        m_Itens.GetChild(rnd).transform.parent = m_ItensChoosed;
        return temp;
    }

    //Spawn Objects in scenario
    private void SpawnObjectsInWave(int _NumberOfObject)
    {
        int[] posIndex = new int[_NumberOfObject];
        int rndPos = Random.Range(0, m_ItensspawnPositions.Length);
        bool pass = false;

        for (int i = 0; i < _NumberOfObject; i++)
        {
            while (!pass)
            {
                rndPos = Random.Range(0, m_ItensspawnPositions.Length);
                pass = true;

                for (int j = 0; j < posIndex.Length; j++)
                {
                    if (posIndex[j] == rndPos)
                    {
                        pass = false;
                        break;
                    }
                }
            }

            pass = false;

            int rndName = Random.Range(0, m_ItensName.Length);
            int rndColor;

            if (m_ItensName[rndName] != "Ocarina")
                rndColor = Random.Range(0, m_ItensColor.Length);
            else
                rndColor = 0;

            GameObject temp = Instantiate(m_ItemPrefab, m_ItensspawnPositions[rndPos].position, Quaternion.identity);
            posIndex[i] = rndPos;
            Sprite spr = Resources.Load<Sprite>("Sprites/Items/" + m_ItensName[rndName]);

            temp.GetComponent<ItemGameplay>().Atributes(m_ItensName[rndName], m_ItensColor[rndColor], spr);
            temp.transform.parent = m_Itens;
        }
    }
}