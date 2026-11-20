using BDD.AI.Coupons.Domain.Exceptions;

namespace BDD.AI.Coupons.Domain.Models;

/// <summary>
/// 買い物かご
/// クーポンの適用とポイントの利用の状態を持つ。クーポンとポイントは併用できない。
/// </summary>
public class Cart
{
    /// <summary>
    /// 買い物かごを生成する。
    /// 生成した時点では、クーポンを適用せず、ポイントも利用していない。
    /// </summary>
    /// <param name="totalAmount">購入金額</param>
    /// <param name="membershipRank">会員ランク</param>
    public Cart(Money totalAmount, MembershipRank membershipRank)
    {
        TotalAmount = totalAmount;
        MembershipRank = membershipRank;
    }

    /// <summary>購入金額</summary>
    public Money TotalAmount { get; }

    /// <summary>会員ランク</summary>
    public MembershipRank MembershipRank { get; }

    /// <summary>適用中のクーポン</summary>
    public Coupon? AppliedCoupon { get; private set; }

    /// <summary>割引額</summary>
    public Money DiscountAmount { get; private set; } = new(0);

    /// <summary>利用ポイント</summary>
    public Points UsedPoints { get; private set; } = new(0);

    /// <summary>ポイントによる値引き額</summary>
    public Money PointDiscountAmount { get; private set; } = new(0);

    /// <summary>支払金額</summary>
    public Money PaymentAmount => TotalAmount.Subtract(DiscountAmount).Subtract(PointDiscountAmount);

    /// <summary>
    /// クーポンが適用中か。
    /// </summary>
    /// <returns>クーポンが適用されている場合は true</returns>
    public bool HasCoupon()
    {
        return AppliedCoupon is not null;
    }

    /// <summary>
    /// クーポンを適用する。
    /// クーポンとポイントは併用できないため、ポイントの利用は解除する。
    /// </summary>
    /// <param name="coupon">適用するクーポン</param>
    /// <param name="discountAmount">割引額</param>
    public void ApplyCoupon(Coupon coupon, Money discountAmount)
    {
        AppliedCoupon = coupon;
        DiscountAmount = discountAmount;
        UsedPoints = new Points(0);
        PointDiscountAmount = new Money(0);
    }

    /// <summary>
    /// クーポンを解除する。
    /// </summary>
    /// <param name="couponCode">解除するクーポンの券種コード</param>
    /// <exception cref="DomainException">券種コードのクーポンが適用されていない場合</exception>
    public void CancelCoupon(CouponCode couponCode)
    {
        if (AppliedCoupon is null || AppliedCoupon.CouponCode != couponCode)
        {
            throw new DomainException($"解除しようとしたクーポンは適用されていません。券種コード: {couponCode.Value}");
        }

        AppliedCoupon = null;
        DiscountAmount = new Money(0);
    }

    /// <summary>
    /// ポイントを利用する。
    /// </summary>
    /// <param name="points">利用するポイント数</param>
    /// <param name="pointRate">ポイントの換算</param>
    /// <exception cref="DomainException">クーポンが適用されている場合</exception>
    public void UsePoints(Points points, PointRate pointRate)
    {
        if (HasCoupon())
        {
            throw new DomainException("クーポンの適用中はポイントを利用できません。");
        }

        UsedPoints = points;
        PointDiscountAmount = pointRate.ToMoney(points);
    }
}
