using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraineeBillboard : MonoBehaviour
{
    [Header("PONTOS DO CAMINHO")]
    [SerializeField] private Transform[] _pontos;
    [SerializeField] private int _pontoAtual;
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

    private void GoTo()
    {
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
        if (caminho.Count > 0 && !_isWalking)
        {
            animator.SetBool("isWalking", true);
            _isWalking = true;
        }

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
            StartCoroutine(WaitForAnimationToFinish());
        }
    }

    private IEnumerator WaitForAnimationToFinish()
    {
        float animationDuration = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(animationDuration);

        estadoAtual = TraineeEstado.Idle;
        _isWalking = false;
        animator.SetBool("isWalking", false);
    }

    private void HandleConsertando()
    {
        animator.SetTrigger("isFixing");

        GoTo();
    }
}
