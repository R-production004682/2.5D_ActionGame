using System;

namespace Const 
{
    /// <summary>
    /// コインの情報
    /// </summary>
    public static class CoinInfo
    {        
        // コインの価値
        public const int COIN_VALUE_NORMAL = 1;
        public const int COIN_VALUE_SILVER = 5;
        public const int COIN_VALUE_GOLD = 10;
    }

    /// <summary>
    /// タグを管理
    /// </summary>
    public static class TagInfo
    {
        // Playerのタグ
        public const string PLAYER = "Player";

        // コインのタグ
        public const string COIN_NORMAL = "Normal";
        public const string COIN_SILVER = "Silver";
        public const string COIN_GOLD = "Gold";

        // Gimmick
        public const string MOVE_PLATFORM = "MovePlatform";

        // Environment
        public const string PLANE = "Plane";
        public const string WALL = "Wall";

        public const string LAYER_GROUND = "Ground";
    }

    public static class GimmickInfo
    {
        // 動く床の目的地へ到達したかどうかを判断する閾値
        public const float MOVE_FLOOR_REACH_THRESHOLD = 0.01f;

        // Playerを動く床と同期させるための速度調整係数
        public const float VELOCITY_SCALE_FACTOR = 1.75f;

        // Gimmickを動かすためのコインの必要枚数
        public const int REQUIRED_COIN_NUM = 10;
    }

    public static class PhysicsInfo
    {
        // 接地しているかどうかを判断するための閾値
        public const float VERSATILE_THRESHOLD = 0.05f;

        // Playerの接壁判定に使用するレイキャストの長さ
        public const float WALL_CHECK_RAYCAST_LENGTH = 0.5f;
    }
}
