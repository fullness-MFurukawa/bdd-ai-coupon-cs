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
}
