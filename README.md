# Payments - C# Homework (base Variant)

How to build:
dotnet new console -o PaymentsDemo
(copy files into PaymentsDemo project root, replace Program.cs if needed)
dotnet build

How to run demo:
dotnet run --project PaymentsDemo

Notes:
- Now interactive: add payments, process them, perform refunds via menu
- Payment.Process() implements NVI: validation -> OnBeforeProcess -> DoProcess -> OnAfterProcess -> raise Processed event
- Only CardPayment implements IRefundable to avoid LSP violations
- This interactive version makes demo more dynamic for extra points
