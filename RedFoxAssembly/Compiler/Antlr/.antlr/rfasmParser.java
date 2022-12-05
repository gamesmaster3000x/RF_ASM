// Generated from c:\Users\GenElectrovise\OneDrive\Desktop\Programming\RF_ASM\RedFoxAssembly\Compiler\Antlr\rfasm.g4 by ANTLR 4.9.2
import org.antlr.v4.runtime.atn.*;
import org.antlr.v4.runtime.dfa.DFA;
import org.antlr.v4.runtime.*;
import org.antlr.v4.runtime.misc.*;
import org.antlr.v4.runtime.tree.*;
import java.util.List;
import java.util.Iterator;
import java.util.ArrayList;

@SuppressWarnings({"all", "warnings", "unchecked", "unused", "cast"})
public class rfasmParser extends Parser {
	static { RuntimeMetaData.checkVersion("4.9.2", RuntimeMetaData.VERSION); }

	protected static final DFA[] _decisionToDFA;
	protected static final PredictionContextCache _sharedContextCache =
		new PredictionContextCache();
	public static final int
		Comment=1, WhiteSpace=2, WIDTH=3, VALUE=4, COLON=5, HLT=6, LDR=7, STM=8, 
		ADD=9, SUB=10, LSL=11, LSR=12, CMP=13, B=14, BEQ=15, BLT=16, BGT=17, BOF=18, 
		BSR=19, RTN=20, STB=21, Char=22, String=23;
	public static final int
		RULE_program = 0, RULE_statement = 1, RULE_label = 2, RULE_directive = 3, 
		RULE_instruction = 4, RULE_argument = 5;
	private static String[] makeRuleNames() {
		return new String[] {
			"program", "statement", "label", "directive", "instruction", "argument"
		};
	}
	public static final String[] ruleNames = makeRuleNames();

	private static String[] makeLiteralNames() {
		return new String[] {
			null, null, null, "'.width'", "'.val'", "':'", "'HLT'", "'LDR'", "'STM'", 
			"'ADD'", "'SUB'", "'LSL'", "'LSR'", "'CMP'", "'B'", "'BEQ'", "'BLT'", 
			"'BGT'", "'BOF'", "'BSR'", "'RTN'", "'STB'"
		};
	}
	private static final String[] _LITERAL_NAMES = makeLiteralNames();
	private static String[] makeSymbolicNames() {
		return new String[] {
			null, "Comment", "WhiteSpace", "WIDTH", "VALUE", "COLON", "HLT", "LDR", 
			"STM", "ADD", "SUB", "LSL", "LSR", "CMP", "B", "BEQ", "BLT", "BGT", "BOF", 
			"BSR", "RTN", "STB", "Char", "String"
		};
	}
	private static final String[] _SYMBOLIC_NAMES = makeSymbolicNames();
	public static final Vocabulary VOCABULARY = new VocabularyImpl(_LITERAL_NAMES, _SYMBOLIC_NAMES);

	/**
	 * @deprecated Use {@link #VOCABULARY} instead.
	 */
	@Deprecated
	public static final String[] tokenNames;
	static {
		tokenNames = new String[_SYMBOLIC_NAMES.length];
		for (int i = 0; i < tokenNames.length; i++) {
			tokenNames[i] = VOCABULARY.getLiteralName(i);
			if (tokenNames[i] == null) {
				tokenNames[i] = VOCABULARY.getSymbolicName(i);
			}

			if (tokenNames[i] == null) {
				tokenNames[i] = "<INVALID>";
			}
		}
	}

	@Override
	@Deprecated
	public String[] getTokenNames() {
		return tokenNames;
	}

	@Override

	public Vocabulary getVocabulary() {
		return VOCABULARY;
	}

	@Override
	public String getGrammarFileName() { return "rfasm.g4"; }

	@Override
	public String[] getRuleNames() { return ruleNames; }

	@Override
	public String getSerializedATN() { return _serializedATN; }

	@Override
	public ATN getATN() { return _ATN; }

