using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RandomActs.Models
{
    // Requires following using statements:

    public class RandomAct
    {
        public int RandomActId { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        [Required]
        [Display(Name = "Start Date & Time")]
        public DateTime StartTime { get; set; }
        [Required]
        [Display(Name = "End Date & Time")]
        public DateTime EndTime { get; set; }
        [Required]
        [Display(Description = "Maximum Number of Volunteers")]
        public int MaxActors { get; set; }

        public virtual ICollection<RandomActActor> Actors { get; set; }
    }

    [DisplayColumn("FullName", "FullName", false)]
    public class RandomActor
    {
        public int RandomActorId { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string Email { get; set; }
        [Display(Name = "Twitter Handle")]
        [RegularExpression("@.*",ErrorMessage="Twitter handle must start with an '@'")]
        public string TwitterHandle { get; set; }
        [NotMapped]
        [Display(Name = "Volunteer")]
        public string FullName
        {
            get
            {
                return LastName + ", " + FirstName;
            }
        }

        public virtual ICollection<RandomActActor> Acts { get; set; }
    }

    public class RandomActActor
    {
        public int RandomActActorId { get; set; }
        [Required]
        public int RandomActId { get; set; }
        [ForeignKey("RandomActId")]
        public virtual RandomAct Act { get; set; }
        [Required]
        public int RandomActorId { get; set; }
        [ForeignKey("RandomActorId")]
        public virtual RandomActor Actor { get; set; }
        [Display(Name="What I Can Bring")]
        public string WhatICanBring { get; set; }
        [Display(Name = "Message to Share")]
        public string Message { get; set; }
        [Display(Name="On Waiting List?")]
        public bool WaitList { get; set; }
    }
}
