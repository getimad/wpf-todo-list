# TodoList App

**TodoList App** is a desktop application that helps you organize your tasks and achieve your goals. With a simple and intuitive user interface, you can easily create, delete, search, and filter tasks by status, priority, or due date.

*TodoList App is built with **WPF**, a powerful framework for creating user interfaces with .NET. It uses data binding and **MVVM pattern** for separation of concerns, and **MS SQL Server** for data storage and retrieval.*

## Features

<picture>
  <img alt="A screenshot of the main window" src="https://github.com/getimad/wpf-todo-list/blob/main/Assets/Screenshots/TodoList-GIF.gif?raw=true">
</picture>

*Note: To remove a task, simply **double-click** on the task you want to delete.*

## Instalation

1. You need to have:
	- .NET SDK
	- MS SQL SERVER

2. Clone this repository and run the application:
	```bash
	# Clone this repository
	git clone https://github.com/getimad/wpf-todo-list

	# Go to the project directory
	cd wpf-todo-list
	```
 
3. Create a databse `TodoList` with a `Tasks` table in MS SQL Server:
	```SQL
	> CREATE DATABASE TodoList;

	> USE TodoList;

	> CREATE TABLE TASKS (
		Id INT IDENTITY(1,1) PRIMARY KEY,
		Content VARCHAR(MAX) NOT NULL,
		Priority CHAR(10) CHECK (Priority IN ('Priority 1', 'Priority 2', 'Priority 3', 'Priority 4')) NOT NULL,
		Date DATETIME DEFAULT SYSDATETIME()
	);
	```

4. Create an `App.config` in the root of the project and set the connection string of the database, e.g.
   ```XML
   <?xml version="1.0" encoding="utf-8" ?>
    <configuration>
    	<connectionStrings>
    		<add name="TodoListDB" connectionString="Server=.\SQLEXPRESS;Database=TodoList;Integrated Security=True;" providerName="System.Data.SqlClient"/>
    	</connectionStrings>
    </configuration>
   ```

5. Run the application:
    ```bash
    # Install dependencies
  	dotnet restore
  
  	# Run the application
  	dotnet run
    ```

## License

This app is licensed under the MIT License.

## Contact Me

If you have any questions, feel free to reach out to me at:

<a href="https://www.linkedin.com/in/getimad/" target="_blank">
  <img alt="Static Badge" src="https://img.shields.io/badge/LinkedIn-blue?style=for-the-badge&logo=linkedin">
</a>
