using BDD.AI.Coupons.Domain.Exceptions;

namespace BDD.AI.Coupons.Domain.Models;

/// <summary>
/// 会員ランク
/// 取りうる値は一般・ゴールド・プラチナに限られるため、コンストラクタを非公開にして
/// これ以外のインスタンスを生成できないようにしている。
/// </summary>
public record MembershipRank
{
    /// <summary>一般</summary>
    public static readonly MembershipRank General = new("一般");

    /// <summary>ゴールド</summary>
    public static readonly MembershipRank Gold = new("ゴールド");

    /// <summary>プラチナ</summary>
    public static readonly MembershipRank Platinum = new("プラチナ");

    private MembershipRank(string displayName)
    {
        DisplayName = displayName;
    }

    /// <summary>会員ランクの名称</summary>
    public string DisplayName { get; }

    /// <summary>
    /// 会員ランクの名称から会員ランクを得る。
    /// </summary>
    /// <param name="displayName">会員ランクの名称（一般・ゴールド・プラチナ）</param>
    /// <returns>名称に対応する会員ランク</returns>
    /// <exception cref="DomainException">該当する名称の会員ランクが無い場合</exception>
    public static MembershipRank Of(string displayName)
    {
        MembershipRank[] ranks = [General, Gold, Platinum];
        return ranks.FirstOrDefault(rank => rank.DisplayName == displayName)
            ?? throw new DomainException($"該当する会員ランクがありません。指定された名称: {displayName}");
    }
}
