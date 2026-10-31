# bdd-ai-coupon-cs

note の連載「BDD × AI」のサンプルコードです。Gherkin で記述した仕様（`.feature`）を、
C# と Reqnroll で実行します。

同じ `.feature` を他の言語でも実行します。言語ごとにリポジトリを分けていますが、
`.feature` の記述はすべて同一です。

| 言語 | リポジトリ |
|------|-----------|
| Java | [bdd-ai-coupon-java](https://github.com/fullness-MFurukawa/bdd-ai-coupon-java) |
| C# | bdd-ai-coupon-cs（本リポジトリ） |
| Python | bdd-ai-coupon-python（予定） |

---

## 連載記事

| 記事 | タイトル |
|------|---------|
| 第2回 | [仕様がそのまま品質基準になる——AIと作るGherkin（ガーキン）入門](https://note.com/fullness_dev/n/n8afa12c67ee3) |
| C#編-① | 仕様から実装へ（C#編-①）——.feature を実行するコードを AI に生成させる |

記事の公開時点のコードは、タグから取得できます。記事の手順に沿って実行する場合は、
対応するタグをチェックアウトしてください。

```bash
git clone https://github.com/fullness-MFurukawa/bdd-ai-coupon-cs.git
cd bdd-ai-coupon-cs
git checkout v1   # C#編-①の公開時点
```

| タグ | 状態 |
|------|------|
| `v1` | ドメインモデルのスケルトンとステップ定義のひな型。15ステップが定義済みで、最初のステップが pending で停止する |

---

## 題材

ECサイトのクーポン機能を題材としています。購入金額や会員ランク、有効期限、
ポイントとの併用可否によって、クーポンを使えるかどうかと割引額が決まります。

要件とシナリオの詳細は第2回の記事を参照してください。

---

## 実行環境

| 項目 | バージョン |
|------|-----------|
| .NET SDK | 10.0 |
| Reqnroll | 3.3.4 |
| MSTest | 4.0.1 |

### VS Code の拡張機能

| 拡張機能 | 発行元 |
|---------|-------|
| C# Dev Kit | Microsoft |
| Cucumber | Cucumber（CucumberOpen） |

C# Dev Kit には、C# の言語サポート、ソリューションエクスプローラー、テストエクスプローラーが
含まれます。Cucumber の拡張機能は、`.feature` の構文強調と補完を提供します。

---

## 実行方法

```bash
dotnet test --logger "console;verbosity=detailed"
```

`--logger` の指定により、`.feature` の各ステップが実行結果とともに表示されます。

`tests/BDD.AI.Coupons.Specs/reqnroll.json` には、未定義や未実装のステップを含むシナリオを
失敗として扱う設定（`missingOrPendingStepsOutcome: Error`）を記述しています。
この設定が無い場合、Reqnroll はそれらのシナリオを「結論なし（Inconclusive）」として扱い、
`dotnet test` はスキップとして報告します。

`.feature` からビルド時に生成される `.feature.cs` は、`obj` の下に出力する設定にしています。
編集しないでください。

---

## ディレクトリ構成

```
bdd-ai-coupon-cs/
├── docs/
│   └── glossary.md                       対訳表（ユビキタス言語と実装名の対応）
├── src/
│   └── BDD.AI.Coupons/
│       └── Domain/
│           ├── Models/                   値オブジェクト・エンティティ
│           ├── Services/                 ドメインサービス
│           └── Exceptions/               DomainException
├── tests/
│   └── BDD.AI.Coupons.Specs/
│       ├── Features/                     .feature
│       ├── Steps/                        ステップ定義
│       └── reqnroll.json                 Reqnroll の設定
├── CLAUDE.md                             Claude Code への指示（規約）
└── BDD.AI.Coupons.slnx
```

### 設計方針

詳細は [CLAUDE.md](CLAUDE.md) に記述しています。主な方針は次のとおりです。

- 値オブジェクトは `record` で実装します
- エンティティは `class` で実装し、プロパティは `get` のみとします。値はコンストラクタで設定します
- エンティティには、状態を変更するメソッドではなく、業務上の判断を表すメソッドを持たせます
- クーポンを使えないことは業務上の正常な結果として `CouponApplicationResult` で返し、
  業務上の制約に反する値は `DomainException` で報告します
- ドメイン層は ASP.NET Core や Entity Framework に依存させません

---

## 対訳表

ユビキタス言語は日本語、C# のクラス名は英語になります。訳し方が一つに決まらないため、
どの用語をどの名前で実装するかを [docs/glossary.md](docs/glossary.md) にまとめています。

他の言語のリポジトリにも同じ表を配置しています。言語ごとに変わるのは実装名の列だけです。

---

## ライセンス

MIT License

本リポジトリのコードは、自由に複製・改変してご利用いただけます。