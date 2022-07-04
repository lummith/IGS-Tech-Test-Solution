using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public record Schedule(int trayNumber)
    {
        public List<LightingInstruction> LightingInstructions { get; set; } 
        public List<wateringInstruction> WateringInstructions { get; set; }
    }

    public record LightingInstruction(LightIntensity intensity, DateTime wateringTime);

    public record wateringInstruction(short amount, DateTime wateringTime);
    
}
