namespace BDD.AI.Coupons.Domain.Models;

/// <summary>
/// 適用結果
/// クーポンを使えないことは業務上の正常な結果であるため、例外ではなくこの型で返す。
/// </summary>
/// <param name="Successful">成否</param>
/// <param name="ErrorMessage">エラーメッセージ（成功時は null）</param>
/// <param name="DiscountAmount">割引額</param>
/// <param name="PaymentAmount">支払金額</param>
/// <param name="AppliedDiscountRate">適用された割引率（失敗時は 0 パーセント）</param>
/// <param name="MaximumDiscountApplied">割引上限の適用</param>
/// <param name="PointUsageCancelled">ポイントの利用の解除</param>
public record CouponApplicationResult(
    bool Successful,
    string? ErrorMessage,
    Money DiscountAmount,
    Money PaymentAmount,
    DiscountRate AppliedDiscountRate,
    bool MaximumDiscountApplied,
    bool PointUsageCancelled)
{
    /// <summary>
    /// 適用の成功。
    /// </summary>
    /// <param name="appliedDiscountRate">適用された割引率</param>
    /// <param name="discountAmount">割引額</param>
    /// <param name="paymentAmount">支払金額</param>
    /// <param name="maximumDiscountApplied">割引上限が適用された場合は true</param>
    /// <param name="pointUsageCancelled">クーポンの適用によりポイントの利用が解除された場合は true</param>
    /// <returns>成功を表す適用結果</returns>
    public static CouponApplicationResult Success(
        DiscountRate appliedDiscountRate,
        Money discountAmount,
        Money paymentAmount,
        bool maximumDiscountApplied,
        bool pointUsageCancelled)
    {
        return new CouponApplicationResult(
            true,
            null,
            discountAmount,
            paymentAmount,
            appliedDiscountRate,
            maximumDiscountApplied,
            pointUsageCancelled);
    }

    /// <summary>
    /// 適用の失敗。
    /// 割引されないため、割引額は 0 円、支払金額は購入金額のままとなる。
    /// </summary>
    /// <param name="errorMessage">エラーメッセージ</param>
    /// <param name="totalAmount">購入金額</param>
    /// <returns>失敗を表す適用結果</returns>
    public static CouponApplicationResult Failure(string errorMessage, Money totalAmount)
    {
        return new CouponApplicationResult(
            false,
            errorMessage,
            new Money(0),
            totalAmount,
            new DiscountRate(0),
            false,
            false);
    }
}
