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

    private const string CouponAndPointsCannotBeCombinedMessage = "クーポンとポイントは併用できません";

    /// <summary>
    /// クーポンを適用する。
    /// 有効期限と最低購入金額の両方を満たさない場合は、有効期限切れのメッセージを返す。
    /// 適用できない場合は、ポイントの利用も含めて買い物かごを変更しない。
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

        var discountRate = cart.MembershipRank.DiscountRate;
        var maximumDiscountApplied = ExceedsMaximumDiscount(coupon, cart);
        var pointUsageCancelled = cart.UsedPoints.Value > 0;
        var discountAmount = CalculateDiscount(coupon, cart);
        cart.ApplyCoupon(coupon, discountAmount);
        return CouponApplicationResult.Success(
            discountRate, discountAmount, cart.PaymentAmount, maximumDiscountApplied, pointUsageCancelled);
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

    /// <summary>
    /// 割引額を計算する。
    /// 会員ランクの割引率を適用して 1 円未満を切り捨てた後、割引上限額と比較する。
    /// </summary>
    /// <param name="coupon">適用するクーポン</param>
    /// <param name="cart">クーポンを適用する買い物かご</param>
    /// <returns>割引額</returns>
    public Money CalculateDiscount(Coupon coupon, Cart cart)
    {
        return ExceedsMaximumDiscount(coupon, cart)
            ? coupon.MaximumDiscountAmount
            : cart.MembershipRank.DiscountRate.ApplyTo(cart.TotalAmount);
    }

    /// <summary>
    /// ポイントを利用する。
    /// クーポンとポイントは併用できないため、クーポンの適用中は利用できない。
    /// </summary>
    /// <param name="cart">ポイントを利用する買い物かご</param>
    /// <param name="points">利用するポイント数</param>
    /// <param name="pointRate">ポイントの換算</param>
    /// <returns>ポイントの利用結果</returns>
    public PointUsageResult UsePoints(Cart cart, Points points, PointRate pointRate)
    {
        if (cart.HasCoupon())
        {
            return PointUsageResult.Failure(CouponAndPointsCannotBeCombinedMessage);
        }

        cart.UsePoints(points, pointRate);
        return PointUsageResult.Success();
    }

    /// <summary>
    /// クーポンを解除する。
    /// </summary>
    /// <param name="cart">クーポンを解除する買い物かご</param>
    /// <param name="couponCode">解除するクーポンの券種コード</param>
    public void Cancel(Cart cart, CouponCode couponCode)
    {
        cart.CancelCoupon(couponCode);
    }

    // 割引上限額と等しい場合は、上限が適用されたとみなさない（シナリオ名が「上限を超える場合」であるため）。
    private static bool ExceedsMaximumDiscount(Coupon coupon, Cart cart)
    {
        var discountAmount = cart.MembershipRank.DiscountRate.ApplyTo(cart.TotalAmount);
        return coupon.MaximumDiscountAmount.IsLessThan(discountAmount);
    }
}
