using Payments___C__Homework__base_Variant_;
using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    private static readonly List<Payment> payments = new();
    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\n=== Меню ===");
            Console.WriteLine("1 - Добавить платеж");
            Console.WriteLine("2 - Обработать все платежи");
            Console.WriteLine("3 - Вернуть средства");
            Console.WriteLine("0 - Выход");
            Console.Write("Ваш выбор: ");
            var choice = Console.ReadLine();


            switch (choice)
            {
                case "1": AddPayment(); break;
                case "2": ProcessAll(payments); break;
                case "3": RefundMenu(); break;
                case "0": return;
                default: Console.WriteLine("Невідомий вибір"); break;
            }
        }
    }

    private static void AddPayment()
    {
        Console.Write("Тип платежа (cash/card/crypto): ");
        var type = Console.ReadLine()?.Trim().ToLower();


        Console.Write("Сумма: ");
        decimal amount = decimal.Parse(Console.ReadLine()!);


        Console.Write("Валюта: ");
        string currency = Console.ReadLine()!;


        Payment p = type switch
        {
            "cash" => new CashPayment(amount, currency, "Клиент"),
            "card" => new CardPayment(amount, currency, "**** **** **** 4242"),
            "crypto" => new CryptoPayment(amount, currency),
            _ => throw new InvalidOperationException("Неизвестный тип")
        };


        p.Processed += Payment_Processed;
        payments.Add(p);
        Console.WriteLine($"{p.GetType().Name} добавлено!");
    }

    private static void RefundMenu()
    {
        var refundable = payments.OfType<IRefundable>().ToList();
        if (!refundable.Any())
        {
            Console.WriteLine("Нет платежей для возврата.");
            return;
        }


        Console.Write("Сумма для возврата: ");
        decimal refundAmount = decimal.Parse(Console.ReadLine()!);


        ProcessRefunds(refundable, refundAmount);
    }


    private static void Payment_Processed(object? sender, PaymentProcessedEventArgs e)
    {
        Console.WriteLine($"[Событие] {e.Payment.GetType().Name} обработано о {e.ProcessedUtc:u}");
    }


    public static void ProcessAll(IEnumerable<Payment> payments)
    {
        foreach (var p in payments)
        {
            p.Process();
        }
    }


    public static void ProcessRefunds(IEnumerable<IRefundable> refunds, decimal amount)
    {
        foreach (var r in refunds)
        {
            try
            {
                r.Refund(amount);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Возвращение не удалось: {ex.Message}");
            }
        }
    }
}
