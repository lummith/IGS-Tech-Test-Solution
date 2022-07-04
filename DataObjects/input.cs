using System.Collections.Generic;
using Newtonsoft.Json;

namespace DataObjects
{
    public class RecipeInput
    {
        public Input? input { get; set; }

        public Input JsonToRecipes(string json) => JsonConvert.DeserializeObject<Input>(json);

    }

    public record Input(List<Tray> input);

    public record Tray(int trayNumber, string recipeName, string startDate);

}
