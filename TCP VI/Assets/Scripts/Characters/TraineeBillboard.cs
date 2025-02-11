using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraineeBillboard : MonoBehaviour
{
    [Header("PONTOS DO CAMINHO")]
    [SerializeField] private Transform[] _pontos;
    [SerializeField] private int _pontoAtual;

    // VARIÁVEL TEMPORÁRIA, APENAS PARA TESTES RÁPIDOS
    public int goTo;
    private bool _isWalking = false;

    [Header("STATUS DO BILLBOARD")]
    public bool isFixing = false;
    [SerializeField] private int speed;

    [SerializeField] Animator animator;

    private Queue<int> caminho = new Queue<int>();

    public enum TraineeEstado
    {
        Idle,
        Andando,
        Consertando
    }

    [Header("ESTADO ATUAL")]
    public TraineeEstado estadoAtual;

    void Start()
    {
        estadoAtual = TraineeEstado.Idle;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (estadoAtual)
        {
            case TraineeEstado.Idle:
                HandleIdle();
                break;
            case TraineeEstado.Andando:
                HandleAndando();
                break;
            case TraineeEstado.Consertando:
                HandleConsertando();
                break;
        }
    }

    // FUNÇÃO TEMPORÁRIA, APENAS PARA TESTES RÁPIDOS
    private void GoTo()
    {
        /*
        for (int i = 0; i <= _pontos.Length; i++)
        {
            if (Input.GetKeyDown((KeyCode)((int)KeyCode.Alpha0 + i)))
            {
                goTo = i;
                Debug.Log("Apertou!");
            }
        }
        */

        if (MechaManager.instance.selectedRightArm == true)
        {
            goTo = 0;

            if (_pontoAtual == 1 && goTo == 0)
            {
                caminho.Enqueue(5);
                caminho.Enqueue(3);
                caminho.Enqueue(0);

                estadoAtual = TraineeEstado.Andando;
            }

            if (_pontoAtual == 2 && goTo == 0)
            {
                caminho.Enqueue(4);
                caminho.Enqueue(3);
                caminho.Enqueue(0);

                estadoAtual = TraineeEstado.Andando;
            }
        }

        if (MechaManager.instance.selectedRightArm == false)
        {
            estadoAtual = TraineeEstado.Idle;
        }

        if (MechaManager.instance.selectedLeftArm == true)
        {
            goTo = 1;

            if (_pontoAtual == 0 && goTo == 1)
            {
                caminho.Enqueue(3);
                caminho.Enqueue(5);
                caminho.Enqueue(1);

                estadoAtual = TraineeEstado.Andando;
            }

            if (_pontoAtual == 2 && goTo == 1)
            {
                caminho.Enqueue(4);
                caminho.Enqueue(5);
                caminho.Enqueue(1);

                // estadoAtual = TraineeEstado.Andando;
            }
        }

        if (MechaManager.instance.selectedLeftArm == false)
        {
            estadoAtual = TraineeEstado.Idle;
        }

        if (MechaManager.instance.selectedChassi == true)
        {
            goTo = 2;

            if (_pontoAtual == 0 && goTo == 2)
            {
                Debug.Log("DEBUGUEI!");
                caminho.Enqueue(3);
                caminho.Enqueue(4);
                caminho.Enqueue(2);

                estadoAtual = TraineeEstado.Consertando;
            }

            if (_pontoAtual == 1 && goTo == 2)
            {
                caminho.Enqueue(5);
                caminho.Enqueue(4);
                caminho.Enqueue(2);

                estadoAtual = TraineeEstado.Andando;
            }
        }

        if (MechaManager.instance.selectedChassi == false)
        {
            estadoAtual = TraineeEstado.Idle;
        }
    }

    // Funções que lidam com cada estado

    private void HandleIdle()
    {
        animator.SetTrigger("isIdle");

        GoTo();

        if (goTo != _pontoAtual && caminho.Count > 0)
        {
            estadoAtual = TraineeEstado.Andando;
        }

        if (isFixing == true)
        {
            estadoAtual = TraineeEstado.Consertando;
        }
    }

    private void HandleAndando()
    {
        if (caminho.Count > 0 && !_isWalking) // Só ativa a animação de andar se o personagem ainda não estiver andando
        {
            animator.SetTrigger("isWalking");
            _isWalking = true; // Marca que o personagem está andando
        }

        // Lógica de movimento
        if (caminho.Count > 0)
        {
            int proximoPonto = caminho.Peek();
            Vector3 _pontoDestino = _pontos[proximoPonto].position;

            transform.position += (_pontoDestino - transform.position).normalized * speed * Time.deltaTime;

            if (Vector3.Distance(_pontoDestino, transform.position) < 0.1f)
            {
                _pontoAtual = proximoPonto;
                caminho.Dequeue();
            }
        }
        else
        {
            // Quando o caminho acaba, retorna para o estado Idle
            estadoAtual = TraineeEstado.Idle;
            _isWalking = false; // Marca que o personagem parou de andar
        }
    }

    private void HandleConsertando()
    {
        animator.SetTrigger("isFixing");

        GoTo();
    }
}