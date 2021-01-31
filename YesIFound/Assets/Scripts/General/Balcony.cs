using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balcony : MonoBehaviour
{
    [SerializeField] int BalconyIndex;
    [SerializeField] WaveManager m_WaveManager;
    bool canDrop = true;
    bool hasNpc = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Item" && canDrop && hasNpc)
        {
            canDrop = false;
            StartCoroutine(DropCooldown());
            ItemGameplay item = collision.gameObject.GetComponent<ItemGameplay>();
            m_WaveManager.DropRequest(collision.gameObject, BalconyIndex, item.ItemName, item.ItemColor);
        }

        if (collision.tag == "NPC") hasNpc = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "NPC") hasNpc = false;
    }

    private IEnumerator DropCooldown()
    {
        yield return new WaitForSeconds(1);
        canDrop = true;
    }
}
