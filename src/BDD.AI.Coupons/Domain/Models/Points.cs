using BDD.AI.Coupons.Domain.Exceptions;

namespace BDD.AI.Coupons.Domain.Models;

/// <summary>
/// ポイント数
/// 負の値を許さない。
/// </summary>
/// <param name="Value">ポイント数の値</param>
public record Points(long Value)
{
    /// <summary>ポイント数の値</summary>
    public long Value { get; } = Value >= 0
        ? Value
        : throw new DomainException($"ポイント数に負の値は指定できません。指定された値: {Value}");
}
