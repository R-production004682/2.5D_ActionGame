using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickController : MonoBehaviour
{
    [SerializeField] private GimmickData gimmickData;

    private GimmickContext context;

    private IGimmickLogic gimmickLogic;
    private IGimmickEffect gimmickEffect;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.interpolation = RigidbodyInterpolation.Interpolate;

        context = new GimmickContext(
            gimmickData,
            rb,
            this
         );

        gimmickLogic = GetComponent<IGimmickLogic>();
        if(gimmickLogic != null)
        {
            gimmickLogic.Initialize(context);
        }

        gimmickEffect = GetComponent<IGimmickEffect>();
        if(gimmickEffect != null)
        {
            gimmickEffect.Initialize(context);
        }
    }

    private void FixedUpdate()
    {
        if(gimmickLogic != null)
        {
            gimmickLogic.Execute();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        gimmickEffect.ApplyEnterEffect(other);
    }

    private void OnTriggerStay(Collider other)
    {
        gimmickEffect.ApplyStayEffect(other);
    }
}
