using System.Globalization;
using BDD.AI.Coupons.Domain.Models;

namespace BDD.AI.Coupons.Domain.Services;

/// <summary>
/// 割引計算
/// </summary>
public class DiscountCalculationService
{
    private const string ExpiredMessage = "このクーポンは有効期限が切れています";

    private const string MinimumPurchaseAmountNotMetMessageFormat = "{0} 円以上のお買い上げでご利用いただけます";

    /// <summary>
    /// クーポンを適用する。
    /// 有効期限と最低購入金額の両方を満たさない場合は、有効期限切れのメッセージを返す。
    /// </summary>
    /// <param name="coupon">適用するクーポン</param>
    /// <param name="cart">クーポンを適用する買い物かご</param>
    /// <param name="now">
    /// 現在日時。シナリオの結果が実行日によって変わらないよう、実際の現在時刻ではなく引数で受け取る
    /// </param>
    /// <returns>適用結果</returns>
    public CouponApplicationResult Apply(Coupon coupon, Cart cart, DateTime now)
    {
        if (!coupon.IsValidOn(now))
        {
            return CouponApplicationResult.Failure(ExpiredMessage, cart.TotalAmount);
        }

        if (!coupon.IsSatisfiedBy(cart.TotalAmount))
        {
            var errorMessage = string.Format(
                CultureInfo.InvariantCulture,
                MinimumPurchaseAmountNotMetMessageFormat,
                coupon.MinimumPurchaseAmount.Amount.ToString("N0", CultureInfo.InvariantCulture));
            return CouponApplicationResult.Failure(errorMessage, cart.TotalAmount);
        }

        throw new NotImplementedException();
    }

    /// <summary>
    /// クーポンが使えるか。
    /// </summary>
    /// <param name="coupon">判定するクーポン</param>
    /// <param name="cart">クーポンを適用する買い物かご</param>
    /// <param name="now">
    /// 現在日時。シナリオの結果が実行日によって変わらないよう、実際の現在時刻ではなく引数で受け取る
    /// </param>
    /// <returns>クーポンが使える場合は true、使えない場合は false</returns>
    public bool CanApply(Coupon coupon, Cart cart, DateTime now)
    {
        return coupon.IsValidOn(now) && coupon.IsSatisfiedBy(cart.TotalAmount);
    }
}
