using Antlr4.Runtime.Misc;
using NLog.Targets;
using RedFoxAssembly.AntlrBuild;
using RedFoxAssembly.CSharp.Statements;

namespace RedFoxAssembly.CSharp.Core
{
    internal class RFASMProgramVisitor : RedFoxAssemblyBaseVisitor<object>
    {
        public override RFASMProgram VisitProgram([NotNull] RedFoxAssemblyParser.ProgramContext context)
        {
            RFASMProgram program = new RFASMProgram();

            foreach (var c in context._configurations)
            {
                program.AddConfiguration(ParseConfiguration(c));
            }

            foreach (var s in context._commands)
            {
                program.AddCommand(ParseCommand(s));
            }

            return program;
        }

        public IConfiguration ParseConfiguration([NotNull] RedFoxAssemblyParser.ConfigurationContext context)
        {
            if (context == null) throw new ParsingException("ConfigurationContext cannot be null");
            if (context is RedFoxAssemblyParser.WidthConfigurationContext) return VisitWidthConfiguration((RedFoxAssemblyParser.WidthConfigurationContext)context);
            if (context is RedFoxAssemblyParser.ValueConfigurationContext) return VisitValueConfiguration((RedFoxAssemblyParser.ValueConfigurationContext)context);
            throw new ParsingException("Cannot parse ConfigurationContext of type " + context.GetText());
        }

        public override WidthConfiguration VisitWidthConfiguration([NotNull] RedFoxAssemblyParser.WidthConfigurationContext context) { return VisitWidth(context.width()); }
        public override WidthConfiguration VisitWidth([NotNull] RedFoxAssemblyParser.WidthContext context) { return new WidthConfiguration(int.Parse(context.val.Text)); }

        public override ValueConfiguration VisitValueConfiguration([NotNull] RedFoxAssemblyParser.ValueConfigurationContext context) { return VisitValue(context.value()); }
        public override ValueConfiguration VisitValue([NotNull] RedFoxAssemblyParser.ValueContext context)
        {
            string id = context.id.Text;

            try
            {
                if (context.byteValue != null)
                {
                    RByte b = VisitByte(context.byteValue);
                    return new ValueConfiguration(context.id.Text, b);
                }
                if (context.wordValue != null)
                {
                    Word w = VisitWord(context.wordValue);
                    return new ValueConfiguration(context.id.Text, VisitWord(context.wordValue));
                }
            }
            catch (Exception e)
            {
                throw new ParsingException($"Error parsing byte value '{context.byteValue?.GetText()}' or word value '{context.wordValue?.GetText()}' for {id}", e);
            }

            throw new ParsingException($"Value {id} must be assigned either a word or byte value.");
        }

        public override Word VisitWord([NotNull] RedFoxAssemblyParser.WordContext context)
        {
            if (string.IsNullOrWhiteSpace(context.GetText()))
                throw new ParsingException("Word cannot be null or whitespace");

            // Process as raw hex
            string? isHex = context.isHex?.Text;
            if (!String.IsNullOrWhiteSpace(isHex))
            {
                if (context._hexData != null)
                {
                    List<byte> data = new List<byte>();
                    foreach (var b in context._hexData)
                    {
                        string d = b.GetText();
                        if (String.IsNullOrWhiteSpace(d)) throw new ParsingException("Cannot parse null or whitespace byte data");
                        byte[] bs = Convert.FromHexString(d);
                        data.AddRange(bs);
                    }
                    if (data.Count < 1) throw new ParsingException("Word " + context.GetText() + " must contain at least 1 hex byte");
                    return new Word(false, data.ToArray());
                }
            }

            // Process as register reference
            string? registerTarget = context.registerTarget?.Text;
            if (!String.IsNullOrWhiteSpace(registerTarget))
            {
                if (registerTarget.Length != 2) throw new ParsingException("Register prefix '" + registerTarget + "' is not of length 2");
                IData.RegisterTarget target = IData.ParseRegisterTarget(registerTarget[0]);

                string registerData = VisitBytedata(context.registerData);
                byte[] rd = Convert.FromHexString(registerData);
                if (rd.Length != 1) throw new ParsingException("Cannot parse register data of > 1 bytes");
                rd[0] = (byte)(rd[0] + IData.GetRegisterOffset(target));

                return new Word(true, rd);
            }

            // Process as value/identifer
            string? valText = context.val?.Text;
            if (!String.IsNullOrWhiteSpace(valText))
            {
                return new Word(false, valText);
            }

            throw new ParsingException("Failed to visit word with no bytedata or value-mapping");
        }

