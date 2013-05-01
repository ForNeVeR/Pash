// Copyright (C) Pash Contributors. License: GPL/BSD. See https://github.com/Pash-Project/Pash/
using System;
using System.Management.Automation;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;

namespace Pash.Implementation
{
    /// <summary>
    /// Command processor for the application command. This is command for executing external file.
    /// </summary>
    internal class ApplicationProcessor : CommandProcessorBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Pash.Implementation.ApplicationProcessor"/> class.
        /// </summary>
        /// <param name="commandInfo">Command info.</param>
        public ApplicationProcessor(ApplicationInfo commandInfo)
            : base(commandInfo)
        {
        }

        internal override void BindArguments(PSObject obj)
        {
        }

        internal override void Initialize()
        {
        }

        internal override void ProcessRecord()
        {
            var process = StartProcess();
            var output = process.StandardOutput;

            while (!output.EndOfStream)
            {
                var line = output.ReadLine();
                CommandRuntime.WriteObject(line);
            }

            // TODO: Should we set $LASTEXITCODE here?
            // TODO: Same for the $? variable.
        }

        internal override void Complete()
        {
        }

        internal override System.Management.Automation.ICommandRuntime CommandRuntime
        {
            get;
            set;
        }

        private ApplicationInfo ApplicationInfo
        {
            get
            {
                return (ApplicationInfo)CommandInfo;
            }
        }

        private Process StartProcess()
        {
            var startInfo = new ProcessStartInfo(ApplicationInfo.Path);
            startInfo.Arguments = PrepareArguments();
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            
            Console.WriteLine(
                string.Format(
                "Starting a process {0} with arguments: <{1}>",
                startInfo.FileName, startInfo.Arguments));
            
            var process = new Process();
            process.StartInfo = startInfo;
            
            if (!process.Start())
            {
                throw new Exception("Cannot start process");
            }
            
            return process;
        }

        private string PrepareArguments()
        {
            var arguments = new StringBuilder();
            foreach (var parameter in Parameters)
            {
                arguments.Append(parameter.Value.ToString());
                arguments.Append(' ');
            }

            return arguments.ToString();
        }
    }
}

