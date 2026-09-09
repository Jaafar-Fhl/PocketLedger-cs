using System.IO;
using System.Text.Json;
using static System.Text.Json.JsonSerializer;

namespace PocketLedger;

public class Main_menu
{
	private readonly string[] menu = ["1.Add expense", "2.List all expenses", "3.Edit expenses", "4.Show total spendings", "5.Show spendings for a chosen month", "6.Exit the app"];
    bool valid_input = false;
    bool exit = false;
    string sub_menu = "";
    public void Load_interactive_menu()
    {
        do
        {
			do
			{
				foreach (string option in menu)
					Console.WriteLine(option);
				Console.WriteLine("\nEnter the number of the option you wish to choose");
				string? user_choice = Console.ReadLine();
				if (user_choice!= null && int.TryParse(user_choice, out int option_number))
				{
					if (option_number >= 1 && option_number <= 6)
					{
						sub_menu = option_number.ToString();
						valid_input = true;
					}
				}
			}
			while (!valid_input);

			Options sub = new ();

			switch(sub_menu)
			{
				case "1":
					sub.Add_expense();
					break;
				case "2":
					sub.List_expenses();
					break;
				case "3":
					sub.Edit_expenses();
					break;
				case "4":
					sub.Total_spending();
					break;
				case "5":
					sub.Spending_chosen_month();
					break;
				case "6":
					exit = true;
					break;
            }
            Console.WriteLine("\nPress 'Enter' or 'Return' to continue");
            Console.ReadLine();
        }
        while (!exit);
    }
}

public class Options
{
	public List<Expenses> expense_data = [];

	bool valid_input = false;

	string e_name = "";
	string e_category = "";
	decimal e_amount = 0.0m;
	DateTime e_date = new ();

	string? user_input = "";

	public void Add_expense()
    {
        Random rand = new();
        List<Expenses> expense_data = [];
        if (File.Exists("expenses.json"))
        {
			string json_old = File.ReadAllText("expenses.json");
            expense_data = JsonSerializer.Deserialize<List<Expenses>>(json_old) ?? [];
            do
			{
				Console.WriteLine("Enter the name of the expense you wish to add (Minimum of 4 characters): ");
				user_input = Console.ReadLine();
				if (user_input?.Length >= 4)
				{
					e_name = user_input;
					valid_input = true;
				}
			}
			while (!valid_input);

			do
			{
				valid_input = false;
				Console.WriteLine("\nWhat category this expense belongs to (Minimum of 4 characters): ");
				user_input = Console.ReadLine();
				if (user_input?.Length >= 4)
				{
					e_category = user_input;
					valid_input = true;
				}
			}
			while (!valid_input);

			do
			{
				valid_input = false;
				Console.WriteLine("\nHow much did you spend: ");
				user_input = Console.ReadLine();
				if (decimal.TryParse(user_input, out e_amount) && e_amount > 0)
				{
					e_date = DateTime.Now;
					valid_input = true;
				}
			}
            while (!valid_input);
            Expenses add_new = new() { name = e_name, category = e_category, amount = e_amount, e_id = rand.Next(10000), date = e_date };
            expense_data.Add(add_new);
            string json_new = JsonSerializer.Serialize<List<Expenses>>(expense_data);
            File.WriteAllText("expenses.json", json_new);
        }

        else
        {
            do
        	{
				Console.WriteLine("Enter the name of the expense you wish to add (Minimum of 4 characters): ");
				user_input = Console.ReadLine();
				if (user_input?.Length >= 4)
				{
					e_name = user_input;
					valid_input = true;
				}
			}
			while (!valid_input);

			do
			{
				valid_input = false;
				Console.WriteLine("\nWhat category this expense belongs to (Minimum of 4 characters): ");
				user_input = Console.ReadLine();
				if (user_input?.Length >= 4)
				{
					e_category = user_input;
					valid_input = true;
				}
			}
			while (!valid_input);

			do
			{
				valid_input = false;
				Console.WriteLine("\nHow much did you spend: ");
				user_input = Console.ReadLine();
				if (decimal.TryParse(user_input, out e_amount) && e_amount > 0)
				{
					e_date = DateTime.Now;
					valid_input = true;
				}
			}
			while (!valid_input);

			Expenses added = new() { name = e_name, category = e_category, amount = e_amount, e_id = rand.Next(10000),date = e_date};
			expense_data.Add(added);
			string json = JsonSerializer.Serialize<List<Expenses>>(expense_data);
			File.WriteAllText("expenses.json", json);
        }
	}

