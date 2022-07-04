using System;
using System.Collections.Generic;
using DataObjects;

namespace QueryProcessor
{
    public class Processor
    {
        public Processor() { }

        public List<Schedule> ProcessQuery(string jsonInput, string instructions, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (string.IsNullOrEmpty(jsonInput) || string.IsNullOrEmpty(instructions))
                return null;

            RecipeResponse rr = new RecipeResponse();
            Recipes r = rr.JsonToRecipes(instructions);

            RecipeInput recipeInput = new RecipeInput();
            var input  = recipeInput.JsonToRecipes(jsonInput);

            try
            {

                List<Schedule> schedules = new List<Schedule>();
                foreach (Tray tray in input.input)
                {
                    switch (tray.recipeName)
                    {
                        case "Basil":
                            var basil = r.recipes.Find(x => x.Name == "Basil");
                            if (basil != null)
                            {
                                Schedule basilSchedule = new Schedule(tray.trayNumber);

                                List<wateringInstruction> WateringTemp = new List<wateringInstruction>();
                                basilSchedule.WateringInstructions = processWateringInstructions(basil.WateringPhases, tray.startDate);

                                List<LightingInstruction> lightingTemp = new List<LightingInstruction>();
                                basilSchedule.LightingInstructions = processLightingInstructions(basil.LightingPhases, tray.startDate);

                                schedules.Add(basilSchedule);
                            }

                            break;
                        case "Strawberries":
                            var Strawberries = r.recipes.Find(x => x.Name == "Strawberries");
                            if (Strawberries != null)
                            {
                                Schedule strawberrySchedule = new Schedule(tray.trayNumber);

                                List<wateringInstruction> WateringTemp = new List<wateringInstruction>();
                                strawberrySchedule.WateringInstructions = processWateringInstructions(Strawberries.WateringPhases, tray.startDate);

                                List<LightingInstruction> lightingTemp = new List<LightingInstruction>();
                                strawberrySchedule.LightingInstructions = processLightingInstructions(Strawberries.LightingPhases, tray.startDate);

                                schedules.Add(strawberrySchedule);
                            }
                            break;
                        default:
                            Console.WriteLine("Invalid plant name");
                            break;
                    }
                }

                return schedules;

            }
            catch (Exception e)
            {
                errorMessage = $"An Exception occured while proceesing the query: {e.Message}";
                return null;
            }            
        }

        private List<LightingInstruction> processLightingInstructions(List<LightingPhase> phases, string startDate)
        {
            if (string.IsNullOrEmpty(startDate))
                return null;

            List<LightingInstruction> lightInstrs = new List<LightingInstruction>();

            DateTime tempTime = DateTime.Parse(startDate).ToUniversalTime();
            foreach (LightingPhase phase in phases)
            {
                for (int i = 0; i < phase.Repetitions; i++)
                {
                    foreach (var ops in phase.Operations)
                    {
                        var lt = new LightingInstruction(ops.LightIntensity, tempTime.AddHours(ops.OffsetHours).AddMinutes(ops.OffsetMinutes));
                        lightInstrs.Add(lt);
                    }

                    tempTime = tempTime.AddHours(phase.Hours).AddMinutes(phase.Minutes);
                }
            }

            return lightInstrs;

        }


        private List<wateringInstruction> processWateringInstructions(List<WateringPhase> phases, string startDate)
        {
            if (string.IsNullOrEmpty(startDate))
                return null;

            List<wateringInstruction> waterInstrs = new List<wateringInstruction>();

            DateTime tempTime = DateTime.Parse(startDate).ToUniversalTime();
            foreach (WateringPhase phase in phases)
            {
                // provent unessary instuctions being sent
                if (phase.Amount == 0)
                    tempTime = tempTime.AddHours(phase.Hours * phase.Repetitions)
                                        .AddMinutes(phase.Minutes * phase.Repetitions);
                else
                {
                    for (int i = 0; i < phase.Repetitions; i++)
                    {
                        var wt = new wateringInstruction(phase.Amount, tempTime);
                        waterInstrs.Add(wt);
                        tempTime = tempTime.AddHours(phase.Hours).AddMinutes(phase.Minutes);
                    }
                }
            };

            return waterInstrs;
        }

        public void outputSchedul(List<Schedule> schedules)
        {
            foreach (Schedule s in schedules)
            {
                Console.WriteLine($"Tray {s.trayNumber} schedule:");

                Console.WriteLine("Watering schedule:");
                foreach (wateringInstruction w in s.WateringInstructions)
                    Console.WriteLine($"Amount: {w.amount}; Date time: {w.wateringTime.ToString()}");

                Console.WriteLine("Lighting schedule:");
                foreach (LightingInstruction w in s.LightingInstructions)
                    Console.WriteLine($"Light intensity: {w.intensity}; Date time: {w.wateringTime.ToString()}");

                Console.WriteLine("");                
            }

            Console.ReadKey(true);
        }
    }
}