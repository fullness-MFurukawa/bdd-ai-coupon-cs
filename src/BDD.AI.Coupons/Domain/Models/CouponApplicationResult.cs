namespace BDD.AI.Coupons.Domain.Models;

/// <summary>
/// 適用結果
/// クーポンを使えないことは業務上の正常な結果であるため、例外ではなくこの型で返す。
/// </summary>
/// <param name="Successful">成否</param>
/// <param name="ErrorMessage">エラーメッセージ（成功時は null）</param>
/// <param name="DiscountAmount">割引額</param>
/// <param name="PaymentAmount">支払金額</param>
public record CouponApplicationResult(
    bool Successful,
    string? ErrorMessage,
    Money DiscountAmount,
    Money PaymentAmount)
{
    /// <summary>
    /// 適用の失敗。
    /// 割引されないため、割引額は 0 円、支払金額は購入金額のままとなる。
    /// </summary>
    /// <param name="errorMessage">エラーメッセージ</param>
    /// <param name="totalAmount">購入金額</param>
    /// <returns>失敗を表す適用結果</returns>
    public static CouponApplicationResult Failure(string errorMessage, Money totalAmount)
    {
        return new CouponApplicationResult(false, errorMessage, new Money(0), totalAmount);
    }
}
