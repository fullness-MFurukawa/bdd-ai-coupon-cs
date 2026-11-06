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

    /// <summary>
    /// 最低購入金額を満たすか。
    /// </summary>
    /// <param name="totalAmount">購入金額</param>
    /// <returns>購入金額が最低購入金額以上であれば true</returns>
    public bool IsSatisfiedBy(Money totalAmount)
    {
        return !totalAmount.IsLessThan(MinimumPurchaseAmount);
    }

    /// <summary>
    /// クーポンが有効期限内か。
    /// 有効期限は日単位で定められているため、時刻は判定に用いない。
    /// </summary>
    /// <param name="now">現在日時</param>
    /// <returns>現在日時の日付が有効期限内であれば true</returns>
    public bool IsValidOn(DateTime now)
    {
        return ExpirationDate.IsValidOn(DateOnly.FromDateTime(now));
    }

    /// <summary>
    /// 同一のクーポンか。
    /// 識別子である券種コードが等しければ同一とみなす。
    /// </summary>
    /// <param name="obj">比較する対象</param>
    /// <returns>券種コードが等しいクーポンであれば true</returns>
    public override bool Equals(object? obj)
    {
        return obj is Coupon other && CouponCode == other.CouponCode;
    }

    /// <summary>
    /// 券種コードに基づくハッシュコードを返す。
    /// </summary>
    /// <returns>ハッシュコード</returns>
    public override int GetHashCode()
    {
        return CouponCode.GetHashCode();
    }
}
