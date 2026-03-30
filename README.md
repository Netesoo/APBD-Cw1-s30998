# System Wypożyczalni Sprzętu (RentalApp)

Aplikacja konsolowa w języku C# realizująca rygorystyczny system zarządzania wypożyczalnią sprzętu. Projekt został zaprojektowany z silnym naciskiem na czystą architekturę, wzorce projektowe oraz zasady obiektowe (OOP) i SOLID, co gwarantuje jego skalowalność i odporność na błędy.

## Architektura i Wzorce Projektowe

* **Wzorzec Repozytorium (Repository Pattern):** Warstwa danych została całkowicie odizolowana od logiki biznesowej za pomocą kontraktów (interfejsów, np. `IUserRepository`, `IRentalRepository`). Umożliwia to w przyszłości bezinwazyjną wymianę obecnej pamięci podręcznej (InMemory) na prawdziwą relacyjną bazę danych.
* **Wstrzykiwanie Zależności (Dependency Injection):** Serwisy nigdy nie tworzą własnych instancji repozytoriów. Zależności są przekazywane wyłącznie przez konstruktory. Gwarantuje to luźne powiązania (Loose Coupling) i sprawia, że klasy są otwarte na testy jednostkowe.
* **Zasada Pojedynczej Odpowiedzialności (SRP):** Logika systemu została hermetycznie podzielona na wąsko wyspecjalizowane komponenty. `RentalService` zarządza wyłącznie procesem transakcji, `PenaltyCalculator` odpowiada stricte za matematykę kar, a `ReportGenerator` zajmuje się tylko i wyłącznie agregacją danych i formatowaniem tekstu.
* **Bogaty Model Domeny (Rich Domain Model) i Kohezja:** Modele (np. klasa `Rental`) są odpowiedzialne za własny stan. Zrezygnowano z anemicznych modeli i publicznych setterów na rzecz metod biznesowych (np. `MarkAsReturned`), które weryfikują poprawność operacji wewnątrz samej klasy.
* **Polimorfizm:** Różne typy sprzętu (`Laptop`, `Camera`, `Projector`) oraz użytkowników (`Student`, `Employee`) dziedziczą z bazowych klas, co pozwala warstwie logiki operować na ogólnych abstrakcjach bez powielania kodu.

## Struktura Projektu

* **Data:** Folder zawierający kontrakty (interfejsy) oraz ich fizyczne implementacje przechowujące stan w pamięci operacyjnej za pomocą prywatnych list.
* **Enums:** Miejsce przechowywania typów wyliczeniowych, takich jak `RentalStatus`, chroniących system przed błędami literowymi (tzw. Magic Strings).
* **Exceptions:** Rozbudowany zbiór dedykowanych wyjątków biznesowych (np. `LimitExceededException`, `EquipmentUnavailableException`), które precyzyjnie informują o naruszeniu reguł domeny i przerywają nieprawidłowe operacje.
* **Models:** Serce aplikacji. Zawiera definicje encji biznesowych (sprzętu, użytkowników i faktów wypożyczeń) z hermetycznym stanem wewnętrznym.
* **Service:** Warstwa logiki aplikacyjnej, podzielona na odpowiednie poddomeny (kalkulacja kar, procesowanie wypożyczeń, generowanie raportów).
* **Program.cs:** Korzeń Kompozycji (Composition Root). To tutaj na samym starcie aplikacji następuje spięcie interfejsów z ich implementacjami oraz uruchomienie demonstracyjnego scenariusza biznesowego.

## Uruchomienie Projektu

Aplikacja nie wymaga zewnętrznej bazy danych, cały stan inicjalizowany jest w pamięci podczas startu. Wymagane środowisko to .NET SDK 9.

1. Otwórz terminal w katalogu głównym projektu (tam, gdzie znajduje się plik `RentalApp.csproj`).
2. Wykonaj polecenie `dotnet run`.
3. System automatycznie zainicjuje pamięć, przeprowadzi symulację logiki (w tym celowe wywołania wyjątków w celu demonstracji mechanizmów obronnych) oraz wygeneruje końcowe raporty analityczne w konsoli.
