namespace BDD.AI.Coupons.Domain.Exceptions;

/// <summary>
/// 業務ルール違反
/// 値オブジェクトやエンティティの制約に反する値を渡された場合にスローする。
/// 業務上の失敗（クーポンを使えないことなど）ではなく、プログラムの誤りを表す。
/// </summary>
public class DomainException : Exception
{
    /// <summary>
    /// 業務ルール違反を生成する。
    /// </summary>
    /// <param name="message">開発者に向けたメッセージ</param>
    public DomainException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// 原因となる例外を包んで、業務ルール違反を生成する。
    /// </summary>
    /// <param name="message">開発者に向けたメッセージ</param>
    /// <param name="innerException">原因となる例外</param>
    public DomainException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
