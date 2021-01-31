using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balcony : MonoBehaviour
{
    [SerializeField] int BalconyIndex;
    WaveManager m_WaveManager;
    bool canDrop = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Item" && canDrop)
        {
            canDrop = false;
            StartCoroutine(DropCooldown());
            ItemGameplay item = collision.gameObject.GetComponent<ItemGameplay>();
            Debug.Log("OKKKKKKK");
            print(collision.gameObject + " " + BalconyIndex + " " + item.ItemName + " " + item.ItemColor);
            //m_WaveManager.DropRequest(collision.gameObject, BalconyIndex, item.ItemName, item.ItemColor);
        }
    }

    private IEnumerator DropCooldown()
    {
        yield return new WaitForSeconds(1);
        canDrop = true;
    }
}
