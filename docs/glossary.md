# 対訳表

ユビキタス言語（日本語）と、C# の実装名の対応を定めます。クラス名・プロパティ名・メソッド名は、
この表に従って記述します。表に無い用語が必要になった場合は、名前を定める前に確認してください。

対象は、`tests/BDD.AI.Coupons.Specs/Features/coupon.feature` のすべてのシナリオに必要な範囲です。
シナリオを追加するたびに、この表も更新します。

| ユビキタス言語 | 分類 | クラス | 属性・メソッド | 備考 |
|---|---|---|---|---|
| 金額 | 値オブジェクト | `Money` | | 負の値を許さない。 |
| 会員ランク | 値オブジェクト | `MembershipRank` | `Of(string)` | 一般 `General`・ゴールド `Gold`・プラチナ `Platinum` の3つの静的な読み取り専用インスタンスを持つ。コンストラクタは非公開。`Of` は日本語の名称からインスタンスを得る。引数の名前は `displayName`。 |
| 有効期限 | 値オブジェクト | `ExpirationDate` | | 当日まで有効。 |
| 券種コード | 値オブジェクト | `CouponCode` | | クーポンを識別する。 |
| 適用結果 | 値オブジェクト | `CouponApplicationResult` | | 成否・エラーメッセージ・割引額・支払金額。 |
| クーポン | エンティティ | `Coupon` | | 識別子は券種コード。 |
| 買い物かご | エンティティ | `Cart` |  |
| 割引計算 | ドメインサービス | `DiscountCalculationService` |  |
| クーポンを適用する | メソッド | `DiscountCalculationService` | `Apply(Coupon, Cart, DateTime)` | 戻り値は `CouponApplicationResult`、引数の名前は `coupon`、`cart`、`now`、ステップ定義はこれを呼び出す。判定は有効期限、最低購入金額の順に行い、両方を満たさない場合は有効期限切れのメッセージを返す。適用できない場合は `CouponApplicationResult.Failure` を返し、買い物かごを変更しない。適用できる場合は `CalculateDiscount` で割引額を計算し、`Cart.ApplyCoupon` で買い物かごに適用して `CouponApplicationResult.Success` を返す。 |
| クーポンが使えるか | メソッド | `DiscountCalculationService` | `CanApply(Coupon, Cart, DateTime)` | 戻り値は `bool`、引数の名前は `coupon`、`cart`、`now`。 |
| 業務ルール違反 | 例外 | `DomainException` | `DomainException(string)`、`DomainException(string, Exception)` | `Exception` を継承、値オブジェクトやエンティティの制約に反する値を渡された場合にスローする。原因となる例外をラップする場合は第2のコンストラクタを使用する。 |
| 券種コード | 属性 | `Coupon` | `CouponCode` | 型は `CouponCode`。 |
| 最低購入金額 | 属性 | `Coupon` | `MinimumPurchaseAmount` | 型は `Money`。 |
| 割引上限額 | 属性 | `Coupon` | `MaximumDiscountAmount` | 型は `Money`。 |
| 有効期限 | 属性 | `Coupon` | `ExpirationDate` | 型は `ExpirationDate`。 |
| 購入金額 | 属性 | `Cart` | `TotalAmount` | 型は `Money`。 |
| 会員ランク | 属性 | `Cart` | `MembershipRank` | 型は `MembershipRank`。 |
| 会員ランクの名称 | 属性 | `MembershipRank` | `DisplayName` | 型は `string`（一般・ゴールド・プラチナ）、`Of` はこの値でインスタンスを探索する。 |
| 成否 | 属性 | `CouponApplicationResult` | `Successful` | 型は `bool`。 |
| エラーメッセージ | 属性 | `CouponApplicationResult` | `ErrorMessage` | 型は `string?`（成功時は `null`）。 |
| 割引額 | 属性 | `CouponApplicationResult` | `DiscountAmount` | 型は `Money`。 |
| 支払金額 | 属性 | `CouponApplicationResult` | `PaymentAmount` | 型は `Money`。 |
| 金額の値 | 属性 | `Money` | `Amount` | 型は `long`（円単位）。 |
| 券種コードの値 | 属性 | `CouponCode` | `Value` | 型は `string`。 |
| 有効期限の日付 | 属性 | `ExpirationDate` | `Date` | 型は `DateOnly`。 |
| 金額が他の金額より少ないか | メソッド | `Money` | `IsLessThan(Money)` | 戻り値は `bool`、引数の名前は `other`。 |
| 指定した日が有効期限内か | メソッド | `ExpirationDate` | `IsValidOn(DateOnly)` | 当日まで有効。引数の日付が有効期限の日付以前であれば `true`。引数の名前は `date`。 |
| 最低購入金額を満たすか | メソッド | `Coupon` | `IsSatisfiedBy(Money)` | 引数は購入金額。購入金額が最低購入金額以上であれば `true`。引数の名前は `totalAmount`。 |
| クーポンが有効期限内か | メソッド | `Coupon` | `IsValidOn(DateTime)` | 引数の日時を日付に変換し、`ExpirationDate.IsValidOn` に委譲する。引数の名前は `now`。 |
| 同一のクーポンか | メソッド | `Coupon` | `Equals`、`GetHashCode` | 券種コードが等しければ同一とみなす（備考「識別子は券種コード」をコードで表す）。 |
| 適用の失敗 | メソッド | `CouponApplicationResult` | `Failure(string, Money)` | 静的ファクトリ。引数はエラーメッセージと購入金額。成否は `false`、割引額は 0 円、支払金額は購入金額とする。引数の名前は `errorMessage`、`totalAmount`。 |
| 最低購入金額未満のエラーメッセージ | 文言 | `DiscountCalculationService` | | 「{最低購入金額} 円以上のお買い上げでご利用いただけます」。金額は最低購入金額から生成し、3桁ごとにカンマで区切る。桁区切りは `CultureInfo.InvariantCulture` を指定して生成する。 |
| 有効期限切れのエラーメッセージ | 文言 | `DiscountCalculationService` | | 「このクーポンは有効期限が切れています」。 |
| 割引率 | 値オブジェクト | `DiscountRate` | `Percent` | パーセント単位。型は `int`。0〜100 以外は `DomainException`。 |
| 割引率 | 属性 | `MembershipRank` | `DiscountRate` | 型は `DiscountRate`。一般 5・ゴールド 10・プラチナ 15。 |
| 割引率を適用する | メソッド | `DiscountRate` | `ApplyTo(Money)` | 購入金額に割引率を掛け、1 円未満を切り捨てた金額を返す。戻り値は `Money`、引数の名前は `amount`。 |
| 割引額を計算する | メソッド | `DiscountCalculationService` | `CalculateDiscount(Coupon, Cart)` | 戻り値は `Money`。割引率の適用と割引上限の判定を行う。引数の名前は `coupon`、`cart`。 |
| 金額を減算する | メソッド | `Money` | `Subtract(Money)` | 戻り値は `Money`。結果が負になる場合は `DomainException`。引数の名前は `other`。 |
| ポイント数 | 値オブジェクト | `Points` | `Value` | 型は `long`。負の値を許さない。 |
| ポイントの換算 | 値オブジェクト | `PointRate` | `Points`、`Yen` | 「{int} ポイントを {int} 円として計算する」。型はいずれも `int`。`Points` は 1 以上、`Yen` は 0 以上。 |
| ポイントを金額に換算する | メソッド | `PointRate` | `ToMoney(Points)` | 戻り値は `Money`。1 円未満は切り捨てる。引数の名前は `points`。 |
| 適用中のクーポン | 属性 | `Cart` | `AppliedCoupon` | 型は `Coupon?`（未適用の場合は `null`）。 |
| 割引額 | 属性 | `Cart` | `DiscountAmount` | 型は `Money`。 |
| 利用ポイント | 属性 | `Cart` | `UsedPoints` | 型は `Points`。 |
| ポイントによる値引き額 | 属性 | `Cart` | `PointDiscountAmount` | 型は `Money`。 |
| 支払金額 | 属性 | `Cart` | `PaymentAmount` | 型は `Money`。購入金額 − 割引額 − ポイントによる値引き額。 |
| クーポンを適用する（状態の変更） | メソッド | `Cart` | `ApplyCoupon(Coupon, Money)` | 割引額を設定し、ポイントの利用を解除する。引数の名前は `coupon`、`discountAmount`。 |
| クーポンを解除する | メソッド | `Cart` | `CancelCoupon(CouponCode)` | 適用中のクーポンと異なる券種コード、または未適用の場合は `DomainException`。引数の名前は `couponCode`。 |
| ポイントを利用する | メソッド | `Cart` | `UsePoints(Points, PointRate)` | クーポンの適用中は `DomainException`。引数の名前は `points`、`pointRate`。 |
| クーポンが適用中か | メソッド | `Cart` | `HasCoupon()` | 戻り値は `bool`。 |
| ポイントを利用する（判定） | メソッド | `DiscountCalculationService` | `UsePoints(Cart, Points, PointRate)` | 戻り値は `PointUsageResult`。クーポンの適用中は `PointUsageResult.Failure` を返す。引数の名前は `cart`、`points`、`pointRate`。 |
| クーポンを解除する（呼び出し） | メソッド | `DiscountCalculationService` | `Cancel(Cart, CouponCode)` | `Cart.CancelCoupon` を呼び出す。引数の名前は `cart`、`couponCode`。 |
| 適用された割引率 | 属性 | `CouponApplicationResult` | `AppliedDiscountRate` | 型は `DiscountRate`（失敗時は 0 パーセント）。 |
| 割引上限の適用 | 属性 | `CouponApplicationResult` | `MaximumDiscountApplied` | 型は `bool`。 |
| ポイントの利用の解除 | 属性 | `CouponApplicationResult` | `PointUsageCancelled` | 型は `bool`。 |
| 適用の成功 | メソッド | `CouponApplicationResult` | `Success(DiscountRate, Money, Money, bool, bool)` | 静的ファクトリ。引数の名前は `appliedDiscountRate`、`discountAmount`、`paymentAmount`、`maximumDiscountApplied`、`pointUsageCancelled`。 |
| ポイントの利用結果 | 値オブジェクト | `PointUsageResult` | `Successful`、`ErrorMessage`、`Success()`、`Failure(string)` | 成否とエラーメッセージ（成功時は `null`）。`Failure` の引数の名前は `errorMessage`。 |
| 併用不可のエラーメッセージ | 文言 | `DiscountCalculationService` | | 「クーポンとポイントは併用できません」。 |

