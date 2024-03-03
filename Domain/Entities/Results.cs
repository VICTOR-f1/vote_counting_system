using electronic_library_6.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace vote_counting_system.Domain.Entities
{
    public class Results: Entity
    {
        
        public int TotalNumberPeopleVoted { get; set; }
        public int VotesforСandidate { get; set; }
        //id
        public int CandidateId { get; set; }
        public Cand2idate Candidate { get; set; } = null!;

        public int BallotsAllocatedSite { get; set; }
        public int BallotsMissingIncorrect { get; set; }
        //id
        public int UsersId { get; set; }
        public User User { get; set; } = null!;
        //id
        public int VotingId { get; set; }
        public Categories Categories { get; set; } = null!;


    }
}
