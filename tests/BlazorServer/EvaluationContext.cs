namespace BlazorServer;

public class EvaluationContext
{
    public object? Result { get; set; }

    public Exception? Exception { get; set; }

    public Func<Task<object?>>? AfterRenderAsync { get; set; }
}
