using WpfApp1.Models;
using System.Collections.Generic;
using System.Linq;

namespace WpfApp1.Services
{
    public class AuthenticationService
    {
        private List<User> users = new List<User>();

        public AuthenticationService()
        {
            // Пример добавления пользователей
            users.Add(new User { Username = "student", Password = "1", Role = UserRole.Student });
            users.Add(new User { Username = "teacher", Password = "1", Role = UserRole.Teacher });
        }

        public bool Register(string username, string password, UserRole role)
        {
            if (users.Any(u => u.Username == username))
                return false;

            users.Add(new User { Username = username, Password = password, Role = role });
            return true;
        }

        public User Login(string username, string password)
        {
            // Логирование для отладки
            System.Diagnostics.Debug.WriteLine($"Попытка входа с логином: {username} и паролем: {password}");

            var user = users.FirstOrDefault(u => u.Username == username && u.Password == password);

            if (user == null)
            {
                System.Diagnostics.Debug.WriteLine("Неверные данные для входа.");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"Вход успешен для пользователя: {user.Username}");
            }

            return user;
        }
    }
}
