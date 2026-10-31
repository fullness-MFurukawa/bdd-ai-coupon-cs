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
    Money PaymentAmount);
