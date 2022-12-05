// Generated from c:\Users\GenElectrovise\OneDrive\Desktop\Programming\RF_ASM\RedFoxAssembly\Compiler\Antlr\rfasm.g4 by ANTLR 4.9.2
import org.antlr.v4.runtime.Lexer;
import org.antlr.v4.runtime.CharStream;
import org.antlr.v4.runtime.Token;
import org.antlr.v4.runtime.TokenStream;
import org.antlr.v4.runtime.*;
import org.antlr.v4.runtime.atn.*;
import org.antlr.v4.runtime.dfa.DFA;
import org.antlr.v4.runtime.misc.*;

@SuppressWarnings({"all", "warnings", "unchecked", "unused", "cast"})
public class rfasmLexer extends Lexer {
	static { RuntimeMetaData.checkVersion("4.9.2", RuntimeMetaData.VERSION); }

	protected static final DFA[] _decisionToDFA;
	protected static final PredictionContextCache _sharedContextCache =
		new PredictionContextCache();
	public static final int
		Comment=1, WhiteSpace=2, WIDTH=3, VALUE=4, COLON=5, HLT=6, LDR=7, STM=8, 
		ADD=9, SUB=10, LSL=11, LSR=12, CMP=13, B=14, BEQ=15, BLT=16, BGT=17, BOF=18, 
		BSR=19, RTN=20, STB=21, Char=22, String=23;
	public static String[] channelNames = {
		"DEFAULT_TOKEN_CHANNEL", "HIDDEN"
	};

	public static String[] modeNames = {
		"DEFAULT_MODE"
	};

