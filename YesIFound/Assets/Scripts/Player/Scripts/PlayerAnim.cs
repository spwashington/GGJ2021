using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    [SerializeField] Animator WalkAnim;
    [SerializeField] PlayerAtribsSO m_AtribsSO;
    [SerializeField] RuntimeAnimatorController[] Animators;

    private void Update()
    {
        if (m_AtribsSO.isHoldingItem)
        {
            int gender = m_AtribsSO.Gender == "Male" ? 1:0;
            WalkAnim.runtimeAnimatorController = Animators[gender];
        }
        else
        {
            int gender = m_AtribsSO.Gender == "Male" ? 3:2;
            WalkAnim.runtimeAnimatorController = Animators[gender];
        }


        Vector2 direction = m_AtribsSO.movement.normalized;

        WalkAnim.SetFloat("x_axis", direction.x);
        WalkAnim.SetFloat("y_axis", direction.y);
    }
}
