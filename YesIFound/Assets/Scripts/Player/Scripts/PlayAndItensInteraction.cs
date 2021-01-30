using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAndItensInteraction : MonoBehaviour
{
    [SerializeField] PlayerAtribsSO m_AtribsSO;
    [SerializeField] Transform ItemHolder;

    private Rigidbody2D rb;

    ItemGameplay HoldedItem;
    ItemGameplay OnAreaItem;
    bool canHighlighAnItem;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        HoldedItem = OnAreaItem = null;
        canHighlighAnItem = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(m_AtribsSO.GetAnDropItemButton))
        {
           if (OnAreaItem != null && HoldedItem == null)
            {
                GetItem();
            }
           else if (HoldedItem != null)
            {
                DropItem();
            }
        }
    }

    private void DropItem()
    {
        OnAreaItem = HoldedItem;
        HoldedItem = null;
        OnAreaItem.Highlight();
        OnAreaItem.transform.SetParent(null);
        OnAreaItem.transform.position = transform.position;
    }

    private void GetItem()
    {
        HoldedItem = OnAreaItem;
        HoldedItem.Unhighlight();
        HoldedItem.transform.position = ItemHolder.position;
        HoldedItem.transform.SetParent(ItemHolder);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Item" && canHighlighAnItem && HoldedItem == null)
        {
            canHighlighAnItem = false;
            OnAreaItem = collision.gameObject.GetComponent<ItemGameplay>();
            OnAreaItem?.Highlight();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Item" && HoldedItem == null)
        {
            OnAreaItem?.Unhighlight();
            OnAreaItem = null;
            canHighlighAnItem = true;
        }
    }
}