	private static String[] makeRuleNames() {
		return new String[] {
			"Comment", "WhiteSpace", "WIDTH", "VALUE", "COLON", "HLT", "LDR", "STM", 
			"ADD", "SUB", "LSL", "LSR", "CMP", "B", "BEQ", "BLT", "BGT", "BOF", "BSR", 
			"RTN", "STB", "Char", "String"
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


	public rfasmLexer(CharStream input) {
		super(input);
		_interp = new LexerATNSimulator(this,_ATN,_decisionToDFA,_sharedContextCache);
	}

	@Override
	public String getGrammarFileName() { return "rfasm.g4"; }

	@Override
	public String[] getRuleNames() { return ruleNames; }

	@Override
	public String getSerializedATN() { return _serializedATN; }

	@Override
	public String[] getChannelNames() { return channelNames; }

	@Override
	public String[] getModeNames() { return modeNames; }

	@Override
	public ATN getATN() { return _ATN; }

	public static final String _serializedATN =
		"\3\u608b\ua72a\u8133\ub9ed\u417c\u3be7\u7786\u5964\2\31\u0093\b\1\4\2"+
		"\t\2\4\3\t\3\4\4\t\4\4\5\t\5\4\6\t\6\4\7\t\7\4\b\t\b\4\t\t\t\4\n\t\n\4"+
		"\13\t\13\4\f\t\f\4\r\t\r\4\16\t\16\4\17\t\17\4\20\t\20\4\21\t\21\4\22"+
		"\t\22\4\23\t\23\4\24\t\24\4\25\t\25\4\26\t\26\4\27\t\27\4\30\t\30\3\2"+
		"\3\2\3\2\3\2\7\2\66\n\2\f\2\16\29\13\2\3\2\3\2\3\3\3\3\3\3\3\3\3\4\3\4"+
		"\3\4\3\4\3\4\3\4\3\4\3\5\3\5\3\5\3\5\3\5\3\6\3\6\3\7\3\7\3\7\3\7\3\b\3"+
		"\b\3\b\3\b\3\t\3\t\3\t\3\t\3\n\3\n\3\n\3\n\3\13\3\13\3\13\3\13\3\f\3\f"+
		"\3\f\3\f\3\r\3\r\3\r\3\r\3\16\3\16\3\16\3\16\3\17\3\17\3\20\3\20\3\20"+
		"\3\20\3\21\3\21\3\21\3\21\3\22\3\22\3\22\3\22\3\23\3\23\3\23\3\23\3\24"+
		"\3\24\3\24\3\24\3\25\3\25\3\25\3\25\3\26\3\26\3\26\3\26\3\27\3\27\3\30"+
		"\6\30\u0090\n\30\r\30\16\30\u0091\2\2\31\3\3\5\4\7\5\t\6\13\7\r\b\17\t"+
		"\21\n\23\13\25\f\27\r\31\16\33\17\35\20\37\21!\22#\23%\24\'\25)\26+\27"+
		"-\30/\31\3\2\5\4\2\f\f\17\17\4\2\13\13\"\"\b\2%%,,\62;C\\aac|\2\u0094"+
		"\2\3\3\2\2\2\2\5\3\2\2\2\2\7\3\2\2\2\2\t\3\2\2\2\2\13\3\2\2\2\2\r\3\2"+
		"\2\2\2\17\3\2\2\2\2\21\3\2\2\2\2\23\3\2\2\2\2\25\3\2\2\2\2\27\3\2\2\2"+
		"\2\31\3\2\2\2\2\33\3\2\2\2\2\35\3\2\2\2\2\37\3\2\2\2\2!\3\2\2\2\2#\3\2"+
		"\2\2\2%\3\2\2\2\2\'\3\2\2\2\2)\3\2\2\2\2+\3\2\2\2\2-\3\2\2\2\2/\3\2\2"+
		"\2\3\61\3\2\2\2\5<\3\2\2\2\7@\3\2\2\2\tG\3\2\2\2\13L\3\2\2\2\rN\3\2\2"+
		"\2\17R\3\2\2\2\21V\3\2\2\2\23Z\3\2\2\2\25^\3\2\2\2\27b\3\2\2\2\31f\3\2"+
		"\2\2\33j\3\2\2\2\35n\3\2\2\2\37p\3\2\2\2!t\3\2\2\2#x\3\2\2\2%|\3\2\2\2"+
		"\'\u0080\3\2\2\2)\u0084\3\2\2\2+\u0088\3\2\2\2-\u008c\3\2\2\2/\u008f\3"+
		"\2\2\2\61\62\7\61\2\2\62\63\7\61\2\2\63\67\3\2\2\2\64\66\n\2\2\2\65\64"+
		"\3\2\2\2\669\3\2\2\2\67\65\3\2\2\2\678\3\2\2\28:\3\2\2\29\67\3\2\2\2:"+
		";\b\2\2\2;\4\3\2\2\2<=\t\3\2\2=>\3\2\2\2>?\b\3\2\2?\6\3\2\2\2@A\7\60\2"+
		"\2AB\7y\2\2BC\7k\2\2CD\7f\2\2DE\7v\2\2EF\7j\2\2F\b\3\2\2\2GH\7\60\2\2"+
		"HI\7x\2\2IJ\7c\2\2JK\7n\2\2K\n\3\2\2\2LM\7<\2\2M\f\3\2\2\2NO\7J\2\2OP"+
		"\7N\2\2PQ\7V\2\2Q\16\3\2\2\2RS\7N\2\2ST\7F\2\2TU\7T\2\2U\20\3\2\2\2VW"+
		"\7U\2\2WX\7V\2\2XY\7O\2\2Y\22\3\2\2\2Z[\7C\2\2[\\\7F\2\2\\]\7F\2\2]\24"+
		"\3\2\2\2^_\7U\2\2_`\7W\2\2`a\7D\2\2a\26\3\2\2\2bc\7N\2\2cd\7U\2\2de\7"+
		"N\2\2e\30\3\2\2\2fg\7N\2\2gh\7U\2\2hi\7T\2\2i\32\3\2\2\2jk\7E\2\2kl\7"+
		"O\2\2lm\7R\2\2m\34\3\2\2\2no\7D\2\2o\36\3\2\2\2pq\7D\2\2qr\7G\2\2rs\7"+
		"S\2\2s \3\2\2\2tu\7D\2\2uv\7N\2\2vw\7V\2\2w\"\3\2\2\2xy\7D\2\2yz\7I\2"+
		"\2z{\7V\2\2{$\3\2\2\2|}\7D\2\2}~\7Q\2\2~\177\7H\2\2\177&\3\2\2\2\u0080"+
		"\u0081\7D\2\2\u0081\u0082\7U\2\2\u0082\u0083\7T\2\2\u0083(\3\2\2\2\u0084"+
		"\u0085\7T\2\2\u0085\u0086\7V\2\2\u0086\u0087\7P\2\2\u0087*\3\2\2\2\u0088"+
		"\u0089\7U\2\2\u0089\u008a\7V\2\2\u008a\u008b\7D\2\2\u008b,\3\2\2\2\u008c"+
		"\u008d\t\4\2\2\u008d.\3\2\2\2\u008e\u0090\5-\27\2\u008f\u008e\3\2\2\2"+
		"\u0090\u0091\3\2\2\2\u0091\u008f\3\2\2\2\u0091\u0092\3\2\2\2\u0092\60"+
		"\3\2\2\2\5\2\67\u0091\3\b\2\2";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}