using System.Globalization;
using BDD.AI.Coupons.Domain.Models;
using BDD.AI.Coupons.Domain.Services;
using Reqnroll;

namespace BDD.AI.Coupons.Specs.Steps;

/// <summary>
/// クーポンによる割引の適用のステップ定義
/// </summary>
[Binding]
public class CouponSteps
{
    private const string DateFormat = "yyyy-MM-dd";

    private const string DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

    private readonly DiscountCalculationService _discountCalculationService;

    private CouponCode _couponCode = null!;

    private Money _minimumPurchaseAmount = null!;

    private Money _maximumDiscountAmount = null!;

    private ExpirationDate _expirationDate = null!;

    private DateTime _now;

    private MembershipRank _membershipRank = null!;

    private Money _totalAmount = null!;

    private CouponApplicationResult _result = null!;

    private PointRate _pointRate = null!;

    private Coupon? _coupon;

    private Cart? _cart;

    private PointUsageResult _pointUsageResult = null!;

    private string? _errorMessage;

    /// <summary>
    /// クーポンによる割引の適用のステップ定義を生成する
    /// </summary>
    /// <param name="discountCalculationService">割引計算サービス</param>
    public CouponSteps(DiscountCalculationService discountCalculationService)
    {
        _discountCalculationService = discountCalculationService;
    }

    /// <summary>
    /// 前提 クーポン {string} が存在する
    /// </summary>
    /// <param name="couponCode">券種コード</param>
    [Given("クーポン {string} が存在する")]
    public void CouponExists(string couponCode)
    {
        _couponCode = new CouponCode(couponCode);
    }

    /// <summary>
    /// 前提 クーポン {string} の最低購入金額は {int} 円である
    /// </summary>
    /// <param name="couponCode">券種コード</param>
    /// <param name="minimumPurchaseAmount">最低購入金額（円）</param>
    [Given("クーポン {string} の最低購入金額は {int} 円である")]
    public void CouponHasMinimumPurchaseAmount(string couponCode, int minimumPurchaseAmount)
    {
        _minimumPurchaseAmount = new Money(minimumPurchaseAmount);
    }

    /// <summary>
    /// 前提 クーポン {string} の割引上限額は {int} 円である
    /// </summary>
    /// <param name="couponCode">券種コード</param>
    /// <param name="maximumDiscountAmount">割引上限額（円）</param>
    [Given("クーポン {string} の割引上限額は {int} 円である")]
    public void CouponHasMaximumDiscountAmount(string couponCode, int maximumDiscountAmount)
    {
        _maximumDiscountAmount = new Money(maximumDiscountAmount);
    }

    /// <summary>
    /// 前提 クーポン {string} の有効期限は {string} である
    /// </summary>
    /// <param name="couponCode">券種コード</param>
    /// <param name="expirationDate">有効期限</param>
    [Given("クーポン {string} の有効期限は {string} である")]
    public void CouponExpiresOn(string couponCode, string expirationDate)
    {
        _expirationDate = new ExpirationDate(
            DateOnly.ParseExact(expirationDate, DateFormat, CultureInfo.InvariantCulture));
    }

    /// <summary>
    /// 前提 割引額の端数は切り捨てる
    /// </summary>
    [Given("割引額の端数は切り捨てる")]
    public void DiscountFractionIsTruncated()
    {
        // 切り捨てはドメインの固定の規則であるため、何も保持しない（docs/decisions.md の 7）。
    }

    /// <summary>
    /// 前提 ポイントは {int} ポイントを {int} 円として計算する
    /// </summary>
    /// <param name="points">ポイント数</param>
    /// <param name="yen">円</param>
    [Given("ポイントは {int} ポイントを {int} 円として計算する")]
    public void PointRateIs(int points, int yen)
    {
        _pointRate = new PointRate(points, yen);
    }