        public override RByte VisitByte([NotNull] RedFoxAssemblyParser.ByteContext context)
        {
            if (string.IsNullOrWhiteSpace(context.GetText())) throw new ParsingException("Word cannot be null or whitespace");

            // Process as raw hex
            if (context.isHex != null && !String.IsNullOrWhiteSpace(context.isHex.Text))
            {
                string d = context.hexData.GetText();
                if (String.IsNullOrWhiteSpace(d)) throw new ParsingException("Cannot parse null or whitespace byte data");
                byte[] bs = Convert.FromHexString(d);
                return new RByte(false, bs[0]);
            }

            // Process as register reference
            string? registerTarget = context.registerTarget?.Text;
            if (!String.IsNullOrWhiteSpace(registerTarget))
            {
                if (registerTarget.Length != 2) throw new ParsingException("Register prefix '" + registerTarget + "' is not of length 2");
                IData.RegisterTarget target = IData.ParseRegisterTarget(registerTarget[0]);

                string registerData = VisitBytedata(context.registerData);
                byte[] rd = Convert.FromHexString(registerData);
                if (rd.Length != 1) throw new ParsingException("Cannot parse register data of > 1 bytes");
                byte r = rd[0];
                r = (byte)(r + IData.GetRegisterOffset(target));

                return new RByte(true, r);
            }

            // Process as value/identifer
            string? valText = context.val?.Text;
            if (!String.IsNullOrWhiteSpace(valText))
            {
                return new RByte(false, context.val!.Text);
            }

            throw new ParsingException("Failed to visit byte with no bytedata or value-mapping");
        }

        public override string VisitBytedata([NotNull] RedFoxAssemblyParser.BytedataContext context)
        {
            return context.GetText();
        }

        public ICommand ParseCommand([NotNull] RedFoxAssemblyParser.CommandContext context)
        {
            if (context == null) throw new ParsingException("RedFoxAssemblyParser.CommandContext cannot be null");
            if (context is RedFoxAssemblyParser.LabelCommandContext) return VisitLabelCommand((RedFoxAssemblyParser.LabelCommandContext)context);
            if (context is RedFoxAssemblyParser.InstructionCommandContext) return VisitInstructionCommand((RedFoxAssemblyParser.InstructionCommandContext)context);
            throw new ParsingException("Cannot parse RedFoxAssemblyParser.CommandContext of type " + context.GetType());
        }

        public override LabelCommand VisitLabelCommand([NotNull] RedFoxAssemblyParser.LabelCommandContext context) { return VisitLabel(context.label()); }
        public override LabelCommand VisitLabel([NotNull] RedFoxAssemblyParser.LabelContext context) { return new LabelCommand(context.id.Text); }

        public override InstructionCommand VisitInstructionCommand([NotNull] RedFoxAssemblyParser.InstructionCommandContext context) { return ParseInstruction(context.instruction()); }
        public InstructionCommand ParseInstruction(RedFoxAssemblyParser.InstructionContext context)
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

        public override InstructionCommand VisitHLTInstruction([NotNull] RedFoxAssemblyParser.HLTInstructionContext context) { return VisitHlt(context.hlt()); }
        public override InstructionCommand VisitHlt([NotNull] RedFoxAssemblyParser.HltContext context) { return new InstructionCommand(InstructionType.HLT, null, null); }

        public override InstructionCommand VisitNOPInstruction([NotNull] RedFoxAssemblyParser.NOPInstructionContext context) { return VisitNop(context.nop()); }
        public override InstructionCommand VisitNop([NotNull] RedFoxAssemblyParser.NopContext context) { return new InstructionCommand(InstructionType.NOP, null, null); }

        public override InstructionCommand VisitADDInstruction([NotNull] RedFoxAssemblyParser.ADDInstructionContext context) { return VisitAdd(context.add()); }
        public override InstructionCommand VisitAdd([NotNull] RedFoxAssemblyParser.AddContext context) { return new InstructionCommand(InstructionType.ADD, null, null); }

        public override InstructionCommand VisitSUBInstruction([NotNull] RedFoxAssemblyParser.SUBInstructionContext context) { return VisitSub(context.sub()); }
        public override InstructionCommand VisitSub([NotNull] RedFoxAssemblyParser.SubContext context) { return new InstructionCommand(InstructionType.SUB, null, null); }

        public override InstructionCommand VisitLSLInstruction([NotNull] RedFoxAssemblyParser.LSLInstructionContext context) { return VisitLsl(context.lsl()); }
        public override InstructionCommand VisitLsl([NotNull] RedFoxAssemblyParser.LslContext context) { return new InstructionCommand(InstructionType.LSL, null, null); }

        public override InstructionCommand VisitLSRInstruction([NotNull] RedFoxAssemblyParser.LSRInstructionContext context) { return VisitLsr(context.lsr()); }
        public override InstructionCommand VisitLsr([NotNull] RedFoxAssemblyParser.LsrContext context) { return new InstructionCommand(InstructionType.LSR, null, null); }

