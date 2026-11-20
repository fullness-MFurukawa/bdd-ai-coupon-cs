using BDD.AI.Coupons.Domain.Exceptions;

namespace BDD.AI.Coupons.Domain.Models;

/// <summary>
/// 金額
/// 負の値を許さない。
/// </summary>
/// <param name="Amount">金額の値（円単位）</param>
public record Money(long Amount)
{
    /// <summary>金額の値</summary>
    public long Amount { get; } = Amount >= 0
        ? Amount
        : throw new DomainException($"金額に負の値は指定できません。指定された値: {Amount}");

    /// <summary>
    /// 金額が他の金額より少ないか。
    /// </summary>
    /// <param name="other">比較する金額</param>
    /// <returns>この金額が比較する金額より少ない場合は true</returns>
    public bool IsLessThan(Money other)
    {
        return Amount < other.Amount;
    }

    /// <summary>
    /// 金額を減算する。
    /// </summary>
    /// <param name="other">差し引く金額</param>
    /// <returns>この金額から差し引く金額を引いた金額</returns>
    /// <exception cref="DomainException">結果が負になる場合</exception>
    public Money Subtract(Money other)
    {
        return new Money(Amount - other.Amount);
    }
}
