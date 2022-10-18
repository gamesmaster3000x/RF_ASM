// Generated from c:\Users\GenElectrovise\OneDrive\Desktop\Programming\RF_ASM\Crimson\CSharp\Antlr\crimson.g4 by ANTLR 4.9.2
import org.antlr.v4.runtime.Lexer;
import org.antlr.v4.runtime.CharStream;
import org.antlr.v4.runtime.Token;
import org.antlr.v4.runtime.TokenStream;
import org.antlr.v4.runtime.*;
import org.antlr.v4.runtime.atn.*;
import org.antlr.v4.runtime.dfa.DFA;
import org.antlr.v4.runtime.misc.*;

@SuppressWarnings({"all", "warnings", "unchecked", "unused", "cast"})
public class crimsonLexer extends Lexer {
	static { RuntimeMetaData.checkVersion("4.9.2", RuntimeMetaData.VERSION); }

	protected static final DFA[] _decisionToDFA;
	protected static final PredictionContextCache _sharedContextCache =
		new PredictionContextCache();
	public static final int
		Package=1, Function=2, Global=3, Return=4, Integer=5, Boolean=6, Equals=7, 
		OpenBracket=8, CloseBracket=9, OpenSquare=10, CloseSquare=11, OpenBrace=12, 
		CloseBrace=13, Comma=14, Dot=15, SemiColon=16, Identifier=17, Value=18;
	public static String[] channelNames = {
		"DEFAULT_TOKEN_CHANNEL", "HIDDEN"
	};

	public static String[] modeNames = {
		"DEFAULT_MODE"
	};

	private static String[] makeRuleNames() {
		return new String[] {
			"Package", "Function", "Global", "Return", "Integer", "Boolean", "Equals", 
			"OpenBracket", "CloseBracket", "OpenSquare", "CloseSquare", "OpenBrace", 
			"CloseBrace", "Comma", "Dot", "SemiColon", "Identifier", "Value", "Alphabetic", 
			"Digit", "Punctuation"
		};
	}
	public static final String[] ruleNames = makeRuleNames();