        public override InstructionCommand VisitNEGInstruction([NotNull] RedFoxAssemblyParser.NEGInstructionContext context) { return VisitNeg(context.neg()); }
        public override InstructionCommand VisitNeg([NotNull] RedFoxAssemblyParser.NegContext context) { return new InstructionCommand(InstructionType.NEG, null, null); }

        public override InstructionCommand VisitNOTInstruction([NotNull] RedFoxAssemblyParser.NOTInstructionContext context) { return VisitNot(context.not()); }
        public override InstructionCommand VisitNot([NotNull] RedFoxAssemblyParser.NotContext context) { return new InstructionCommand(InstructionType.NOT, null, null); }

        public override InstructionCommand VisitCMPInstruction([NotNull] RedFoxAssemblyParser.CMPInstructionContext context) { return VisitCmp(context.cmp()); }
        public override InstructionCommand VisitCmp([NotNull] RedFoxAssemblyParser.CmpContext context) { return new InstructionCommand(InstructionType.CMP, null, null); }

        public override InstructionCommand VisitJMPInstruction([NotNull] RedFoxAssemblyParser.JMPInstructionContext context) { return VisitJmp(context.jmp()); }
        public override InstructionCommand VisitJmp([NotNull] RedFoxAssemblyParser.JmpContext context) { return new InstructionCommand(InstructionType.JMP, VisitWord(context.arg1w), null); }

        public override InstructionCommand VisitBFGInstruction([NotNull] RedFoxAssemblyParser.BFGInstructionContext context) { return VisitBfg(context.bfg()); }
        public override InstructionCommand VisitBfg([NotNull] RedFoxAssemblyParser.BfgContext context) { return new InstructionCommand(InstructionType.BFG, VisitWord(context.arg1w), VisitByte(context.arg2b)); }

        //public override Instruction VisitBFGInstruction([NotNull] RedFoxAssemblyParser.BFGInstructionContext context) { return VisitBfg(context.bfg()); }
        //public override Instruction VisitBfg([NotNull] RedFoxAssemblyParser.BfgContext context) { return new Instruction(InstructionType.BFG, null, null); }

        //public override Instruction VisitBFGInstruction([NotNull] RedFoxAssemblyParser.BFGInstructionContext context) { return VisitBfg(context.bfg()); }
        //public override Instruction VisitBfg([NotNull] RedFoxAssemblyParser.BfgContext context) { return new Instruction(InstructionType.BFG, null, null); }

        //public override Instruction VisitBFGInstruction([NotNull] RedFoxAssemblyParser.BFGInstructionContext context) { return VisitBfg(context.bfg()); }
        //public override Instruction VisitBfg([NotNull] RedFoxAssemblyParser.BfgContext context) { return new Instruction(InstructionType.BFG, null, null); }

        public override InstructionCommand VisitBSRInstruction([NotNull] RedFoxAssemblyParser.BSRInstructionContext context) { return VisitBsr(context.bsr()); }
        public override InstructionCommand VisitBsr([NotNull] RedFoxAssemblyParser.BsrContext context) { return new InstructionCommand(InstructionType.BSR, VisitWord(context.arg1w), null); }

        public override InstructionCommand VisitRTNInstruction([NotNull] RedFoxAssemblyParser.RTNInstructionContext context) { return VisitRtn(context.rtn()); }
        public override InstructionCommand VisitRtn([NotNull] RedFoxAssemblyParser.RtnContext context) { return new InstructionCommand(InstructionType.RTN, null, null); }

        public override InstructionCommand VisitRRBInstruction([NotNull] RedFoxAssemblyParser.RRBInstructionContext context) { return VisitRrb(context.rrb()); }
        public override InstructionCommand VisitRrb([NotNull] RedFoxAssemblyParser.RrbContext context) { return new InstructionCommand(InstructionType.RRB, VisitByte(context.arg1b), VisitByte(context.arg2b)); }

        public override InstructionCommand VisitRRWInstruction([NotNull] RedFoxAssemblyParser.RRWInstructionContext context) { return VisitRrw(context.rrw()); }
        public override InstructionCommand VisitRrw([NotNull] RedFoxAssemblyParser.RrwContext context) { return new InstructionCommand(InstructionType.RRW, VisitByte(context.arg1b), null); }

        public override InstructionCommand VisitRMBInstruction([NotNull] RedFoxAssemblyParser.RMBInstructionContext context) { return VisitRmb(context.rmb()); }
        public override InstructionCommand VisitRmb([NotNull] RedFoxAssemblyParser.RmbContext context) { return new InstructionCommand(InstructionType.RMB, VisitWord(context.arg1w), null); }