## ステップ定義

ステップ定義は、テストプロジェクトの `BDD.AI.Coupons.Specs.Steps` 名前空間の `CouponSteps` クラスに記述します。
属性には `.feature` のステップ文をそのまま使用し、メソッド名は次の表に従います。
`{string}` と `{int}` は Reqnroll のパラメータで、それぞれ `string` と `int` の引数になります。

| 種別 | ステップ文 | メソッド名 |
|---|---|---|
| 前提 | クーポン {string} が存在する | `CouponExists` |
| 前提 | クーポン {string} の最低購入金額は {int} 円である | `CouponHasMinimumPurchaseAmount` |
| 前提 | クーポン {string} の割引上限額は {int} 円である | `CouponHasMaximumDiscountAmount` |
| 前提 | クーポン {string} の有効期限は {string} である | `CouponExpiresOn` |
| 前提 | 割引額の端数は切り捨てる | `DiscountFractionIsTruncated` |
| 前提 | ポイントは {int} ポイントを {int} 円として計算する | `PointRateIs` |
| 前提 | 現在日時は {string} である | `CurrentDateTimeIs` |
| 前提 | ポイントを利用しない | `PointsAreNotUsed` |
| 前提 | 会員ランクは {string} である | `MembershipRankIs` |
| 前提 | カートの購入金額は {int} 円である | `CartTotalAmountIs` |
| もし | クーポン {string} を適用する | `ApplyCoupon` |
| ならば | クーポンの適用は失敗する | `CouponApplicationFails` |
| ならば | エラーメッセージ {string} が表示される | `ErrorMessageIsShown` |
| ならば | 割引額は {int} 円である | `DiscountAmountIs` |
| ならば | 支払金額は {int} 円である | `PaymentAmountIs` |
| ならば | クーポンの適用は成功する | `CouponApplicationSucceeds` |
| ならば | 適用された割引率は {int} パーセントである | `AppliedDiscountRateIs` |
| ならば | 割引上限が適用される | `MaximumDiscountIsApplied` |
| 前提 | クーポン {string} を適用済みである | `CouponIsAlreadyApplied` |
| もし | クーポン {string} を解除する | `CancelCoupon` |
| 前提・もし | ポイントを {int} ポイント利用する | `UsePoints` |
| ならば | ポイントの利用は成功する | `PointUsageSucceeds` |
| ならば | ポイントの利用は失敗する | `PointUsageFails` |
| ならば | ポイントの利用は解除される | `PointUsageIsCancelled` |
| ならば | 利用ポイントは {int} ポイントである | `UsedPointsIs` |
| 前提・ならば | ポイントによる値引き額は {int} 円である | `PointDiscountAmountIs` |

