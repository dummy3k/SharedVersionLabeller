using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Exortech.NetReflector;
using ThoughtWorks.CruiseControl.Core;
using ThoughtWorks.CruiseControl.Core.Util;
using System.Xml.Serialization;

namespace CCNet.SharedVersionLabeller.Plugin
{
    [ReflectorType("SharedVersionLabeller")]
    public class Labeller : ILabeller
    {

        #region Properties

        /// <summary>
        /// Gets or sets the version prefix.
        /// </summary>
        /// <value>The version prefix.</value>
        [ReflectorProperty("prefix", Required = true)]
        public string Prefix { get; set; }

        /// <summary>
        /// Gets or sets the version prefix.
        /// </summary>
        /// <value>The version prefix.</value>
        [ReflectorProperty("stateFileName", Required = true)]
        public string StateFileName { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Runs the task, given the specified <see cref="IIntegrationResult"/>, in the specified <see cref="IProject"/>.
        /// </summary>
        /// <param name="result">The label for the current build.</param>
        public void Run(IIntegrationResult result)
        {
            Log.Debug("Run(Succeeded={0})", result.Succeeded);
            result.Label = Generate(result);
        }

        /// <summary>
        /// Returns the label to use for the current build.
        /// </summary>
        /// <param name="resultFromLastBuild">IntegrationResult from last build used to determine the next label.</param>
        /// <returns>The label for the current build.</returns>
        /// <exception cref="System.ArgumentException">Thrown when an error occurs while formatting the version number using the various formatting tokens.</exception>
        /// <exception cref="System.ArgumentNullException">Thrown when an error occurs while formatting the version number and an argument has not been specified.</exception>
        public string Generate(IIntegrationResult integrationResult)
        {
            Log.Debug("Generate(Succeeded={0})", integrationResult.Succeeded);

            var serializer = new XmlSerializer(typeof(State));
            State s;
            if (!System.IO.File.Exists(StateFileName))
            {
                s = new State();
                s.Prefixes = new List<PrefixState>();
            }
            else
            {
                using (var stream = System.IO.File.OpenRead(StateFileName))
                {
                    s = (State)serializer.Deserialize(stream);
                }
            }

            var prefix = (from item in s.Prefixes
                              where item.Prefix == Prefix
                              select item).FirstOrDefault();
            if (prefix == null)
            {
                prefix = new PrefixState() { Prefix = Prefix };
                s.Prefixes.Add(prefix);
            }

                prefix.Build++;

                using (var stream = System.IO.File.OpenWrite(StateFileName))
                {
                    serializer.Serialize(stream, s);
                }


            return Prefix + prefix.Build.ToString();
        }

        #endregion

    }
}
