using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    bool isDashing, canDash, finishedDash;

    [SerializeField] PlayerAtribsSO m_AtribsSO;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        isDashing = false;
        canDash = finishedDash = true;
    }

    void Update()
    {
        m_AtribsSO.movement.x = Input.GetAxisRaw("Horizontal");
        m_AtribsSO.movement.y = Input.GetAxisRaw("Vertical");
        if (canDash && m_AtribsSO.movement.normalized != Vector2.zero && Input.GetAxis(m_AtribsSO.DashButton) > 0)
        {
            StartCoroutine(DashControl());
        }

        if (Input.GetAxis(m_AtribsSO.DashButton) == 0 && finishedDash) canDash = true;
    }


    private void FixedUpdate()
    {
        float speed = isDashing ? m_AtribsSO.DashSpeed : m_AtribsSO.WalkSpeed; 
        rb.MovePosition(rb.position + m_AtribsSO.movement.normalized * speed * Time.fixedDeltaTime);
    }

    private IEnumerator DashControl()
    {
        isDashing = true;
        canDash = finishedDash = false;

        yield return new WaitForSeconds(m_AtribsSO.DashDuration);
        isDashing = false;
        yield return new WaitForSeconds(m_AtribsSO.DashCooldown);
        finishedDash = true;
    }

}
