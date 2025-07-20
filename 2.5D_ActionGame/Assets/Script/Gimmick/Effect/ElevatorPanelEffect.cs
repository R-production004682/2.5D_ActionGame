using Const;
using UnityEngine;

public class ElevatorPanelEffect : MonoBehaviour, IGimmickEffect
{
    [SerializeField] private ElevatorID elevatorID;
    private GimmickContext context;

    public void Initialize(GimmickContext context)
    {
        this.context = context;
    }

    /// <summary>
    ///  UIを出したりとかで使用する（例 : 「Wキーを押してね」的なやつ）
    /// </summary>
    /// <param name="other"></param>
    public void ApplyEnterEffect(Collider other) { }

    public void ApplyStayEffect(Collider other)
    {
        if (!other.CompareTag(TagInfo.PLAYER))
        {
            return;
        }

        var gameManagerInstance = GameManager.Instance;
        var currentCoinNum = gameManagerInstance.GetCoinCount();

        if (InputBuffer.Instance.ConsumeElevator() && GimmickInfo.REQUIRED_COIN_NUM <= currentCoinNum)
        {
            gameManagerInstance.SubCoins(GimmickInfo.REQUIRED_COIN_NUM);

            var thisPoint = GetComponentInChildren<ElevatorPoint>();
            if (thisPoint == null)
            {
                Debug.LogError("このオブジェクトに ElevatorPoint がアタッチされていません！");
                return;
            }

            var pairedPoint = ElevatorManager.Instance.GetExitPoint(elevatorID, thisPoint);
            if (pairedPoint != null)
            {
                other.transform.position = pairedPoint.position;
                Debug.Log("ワープしました！");
            }
            else
            {
                Debug.LogWarning("対応するエレベーターの出口が見つかりません！");
            }
        }
    }
}