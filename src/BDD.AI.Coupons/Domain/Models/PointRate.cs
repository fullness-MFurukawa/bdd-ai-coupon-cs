using BDD.AI.Coupons.Domain.Exceptions;

namespace BDD.AI.Coupons.Domain.Models;

/// <summary>
/// ポイントの換算
/// 「{Points} ポイントを {Yen} 円として計算する」を表す。
/// </summary>
/// <param name="Points">換算の基準となるポイント数（1 以上）</param>
/// <param name="Yen">基準のポイント数に相当する円（0 以上）</param>
public record PointRate(int Points, int Yen)
{
    /// <summary>換算の基準となるポイント数</summary>
    public int Points { get; } = Points >= 1
        ? Points
        : throw new DomainException($"換算の基準となるポイント数は 1 以上で指定してください。指定された値: {Points}");

    /// <summary>基準のポイント数に相当する円</summary>
    public int Yen { get; } = Yen >= 0
        ? Yen
        : throw new DomainException($"ポイントに相当する円に負の値は指定できません。指定された値: {Yen}");

    /// <summary>
    /// ポイントを金額に換算する。
    /// 1 円未満の端数は切り捨てる。
    /// </summary>
    /// <param name="points">換算するポイント数</param>
    /// <returns>ポイントに相当する金額</returns>
    public Money ToMoney(Points points)
    {
        return new Money(points.Value * Yen / Points);
    }
}
