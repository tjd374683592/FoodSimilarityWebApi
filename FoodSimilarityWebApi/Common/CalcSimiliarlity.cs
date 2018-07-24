using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using FoodSimilarityWebApi.Models;
using Microsoft.Cognitive.CustomVision.Prediction;

namespace FoodSimilarityWebApi.Common
{
    public class CalcSimiliarlity
    {
        public static FoodSimResult CalcSim(string fullPath)
        {
            // Add your prediction key from the settings page of the portal
            // The prediction key is used in place of the training key when making predictions
            string predictionKey = "72de5d5f661e4114a737d5f56561b604";

            // Create a prediction endpoint, passing in obtained prediction key
            PredictionEndpoint endpoint = new PredictionEndpoint() { ApiKey = predictionKey };

            // Make a prediction against the new project
            var testImage = new MemoryStream(System.IO.File.ReadAllBytes(fullPath));
            Guid strProjectId = new Guid("6248af60-83de-4488-837e-6ba0d9e5ca14");

            var result = endpoint.PredictImage(strProjectId, testImage);

            //// Loop over each prediction and write out the results
            //foreach (var c in result.Predictions)
            //{
            //    Console.WriteLine($"\t{c.Tag}: {c.Probability:P1}");
            //}

            double max = 0;
            var name = "";

            foreach (var c in result.Predictions)
            {
                if (c.Probability > max)
                {
                    max = c.Probability;
                    name = c.Tag;
                }
            }

            return new FoodSimResult() {Name = name, Similarity = max};
        }
    }
}