「支払金額は {int} 円である」「ポイントによる値引き額は {int} 円である」は、前提の中にも記述されているため、
`[Given]` と `[Then]` の両方を付けて `Assert` で検証します。「ポイントを {int} ポイント利用する」は、
`[Given]` と `[When]` の両方を付けます。

メソッド名は、前提では状況を表す名前（`CouponExists` など）、もしでは動詞から始まる名前（`ApplyCoupon`）、
ならばでは検証する内容を表す名前（`CouponApplicationFails` など）とします。

`.feature` で「かつ」と記述されたステップは、直前のステップと同じ種別（`[Given]` または `[Then]`）で
定義します。金額は `{int}` で受け取り、`int` の引数とします。`Money` への変換はステップ定義の中で行います。

### ステップ定義が保持する状態

背景の各ステップで受け取った値はフィールドに保持し、`Coupon` と `Cart` は最初に必要になったステップで
生成します。「クーポン {string} を適用済みである」「ポイントを {int} ポイント利用する」は前提の中にも
記述されているため、「もし」より前に買い物かごが必要になるためです。

「割引額は {int} 円である」「支払金額は {int} 円である」は、適用結果ではなく買い物かごから読んで検証します。
ポイントに関するシナリオでは、クーポンの適用結果が存在しないことがあるためです。
「エラーメッセージ {string} が表示される」は、直前の操作（クーポンの適用、またはポイントの利用）の
エラーメッセージを検証します。

