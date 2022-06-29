using CrazyBooks_Models.Models;

namespace CrazyBooks_Models.ViewModels
{
    public class SubjectForDisplayVM
    {
        public SubjectForDisplayVM()
        { }
        public SubjectForDisplayVM(Subject s)
        {
            Id = s.Id;
            Name = s.Name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
