using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class MonsterCalmMode : StateMachineBehaviour
{
 
    NavMeshAgent agent;
    Animator animator;

    [Header("Velocidade do monstro")]
    [SerializeField]
    float moveSpeed;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(agent == null)
        {
            agent = animator.GetComponent<NavMeshAgent>();

        }
        if(animator == null)
        {
            animator.GetComponent<Animator>();
        }
        if(agent.speed != moveSpeed)
        {
            agent.speed = moveSpeed;

        }


    }

    override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
