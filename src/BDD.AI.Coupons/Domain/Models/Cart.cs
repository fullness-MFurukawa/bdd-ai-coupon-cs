namespace BDD.AI.Coupons.Domain.Models;

/// <summary>
/// 買い物かご
/// </summary>
public class Cart
{
    /// <summary>
    /// 買い物かごを生成する。
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
}
