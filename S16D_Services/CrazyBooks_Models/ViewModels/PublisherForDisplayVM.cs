using CrazyBooks_Models.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace CrazyBooks_Models.ViewModels
{
    public class PublisherForDisplayVM
    {
        public PublisherForDisplayVM()
        { }
        public PublisherForDisplayVM(Publisher p)
        {
            Id = p.Id;
            Name = p.Name;
            Speciality = p.Speciality;
            PublisherSite = p.PublisherSite;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Speciality { get; set; }
        public string PublisherSite { get; set; }
    }
}
