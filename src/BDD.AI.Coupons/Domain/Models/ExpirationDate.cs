namespace BDD.AI.Coupons.Domain.Models;

/// <summary>
/// 有効期限
/// 当日まで有効。
/// </summary>
/// <param name="Date">有効期限の日付</param>
public record ExpirationDate(DateOnly Date);
