﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Irony.Ast;
using Irony.Parsing;

namespace Pash.ParserIntrinsics.Nodes
{
    public class statement_list_node : _node
    {
        public statement_list_node(AstContext astContext, ParseTreeNode parseTreeNode)
            : base(astContext, parseTreeNode)
        {
        }

        internal override void Execute(Implementation.ExecutionContext context, System.Management.Automation.ICommandRuntime commandRuntime)
        {
            throw new NotImplementedException();
        }

        internal override object GetValue(Implementation.ExecutionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