    /// <summary>
    /// 前提 現在日時は {string} である
    /// </summary>
    /// <param name="now">現在日時</param>
    [Given("現在日時は {string} である")]
    public void CurrentDateTimeIs(string now)
    {
        _now = DateTime.ParseExact(now, DateTimeFormat, CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// 前提 ポイントを利用しない
    /// </summary>
    [Given("ポイントを利用しない")]
    public void PointsAreNotUsed()
    {
        // 生成した買い物かごはポイントを利用していない状態であるため、何もしない。
    }

    /// <summary>
    /// 前提 会員ランクは {string} である
    /// </summary>
    /// <param name="membershipRank">会員ランクの名称</param>
    [Given("会員ランクは {string} である")]
    public void MembershipRankIs(string membershipRank)
    {
        _membershipRank = MembershipRank.Of(membershipRank);
    }

    /// <summary>
    /// 前提 カートの購入金額は {int} 円である
    /// </summary>
    /// <param name="totalAmount">購入金額（円）</param>
    [Given("カートの購入金額は {int} 円である")]
    public void CartTotalAmountIs(int totalAmount)
    {
        _totalAmount = new Money(totalAmount);
    }

    /// <summary>
    /// もし クーポン {string} を適用する
    /// </summary>
    /// <param name="couponCode">券種コード</param>
    [When("クーポン {string} を適用する")]
    public void ApplyCoupon(string couponCode)
    {
        _result = _discountCalculationService.Apply(GetCoupon(), GetCart(), _now);
        _errorMessage = _result.ErrorMessage;
    }

    /// <summary>
    /// 前提 クーポン {string} を適用済みである
    /// </summary>
    /// <param name="couponCode">券種コード</param>
    [Given("クーポン {string} を適用済みである")]
    public void CouponIsAlreadyApplied(string couponCode)
    {
        _result = _discountCalculationService.Apply(GetCoupon(), GetCart(), _now);
        _errorMessage = _result.ErrorMessage;
    }

    /// <summary>
    /// もし クーポン {string} を解除する
    /// </summary>
    /// <param name="couponCode">券種コード</param>
    [When("クーポン {string} を解除する")]
    public void CancelCoupon(string couponCode)
    {
        _discountCalculationService.Cancel(GetCart(), new CouponCode(couponCode));
    }

    /// <summary>
    /// 前提・もし ポイントを {int} ポイント利用する
    /// </summary>
    /// <param name="points">利用ポイント</param>
    [Given("ポイントを {int} ポイント利用する")]
    [When("ポイントを {int} ポイント利用する")]
    public void UsePoints(int points)
    {
        _pointUsageResult = _discountCalculationService.UsePoints(GetCart(), new Points(points), _pointRate);
        _errorMessage = _pointUsageResult.ErrorMessage;
    }

    /// <summary>
    /// ならば クーポンの適用は成功する
    /// </summary>
    [Then("クーポンの適用は成功する")]
    public void CouponApplicationSucceeds()
    {
        Assert.IsTrue(_result.Successful);
    }

    /// <summary>
    /// ならば クーポンの適用は失敗する
    /// </summary>
    [Then("クーポンの適用は失敗する")]
    public void CouponApplicationFails()
    {
        Assert.IsFalse(_result.Successful);
    }

    /// <summary>
    /// ならば エラーメッセージ {string} が表示される
    /// </summary>
    /// <param name="errorMessage">エラーメッセージ</param>
    [Then("エラーメッセージ {string} が表示される")]
    public void ErrorMessageIsShown(string errorMessage)
    {
        Assert.AreEqual(errorMessage, _errorMessage);
    }

    /// <summary>
    /// ならば 割引額は {int} 円である
    /// </summary>
    /// <param name="discountAmount">割引額（円）</param>
    [Then("割引額は {int} 円である")]
    public void DiscountAmountIs(int discountAmount)
    {
        Assert.AreEqual(new Money(discountAmount), GetCart().DiscountAmount);
    }

    /// <summary>
    /// 前提・ならば 支払金額は {int} 円である
    /// </summary>
    /// <param name="paymentAmount">支払金額（円）</param>
    [Given("支払金額は {int} 円である")]
    [Then("支払金額は {int} 円である")]
    public void PaymentAmountIs(int paymentAmount)
    {
        Assert.AreEqual(new Money(paymentAmount), GetCart().PaymentAmount);
    }

    /// <summary>
    /// ならば 適用された割引率は {int} パーセントである
    /// </summary>
    /// <param name="discountRate">割引率（パーセント）</param>
    [Then("適用された割引率は {int} パーセントである")]
    public void AppliedDiscountRateIs(int discountRate)
    {
        Assert.AreEqual(new DiscountRate(discountRate), _result.AppliedDiscountRate);
    }

    /// <summary>
    /// ならば 割引上限が適用される
    /// </summary>
    [Then("割引上限が適用される")]
    public void MaximumDiscountIsApplied()
    {
        Assert.IsTrue(_result.MaximumDiscountApplied);
    }

    /// <summary>
    /// ならば ポイントの利用は成功する
    /// </summary>
    [Then("ポイントの利用は成功する")]
    public void PointUsageSucceeds()
    {
        Assert.IsTrue(_pointUsageResult.Successful);
    }

    /// <summary>
    /// ならば ポイントの利用は失敗する
    /// </summary>
    [Then("ポイントの利用は失敗する")]
    public void PointUsageFails()
    {
        Assert.IsFalse(_pointUsageResult.Successful);
    }

    /// <summary>
    /// ならば ポイントの利用は解除される
    /// </summary>
    [Then("ポイントの利用は解除される")]
    public void PointUsageIsCancelled()
    {
        Assert.IsTrue(_result.PointUsageCancelled);
    }

    /// <summary>
    /// ならば 利用ポイントは {int} ポイントである
    /// </summary>
    /// <param name="usedPoints">利用ポイント</param>
    [Then("利用ポイントは {int} ポイントである")]
    public void UsedPointsIs(int usedPoints)
    {
        Assert.AreEqual(new Points(usedPoints), GetCart().UsedPoints);
    }

    /// <summary>
    /// 前提・ならば ポイントによる値引き額は {int} 円である
    /// </summary>
    /// <param name="pointDiscountAmount">ポイントによる値引き額（円）</param>
    [Given("ポイントによる値引き額は {int} 円である")]
    [Then("ポイントによる値引き額は {int} 円である")]
    public void PointDiscountAmountIs(int pointDiscountAmount)
    {
        Assert.AreEqual(new Money(pointDiscountAmount), GetCart().PointDiscountAmount);
    }

    // 前提の中でクーポンの適用やポイントの利用を行うシナリオがあるため、最初に必要になった時点で生成する。
    private Coupon GetCoupon()
    {
        return _coupon ??= new Coupon(_couponCode, _minimumPurchaseAmount, _maximumDiscountAmount, _expirationDate);
    }

    private Cart GetCart()
    {
        return _cart ??= new Cart(_totalAmount, _membershipRank);
    }
}