	public rfasmParser(TokenStream input) {
		super(input);
		_interp = new ParserATNSimulator(this,_ATN,_decisionToDFA,_sharedContextCache);
	}

	public static class ProgramContext extends ParserRuleContext {
		public TerminalNode EOF() { return getToken(rfasmParser.EOF, 0); }
		public List<StatementContext> statement() {
			return getRuleContexts(StatementContext.class);
		}
		public StatementContext statement(int i) {
			return getRuleContext(StatementContext.class,i);
		}
		public ProgramContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_program; }
	}

	public final ProgramContext program() throws RecognitionException {
		ProgramContext _localctx = new ProgramContext(_ctx, getState());
		enterRule(_localctx, 0, RULE_program);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(13); 
			_errHandler.sync(this);
			_la = _input.LA(1);
			do {
				{
				{
				setState(12);
				statement();
				}
				}
				setState(15); 
				_errHandler.sync(this);
				_la = _input.LA(1);
			} while ( (((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << WIDTH) | (1L << VALUE) | (1L << COLON) | (1L << HLT) | (1L << LDR) | (1L << STM) | (1L << ADD) | (1L << SUB) | (1L << LSL) | (1L << LSR) | (1L << CMP) | (1L << B) | (1L << BEQ) | (1L << BLT) | (1L << BGT) | (1L << BOF) | (1L << BSR) | (1L << RTN) | (1L << STB))) != 0) );
			setState(17);
			match(EOF);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class StatementContext extends ParserRuleContext {
		public InstructionContext instruction() {
			return getRuleContext(InstructionContext.class,0);
		}
		public DirectiveContext directive() {
			return getRuleContext(DirectiveContext.class,0);
		}
		public LabelContext label() {
			return getRuleContext(LabelContext.class,0);
		}
		public StatementContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_statement; }
	}

	public final StatementContext statement() throws RecognitionException {
		StatementContext _localctx = new StatementContext(_ctx, getState());
		enterRule(_localctx, 2, RULE_statement);
		try {
			setState(22);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case HLT:
			case LDR:
			case STM:
			case ADD:
			case SUB:
			case LSL:
			case LSR:
			case CMP:
			case B:
			case BEQ:
			case BLT:
			case BGT:
			case BOF:
			case BSR:
			case RTN:
			case STB:
				enterOuterAlt(_localctx, 1);
				{
				setState(19);
				instruction();
				}
				break;
			case WIDTH:
			case VALUE:
				enterOuterAlt(_localctx, 2);
				{
				setState(20);
				directive();
				}
				break;
			case COLON:
				enterOuterAlt(_localctx, 3);
				{
				setState(21);
				label();
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class LabelContext extends ParserRuleContext {
		public TerminalNode COLON() { return getToken(rfasmParser.COLON, 0); }
		public ArgumentContext argument() {
			return getRuleContext(ArgumentContext.class,0);
		}
		public LabelContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_label; }
	}

	public final LabelContext label() throws RecognitionException {
		LabelContext _localctx = new LabelContext(_ctx, getState());
		enterRule(_localctx, 4, RULE_label);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(24);
			match(COLON);
			setState(25);
			argument();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class DirectiveContext extends ParserRuleContext {
		public TerminalNode WIDTH() { return getToken(rfasmParser.WIDTH, 0); }
		public List<ArgumentContext> argument() {
			return getRuleContexts(ArgumentContext.class);
		}
		public ArgumentContext argument(int i) {
			return getRuleContext(ArgumentContext.class,i);
		}
		public TerminalNode VALUE() { return getToken(rfasmParser.VALUE, 0); }
		public DirectiveContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_directive; }
	}

	public final DirectiveContext directive() throws RecognitionException {
		DirectiveContext _localctx = new DirectiveContext(_ctx, getState());
		enterRule(_localctx, 6, RULE_directive);
		try {
			setState(33);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case WIDTH:
				enterOuterAlt(_localctx, 1);
				{
				setState(27);
				match(WIDTH);
				setState(28);
				argument();
				}
				break;
			case VALUE:
				enterOuterAlt(_localctx, 2);
				{
				setState(29);
				match(VALUE);
				setState(30);
				argument();
				setState(31);
				argument();
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class InstructionContext extends ParserRuleContext {
		public TerminalNode HLT() { return getToken(rfasmParser.HLT, 0); }
		public TerminalNode LDR() { return getToken(rfasmParser.LDR, 0); }
		public List<ArgumentContext> argument() {
			return getRuleContexts(ArgumentContext.class);
		}
		public ArgumentContext argument(int i) {
			return getRuleContext(ArgumentContext.class,i);
		}
		public TerminalNode STM() { return getToken(rfasmParser.STM, 0); }
		public TerminalNode ADD() { return getToken(rfasmParser.ADD, 0); }
		public TerminalNode SUB() { return getToken(rfasmParser.SUB, 0); }
		public TerminalNode LSL() { return getToken(rfasmParser.LSL, 0); }
		public TerminalNode LSR() { return getToken(rfasmParser.LSR, 0); }
		public TerminalNode CMP() { return getToken(rfasmParser.CMP, 0); }
		public TerminalNode B() { return getToken(rfasmParser.B, 0); }
		public TerminalNode BEQ() { return getToken(rfasmParser.BEQ, 0); }
		public TerminalNode BLT() { return getToken(rfasmParser.BLT, 0); }
		public TerminalNode BGT() { return getToken(rfasmParser.BGT, 0); }
		public TerminalNode BOF() { return getToken(rfasmParser.BOF, 0); }
		public TerminalNode BSR() { return getToken(rfasmParser.BSR, 0); }
		public TerminalNode RTN() { return getToken(rfasmParser.RTN, 0); }
		public TerminalNode STB() { return getToken(rfasmParser.STB, 0); }
		public InstructionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_instruction; }
	}

	public final InstructionContext instruction() throws RecognitionException {
		InstructionContext _localctx = new InstructionContext(_ctx, getState());
		enterRule(_localctx, 8, RULE_instruction);
		try {
			setState(69);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case HLT:
				enterOuterAlt(_localctx, 1);
				{
				setState(35);
				match(HLT);
				}
				break;
			case LDR:
				enterOuterAlt(_localctx, 2);
				{
				setState(36);
				match(LDR);
				setState(37);
				argument();
				setState(38);
				argument();
				}
				break;
			case STM:
				enterOuterAlt(_localctx, 3);
				{
				setState(40);
				match(STM);
				setState(41);
				argument();
				setState(42);
				argument();
				}
				break;
			case ADD:
				enterOuterAlt(_localctx, 4);
				{
				setState(44);
				match(ADD);
				}
				break;
			case SUB:
				enterOuterAlt(_localctx, 5);
				{
				setState(45);
				match(SUB);
				}
				break;
			case LSL:
				enterOuterAlt(_localctx, 6);
				{
				setState(46);
				match(LSL);
				}
				break;
			case LSR:
				enterOuterAlt(_localctx, 7);
				{
				setState(47);
				match(LSR);
				}
				break;
			case CMP:
				enterOuterAlt(_localctx, 8);
				{
				setState(48);
				match(CMP);
				setState(49);
				argument();
				setState(50);
				argument();
				}
				break;
			case B:
				enterOuterAlt(_localctx, 9);
				{
				setState(52);
				match(B);
				setState(53);
				argument();
				}
				break;
			case BEQ:
				enterOuterAlt(_localctx, 10);
				{
				setState(54);
				match(BEQ);
				setState(55);
				argument();
				}
				break;
			case BLT:
				enterOuterAlt(_localctx, 11);
				{
				setState(56);
				match(BLT);
				setState(57);
				argument();
				}
				break;
			case BGT:
				enterOuterAlt(_localctx, 12);
				{
				setState(58);
				match(BGT);
				setState(59);
				argument();
				}
				break;
			case BOF:
				enterOuterAlt(_localctx, 13);
				{
				setState(60);
				match(BOF);
				setState(61);
				argument();
				}
				break;
			case BSR:
				enterOuterAlt(_localctx, 14);
				{
				setState(62);
				match(BSR);
				setState(63);
				argument();
				}
				break;
			case RTN:
				enterOuterAlt(_localctx, 15);
				{
				setState(64);
				match(RTN);
				}
				break;
			case STB:
				enterOuterAlt(_localctx, 16);
				{
				setState(65);
				match(STB);
				setState(66);
				argument();
				setState(67);
				argument();
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ArgumentContext extends ParserRuleContext {
		public TerminalNode String() { return getToken(rfasmParser.String, 0); }
		public ArgumentContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_argument; }
	}

	public final ArgumentContext argument() throws RecognitionException {
		ArgumentContext _localctx = new ArgumentContext(_ctx, getState());
		enterRule(_localctx, 10, RULE_argument);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(71);
			match(String);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static final String _serializedATN =
		"\3\u608b\ua72a\u8133\ub9ed\u417c\u3be7\u7786\u5964\3\31L\4\2\t\2\4\3\t"+
		"\3\4\4\t\4\4\5\t\5\4\6\t\6\4\7\t\7\3\2\6\2\20\n\2\r\2\16\2\21\3\2\3\2"+
		"\3\3\3\3\3\3\5\3\31\n\3\3\4\3\4\3\4\3\5\3\5\3\5\3\5\3\5\3\5\5\5$\n\5\3"+
		"\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6"+
		"\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\6\5\6H"+
		"\n\6\3\7\3\7\3\7\2\2\b\2\4\6\b\n\f\2\2\2X\2\17\3\2\2\2\4\30\3\2\2\2\6"+
		"\32\3\2\2\2\b#\3\2\2\2\nG\3\2\2\2\fI\3\2\2\2\16\20\5\4\3\2\17\16\3\2\2"+
		"\2\20\21\3\2\2\2\21\17\3\2\2\2\21\22\3\2\2\2\22\23\3\2\2\2\23\24\7\2\2"+
		"\3\24\3\3\2\2\2\25\31\5\n\6\2\26\31\5\b\5\2\27\31\5\6\4\2\30\25\3\2\2"+
		"\2\30\26\3\2\2\2\30\27\3\2\2\2\31\5\3\2\2\2\32\33\7\7\2\2\33\34\5\f\7"+
		"\2\34\7\3\2\2\2\35\36\7\5\2\2\36$\5\f\7\2\37 \7\6\2\2 !\5\f\7\2!\"\5\f"+
		"\7\2\"$\3\2\2\2#\35\3\2\2\2#\37\3\2\2\2$\t\3\2\2\2%H\7\b\2\2&\'\7\t\2"+
		"\2\'(\5\f\7\2()\5\f\7\2)H\3\2\2\2*+\7\n\2\2+,\5\f\7\2,-\5\f\7\2-H\3\2"+
		"\2\2.H\7\13\2\2/H\7\f\2\2\60H\7\r\2\2\61H\7\16\2\2\62\63\7\17\2\2\63\64"+
		"\5\f\7\2\64\65\5\f\7\2\65H\3\2\2\2\66\67\7\20\2\2\67H\5\f\7\289\7\21\2"+
		"\29H\5\f\7\2:;\7\22\2\2;H\5\f\7\2<=\7\23\2\2=H\5\f\7\2>?\7\24\2\2?H\5"+
		"\f\7\2@A\7\25\2\2AH\5\f\7\2BH\7\26\2\2CD\7\27\2\2DE\5\f\7\2EF\5\f\7\2"+
		"FH\3\2\2\2G%\3\2\2\2G&\3\2\2\2G*\3\2\2\2G.\3\2\2\2G/\3\2\2\2G\60\3\2\2"+
		"\2G\61\3\2\2\2G\62\3\2\2\2G\66\3\2\2\2G8\3\2\2\2G:\3\2\2\2G<\3\2\2\2G"+
		">\3\2\2\2G@\3\2\2\2GB\3\2\2\2GC\3\2\2\2H\13\3\2\2\2IJ\7\31\2\2J\r\3\2"+
		"\2\2\6\21\30#G";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}