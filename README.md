
# PocketLedger - Console Expense Tracker

PocketLedger is a lightweight, local console application written in C# designed to track personal expenses. It features persistent storage using JSON files, allowing your transaction history to remain securely saved every time you close and restart the application.
It is also my first C# project series where i learn more about development and come back to add and apply what I've learned by upgrading this app one step at a time.

## 🚀 Features

- **Add Expenses:** Log new transactions automatically capturing the transaction date and time using your computer's clock.
- **Persistent Data Store:** Uses `System.Text.Json` to read and write data directly to a local file system database.
- **View History:** List all historical expenses printed in a clean, human-readable format.
- **In-Memory Modification:** Safely edits specific entries inside local memory before committing structural updates over the local data stream.

## 🛠️ Data Architecture Under the Hood

The application follows an interactive architecture built upon C# core paradigms:
- **Strong Typing:** Utilizes a custom `Expense` class featuring precise `decimal` types for accuracy with currency data.
- **Serialization Pipelines:** Seamlessly transforms multi-item C# collections into a JSON string.
- **Safe File Streams:** Leverages `System.IO` primitives alongside boolean validation checks (`File.Exists`) to completely neutralize runtime crash vectors.

## 📋 Prerequisites

To run this project, make sure you have the following installed on your machine:
- [.NET SDK](https://microsoft.com) (Version 10.0)
- A terminal or command-line interface

## ⚙️ How to Build and Run

Follow these steps to run the console app locally:

1. **Clone or download** the project directory to your local computer.
2. Open your terminal and **navigate into the project root directory** (where the `.csproj` file is located):
   ```bash
   cd path/to/PocketLedger
   ```
3. **Build the application** to restore dependencies:
   ```bash
   dotnet build
   ```
4. **Execute the console app**:
   ```bash
   dotnet run
   ```

## 📂 Where Is My Data Kept?

When you log an expense, the app automatically generates a file called `expenses.json`. 

By default, C# places this database file directly inside your application's active working directory.

## 🔮 Future Enhancements (Roadmap)
- Adding Category-based data filtering.
- Implementing monthly budget threshold limits.
- Support custom currency selection symbols.
- Adding recurrent spendings (monthly rent for example)
- Upgrading to SQLite database when i learn it
- Frontend + APIs + Mini Assistant