	public void List_expenses()
	{
		List<Expenses>? data = [];

		if (File.Exists("expenses.json"))
		{
			string? json_text = File.ReadAllText("expenses.json");
            data = JsonSerializer.Deserialize<List<Expenses>>(json_text) ?? [];
			foreach (Expenses spending in data)
				Console.WriteLine($"ID: {spending.e_id} | Category: {spending.category} | {spending.name}: {spending.amount:C2} | At: {spending.date}");
		}
		else
		{
			Console.WriteLine("No expenses recorded yet!");
		}
    }

    public void Edit_expenses()
	{
        List<Expenses> to_edit = [];
        bool valid_id = false;
        int choice = 0;
        int edit_index = 0;
        Expenses editable = new ();

        if (File.Exists("expenses.json"))
        {
            string? json_text = File.ReadAllText("expenses.json");
            to_edit = JsonSerializer.Deserialize<List<Expenses>>(json_text) ?? [];
            foreach (Expenses spent in to_edit)
            {
            	Console.WriteLine($"{spent.e_id}.{spent.name} | At: {spent.date}");
            }
            do
            {
                Console.WriteLine("Enter the Id of the expense you wan to edit: ");
                string? id = Console.ReadLine();
                if (id != null)
                {
                    if (int.TryParse(id, out int identity) && identity < 10000)
                    {
                        foreach (Expenses spent in to_edit)
                        {
                            if (spent.e_id == identity)
                            {
                            	edit_index = to_edit.IndexOf(spent);
                                editable = spent;
                                valid_id = true;
                            }
                        }
                    }
                }
            }
            while (!valid_id);

            do
            {
            	valid_id = false;
                Console.WriteLine("\n1.Change category\n2.Change name\n3.Change amount\n");
				Console.WriteLine("Choose one of the options above to edit: ");
                string? input = Console.ReadLine();
                if (input != null)
                {
                    if (int.TryParse(input, out choice) && (choice >= 1 && choice <= 3))
						valid_id = true;
                }
            }
            while (!valid_id);

            switch (choice)
            {
                case 1:
                    {
                        Console.WriteLine("Enter the new category: ");
                        string? temp = Console.ReadLine();
                        if (temp != null)
                            editable.category = temp;
                        break;
                    }
                case 2:
                    {
                        Console.WriteLine("Enter the new name: ");
                        string? temp = Console.ReadLine();
                        if (temp != null)
                            editable.name = temp;
                        break;
                    }
                case 3:
                    {
                        Console.WriteLine("Enter the new amount spent: ");
                        string? temp = Console.ReadLine();
                        if (decimal.TryParse(temp, out decimal new_amount) && new_amount > 0.0m)
                        {
                        	editable.amount = new_amount;
                        }
                        break;
                    }
            }
            Console.WriteLine("Press 'Enter' or 'Return' to save changes");
            Console.ReadLine();
            Console.WriteLine("This expense now became: ");
            Console.Write($"{editable.e_id}.{editable.name} = {editable.amount} | Category: {editable.category} | At: {editable.date}");
            to_edit[edit_index] = editable;
            string new_json = JsonSerializer.Serialize<List<Expenses>>(to_edit);
            File.WriteAllText("expenses.json", new_json);
        }
        else
        {
        	Console.WriteLine("No expenses recorded yet!");
        }
    }

    public void Total_spending()
    {
        List<Expenses> data = [];
        string? json_text = File.ReadAllText("expenses.json");
        data = JsonSerializer.Deserialize<List<Expenses>>(json_text) ?? [];
        decimal total = 0.0m;
        foreach (Expenses spent in data)
        {
			total += spent.amount;
        }
        Console.WriteLine($"Total spending: {total:C2} spent on {data.Count} expenses in a total of {data.Max(x => x.date) - data.Min(x => x.date)} days");
    }

    public void Spending_chosen_month()
    {
    	List<Expenses> data = [];
		string? json_text = File.ReadAllText("expenses.json");
		data = JsonSerializer.Deserialize<List<Expenses>>(json_text) ?? [];
        Console.WriteLine("Enter the year you want to see the spendings for (e.g. 2026): ");
        string? year_input = Console.ReadLine();
        if (year_input != null)
        {
            if (int.TryParse(year_input, out int year) && year >= 2000 && year <= 2100)
            {
				Console.WriteLine("Enter the month you want to see the spendings for (1-12): ");
				string? month_input = Console.ReadLine();
				if (month_input != null)
				{
					if (int.TryParse(month_input, out int month) && month >= 1 && month <= 12)
					{
						decimal total = 0.0m;
						foreach (Expenses spent in data)
						{
							if (spent.date.Month == month)
							{
								total += spent.amount;
							}
						}
						Console.WriteLine($"Total spending for {new DateTime(year, month, 1).ToString("MMMM")}: {total:C2}");
					}
					else
					{
						Console.WriteLine("Invalid month input.");
					}
				}
			}
        }
        else
        {
        	Console.WriteLine("Invalid year input!");
        }
    }
}