| 用語 | フィールド | 型 | 設定するステップ |
|---|---|---|---|
| 券種コード | `_couponCode` | `CouponCode` | クーポン {string} が存在する |
| 最低購入金額 | `_minimumPurchaseAmount` | `Money` | クーポン {string} の最低購入金額は {int} 円である |
| 割引上限額 | `_maximumDiscountAmount` | `Money` | クーポン {string} の割引上限額は {int} 円である |
| 有効期限 | `_expirationDate` | `ExpirationDate` | クーポン {string} の有効期限は {string} である |
| 現在日時 | `_now` | `DateTime` | 現在日時は {string} である |
| 会員ランク | `_membershipRank` | `MembershipRank` | 会員ランクは {string} である |
| 購入金額 | `_totalAmount` | `Money` | カートの購入金額は {int} 円である |
| 適用結果 | `_result` | `CouponApplicationResult` | クーポン {string} を適用する、クーポン {string} を適用済みである |
| ポイントの換算 | `_pointRate` | `PointRate` | ポイントは {int} ポイントを {int} 円として計算する |
| クーポン | `_coupon` | `Coupon?` | 最初に必要になったステップ |
| 買い物かご | `_cart` | `Cart?` | 最初に必要になったステップ |
| ポイントの利用結果 | `_pointUsageResult` | `PointUsageResult` | ポイントを {int} ポイント利用する |
| エラーメッセージ | `_errorMessage` | `string?` | クーポン {string} を適用する、ポイントを {int} ポイント利用する など、直前の操作 |
| 割引計算 | `_discountCalculationService` | `DiscountCalculationService` | （Reqnroll のコンテキスト注入によりコンストラクタで受け取る） |

