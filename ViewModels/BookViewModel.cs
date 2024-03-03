using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace electronic_library_6.ViewModels {
    public class BookViewModel 
    {
     
        [Required]
        [Display(Name = "Категория")]
        public int CategoryId { get; set; }
        
        public List<SelectListItem> Categories { get; set; } = new();

        [Display(Name = "общее число проголосовавших людей")]
        public int TotalNumberPeopleVoted { get; set; }

        [Display(Name = "билютени выделенные на участок")]
        public int BallotsAllocatedSite { get; set; }
        [Display(Name = "билютени которые пропали или не правильно заполненны")]
        public int BallotsMissingIncorrect { get; set; }

        [Required]
        [Display(Name = "Номер участка")]
        public int PollingStationsId { get; set; }

        public List<SelectListItem> PollingStations { get; set; } = new();

       
    }
}
