## 言語
- レビューのコメントは **日本語** で記述してください。
- 専門用語やライブラリ名は英語のままで構いませんが、補足説明があると助かります。

## コメントスタイル
- 改善提案は「なぜそれが望ましいのか」理由も添えてください。
- 明確な問題がある場合は「バグ」「セキュリティリスク」「非効率」などカテゴリを明示してください。
- 細かいスタイルの指摘（インデント、命名など）はまとめて書いても構いません。
- 特に目立った修正点が無いのであれば、問題ない旨の回答をしてください。
- レビューする際は、**.csファイルのみ**レビューしてください。
- 初回に出力した解答以外の指摘点を追加レビューの際に出さないでください。
- 初回に出力された問題点が修正された場合、問題ない旨の回答をしてください。
- 初回に出力された問題点が修正しきれていない場合は、直すか、コメントで問題ない旨の回答が来るまで指摘してください。

## 優先順位の区別
- コメントには重要度を添えてください（例: `必須`, `推奨`, `任意`）。
  - **必須**: 動作に影響する、バグやセキュリティリスクを含む場合。
  - **推奨**: パフォーマンス、読みやすさ、保守性向上のための提案。
  - **任意**: スタイルや個人の好みレベルの意見。

## 対応の方向性
- 冗長なコードは簡潔化を提案してください。
- 同じ処理が繰り返されている場合、共通化や関数化を提案してください。
- 型や null チェックが不十分な場合は指摘してください。

## NG事項
- 曖昧なフィードバック（例:「なんとなく読みにくい」など）は避けてください。
- 修正案を提示せずに否定的なだけのコメントはしないでください。
- コードのコメントの文字化けについての指摘は避けてください。
- レビューを依頼する毎に何度も小出しにして改善コメントを出さないでください。

## その他
- テストコードが不足している場合は、その指摘もお願いします。
- ドキュメント不足・コメント不足もあれば指摘してください。
- フレームワークのベストプラクティスに反している場合は、その情報源と一緒に教えてください。

