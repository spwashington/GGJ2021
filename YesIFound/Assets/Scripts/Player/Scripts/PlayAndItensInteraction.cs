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
    bool canHighlighAnItem, CanGetOrDrop;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        HoldedItem = OnAreaItem = null;
        canHighlighAnItem = CanGetOrDrop = true;
        m_AtribsSO.isHoldingItem = false;
    }

    void Update()
    {
        if (Input.GetAxis(m_AtribsSO.GetAnDropItemButton) > 0 && CanGetOrDrop)
        {
           CanGetOrDrop = false;
            StartCoroutine(Cooldown());
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
        m_AtribsSO.isHoldingItem = false;
    }

    private void GetItem()
    {
        HoldedItem = OnAreaItem;
        HoldedItem.Unhighlight();
        HoldedItem.transform.position = ItemHolder.position;
        HoldedItem.transform.SetParent(ItemHolder);
        m_AtribsSO.isHoldingItem = true;
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

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(0.3f);
        CanGetOrDrop = true;
    }
}
