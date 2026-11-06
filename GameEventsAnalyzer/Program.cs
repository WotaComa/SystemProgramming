namespace GameEventsAnalyzer;

// TODO 1. Реализуйте AnalyzeWithLinq.
// Отберите события Kill.
// Сгруппируйте их по PlayerId.
// Посчитайте количество убийств и сумму очков(через PlayerStatistics).

// TODO 2. Реализуйте AnalyzeWithPlinq.
// Используйте AsParallel().
// Сохраните ту же бизнес-логику.

// TODO 3. Добавьте сортировку по TotalPoints
// от большего значения к меньшему.

// TODO 4. Добавьте ThenBy по PlayerId,
// чтобы результат имел стабильный порядок
// при одинаковом количестве очков.

// TODO 5. Сравните результаты LINQ и PLINQ
// через SequenceEqual.

// TODO 6. Проверьте пустую коллекцию.

// TODO 7. Проверьте коллекцию,
// в которой нет событий Kill.

// TODO 8. Проверьте коллекцию,
// в которой все события относятся к одному игроку.

// TODO 9. Проверьте передачу null.

// TODO 10. Объясните, почему PLINQ
// не обязательно быстрее LINQ.
internal class Program
{
    static void Main()
    {
        Console.WriteLine("=== Game Events Analyzer ===");

        List<GameEvent> events =
            GameEventGenerator.Generate(
                count: 1_000_000,
                seed: 12345);

        Console.WriteLine($"Всего событий: {events.Count:N0}");

        var linqResult =
            GameEventAnalyzer.AnalyzeWithLinq(events);

        var plinqResult =
            GameEventAnalyzer.AnalyzeWithPlinq(events);

        Console.WriteLine();
        Console.WriteLine("Топ игроков:");

        foreach (var player in plinqResult.Take(10))
        {
            Console.WriteLine(
                $"Игрок {player.PlayerId}: " +
                $"убийств {player.KillCount}, " +
                $"очков {player.TotalPoints}");
        }

        Console.WriteLine();

        bool same = linqResult
            .SequenceEqual(plinqResult);

        Console.WriteLine(
            $"Результаты LINQ и PLINQ совпадают: {same}");
    }
}