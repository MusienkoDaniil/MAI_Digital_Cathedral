public interface INumericalIntegrationMethod
{
    double Integrate(Func<double, double> f, double a, double b, double accuracy);
    string MethodName { get; }
}

public class LeftRectangleMethod : INumericalIntegrationMethod
{
    public string MethodName => "Left Rectangle";

    public double Integrate(Func<double, double> f, double a, double b, double accuracy)
    {
        int n = 1;
        double result;
        double prevResult;

        do
        {
            prevResult = LeftRectangle(f, a, b, n);
            n *= 2;
            result = LeftRectangle(f, a, b, n);
        } while (Math.Abs(result - prevResult) > accuracy);

        return result;
    }

    private double LeftRectangle(Func<double, double> f, double a, double b, int n)
    {
        var h = (b - a) / n;
        var sum = 0d;
        for (var i = 0; i < n; i++)
        {
            var x = a + i * h;
            sum += f(x);
        }
        return h * sum;
    }
}

public class RightRectangleMethod : INumericalIntegrationMethod
{
    public string MethodName => "Right Rectangle";

    public double Integrate(Func<double, double> f, double a, double b, double accuracy)
    {
        int n = 1;
        double result;
        double prevResult;

        do
        {
            prevResult = RightRectangle(f, a, b, n);
            n *= 2;
            result = RightRectangle(f, a, b, n);
        } while (Math.Abs(result - prevResult) > accuracy);

        return result;
    }

    private double RightRectangle(Func<double, double> f, double a, double b, int n)
    {
        var h = (b - a) / n;
        var sum = 0d;
        for (var i = 1; i <= n; i++)
        {
            var x = a + i * h;
            sum += f(x);
        }
        return h * sum;
    }
}

public class MidRectangleMethod : INumericalIntegrationMethod
{
    public string MethodName => "Mid Rectangle";

    public double Integrate(Func<double, double> f, double a, double b, double accuracy)
    {
        int n = 1;
        double result;
        double prevResult;

        do
        {
            prevResult = MidRectangle(f, a, b, n);
            n *= 2;
            result = MidRectangle(f, a, b, n);
        } while (Math.Abs(result - prevResult) > accuracy);

        return result;
    }

    private double MidRectangle(Func<double, double> f, double a, double b, int n)
    {
        var h = (b - a) / n;
        var sum = 0d;
        for (var i = 0; i < n; i++)
        {
            var x = a + (i + 0.5) * h;
            sum += f(x);
        }
        return h * sum;
    }
}

public class TrapezoidalMethod : INumericalIntegrationMethod
{
    public string MethodName => "Trapezoidal Method";

    public double Integrate(Func<double, double> f, double a, double b, double accuracy)
    {
        int n = 1;
        double result;
        double prevResult;

        do
        {
            prevResult = Trapezoidal(f, a, b, n);
            n *= 2;
            result = Trapezoidal(f, a, b, n);
        } while (Math.Abs(result - prevResult) > accuracy);

        return result;
    }

    private double Trapezoidal(Func<double, double> f, double a, double b, int n)
    {
        var h = (b - a) / n;
        var sum = 0.5 * (f(a) + f(b));
        for (var i = 1; i < n; i++)
        {
            var x = a + i * h;
            sum += f(x);
        }
        return h * sum;
    }
}

public class SimpsonMethod : INumericalIntegrationMethod
{
    public string MethodName => "Simpson Method";

    public double Integrate(Func<double, double> f, double a, double b, double accuracy)
    {
        int n = 2; // Simpson's rule requires n to be even
        double result;
        double prevResult;

        do
        {
            prevResult = Simpson(f, a, b, n);
            n *= 2;
            result = Simpson(f, a, b, n);
        } while (Math.Abs(result - prevResult) > accuracy);

        return result;
    }

    private double Simpson(Func<double, double> f, double a, double b, int n)
    {
        var h = (b - a) / n;
        var sum = f(a) + f(b);

        for (var i = 1; i < n; i += 2)
        {
            sum += 4 * f(a + i * h);
        }

        for (var i = 2; i < n - 1; i += 2)
        {
            sum += 2 * f(a + i * h);
        }

        return h / 3 * sum;
    }
}

class Program
{
    public static void Main(string[] args)
    {
        Func<double, double> f = x => Math.Exp(x) * Math.Log(x + 1) * Math.Sqrt(x); // Пример функции: f(x) = e^x * ln(x+1) * sqrt(x)

        // Выбор метода
        var methods = new INumericalIntegrationMethod[]
        {
            new LeftRectangleMethod(),
            new RightRectangleMethod(),
            new MidRectangleMethod(),
            new TrapezoidalMethod(),
            new SimpsonMethod()
        };

        foreach (var method in methods)
        {
            // Засекаем время с использованием DateTime
            DateTime startTime = DateTime.Now;

            // Вычисление интеграла с точностью 0.0000001
            double result = method.Integrate(f, 0.01, 1, 0.0000001);

            // Засекаем время после выполнения
            DateTime endTime = DateTime.Now;

            // Вычисление времени выполнения в миллисекундах
            TimeSpan elapsedTime = endTime - startTime;

            // Вывод результата и времени выполнения
            Console.WriteLine($"{method.MethodName} Result: {result}, Time: {elapsedTime.TotalMilliseconds} ms");
        }
    }
}
