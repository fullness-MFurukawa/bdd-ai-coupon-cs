namespace BDD.AI.Coupons.Domain.Models;

/// <summary>
/// 有効期限
/// 当日まで有効。
/// </summary>
/// <param name="Date">有効期限の日付</param>
public record ExpirationDate(DateOnly Date)
{
    /// <summary>
    /// 指定した日が有効期限内か。
    /// 当日まで有効。
    /// </summary>
    /// <param name="date">判定する日付</param>
    /// <returns>判定する日付が有効期限の日付以前であれば true</returns>
    public bool IsValidOn(DateOnly date)
    {
        return date <= Date;
    }
}
