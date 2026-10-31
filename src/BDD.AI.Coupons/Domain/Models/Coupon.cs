namespace BDD.AI.Coupons.Domain.Models;

/// <summary>
/// クーポン
/// 識別子は券種コード。
/// </summary>
public class Coupon
{
    /// <summary>
    /// クーポンを生成する。
    /// </summary>
    /// <param name="couponCode">券種コード</param>
    /// <param name="minimumPurchaseAmount">最低購入金額</param>
    /// <param name="maximumDiscountAmount">割引上限額</param>
    /// <param name="expirationDate">有効期限</param>
    public Coupon(
        CouponCode couponCode,
        Money minimumPurchaseAmount,
        Money maximumDiscountAmount,
        ExpirationDate expirationDate)
    {
        CouponCode = couponCode;
        MinimumPurchaseAmount = minimumPurchaseAmount;
        MaximumDiscountAmount = maximumDiscountAmount;
        ExpirationDate = expirationDate;
    }

    /// <summary>券種コード</summary>
    public CouponCode CouponCode { get; }

    /// <summary>最低購入金額</summary>
    public Money MinimumPurchaseAmount { get; }

    /// <summary>割引上限額</summary>
    public Money MaximumDiscountAmount { get; }

    /// <summary>有効期限</summary>
    public ExpirationDate ExpirationDate { get; }
}
