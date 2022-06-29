﻿using CrazyBooks_Models.Models;

namespace CrazyBooks_Models.ViewModels
{
    public class AuthorForDisplayVM
    {
        public AuthorForDisplayVM()
        { }
        public AuthorForDisplayVM(Author a)
        {
            Id = a.Id;
            FirstName = a.FirstName;
            LastName = a.LastName;
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
