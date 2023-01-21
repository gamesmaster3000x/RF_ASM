using RedFoxAssembly.AntlrBuild;
using RedFoxAssembly.CSharp.Compiler.Tokens;
using RedFoxAssembly.CSharp.Statements;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedFoxAssembly.CSharp.Compiler
{
    internal class RFASMProgramVisitor : RedFoxAssemblyBaseVisitor<object>
    {
        public override object VisitProgram([NotNull] RedFoxAssemblyParser.ProgramContext context)
        {
            return base.VisitProgram(context);
        }

        public override object VisitConfiguration([NotNull] RedFoxAssemblyParser.ConfigurationContext context)
        {
            return base.VisitConfiguration(context);
        }

        public override object VisitWidth([NotNull] RedFoxAssemblyParser.WidthContext context)
        {
            return base.VisitWidth(context);
        }

        public override object VisitValue([NotNull] RedFoxAssemblyParser.ValueContext context)
        {
            return base.VisitValue(context);
        }

        public override object VisitCommand([NotNull] RedFoxAssemblyParser.CommandContext context)
        {
            return base.VisitCommand(context);
        }

        public override object VisitLabel([NotNull] RedFoxAssemblyParser.LabelContext context)
        {
            return base.VisitLabel(context);
        }

        public object ParseInstruction(RedFoxAssemblyParser.InstructionContext context)
        {
            if (context == null) throw new ParsingException("Cannot parse null InstructionContext");
            if (context is RedFoxAssemblyParser.HLTInstructionContext) return VisitHLTInstruction((RedFoxAssemblyParser.HLTInstructionContext)context);
            if (context is RedFoxAssemblyParser.NOPInstructionContext) return VisitNOPInstruction((RedFoxAssemblyParser.NOPInstructionContext)context);
            if (context is RedFoxAssemblyParser.ADDInstructionContext) return VisitADDInstruction((RedFoxAssemblyParser.ADDInstructionContext)context);
            if (context is RedFoxAssemblyParser.SUBInstructionContext) return VisitSUBInstruction((RedFoxAssemblyParser.SUBInstructionContext)context);
            if (context is RedFoxAssemblyParser.LSLInstructionContext) return VisitLSLInstruction((RedFoxAssemblyParser.LSLInstructionContext)context);
            if (context is RedFoxAssemblyParser.LSRInstructionContext) return VisitLSRInstruction((RedFoxAssemblyParser.LSRInstructionContext)context);
            if (context is RedFoxAssemblyParser.NEGInstructionContext) return VisitNEGInstruction((RedFoxAssemblyParser.NEGInstructionContext)context);
            if (context is RedFoxAssemblyParser.NOTInstructionContext) return VisitNOTInstruction((RedFoxAssemblyParser.NOTInstructionContext)context);
            if (context is RedFoxAssemblyParser.CMPInstructionContext) return VisitCMPInstruction((RedFoxAssemblyParser.CMPInstructionContext)context);
            if (context is RedFoxAssemblyParser.JMPInstructionContext) return VisitJMPInstruction((RedFoxAssemblyParser.JMPInstructionContext)context);
            if (context is RedFoxAssemblyParser.BFGInstructionContext) return VisitBFGInstruction((RedFoxAssemblyParser.BFGInstructionContext)context);
            //if (context is RedFoxAssemblyParser.HLTInstructionContext) return VisitHLTInstruction((RedFoxAssemblyParser.HLTInstructionContext)context);
            //if (context is RedFoxAssemblyParser.HLTInstructionContext) return VisitHLTInstruction((RedFoxAssemblyParser.HLTInstructionContext)context);
            //if (context is RedFoxAssemblyParser.HLTInstructionContext) return VisitHLTInstruction((RedFoxAssemblyParser.HLTInstructionContext)context);
            if (context is RedFoxAssemblyParser.BSRInstructionContext) return VisitBSRInstruction((RedFoxAssemblyParser.BSRInstructionContext)context);
            if (context is RedFoxAssemblyParser.RTNInstructionContext) return VisitRTNInstruction((RedFoxAssemblyParser.RTNInstructionContext)context);
            if (context is RedFoxAssemblyParser.RRBInstructionContext) return VisitRRBInstruction((RedFoxAssemblyParser.RRBInstructionContext)context);
            if (context is RedFoxAssemblyParser.RRWInstructionContext) return VisitRRWInstruction((RedFoxAssemblyParser.RRWInstructionContext)context);
            if (context is RedFoxAssemblyParser.RMBInstructionContext) return VisitRMBInstruction((RedFoxAssemblyParser.RMBInstructionContext)context);
            if (context is RedFoxAssemblyParser.RMWInstructionContext) return VisitRMWInstruction((RedFoxAssemblyParser.RMWInstructionContext)context);
            if (context is RedFoxAssemblyParser.WRBInstructionContext) return VisitWRBInstruction((RedFoxAssemblyParser.WRBInstructionContext)context);
            if (context is RedFoxAssemblyParser.WRWInstructionContext) return VisitWRWInstruction((RedFoxAssemblyParser.WRWInstructionContext)context);
            if (context is RedFoxAssemblyParser.WMBInstructionContext) return VisitWMBInstruction((RedFoxAssemblyParser.WMBInstructionContext)context);
            if (context is RedFoxAssemblyParser.WMWInstructionContext) return VisitWMWInstruction((RedFoxAssemblyParser.WMWInstructionContext)context);
            if (context is RedFoxAssemblyParser.RVBInstructionContext) return VisitRVBInstruction((RedFoxAssemblyParser.RVBInstructionContext)context);
            if (context is RedFoxAssemblyParser.RVWInstructionContext) return VisitRVWInstruction((RedFoxAssemblyParser.RVWInstructionContext)context);
            if (context is RedFoxAssemblyParser.SINInstructionContext) return VisitSINInstruction((RedFoxAssemblyParser.SINInstructionContext)context);
            if (context is RedFoxAssemblyParser.INTInstructionContext) return VisitINTInstruction((RedFoxAssemblyParser.INTInstructionContext)context);
            if (context is RedFoxAssemblyParser.SFGInstructionContext) return VisitSFGInstruction((RedFoxAssemblyParser.SFGInstructionContext)context);
            if (context is RedFoxAssemblyParser.ANDInstructionContext) return VisitANDInstruction((RedFoxAssemblyParser.ANDInstructionContext)context);
            if (context is RedFoxAssemblyParser.LORInstructionContext) return VisitLORInstruction((RedFoxAssemblyParser.LORInstructionContext)context);
            if (context is RedFoxAssemblyParser.XORInstructionContext) return VisitXORInstruction((RedFoxAssemblyParser.XORInstructionContext)context);
            else throw new ParsingException("Unable to ParseInstruction of type " + context.GetType());
        }

        public override Instruction VisitHLTInstruction([NotNull] RedFoxAssemblyParser.HLTInstructionContext context) { return VisitHlt(context.hlt()); }
        public override Instruction VisitHlt([NotNull] RedFoxAssemblyParser.HltContext context) { return new Instruction(InstructionType.HLT, null, null); }

        public override Instruction VisitNOPInstruction([NotNull] RedFoxAssemblyParser.NOPInstructionContext context) { return VisitNop(context.nop()); }
        public override Instruction VisitNop([NotNull] RedFoxAssemblyParser.NopContext context) { return new Instruction(InstructionType.NOP, null, null); }

        public override Instruction VisitADDInstruction([NotNull] RedFoxAssemblyParser.ADDInstructionContext context) { return VisitAdd(context.add()); }
        public override Instruction VisitAdd([NotNull] RedFoxAssemblyParser.AddContext context) { return new Instruction(InstructionType.ADD, null, null); }

        public override Instruction VisitSUBInstruction([NotNull] RedFoxAssemblyParser.SUBInstructionContext context) { return VisitSub(context.sub()); }
        public override Instruction VisitSub([NotNull] RedFoxAssemblyParser.SubContext context) { return new Instruction(InstructionType.SUB, null, null); }

        public override Instruction VisitLSLInstruction([NotNull] RedFoxAssemblyParser.LSLInstructionContext context) { return VisitLsl(context.lsl()); }
        public override Instruction VisitLsl([NotNull] RedFoxAssemblyParser.LslContext context) { return new Instruction(InstructionType.LSL, null, null); }

        public override Instruction VisitLSRInstruction([NotNull] RedFoxAssemblyParser.LSRInstructionContext context) { return VisitLsr(context.lsr()); }
        public override Instruction VisitLsr([NotNull] RedFoxAssemblyParser.LsrContext context) { return new Instruction(InstructionType.LSR, null, null); }

        public override Instruction VisitNEGInstruction([NotNull] RedFoxAssemblyParser.NEGInstructionContext context) { return VisitNeg(context.neg()); }
        public override Instruction VisitNeg([NotNull] RedFoxAssemblyParser.NegContext context) { return new Instruction(InstructionType.NEG, null, null); }

        public override Instruction VisitNOTInstruction([NotNull] RedFoxAssemblyParser.NOTInstructionContext context) { return VisitNot(context.not()); }
        public override Instruction VisitNot([NotNull] RedFoxAssemblyParser.NotContext context) { return new Instruction(InstructionType.NOT, null, null); }

        public override Instruction VisitCMPInstruction([NotNull] RedFoxAssemblyParser.CMPInstructionContext context) { return VisitCmp(context.cmp()); }
        public override Instruction VisitCmp([NotNull] RedFoxAssemblyParser.CmpContext context) { return new Instruction(InstructionType.CMP, null, null); }

        public override Instruction VisitJMPInstruction([NotNull] RedFoxAssemblyParser.JMPInstructionContext context) { return VisitJmp(context.jmp()); }
        public override Instruction VisitJmp([NotNull] RedFoxAssemblyParser.JmpContext context) { return new Instruction(InstructionType.JMP, null, null); }

        public override Instruction VisitBFGInstruction([NotNull] RedFoxAssemblyParser.BFGInstructionContext context) { return VisitBfg(context.bfg()); }
        public override Instruction VisitBfg([NotNull] RedFoxAssemblyParser.BfgContext context) { return new Instruction(InstructionType.BFG, null, null); }

        //public override Instruction VisitBFGInstruction([NotNull] RedFoxAssemblyParser.BFGInstructionContext context) { return VisitBfg(context.bfg()); }
        //public override Instruction VisitBfg([NotNull] RedFoxAssemblyParser.BfgContext context) { return new Instruction(InstructionType.BFG, null, null); }

        //public override Instruction VisitBFGInstruction([NotNull] RedFoxAssemblyParser.BFGInstructionContext context) { return VisitBfg(context.bfg()); }
        //public override Instruction VisitBfg([NotNull] RedFoxAssemblyParser.BfgContext context) { return new Instruction(InstructionType.BFG, null, null); }

        //public override Instruction VisitBFGInstruction([NotNull] RedFoxAssemblyParser.BFGInstructionContext context) { return VisitBfg(context.bfg()); }
        //public override Instruction VisitBfg([NotNull] RedFoxAssemblyParser.BfgContext context) { return new Instruction(InstructionType.BFG, null, null); }

        public override Instruction VisitBSRInstruction([NotNull] RedFoxAssemblyParser.BSRInstructionContext context) { return VisitBsr(context.bsr()); }
        public override Instruction VisitBsr([NotNull] RedFoxAssemblyParser.BsrContext context) { return new Instruction(InstructionType.BSR, null, null); }

        public override Instruction VisitRTNInstruction([NotNull] RedFoxAssemblyParser.RTNInstructionContext context) { return VisitRtn(context.rtn()); }
        public override Instruction VisitRtn([NotNull] RedFoxAssemblyParser.RtnContext context) { return new Instruction(InstructionType.RTN, null, null); }

        public override Instruction VisitRRBInstruction([NotNull] RedFoxAssemblyParser.RRBInstructionContext context) { return VisitRrb(context.rrb()); }
        public override Instruction VisitRrb([NotNull] RedFoxAssemblyParser.RrbContext context) { return new Instruction(InstructionType.RRB, null, null); }

        public override Instruction VisitRRWInstruction([NotNull] RedFoxAssemblyParser.RRWInstructionContext context) { return VisitRrw(context.rrw()); }
        public override Instruction VisitRrw([NotNull] RedFoxAssemblyParser.RrwContext context) { return new Instruction(InstructionType.RRW, null, null); }

        public override Instruction VisitRMBInstruction([NotNull] RedFoxAssemblyParser.RMBInstructionContext context) { return VisitRmb(context.rmb()); }
        public override Instruction VisitRmb([NotNull] RedFoxAssemblyParser.RmbContext context) { return new Instruction(InstructionType.RMB, null, null); }

        public override Instruction VisitRMWInstruction([NotNull] RedFoxAssemblyParser.RMWInstructionContext context) { return VisitRmw(context.rmw()); }
        public override Instruction VisitRmw([NotNull] RedFoxAssemblyParser.RmwContext context) { return new Instruction(InstructionType.RMW, null, null); }

        public override Instruction VisitWRBInstruction([NotNull] RedFoxAssemblyParser.WRBInstructionContext context) { return VisitWrb(context.wrb()); }
        public override Instruction VisitWrb([NotNull] RedFoxAssemblyParser.WrbContext context) { return new Instruction(InstructionType.WRB, null, null); }

        public override Instruction VisitWRWInstruction([NotNull] RedFoxAssemblyParser.WRWInstructionContext context) { return VisitWrw(context.wrw()); }
        public override Instruction VisitWrw([NotNull] RedFoxAssemblyParser.WrwContext context) { return new Instruction(InstructionType.WRW, null, null); }

        public override Instruction VisitWMBInstruction([NotNull] RedFoxAssemblyParser.WMBInstructionContext context) { return VisitWmb(context.wmb()); }
        public override Instruction VisitWmb([NotNull] RedFoxAssemblyParser.WmbContext context) { return new Instruction(InstructionType.WMB, null, null); }

        public override Instruction VisitWMWInstruction([NotNull] RedFoxAssemblyParser.WMWInstructionContext context) { return VisitWmw(context.wmw()); }
        public override Instruction VisitWmw([NotNull] RedFoxAssemblyParser.WmwContext context) { return new Instruction(InstructionType.WMW, null, null); }

        public override Instruction VisitRVBInstruction([NotNull] RedFoxAssemblyParser.RVBInstructionContext context) { return VisitRvb(context.rvb()); }
        public override Instruction VisitRvb([NotNull] RedFoxAssemblyParser.RvbContext context) { return new Instruction(InstructionType.RVB, null, null); }

        public override Instruction VisitRVWInstruction([NotNull] RedFoxAssemblyParser.RVWInstructionContext context) { return VisitRvw(context.rvw()); }
        public override Instruction VisitRvw([NotNull] RedFoxAssemblyParser.RvwContext context) { return new Instruction(InstructionType.RVW, null, null); }

        public override Instruction VisitSINInstruction([NotNull] RedFoxAssemblyParser.SINInstructionContext context) { return VisitSin(context.sin()); }
        public override Instruction VisitSin([NotNull] RedFoxAssemblyParser.SinContext context) { return new Instruction(InstructionType.SIN, null, null); }

        public override Instruction VisitINTInstruction([NotNull] RedFoxAssemblyParser.INTInstructionContext context) { return VisitInt(context.@int()); }
        public override Instruction VisitInt([NotNull] RedFoxAssemblyParser.IntContext context) { return new Instruction(InstructionType.INT, null, null); }

        public override Instruction VisitSFGInstruction([NotNull] RedFoxAssemblyParser.SFGInstructionContext context) { return VisitSfg(context.sfg()); }
        public override Instruction VisitSfg([NotNull] RedFoxAssemblyParser.SfgContext context) { return new Instruction(InstructionType.SFG, null, null); }

        public override Instruction VisitANDInstruction([NotNull] RedFoxAssemblyParser.ANDInstructionContext context) { return VisitAnd(context.and()); }
        public override Instruction VisitAnd([NotNull] RedFoxAssemblyParser.AndContext context) { return new Instruction(InstructionType.AND, null, null); }

        public override Instruction VisitLORInstruction([NotNull] RedFoxAssemblyParser.LORInstructionContext context) { return VisitLor(context.lor()); }
        public override Instruction VisitLor([NotNull] RedFoxAssemblyParser.LorContext context) { return new Instruction(InstructionType.LOR, null, null); }

        public override Instruction VisitXORInstruction([NotNull] RedFoxAssemblyParser.XORInstructionContext context) { return VisitXor(context.xor()); }
        public override Instruction VisitXor([NotNull] RedFoxAssemblyParser.XorContext context) { return new Instruction(InstructionType.XOR, null, null); }
    }
}
