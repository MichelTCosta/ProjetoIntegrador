using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterRageMode : StateMachineBehaviour
{
    
    NavMeshAgent agent;
    Animator animator;
    [Header("Velocidade Do Monstro")]
    [SerializeField]
    float moveSpeed;

    [Header("Modo de perseguisão infinita")]
    [SerializeField]
    bool infiniteRage;

    [Header("Tempo que o monstro fica irritado")]
    [SerializeField]
    float rageTimer;
    float rageCounter;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rageCounter = rageTimer;
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent == null)
        {
            agent = animator.GetComponent<NavMeshAgent>();

        }
        if (animator == null)
        {
            animator.GetComponent<Animator>();
        }

        if(agent.speed != moveSpeed)
        {
            agent.speed = moveSpeed;

        }


        if (infiniteRage == false)
        {
            if(rageCounter > 0)
            {
                rageCounter-= Time.deltaTime;
            }

            if(rageCounter <= 0)
            {
                animator.SetTrigger("EnterCalmMode");
            }
    
        }


    }


}
