using System.ComponentModel.DataAnnotations;

namespace electronic_library_6.Domain.Entities
{
    public class Book : Entity
    {
       
     
        public int CategoryId { get; set; }
        public Categories Category { get; set; } = null!;
        public int TotalNumberPeopleVoted { get; set; }
        public int BallotsAllocatedSite { get; set; }
        public int BallotsMissingIncorrect { get; set; }
        public int PollingStationsId { get; set; }
        public PollingStations PollingStations { get; set; } = null!;
    }
}
