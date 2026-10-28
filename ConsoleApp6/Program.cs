using System;

public record Report(
    string Title,
    string Body,
    DateTime CreatedAt
);

public interface IReportExporter
{
    string Export(Report report);
}

// Старый компонент: его код не меняем.
public class LegacyReportPrinter
{
    public string Print(string title, string body, string footer)
    {
        return $"=== {title} ===\n{body}\n{footer}";
    }
}

// Новый адаптер.
public class LegacyReportAdapter : IReportExporter
{
    private readonly LegacyReportPrinter _printer;

    public LegacyReportAdapter(LegacyReportPrinter printer)
    {
        ArgumentNullException.ThrowIfNull(printer);
        _printer = printer;
    }

    public string Export(Report report)
    {
        ArgumentNullException.ThrowIfNull(report);

        string footer =
            $"Создан: {report.CreatedAt:yyyy-MM-dd HH:mm}";

        return _printer.Print(
            report.Title,
            report.Body,
            footer
        );
    }
}
public class LegacyReportAdapter1 : IReportExporter
{
    private readonly LegacyReportPrinter _printer;

    public LegacyReportAdapter1(LegacyReportPrinter printer)
    {
        ArgumentNullException.ThrowIfNull(printer);
        _printer = printer;
    }

    public string Export(Report report)
    {
        ArgumentNullException.ThrowIfNull(report);

        string footer =
            $"Создан: {report.CreatedAt:yyyy-MM-dd HH:mm}";

        return _printer.Print(
            report.Title,
            report.Body,
            footer
        );
    }
}
/**
* 1. Где происходит преобразование данных?
* 2. Почему `LegacyReportPrinter` не реализует новый интерфейс?
* 3. Как проверить, что старый класс не пришлось изменять?
* 4. Что нужно поменять, если вместо старого принтера потребуется Markdown-экспортёр?*
*/
public class Program
{
    public static void Main()
    {
        decimal price = 350m;
        int count = 3;

        decimal total = CalculateTotal(price, count);

        string body = $"Цена товара: {price} руб.\n" +
            $"Количество: {count}\n" +
            $"Итого: {total} руб.\n";

        Report report = new(
            "КАССА МАГАЗИНА",
            body,
            new DateTime(2026, 10, 28, 9, 0, 0)
        );

        IReportExporter exporter =
            new LegacyReportAdapter1(new LegacyReportPrinter());

        Console.WriteLine(exporter.Export(report));
        
   

    }


    static decimal CalculateTotal(decimal price, int count)
    {
        decimal total = price * count;
        return total;
    }

}