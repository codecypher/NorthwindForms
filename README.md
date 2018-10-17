
# Northwind Forms

Sample Windows Forms Application using C#.Net and <a href="https://www.microsoft.com/en-us/download/details.aspx?id=23654">Northwind</a> database.

## Form1.cs

Displays Customers table in DataGridView control and double-clicking a row in grid
opens Form3.cs to allow user to view and/or edit the customer record.

Also shows how to call methods on another form (Form3) and have it call a method on this form.

## Form2.cs

Master/Detail Form using DataGridView control that displays Customers table and selecting a row
in master grid populates the bottom grid with the order details for the selected customer.

## Form3.cs

Displays customer record selected using Form1.cs for user to view and/or edit.
Also displays how to handle concurrency issues.

