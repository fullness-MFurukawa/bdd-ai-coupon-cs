using System.Globalization;
using BDD.AI.Coupons.Specs.Tracing;
using Reqnroll;
using Reqnroll.Bindings;
using Reqnroll.Bindings.Reflection;
using Reqnroll.Plugins;
using Reqnroll.Tracing;
using Reqnroll.UnitTestProvider;

[assembly: RuntimePlugin(typeof(StepResultTracerPlugin))]

namespace BDD.AI.Coupons.Specs.Tracing;

/// <summary>
/// ステップごとの結果を出力するトレーサーを Reqnroll に登録する
/// </summary>
public class StepResultTracerPlugin : IRuntimePlugin
{
    public void Initialize(
        RuntimePluginEvents runtimePluginEvents,
        RuntimePluginParameters runtimePluginParameters,
        UnitTestProviderConfiguration unitTestProviderConfiguration)
    {
        runtimePluginEvents.CustomizeTestThreadDependencies += (_, e) =>
            e.ObjectContainer.RegisterTypeAs<StepResultTracer, ITestTracer>();
    }
}

/// <summary>
/// ステップの結果を「✔ 前提 クーポン "SALE2026" が存在する」の形式で出力する。
/// 失敗や未定義の詳細は、Reqnroll の既定のトレーサーにそのまま出力させる
/// </summary>
public class StepResultTracer(TestTracer defaultTracer, ITraceListener traceListener) : ITestTracer
{
    private StepInstance? _currentStep;

    public void TraceStep(StepInstance stepInstance, bool showAdditionalArguments)
    {
        _currentStep = stepInstance;
    }

    public void TraceStepDone(BindingMatch match, object[] arguments, TimeSpan duration)
    {
        WriteResult("✔");
    }

    public void TraceStepPending(BindingMatch match, object[] arguments, Exception exception)
    {
        WriteResult("？");
        defaultTracer.TraceStepPending(match, arguments, exception);
    }

    public void TraceNoMatchingStepDefinition(
        StepInstance stepInstance,
        ProgrammingLanguage targetLanguage,
        CultureInfo bindingCulture,
        List<BindingMatch> matchesWithoutScopeCheck)
    {
        WriteResult("？");
        defaultTracer.TraceNoMatchingStepDefinition(stepInstance, targetLanguage, bindingCulture, matchesWithoutScopeCheck);
    }

    public void TraceStepSkipped(Exception exception)
    {
        WriteResult("－");
        defaultTracer.TraceStepSkipped(exception);
    }

    public void TraceStepSkippedBecauseOfPreviousErrors()
    {
        WriteResult("－");
        defaultTracer.TraceStepSkippedBecauseOfPreviousErrors();
    }

    public void TraceError(Exception ex, TimeSpan duration)
    {
        WriteResult("✘");
        defaultTracer.TraceError(ex, duration);
    }

    public void TraceBindingError(Exception ex)
    {
        WriteResult("✘");
        defaultTracer.TraceBindingError(ex);
    }

    public void TraceWarning(string text)
    {
        defaultTracer.TraceWarning(text);
    }

    public void TraceDuration(TimeSpan elapsed, IBindingMethod method, object[] arguments)
    {
        defaultTracer.TraceDuration(elapsed, method, arguments);
    }

    public void TraceDuration(TimeSpan elapsed, string text)
    {
        defaultTracer.TraceDuration(elapsed, text);
    }

    private void WriteResult(string mark)
    {
        if (_currentStep is null)
        {
            return;
        }

        traceListener.WriteTestOutput($"{mark} {_currentStep.Keyword} {_currentStep.Text}");
    }
}
