using UnityEngine;
using UnityEditor;

public class SceneMemo : MonoBehaviour
{
    [TextArea(1, 20)]
    public string memoText = "ここにメモを記入";
    public Color textColor = Color.yellow;
    public Vector3 offset = Vector3.up;
}

[InitializeOnLoad]
public class SceneMemoDrawer
{
    static SceneMemoDrawer()
    {
        SceneView.duringSceneGui += OnSceneGUI;
    }

    static void OnSceneGUI(SceneView sceneView)
    {
        Handles.BeginGUI();

        foreach (SceneMemo memo in GameObject.FindObjectsOfType<SceneMemo>())
        {
            if (string.IsNullOrEmpty(memo.memoText)) continue;

            Vector3 worldPos = memo.transform.position + memo.offset;
            Vector2 guiPoint = HandleUtility.WorldToGUIPoint(worldPos);

            GUI.color = memo.textColor;
            GUIStyle style = new GUIStyle(EditorStyles.label)
            {
                fontSize = 12,
                fontStyle = FontStyle.Bold,
                normal = new GUIStyleState { textColor = memo.textColor }
            };

            GUI.Label(new Rect(guiPoint.x, guiPoint.y, 400, 60), memo.memoText, style);
        }

        Handles.EndGUI();
    }
}
