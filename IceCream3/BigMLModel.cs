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
        public async Task<bool> CreateModel(string dataFilePath)
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
            Source source = await client.CreateSource(dataFilePath, "Data");
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
            string[] inputFields = { "City", "Quantity", "DayOfWeek", "Season", "Degree", "Humidity" };
            //string[] inputFields = { "000000","000001","000002","000003", "000004", "000005" };
            var parameters = new Model.Arguments();
            parameters.Add("name", "sell_model");
            // using the dataset ID as argument
            parameters.Add("dataset", dataset.Resource);
            parameters.Add("input_fields", inputFields);
            parameters.Add("objective_field", "Flavor");
            //var model = await client.CreateModel(dataset, "sell", inputFields);
            var model = await client.CreateModel(parameters);

            // No push, so we need to busy wait for the source to be processed.
            while ((model = await client.Get(model)).StatusMessage.StatusCode != Code.Finished) await Task.Delay(10);
            Console.WriteLine(model.StatusMessage.ToString());
            Console.WriteLine(model.InputFields);
            Console.WriteLine(model.Object);
            Console.WriteLine(model.ObjectiveField);
            // Transforms JSON in tree structure
            Model.LocalModel localModel = model.ModelStructure();

            // --- Specify prediction inputs and calculate the prediction ---
            // input data can be provided by fieldID or by name
            Dictionary<string, dynamic> inputData = new Dictionary<string, dynamic>();
            inputData.Add("City", "Jerusalem");
            inputData.Add("Quantity", 1);
            inputData.Add("DayOfWeek", "Tuesday");
            inputData.Add("Season", "Autumn");
            inputData.Add("Degree", 19.2);
            inputData.Add("humidity", 22);
            
            // Other values are ommited or unknown
            Model.Node prediction = localModel.predict(inputData);

            Console.WriteLine("result = {0}, expected = {1}", prediction.Output, "Vanilla");

            //// Set the project and update the Source
            //JObject changes = new JObject();
            //changes["project"] = projectID;
            //source = await client.Update<Source>(source.Resource, changes);
            return true;
        }
    }
}