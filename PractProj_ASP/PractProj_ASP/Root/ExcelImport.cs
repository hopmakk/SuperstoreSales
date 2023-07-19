using PractProj_ASP.Models.Entities;
using OfficeOpenXml;
using System.IO;

namespace PractProj_ASP.Root
{
    public class ExcelImport
    {
        public static void DoImportTo(List<Orders> OrdersList, List<Returns> ReturnsList, List<Users> UsersList)
        {
            FileInfo fileInfo = new FileInfo(@"C:\Users\Андрей\Downloads\Sample-Superstore-Sales-_Excel_.xlsx");

            using (ExcelPackage package = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0]; // orders
                ExcelRange range = worksheet.Cells["A2:U" + worksheet.Dimension.End.Row]; // Получаем диапазон данных
                int count = range.End.Row;
                for (int row = range.Start.Row; row <= count; row++)
                {
                    Orders item = new Orders();

                    if (int.TryParse(range[row, 1]?.Value?.ToString(), out int rowId))
                    {
                        item.RowId = rowId;
                    }

                    if (int.TryParse(range[row, 2]?.Value?.ToString(), out int orderId))
                    {
                        item.OrderId = orderId;
                    }

                    if (DateTime.TryParse(range[row, 3]?.Value?.ToString(), out DateTime orderDate))
                    {
                        item.OrderDate = orderDate;
                    }

                    item.OrderPriority = range[row, 4]?.Value?.ToString();

                    if (int.TryParse(range[row, 5]?.Value?.ToString(), out int orderQuantity))
                    {
                        item.OrderQuantity = orderQuantity;
                    }

                    if (double.TryParse(range[row, 6]?.Value?.ToString(), out double sales))
                    {
                        item.Sales = sales;
                    }

                    if (double.TryParse(range[row, 7]?.Value?.ToString(), out double discount))
                    {
                        item.Discount = discount;
                    }

                    item.ShipMode = range[row, 8]?.Value?.ToString();

                    if (double.TryParse(range[row, 9]?.Value?.ToString(), out double profit))
                    {
                        item.Profit = profit;
                    }

                    if (double.TryParse(range[row, 10]?.Value?.ToString(), out double unitPrice))
                    {
                        item.UnitPrice = unitPrice;
                    }

                    if (double.TryParse(range[row, 11]?.Value?.ToString(), out double shippingCost))
                    {
                        item.ShippingCost = shippingCost;
                    }

                    item.CustomerName = range[row, 12]?.Value?.ToString();
                    item.Privince = range[row, 13]?.Value?.ToString();
                    item.Region = range[row, 14]?.Value?.ToString();
                    item.CustomerSegment = range[row, 15]?.Value?.ToString();
                    item.ProductCategory = range[row, 16]?.Value?.ToString();
                    item.ProductSubCategory = range[row, 17]?.Value?.ToString();
                    item.ProductName = range[row, 18]?.Value?.ToString();
                    item.ProductContainer = range[row, 19]?.Value?.ToString();
                    item.ProductBaseMargin = range[row, 20]?.Value?.ToString();

                    if (DateTime.TryParse(range[row, 21]?.Value?.ToString(), out DateTime shipDate))
                    {
                        item.ShiprDate = shipDate;
                    }

                    OrdersList.Add(item);
                }

                worksheet = package.Workbook.Worksheets[1]; // returns
                range = worksheet.Cells["A2:B" + worksheet.Dimension.End.Row]; // Получаем диапазон данных
                count = range.End.Row;
                for (int row = range.Start.Row; row <= count; row++)
                {
                    Returns item = new Returns();

                    if (int.TryParse(range[row, 1]?.Value?.ToString(), out int intValue))
                    {
                        item.OrderId = intValue;
                    }

                    item.Status = range[row, 2]?.Value?.ToString();

                    ReturnsList.Add(item);
                }

                worksheet = package.Workbook.Worksheets[2]; // users
                range = worksheet.Cells["A2:B" + worksheet.Dimension.End.Row]; // Получаем диапазон данных

                count = range.End.Row;
                for (int row = range.Start.Row; row <= count; row++)
                {
                    Users item = new Users();

                    item.Region = range[row, 1]?.Value?.ToString();
                    item.Manager = range[row, 2]?.Value?.ToString();

                    UsersList.Add(item);
                }
            }
        }
    }
}