`.feature` の文字列から日付と日時への変換はステップ定義で行います。書式は、日付が `yyyy-MM-dd`、
日時が `yyyy-MM-dd HH:mm:ss` です。

「割引額の端数は切り捨てる」は、切り捨てがドメインの固定の規則であるため何もしません
（`docs/decisions.md` の 7）。「ポイントを利用しない」は、生成した買い物かごがポイントを利用していない
状態であるため何もしません。

## 補足

**1つの用語が1つのクラスになるとは限りません。**
最低購入金額、購入金額、支払金額、割引額は、概念としてはいずれも金額です。違いは、
どのような役割で用いられているかだけです。したがって、クラスは `Money` のみとし、役割はプロパティ名で表します。
別の型に分けるのは、その概念だけが持つ制約がある場合や、取り違えを型で防ぎたい場合に限られます。

**.feature に現れない構成要素もあります。**
「ならば クーポンの適用は失敗する」「かつ エラーメッセージ … が表示される」を検証するには、
適用した結果を受け取る型が必要です。`CouponApplicationResult` がこれにあたります。

**値オブジェクトの内部も、この表で定義します。**
`Money` や `CouponCode` は、クラス名だけでは実装できません。保持する値まで定義して、
はじめて `record` として記述できます。金額は円単位の整数で扱えるため `long` とし、
整数のみの計算にしています。

**会員ランクは、静的インスタンスを持つクラスにしています。**
取りうる値は一般・ゴールド・プラチナの3つに限られますが、C# の `enum` は整数に名前を付けたものであり、
日本語の名称を保持できません。そこで、`General`、`Gold`、`Platinum` の3つを静的な読み取り専用の
インスタンスとして持ち、コンストラクタを非公開にして、これ以外のインスタンスを生成できないようにします。
`.feature` には「一般」と日本語で記述されていますので、日本語の名称からインスタンスを得る `Of(string)` を
用意します。この変換をステップ定義に記述すると、ステップ定義が業務の知識を持つことになるためです。

**現在日時は `Apply` の引数で受け取るようにしています。**
背景には「現在日時は "2026-09-20 10:00:00" である」が記述されています。この日時で有効期限を判定するには、
ドメインサービスが背景で指定された日時を使用する必要があります。ドメインサービスの内部で
`DateTime.Now` のように実際の現在時刻を取得する実装にすると、背景の指定とは無関係に
実行した日の日時で判定されてしまい、シナリオの結果が実行日によって変わります。

**業務ルールによる不成立と、プログラムの誤りを分けます。**
処理が正常に完了しない場合には、業務ルールによる不成立とプログラムの誤りの2種類があり、この2つを区別して扱います。
クーポンを使えないことは業務ルールによる不成立で、`.feature` にも「ならば クーポンの適用は失敗する」と
記述されているとおり、業務としては正常な結果です。したがって例外にはせず、`CouponApplicationResult` の
成否とエラーメッセージで呼び出し側に返します。一方、金額に負の値が渡されることはプログラムの誤りで、
仕様のどこにも記述されていません。これは正常な結果として返すものではないため、`DomainException` を
スローして処理を中断します。対訳表に「業務ルール違反」として `DomainException` の行があるのは、
この例外を表すためです。