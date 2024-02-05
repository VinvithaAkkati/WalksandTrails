using System.ComponentModel.DataAnnotations;

namespace Project1.Models.Domain
{
    public class Regions
    {
     
        public Guid Id { get; set; }

      
        public string Code { get; set; }

        
        public string Name { get; set; }
       
        public string? RegionImageUrl { get; set; }
    }
}
