namespace BDD.AI.Coupons.Domain.Models;

/// <summary>
/// ポイントの利用結果
/// ポイントを利用できないことは業務上の正常な結果であるため、例外ではなくこの型で返す。
/// </summary>
/// <param name="Successful">成否</param>
/// <param name="ErrorMessage">エラーメッセージ（成功時は null）</param>
public record PointUsageResult(bool Successful, string? ErrorMessage)
{
    /// <summary>
    /// ポイントの利用の成功。
    /// </summary>
    /// <returns>成功を表すポイントの利用結果</returns>
    public static PointUsageResult Success()
    {
        return new PointUsageResult(true, null);
    }

    /// <summary>
    /// ポイントの利用の失敗。
    /// </summary>
    /// <param name="errorMessage">エラーメッセージ</param>
    /// <returns>失敗を表すポイントの利用結果</returns>
    public static PointUsageResult Failure(string errorMessage)
    {
        return new PointUsageResult(false, errorMessage);
    }
}
