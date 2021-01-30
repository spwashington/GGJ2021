using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    Vector2 movement;
    private Rigidbody2D rb;
    bool isDashing, canDash;

    [SerializeField] PlayerAtribsSO m_AtribsSO;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        isDashing = false;
        canDash = true;
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if (canDash && movement.normalized != Vector2.zero && Input.GetKeyDown(m_AtribsSO.DashButton))
        {
            StartCoroutine(DashControl());
        }
    }


    private void FixedUpdate()
    {
        float speed = isDashing ? m_AtribsSO.DashSpeed : m_AtribsSO.WalkSpeed; 
        rb.MovePosition(rb.position + movement.normalized * speed * Time.fixedDeltaTime);
    }

    private IEnumerator DashControl()
    {
        isDashing = true;
        canDash = false;

        yield return new WaitForSeconds(m_AtribsSO.DashDuration);
        isDashing = false;
        yield return new WaitForSeconds(m_AtribsSO.DashCooldown);
        canDash = true;
    }

}
