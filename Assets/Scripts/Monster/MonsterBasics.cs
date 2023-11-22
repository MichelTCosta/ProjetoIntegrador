using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBasics : MonoBehaviour
{

    public bool getKey; // Toda vez que pegar uma chave ativar
    public int numberOfKeys; //Aumentar toda vez que pegar um chave

    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetInteger("NumberOfKeys", numberOfKeys);//Colocar o numero de chaves pegas no parametro de animação do monstro


        if (getKey)//Checa se uma chave foi pega
        {
            getKey = false;
            animator.SetTrigger("GetAKey"); //ativa um trigger no animator que faz com que o monstro entre em modo enfurecido
        }
    }
}