	private static String[] makeLiteralNames() {
		return new String[] {
			null, "'package'", "'function'", "'global'", "'return'", "'int'", "'bool'", 
			"'='", "'('", "')'", "'['", "']'", "'{'", "'}'", "','", "'.'", "';'"
		};
	}
	private static final String[] _LITERAL_NAMES = makeLiteralNames();
	private static String[] makeSymbolicNames() {
		return new String[] {
			null, "Package", "Function", "Global", "Return", "Integer", "Boolean", 
			"Equals", "OpenBracket", "CloseBracket", "OpenSquare", "CloseSquare", 
			"OpenBrace", "CloseBrace", "Comma", "Dot", "SemiColon", "Identifier", 
			"Value"
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


	public crimsonLexer(CharStream input) {
		super(input);
		_interp = new LexerATNSimulator(this,_ATN,_decisionToDFA,_sharedContextCache);
	}

	@Override
	public String getGrammarFileName() { return "crimson.g4"; }

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
		"\3\u608b\ua72a\u8133\ub9ed\u417c\u3be7\u7786\u5964\2\24\u0081\b\1\4\2"+
		"\t\2\4\3\t\3\4\4\t\4\4\5\t\5\4\6\t\6\4\7\t\7\4\b\t\b\4\t\t\t\4\n\t\n\4"+
		"\13\t\13\4\f\t\f\4\r\t\r\4\16\t\16\4\17\t\17\4\20\t\20\4\21\t\21\4\22"+
		"\t\22\4\23\t\23\4\24\t\24\4\25\t\25\4\26\t\26\3\2\3\2\3\2\3\2\3\2\3\2"+
		"\3\2\3\2\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\4\3\4\3\4\3\4\3\4\3\4\3"+
		"\4\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\6\3\6\3\6\3\6\3\7\3\7\3\7\3\7\3\7\3\b"+
		"\3\b\3\t\3\t\3\n\3\n\3\13\3\13\3\f\3\f\3\r\3\r\3\16\3\16\3\17\3\17\3\20"+
		"\3\20\3\21\3\21\3\22\3\22\5\22l\n\22\3\22\3\22\3\22\7\22q\n\22\f\22\16"+
		"\22t\13\22\3\23\3\23\6\23x\n\23\r\23\16\23y\3\24\3\24\3\25\3\25\3\26\3"+
		"\26\2\2\27\3\3\5\4\7\5\t\6\13\7\r\b\17\t\21\n\23\13\25\f\27\r\31\16\33"+
		"\17\35\20\37\21!\22#\23%\24\'\2)\2+\2\3\2\5\4\2C\\c|\3\2\62;\4\2\60\60"+
		"aa\2\u0083\2\3\3\2\2\2\2\5\3\2\2\2\2\7\3\2\2\2\2\t\3\2\2\2\2\13\3\2\2"+
		"\2\2\r\3\2\2\2\2\17\3\2\2\2\2\21\3\2\2\2\2\23\3\2\2\2\2\25\3\2\2\2\2\27"+
		"\3\2\2\2\2\31\3\2\2\2\2\33\3\2\2\2\2\35\3\2\2\2\2\37\3\2\2\2\2!\3\2\2"+
		"\2\2#\3\2\2\2\2%\3\2\2\2\3-\3\2\2\2\5\65\3\2\2\2\7>\3\2\2\2\tE\3\2\2\2"+
		"\13L\3\2\2\2\rP\3\2\2\2\17U\3\2\2\2\21W\3\2\2\2\23Y\3\2\2\2\25[\3\2\2"+
		"\2\27]\3\2\2\2\31_\3\2\2\2\33a\3\2\2\2\35c\3\2\2\2\37e\3\2\2\2!g\3\2\2"+
		"\2#k\3\2\2\2%w\3\2\2\2\'{\3\2\2\2)}\3\2\2\2+\177\3\2\2\2-.\7r\2\2./\7"+
		"c\2\2/\60\7e\2\2\60\61\7m\2\2\61\62\7c\2\2\62\63\7i\2\2\63\64\7g\2\2\64"+
		"\4\3\2\2\2\65\66\7h\2\2\66\67\7w\2\2\678\7p\2\289\7e\2\29:\7v\2\2:;\7"+
		"k\2\2;<\7q\2\2<=\7p\2\2=\6\3\2\2\2>?\7i\2\2?@\7n\2\2@A\7q\2\2AB\7d\2\2"+
		"BC\7c\2\2CD\7n\2\2D\b\3\2\2\2EF\7t\2\2FG\7g\2\2GH\7v\2\2HI\7w\2\2IJ\7"+
		"t\2\2JK\7p\2\2K\n\3\2\2\2LM\7k\2\2MN\7p\2\2NO\7v\2\2O\f\3\2\2\2PQ\7d\2"+
		"\2QR\7q\2\2RS\7q\2\2ST\7n\2\2T\16\3\2\2\2UV\7?\2\2V\20\3\2\2\2WX\7*\2"+
		"\2X\22\3\2\2\2YZ\7+\2\2Z\24\3\2\2\2[\\\7]\2\2\\\26\3\2\2\2]^\7_\2\2^\30"+
		"\3\2\2\2_`\7}\2\2`\32\3\2\2\2ab\7\177\2\2b\34\3\2\2\2cd\7.\2\2d\36\3\2"+
		"\2\2ef\7\60\2\2f \3\2\2\2gh\7=\2\2h\"\3\2\2\2il\5\'\24\2jl\5+\26\2ki\3"+
		"\2\2\2kj\3\2\2\2lr\3\2\2\2mq\5\'\24\2nq\5)\25\2oq\5+\26\2pm\3\2\2\2pn"+
		"\3\2\2\2po\3\2\2\2qt\3\2\2\2rp\3\2\2\2rs\3\2\2\2s$\3\2\2\2tr\3\2\2\2u"+
		"x\5\'\24\2vx\5)\25\2wu\3\2\2\2wv\3\2\2\2xy\3\2\2\2yw\3\2\2\2yz\3\2\2\2"+
		"z&\3\2\2\2{|\t\2\2\2|(\3\2\2\2}~\t\3\2\2~*\3\2\2\2\177\u0080\t\4\2\2\u0080"+
		",\3\2\2\2\b\2kprwy\2";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}