using BigML;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace IceCream3
{
    /// <summary>
    /// This example creates a new project, uploads a local file that generates a
    /// source and includes it in the previosly created project.
    ///
    /// See complete API developers documentation at https://bigml.com/api
    /// </summary>
    class BigMLModel
    {
        public async Task<bool> CreateModel()
        {
            string User = "RMI1234";
            string ApiKey = "62065a8a439292dec60532884585a9828ddb4b10";
            var client = new Client(User, ApiKey);

            // Create a new project
            Project.Arguments pArgs = new Project.Arguments();
            pArgs.Add("name", "My tests");
            Project p = await client.CreateProject(pArgs);

            string projectID = p.Resource;

            // Create a new source
            Source source = await client.CreateSource("orders_data.csv", "Data");
            while ((source = await client.Get<Source>(source.Resource))
                                    .StatusMessage.NotSuccessOrFail())
            {
                await Task.Delay(5000);
            }

            // Default dataset from source
            var dataset = await client.CreateDataset(source);
            // No push, so we need to busy wait for the source to be processed.
            while ((dataset = await client.Get(dataset)).StatusMessage.StatusCode != Code.Finished) await Task.Delay(10);
            Console.WriteLine(dataset.StatusMessage.ToString());

            // Default model from dataset
            string[] inputFields = { "city", "humidity", "temperature" };
            var model = await client.CreateModel(dataset, "sell", inputFields);
            // No push, so we need to busy wait for the source to be processed.
            while ((model = await client.Get(model)).StatusMessage.StatusCode != Code.Finished) await Task.Delay(10);
            Console.WriteLine(model.StatusMessage.ToString());

            // Transforms JSON in tree structure
            Model.LocalModel localModel = model.ModelStructure();

            // --- Specify prediction inputs and calculate the prediction ---
            // input data can be provided by fieldID or by name
            Dictionary<string, dynamic> inputData = new Dictionary<string, dynamic>();
            inputData.Add("city", "Jerusalem");
            inputData.Add("humidity", 2);
            inputData.Add("temperature", 11);
            // Other values are ommited or unknown
            Model.Node prediction = localModel.predict(inputData);

            Console.WriteLine("result = {0}, expected = {1}", prediction.Output, "Chocolate");

            //// Set the project and update the Source
            //JObject changes = new JObject();
            //changes["project"] = projectID;
            //source = await client.Update<Source>(source.Resource, changes);
            return true;
        }
    }
}