        public override InstructionCommand VisitRMWInstruction([NotNull] RedFoxAssemblyParser.RMWInstructionContext context) { return VisitRmw(context.rmw()); }
        public override InstructionCommand VisitRmw([NotNull] RedFoxAssemblyParser.RmwContext context) { return new InstructionCommand(InstructionType.RMW, VisitWord(context.arg1w), null); }

        public override InstructionCommand VisitWRBInstruction([NotNull] RedFoxAssemblyParser.WRBInstructionContext context) { return VisitWrb(context.wrb()); }
        public override InstructionCommand VisitWrb([NotNull] RedFoxAssemblyParser.WrbContext context) { return new InstructionCommand(InstructionType.WRB, VisitByte(context.arg1b), VisitByte(context.arg2b)); }

        public override InstructionCommand VisitWRWInstruction([NotNull] RedFoxAssemblyParser.WRWInstructionContext context) { return VisitWrw(context.wrw()); }
        public override InstructionCommand VisitWrw([NotNull] RedFoxAssemblyParser.WrwContext context) { return new InstructionCommand(InstructionType.WRW, VisitByte(context.arg1b), null); }

        public override InstructionCommand VisitWMBInstruction([NotNull] RedFoxAssemblyParser.WMBInstructionContext context) { return VisitWmb(context.wmb()); }
        public override InstructionCommand VisitWmb([NotNull] RedFoxAssemblyParser.WmbContext context) { return new InstructionCommand(InstructionType.WMB, VisitWord(context.arg1w), null); }

        public override InstructionCommand VisitWMWInstruction([NotNull] RedFoxAssemblyParser.WMWInstructionContext context) { return VisitWmw(context.wmw()); }
        public override InstructionCommand VisitWmw([NotNull] RedFoxAssemblyParser.WmwContext context) { return new InstructionCommand(InstructionType.WMW, VisitWord(context.arg1w), null); }

        public override InstructionCommand VisitRVBInstruction([NotNull] RedFoxAssemblyParser.RVBInstructionContext context) { return VisitRvb(context.rvb()); }
        public override InstructionCommand VisitRvb([NotNull] RedFoxAssemblyParser.RvbContext context) { return new InstructionCommand(InstructionType.RVB, VisitByte(context.arg1b), null); }

        public override InstructionCommand VisitRVWInstruction([NotNull] RedFoxAssemblyParser.RVWInstructionContext context) { return VisitRvw(context.rvw()); }
        public override InstructionCommand VisitRvw([NotNull] RedFoxAssemblyParser.RvwContext context) { return new InstructionCommand(InstructionType.RVW, VisitWord(context.arg1w), null); }

        public override InstructionCommand VisitSINInstruction([NotNull] RedFoxAssemblyParser.SINInstructionContext context) { return VisitSin(context.sin()); }
        public override InstructionCommand VisitSin([NotNull] RedFoxAssemblyParser.SinContext context) { return new InstructionCommand(InstructionType.SIN, VisitWord(context.arg1w), VisitByte(context.arg2b)); }

        public override InstructionCommand VisitINTInstruction([NotNull] RedFoxAssemblyParser.INTInstructionContext context) { return VisitInt(context.@int()); }
        public override InstructionCommand VisitInt([NotNull] RedFoxAssemblyParser.IntContext context) { return new InstructionCommand(InstructionType.INT, VisitByte(context.arg1b), null); }

        public override InstructionCommand VisitSFGInstruction([NotNull] RedFoxAssemblyParser.SFGInstructionContext context) { return VisitSfg(context.sfg()); }
        public override InstructionCommand VisitSfg([NotNull] RedFoxAssemblyParser.SfgContext context) { return new InstructionCommand(InstructionType.SFG, VisitByte(context.arg1b), VisitByte(context.arg2b)); }

        public override InstructionCommand VisitANDInstruction([NotNull] RedFoxAssemblyParser.ANDInstructionContext context) { return VisitAnd(context.and()); }
        public override InstructionCommand VisitAnd([NotNull] RedFoxAssemblyParser.AndContext context) { return new InstructionCommand(InstructionType.AND, null, null); }

        public override InstructionCommand VisitLORInstruction([NotNull] RedFoxAssemblyParser.LORInstructionContext context) { return VisitLor(context.lor()); }
        public override InstructionCommand VisitLor([NotNull] RedFoxAssemblyParser.LorContext context) { return new InstructionCommand(InstructionType.LOR, null, null); }

        public override InstructionCommand VisitXORInstruction([NotNull] RedFoxAssemblyParser.XORInstructionContext context) { return VisitXor(context.xor()); }
        public override InstructionCommand VisitXor([NotNull] RedFoxAssemblyParser.XorContext context) { return new InstructionCommand(InstructionType.XOR, null, null); }
    }
}
