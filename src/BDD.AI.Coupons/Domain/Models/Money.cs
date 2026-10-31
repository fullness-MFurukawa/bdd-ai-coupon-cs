namespace BDD.AI.Coupons.Domain.Models;

/// <summary>
/// 金額
/// 負の値を許さない。
/// </summary>
/// <param name="Amount">金額の値（円単位）</param>
public record Money(long Amount);
