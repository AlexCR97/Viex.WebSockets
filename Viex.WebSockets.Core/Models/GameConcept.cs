using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Viex.WebSockets.Core.Models
{
    public class GameConcept
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GameConceptId { get; set; }
        public string Description { get; set; }
    }
}
