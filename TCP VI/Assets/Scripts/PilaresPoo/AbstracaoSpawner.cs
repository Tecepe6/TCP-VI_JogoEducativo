using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstracaoSpawner : MonoBehaviour
{
    [SerializeField] PilarAbstracao pilarAbstacao;

    [SerializeField] GameObject abstracaoObj;
    [SerializeField] float minCooldownBase, maxCooldownBase;

    
    float cooldownTime;
    // Start is called before the first frame update
    void Start()
    {
        SetRandomCooldown();
    }

    private void SetRandomCooldown()
    {
        cooldownTime = Random.Range(minCooldownBase, maxCooldownBase);
    }

    // Update is called once per frame
    void Update()
    {
        if (!pilarAbstacao.StillActive)
            return;

        if (cooldownTime <= 0)
        {
            SetRandomCooldown();
            GameObject instance = Instantiate(abstracaoObj, transform.position, abstracaoObj.transform.rotation);
            
            AbstBotao abstBotao = instance.GetComponent<AbstBotao>();

            Vector3 direction = transform.right + (transform.up * Random.Range(-1f, 2f));
            abstBotao.SetDirection(direction);
        }
        cooldownTime -= Time.deltaTime;
    }
}
