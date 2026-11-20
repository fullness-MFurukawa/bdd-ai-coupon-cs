using BDD.AI.Coupons.Domain.Exceptions;

namespace BDD.AI.Coupons.Domain.Models;

/// <summary>
/// 割引率
/// パーセント単位。0〜100 以外は許さない。
/// </summary>
/// <param name="Percent">割引率（パーセント）</param>
public record DiscountRate(int Percent)
{
    /// <summary>割引率（パーセント）</summary>
    public int Percent { get; } = Percent is >= 0 and <= 100
        ? Percent
        : throw new DomainException($"割引率は 0〜100 の範囲で指定してください。指定された値: {Percent}");

    /// <summary>
    /// 割引率を適用する。
    /// 割引額の 1 円未満の端数は切り捨てる。
    /// </summary>
    /// <param name="amount">割引率を掛ける金額（購入金額）</param>
    /// <returns>金額に割引率を掛け、1 円未満を切り捨てた金額</returns>
    public Money ApplyTo(Money amount)
    {
        return new Money(amount.Amount * Percent / 100);
    }
}
