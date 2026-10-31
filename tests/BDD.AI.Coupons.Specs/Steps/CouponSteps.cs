using Reqnroll;

namespace BDD.AI.Coupons.Specs.Steps;

/// <summary>
/// クーポンによる割引の適用のステップ定義
/// </summary>
[Binding]
public class CouponSteps
{
    /// <summary>
    /// 前提 クーポン {string} が存在する
    /// </summary>
    /// <param name="couponCode">券種コード</param>
    [Given("クーポン {string} が存在する")]
    public void CouponExists(string couponCode)
    {
        throw new PendingStepException();
    }

    /// <summary>
    /// 前提 クーポン {string} の最低購入金額は {int} 円である
    /// </summary>
    /// <param name="couponCode">券種コード</param>
    /// <param name="minimumPurchaseAmount">最低購入金額（円）</param>
    [Given("クーポン {string} の最低購入金額は {int} 円である")]
    public void CouponHasMinimumPurchaseAmount(string couponCode, int minimumPurchaseAmount)
    {
        throw new PendingStepException();
    }

    /// <summary>
    /// 前提 クーポン {string} の割引上限額は {int} 円である
    /// </summary>
    /// <param name="couponCode">券種コード</param>
    /// <param name="maximumDiscountAmount">割引上限額（円）</param>
    [Given("クーポン {string} の割引上限額は {int} 円である")]
    public void CouponHasMaximumDiscountAmount(string couponCode, int maximumDiscountAmount)
    {
        throw new PendingStepException();
    }

    /// <summary>
    /// 前提 クーポン {string} の有効期限は {string} である
    /// </summary>
    /// <param name="couponCode">券種コード</param>
    /// <param name="expirationDate">有効期限</param>
    [Given("クーポン {string} の有効期限は {string} である")]
    public void CouponExpiresOn(string couponCode, string expirationDate)
    {
        throw new PendingStepException();
    }

    /// <summary>
    /// 前提 割引額の端数は切り捨てる
    /// </summary>
    [Given("割引額の端数は切り捨てる")]
    public void DiscountFractionIsTruncated()
    {
        throw new PendingStepException();
    }

    /// <summary>
    /// 前提 ポイントは {int} ポイントを {int} 円として計算する
    /// </summary>
    /// <param name="points">ポイント数</param>
    /// <param name="yen">円</param>
    [Given("ポイントは {int} ポイントを {int} 円として計算する")]
    public void PointRateIs(int points, int yen)
    {
        throw new PendingStepException();
    }

    /// <summary>
    /// 前提 現在日時は {string} である
    /// </summary>
    /// <param name="now">現在日時</param>
    [Given("現在日時は {string} である")]
    public void CurrentDateTimeIs(string now)
    {
        throw new PendingStepException();
    }

    /// <summary>
    /// 前提 ポイントを利用しない
    /// </summary>
    [Given("ポイントを利用しない")]
    public void PointsAreNotUsed()
    {
        throw new PendingStepException();
    }

    /// <summary>
    /// 前提 会員ランクは {string} である
    /// </summary>
    /// <param name="membershipRank">会員ランクの名称</param>
    [Given("会員ランクは {string} である")]
    public void MembershipRankIs(string membershipRank)
    {
        throw new PendingStepException();
    }

    /// <summary>
    /// 前提 カートの購入金額は {int} 円である
    /// </summary>
    /// <param name="totalAmount">購入金額（円）</param>
    [Given("カートの購入金額は {int} 円である")]
    public void CartTotalAmountIs(int totalAmount)
    {
        throw new PendingStepException();
    }

    /// <summary>
    /// もし クーポン {string} を適用する
    /// </summary>
    /// <param name="couponCode">券種コード</param>
    [When("クーポン {string} を適用する")]
    public void ApplyCoupon(string couponCode)
    {
        throw new PendingStepException();
    }

    /// <summary>
    /// ならば クーポンの適用は失敗する
    /// </summary>
    [Then("クーポンの適用は失敗する")]
    public void CouponApplicationFails()
    {
        throw new PendingStepException();
    }

    /// <summary>
    /// ならば エラーメッセージ {string} が表示される
    /// </summary>
    /// <param name="errorMessage">エラーメッセージ</param>
    [Then("エラーメッセージ {string} が表示される")]
    public void ErrorMessageIsShown(string errorMessage)
    {
        throw new PendingStepException();
    }

    /// <summary>
    /// ならば 割引額は {int} 円である
    /// </summary>
    /// <param name="discountAmount">割引額（円）</param>
    [Then("割引額は {int} 円である")]
    public void DiscountAmountIs(int discountAmount)
    {
        throw new PendingStepException();
    }

    /// <summary>
    /// ならば 支払金額は {int} 円である
    /// </summary>
    /// <param name="paymentAmount">支払金額（円）</param>
    [Then("支払金額は {int} 円である")]
    public void PaymentAmountIs(int paymentAmount)
    {
        throw new PendingStepException();
    }
}
