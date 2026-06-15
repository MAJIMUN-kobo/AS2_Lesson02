using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class UiMainGame : MonoBehaviour
{
    // UIDocumentを参照して、UIを扱う
    [Header("* * * UI Document * * *")]
    public UIDocument uid;

    private VisualElement _root;    // UIの親
    private Label _scoreText;       // スコアのテキスト表示
    private Button _gameQuitButton; // ゲーム終了ボタン

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // ルート要素（親）の取得
        _root = uid.rootVisualElement;

        // === スコアテキストの取得 === //
        _scoreText = _root.Q<Label>("score-label");
        // スコアテキストの更新
        _scoreText.text = "ココにスコアの文字列を表示する。";

        // === ゲーム終了ボタンの取得 === //
        _gameQuitButton = _root.Q<Button>("game-quit-button");
        // ボタンの押下イベントの設定
        _gameQuitButton.clicked += () => 
        { 
            Debug.Log("ゲーム終了");

#if UNITY_EDITOR
            // エディタの再生を止める（エディタ用）
            EditorApplication.isPlaying = false;
#endif
            // アプリケーションを終了させる（ビルド用）
            Application.Quit();
        };

        // === 要素を新規作成する === //
        /*
        Slider slider = new Slider("sample-slider");
        VisualElement option = _root.Q<VisualElement>("option-container");
        option.Add(slider);
        */
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
