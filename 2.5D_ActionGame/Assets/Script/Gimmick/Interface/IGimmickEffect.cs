using UnityEngine;

public interface IGimmickEffect
{
    void Initialize(GimmickContext context);
    void ApplyEnterEffect(Collider other);

    void ApplyStayEffect(Collider other);
}
