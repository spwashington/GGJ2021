using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

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
    public ResetWave resetWave;
    public int m_WaveCount;
    private float m_SpawnDelay;
    public bool m_StartGame;
    private bool m_SinglePlayer;
    private int m_NumberOfItensInWave;
    private float m_WaveSpeed;

    private int m_lives;
    [SerializeField] private GameObject[] m_hearts;
    [SerializeField] TextMeshProUGUI m_BigText;



    private void Start()
    {
        m_WaveSpeed = 1.5f;
        m_NumberOfItensInWave = 10;
        m_SpawnDelay = 1.5f;
        m_WaveCount = 1;
        m_SinglePlayer = true;
        m_StartGame = false;
        m_lives = 5;

    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
            //m_StartGame = true;

        //if (Input.GetKeyDown(KeyCode.V))
            //WaveStart();

        //if (Input.GetKeyDown(KeyCode.C))
            //WaveReset();

        if (Input.GetKeyDown(KeyCode.P))
            m_WaveSpeed += 1f;

        if (m_StartGame)
        {
            ItemCheck();
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
                            if (m_Itens.childCount > 0)
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
    }

    public float GetWaveSpeed()
    {
        return m_WaveSpeed;
    }

    public void WaveStart()
    {
        SpawnObjectsInWave(m_NumberOfItensInWave);
    }

    public void DestroyAllNpcs()
    {
        for (int i = 0; i < m_ActiveNpcPlace.Length; i++)
        {
            if (m_ActiveNpcPlace[i].transform.childCount > 0)
                Destroy(m_ActiveNpcPlace[i].transform.GetChild(0).gameObject);
        }

        if (m_InactiveNpc.childCount > 0)
        {
            for (int i = 0; i < m_InactiveNpc.childCount; i++)
            {
                Destroy(m_InactiveNpc.GetChild(i).gameObject);
            }
        }
    }

    public void WaveReset()
    {
        m_StartGame = false;
        DestroyAllNpcs();
        m_WaveCount++;
        m_WaveSpeed += 0.5f;

        if (m_WaveSpeed > 6)
            m_WaveSpeed = 6f;

        if (m_WaveCount % 2 == 0)
        {
            if (m_WaveCount > 2)
            {
                m_NumberOfItensInWave += 5;

                if (m_NumberOfItensInWave > 35)
                    m_NumberOfItensInWave = 35;
            }
        }

        WaveStart();
        resetWave.ResetWaveMethod();
    }

    //Drop Item Logic to accept npc request in Balcony
    public void DropRequest(GameObject _Item, int _NpcIndex, string _ItemName, string _Color)
    {
        if (m_ActiveNpcPlace[_NpcIndex].transform.childCount > 0)
        {
            m_ActiveNpcPlace[_NpcIndex].transform.GetChild(0).GetComponent<Npc>().TakeItem(_ItemName, _Color);
            //m_ActiveNpcPlace[_NpcIndex].transform.GetChild(0).transform.parent = m_InactiveNpc; //ADICIONEI ISSO, TESTAR SE � BUGA 
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

    public void RemoveAllItens()
    {
        if (m_Itens.childCount > 0)
        {
            for (int i = 0; i < m_Itens.childCount; i++)
            {
                Destroy(m_Itens.GetChild(i).gameObject);
            }
        }

        if (m_ItensChoosed.childCount > 0)
        {
            for (int i = 0; i < m_ItensChoosed.childCount; i++)
            {
                Destroy(m_ItensChoosed.GetChild(i).gameObject);
            }
        }
    }

    //Spawn Objects in scenario
    private void SpawnObjectsInWave(int _NumberOfObject)
    {
        RemoveAllItens();

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

        m_StartGame = true;
    }

    void ItemCheck()
    {
        if(m_Itens.childCount == 0 && m_ItensChoosed.childCount == 0)
        {
            //if('tem vida?')
            WaveReset();
        }
    }

    public void DeleteChooseItems(string name, string color)
    {
        if(m_ItensChoosed.childCount > 0)
        {
            for(int i=0; i < m_ItensChoosed.childCount; i++)
            {
                if(m_ItensChoosed.GetChild(i).GetComponent<ItemGameplay>().GetName() == name && m_ItensChoosed.GetChild(i).GetComponent<ItemGameplay>().GetColor() == color)
                {
                    Destroy(m_ItensChoosed.GetChild(i).gameObject);
                    break;
                }
                
            }
        }
    }

    public void DecreaseLife()
    {
        if (m_lives > 1) m_lives--;
        else StartCoroutine(GameOver());

        foreach (GameObject heart in m_hearts) heart.SetActive(false);

        for (int i = 0; i < m_lives; ++i)
        {
            m_hearts[i].SetActive(true);
        }
    }


    private IEnumerator GameOver()
    {
        m_BigText.gameObject.SetActive(true);
        m_BigText.text = "You\nLose";
        yield return new WaitForSeconds(2);
        ReloadScene();
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene("MainScene");
    }



}