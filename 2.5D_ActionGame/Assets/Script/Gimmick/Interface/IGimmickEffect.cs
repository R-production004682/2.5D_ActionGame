using UnityEngine;

public interface IGimmickEffect
{
    void Initialize(GimmickContext context);
    void ApplyEffect(Collider other);
}
