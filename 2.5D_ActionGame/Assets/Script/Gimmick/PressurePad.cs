using Const;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePad : MonoBehaviour, IGimmickEffect
{
    private GimmickContext gimmickContext;

    public void Initialize(GimmickContext context)
    {
        gimmickContext = context;
    }

    public void ApplyEnterEffect(Collider other)
    {
    }

    public void ApplyStayEffect(Collider other)
    {
        // 移動可能オブジェクトが接触した場合の処理
        if (other.CompareTag(TagInfo.MOVABLE_OBJECT))
        {
            var distance = Vector3.Distance(transform.position, other.transform.position);
            Debug.Log($"Distance: {distance}");

            // 所定の位置まで移動後、絶対不変の位置に固定する
            if (distance < GimmickInfo.MOVE_FLOOR_CONTACT_THRESHOLD)
            {
                var rigidbody = other.GetComponent<Rigidbody>();
                if (rigidbody != null)
                {
                    rigidbody.isKinematic = true;
                    rigidbody.velocity = Vector3.zero;
                }
                other.transform.position = transform.position;
            }

            var meshRenderer = GetComponent<MeshRenderer>();
            if(meshRenderer != null)
            {
                meshRenderer.material.color = Color.blue;
            }

            // 何度も判定を取られては処理が重くなるため、
            // Gimmickに接地したotherGameObjectのタグを変更して再度判定されないようにする
            other.gameObject.tag = TagInfo.DEFAULT;
        }
    }

}
