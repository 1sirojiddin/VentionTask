using CsvProject3.Data;
using CsvProject3.Models;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System;

namespace CsvProject3.Services
{
    public class UserService
    {
        private readonly DataContext _context;

        public UserService(DataContext context)
        {
            _context = context;
        }

        public async Task ProcessExcelFile(Stream fileStream)
        {
            using (var package = new ExcelPackage(fileStream))
            {
                var worksheet = package.Workbook.Worksheets[0]; // Assuming data is in the first worksheet

                for (int row = 2; row <= worksheet.Dimension.Rows; row++) // Start from the second row assuming headers in the first row
                {
                    var Id = worksheet.Cells[row, 1].Value.ToString();
                    var Username = worksheet.Cells[row, 2].Value.ToString();
                    var UserIdentifier = worksheet.Cells[row, 3].Value.ToString();
                    var Age = worksheet.Cells[row, 4].Value.ToString();
                    var City = worksheet.Cells[row, 5].Value.ToString();
                    var PhoneNumber = worksheet.Cells[row, 6].Value.ToString();
                    var Email = worksheet.Cells[row, 7].Value?.ToString();

                    if (int.TryParse(Id, out int id) &&
          int.TryParse(Age, out int age))
                    {
                        var existingUser = _context.Users.FirstOrDefault(u => u.Id == id);

                        if (existingUser != null)
                        {
                            existingUser.Username = Username;
                            existingUser.Age = age;
                            existingUser.Email = Email;
                        }
                        else
                        {
                            _context.Users.Add(new User { Id = id, Username = Username, UserIdentifier = UserIdentifier, Age = age, City = City, PhoneNumber = PhoneNumber, Email = Email });
                        }
                    }
                    else
                    {
                        // Handle parsing errors here
                    }
                }

                await _context.SaveChangesAsync();
            }
        }
        public IEnumerable<User> GetUsersSortedByUsername(bool isAscending )
        {
            IQueryable<User> query = _context.Users;

            if (isAscending)
            {
                query = query.OrderBy(u => u.Username);
            }
            else
            {
                query = query.OrderByDescending(u => u.Username);
            }

            return query.ToList();
        }
    }
}

