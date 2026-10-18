using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class DatabaseResource
    {
        private List<User> _users =
        [
            new () {Id = 1, Name ="Mohammad"},
            new () {Id = 2, Name ="Khalid"},
            new () {Id = 3, Name ="Ahamd"},
            new () {Id = 4, Name ="Rabe"},
        ];


        public Task<List<User>> GetAsync()
        {
            Task.Delay(5000);

            return Task.FromResult(_users);
        }
    }





}
