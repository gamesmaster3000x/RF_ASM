// Generated from c:\Users\GenElectrovise\OneDrive\Desktop\Programming\RF_ASM\Crimson\CSharp\Antlr\crimson.g4 by ANTLR 4.9.2
import org.antlr.v4.runtime.atn.*;
import org.antlr.v4.runtime.dfa.DFA;
import org.antlr.v4.runtime.*;
import org.antlr.v4.runtime.misc.*;
import org.antlr.v4.runtime.tree.*;
import java.util.List;
import java.util.Iterator;
import java.util.ArrayList;

@SuppressWarnings({"all", "warnings", "unchecked", "unused", "cast"})
public class crimsonParser extends Parser {
	static { RuntimeMetaData.checkVersion("4.9.2", RuntimeMetaData.VERSION); }

	protected static final DFA[] _decisionToDFA;
	protected static final PredictionContextCache _sharedContextCache =
		new PredictionContextCache();
	public static final int
		Package=1, Integer=2, Boolean=3, OpenBracket=4, CloseBracket=5, OpenBrace=6, 
		CloseBrace=7, Comma=8, SemiColon=9, Identifier=10;
	public static final int
		RULE_program = 0, RULE_packageDefinitionList = 1, RULE_packageDefinition = 2, 
		RULE_packageDependencyList = 3, RULE_packageDependency = 4, RULE_packageBody = 5, 
		RULE_parameterList = 6, RULE_parameter = 7, RULE_parameterType = 8, RULE_statement = 9;
	private static String[] makeRuleNames() {
		return new String[] {
			"program", "packageDefinitionList", "packageDefinition", "packageDependencyList", 
			"packageDependency", "packageBody", "parameterList", "parameter", "parameterType", 
			"statement"
		};
	}
	public static final String[] ruleNames = makeRuleNames();

	private static String[] makeLiteralNames() {
		return new String[] {
			null, "'package'", "'int'", "'bool'", "'('", "')'", "'{'", "'}'", "','", 
			"';'"
		};
	}
	private static final String[] _LITERAL_NAMES = makeLiteralNames();
	private static String[] makeSymbolicNames() {
		return new String[] {
			null, "Package", "Integer", "Boolean", "OpenBracket", "CloseBracket", 
			"OpenBrace", "CloseBrace", "Comma", "SemiColon", "Identifier"
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
	public String getGrammarFileName() { return "crimson.g4"; }

	@Override
	public String[] getRuleNames() { return ruleNames; }

	@Override
	public String getSerializedATN() { return _serializedATN; }

	@Override
	public ATN getATN() { return _ATN; }

	public crimsonParser(TokenStream input) {
		super(input);
		_interp = new ParserATNSimulator(this,_ATN,_decisionToDFA,_sharedContextCache);
	}

	public static class ProgramContext extends ParserRuleContext {
		public PackageDefinitionListContext packageDefinitionList() {
			return getRuleContext(PackageDefinitionListContext.class,0);
		}
		public TerminalNode EOF() { return getToken(crimsonParser.EOF, 0); }
		public ProgramContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_program; }
	}

	public final ProgramContext program() throws RecognitionException {
		ProgramContext _localctx = new ProgramContext(_ctx, getState());
		enterRule(_localctx, 0, RULE_program);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(20);
			packageDefinitionList();
			setState(21);
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

	public static class PackageDefinitionListContext extends ParserRuleContext {
		public List<PackageDefinitionContext> packageDefinition() {
			return getRuleContexts(PackageDefinitionContext.class);
		}
		public PackageDefinitionContext packageDefinition(int i) {
			return getRuleContext(PackageDefinitionContext.class,i);
		}
		public PackageDefinitionListContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_packageDefinitionList; }
	}

	public final PackageDefinitionListContext packageDefinitionList() throws RecognitionException {
		PackageDefinitionListContext _localctx = new PackageDefinitionListContext(_ctx, getState());
		enterRule(_localctx, 2, RULE_packageDefinitionList);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(26);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==Package) {
				{
				{
				setState(23);
				packageDefinition();
				}
				}
				setState(28);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
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

	public static class PackageDefinitionContext extends ParserRuleContext {
		public TerminalNode Package() { return getToken(crimsonParser.Package, 0); }
		public TerminalNode Identifier() { return getToken(crimsonParser.Identifier, 0); }
		public PackageDependencyListContext packageDependencyList() {
			return getRuleContext(PackageDependencyListContext.class,0);
		}
		public PackageBodyContext packageBody() {
			return getRuleContext(PackageBodyContext.class,0);
		}
		public PackageDefinitionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_packageDefinition; }
	}

	public final PackageDefinitionContext packageDefinition() throws RecognitionException {
		PackageDefinitionContext _localctx = new PackageDefinitionContext(_ctx, getState());
		enterRule(_localctx, 4, RULE_packageDefinition);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(29);
			match(Package);
			setState(30);
			match(Identifier);
			setState(31);
			packageDependencyList();
			setState(32);
			packageBody();
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

	public static class PackageDependencyListContext extends ParserRuleContext {
		public TerminalNode OpenBracket() { return getToken(crimsonParser.OpenBracket, 0); }
		public TerminalNode CloseBracket() { return getToken(crimsonParser.CloseBracket, 0); }
		public List<PackageDependencyContext> packageDependency() {
			return getRuleContexts(PackageDependencyContext.class);
		}
		public PackageDependencyContext packageDependency(int i) {
			return getRuleContext(PackageDependencyContext.class,i);
		}
		public List<TerminalNode> Comma() { return getTokens(crimsonParser.Comma); }
		public TerminalNode Comma(int i) {
			return getToken(crimsonParser.Comma, i);
		}
		public PackageDependencyListContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_packageDependencyList; }
	}

	public final PackageDependencyListContext packageDependencyList() throws RecognitionException {
		PackageDependencyListContext _localctx = new PackageDependencyListContext(_ctx, getState());
		enterRule(_localctx, 6, RULE_packageDependencyList);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(34);
			match(OpenBracket);
			setState(36);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==Identifier) {
				{
				setState(35);
				packageDependency();
				}
			}

			setState(42);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==Comma) {
				{
				{
				setState(38);
				match(Comma);
				setState(39);
				packageDependency();
				}
				}
				setState(44);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(45);
			match(CloseBracket);
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

	public static class PackageDependencyContext extends ParserRuleContext {
		public List<TerminalNode> Identifier() { return getTokens(crimsonParser.Identifier); }
		public TerminalNode Identifier(int i) {
			return getToken(crimsonParser.Identifier, i);
		}
		public TerminalNode OpenBracket() { return getToken(crimsonParser.OpenBracket, 0); }
		public TerminalNode CloseBracket() { return getToken(crimsonParser.CloseBracket, 0); }
		public PackageDependencyContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_packageDependency; }
	}

	public final PackageDependencyContext packageDependency() throws RecognitionException {
		PackageDependencyContext _localctx = new PackageDependencyContext(_ctx, getState());
		enterRule(_localctx, 8, RULE_packageDependency);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(47);
			match(Identifier);
			setState(48);
			match(OpenBracket);
			setState(49);
			match(Identifier);
			setState(50);
			match(CloseBracket);
			setState(51);
			match(Identifier);
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

	public static class PackageBodyContext extends ParserRuleContext {
		public TerminalNode OpenBrace() { return getToken(crimsonParser.OpenBrace, 0); }
		public TerminalNode CloseBrace() { return getToken(crimsonParser.CloseBrace, 0); }
		public List<StatementContext> statement() {
			return getRuleContexts(StatementContext.class);
		}
		public StatementContext statement(int i) {
			return getRuleContext(StatementContext.class,i);
		}
		public PackageBodyContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_packageBody; }
	}

	public final PackageBodyContext packageBody() throws RecognitionException {
		PackageBodyContext _localctx = new PackageBodyContext(_ctx, getState());
		enterRule(_localctx, 10, RULE_packageBody);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(53);
			match(OpenBrace);
			setState(57);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==Identifier) {
				{
				{
				setState(54);
				statement();
				}
				}
				setState(59);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(60);
			match(CloseBrace);
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

	public static class ParameterListContext extends ParserRuleContext {
		public TerminalNode OpenBracket() { return getToken(crimsonParser.OpenBracket, 0); }
		public TerminalNode CloseBracket() { return getToken(crimsonParser.CloseBracket, 0); }
		public List<ParameterContext> parameter() {
			return getRuleContexts(ParameterContext.class);
		}
		public ParameterContext parameter(int i) {
			return getRuleContext(ParameterContext.class,i);
		}
		public List<TerminalNode> Comma() { return getTokens(crimsonParser.Comma); }
		public TerminalNode Comma(int i) {
			return getToken(crimsonParser.Comma, i);
		}
		public ParameterListContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_parameterList; }
	}

	public final ParameterListContext parameterList() throws RecognitionException {
		ParameterListContext _localctx = new ParameterListContext(_ctx, getState());
		enterRule(_localctx, 12, RULE_parameterList);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(62);
			match(OpenBracket);
			setState(64);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==Integer || _la==Boolean) {
				{
				setState(63);
				parameter();
				}
			}

			setState(70);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==Comma) {
				{
				{
				setState(66);
				match(Comma);
				setState(67);
				parameter();
				}
				}
				setState(72);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(73);
			match(CloseBracket);
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

	public static class ParameterContext extends ParserRuleContext {
		public ParameterTypeContext parameterType() {
			return getRuleContext(ParameterTypeContext.class,0);
		}
		public TerminalNode Identifier() { return getToken(crimsonParser.Identifier, 0); }
		public ParameterContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_parameter; }
	}

	public final ParameterContext parameter() throws RecognitionException {
		ParameterContext _localctx = new ParameterContext(_ctx, getState());
		enterRule(_localctx, 14, RULE_parameter);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(75);
			parameterType();
			setState(76);
			match(Identifier);
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

	public static class ParameterTypeContext extends ParserRuleContext {
		public TerminalNode Integer() { return getToken(crimsonParser.Integer, 0); }
		public TerminalNode Boolean() { return getToken(crimsonParser.Boolean, 0); }
		public ParameterTypeContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_parameterType; }
	}

	public final ParameterTypeContext parameterType() throws RecognitionException {
		ParameterTypeContext _localctx = new ParameterTypeContext(_ctx, getState());
		enterRule(_localctx, 16, RULE_parameterType);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(78);
			_la = _input.LA(1);
			if ( !(_la==Integer || _la==Boolean) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
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
		public TerminalNode Identifier() { return getToken(crimsonParser.Identifier, 0); }
		public TerminalNode SemiColon() { return getToken(crimsonParser.SemiColon, 0); }
		public StatementContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_statement; }
	}

	public final StatementContext statement() throws RecognitionException {
		StatementContext _localctx = new StatementContext(_ctx, getState());
		enterRule(_localctx, 18, RULE_statement);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(80);
			match(Identifier);
			setState(81);
			match(SemiColon);
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
		"\3\u608b\ua72a\u8133\ub9ed\u417c\u3be7\u7786\u5964\3\fV\4\2\t\2\4\3\t"+
		"\3\4\4\t\4\4\5\t\5\4\6\t\6\4\7\t\7\4\b\t\b\4\t\t\t\4\n\t\n\4\13\t\13\3"+
		"\2\3\2\3\2\3\3\7\3\33\n\3\f\3\16\3\36\13\3\3\4\3\4\3\4\3\4\3\4\3\5\3\5"+
		"\5\5\'\n\5\3\5\3\5\7\5+\n\5\f\5\16\5.\13\5\3\5\3\5\3\6\3\6\3\6\3\6\3\6"+
		"\3\6\3\7\3\7\7\7:\n\7\f\7\16\7=\13\7\3\7\3\7\3\b\3\b\5\bC\n\b\3\b\3\b"+
		"\7\bG\n\b\f\b\16\bJ\13\b\3\b\3\b\3\t\3\t\3\t\3\n\3\n\3\13\3\13\3\13\3"+
		"\13\2\2\f\2\4\6\b\n\f\16\20\22\24\2\3\3\2\4\5\2Q\2\26\3\2\2\2\4\34\3\2"+
		"\2\2\6\37\3\2\2\2\b$\3\2\2\2\n\61\3\2\2\2\f\67\3\2\2\2\16@\3\2\2\2\20"+
		"M\3\2\2\2\22P\3\2\2\2\24R\3\2\2\2\26\27\5\4\3\2\27\30\7\2\2\3\30\3\3\2"+
		"\2\2\31\33\5\6\4\2\32\31\3\2\2\2\33\36\3\2\2\2\34\32\3\2\2\2\34\35\3\2"+
		"\2\2\35\5\3\2\2\2\36\34\3\2\2\2\37 \7\3\2\2 !\7\f\2\2!\"\5\b\5\2\"#\5"+
		"\f\7\2#\7\3\2\2\2$&\7\6\2\2%\'\5\n\6\2&%\3\2\2\2&\'\3\2\2\2\',\3\2\2\2"+
		"()\7\n\2\2)+\5\n\6\2*(\3\2\2\2+.\3\2\2\2,*\3\2\2\2,-\3\2\2\2-/\3\2\2\2"+
		".,\3\2\2\2/\60\7\7\2\2\60\t\3\2\2\2\61\62\7\f\2\2\62\63\7\6\2\2\63\64"+
		"\7\f\2\2\64\65\7\7\2\2\65\66\7\f\2\2\66\13\3\2\2\2\67;\7\b\2\28:\5\24"+
		"\13\298\3\2\2\2:=\3\2\2\2;9\3\2\2\2;<\3\2\2\2<>\3\2\2\2=;\3\2\2\2>?\7"+
		"\t\2\2?\r\3\2\2\2@B\7\6\2\2AC\5\20\t\2BA\3\2\2\2BC\3\2\2\2CH\3\2\2\2D"+
		"E\7\n\2\2EG\5\20\t\2FD\3\2\2\2GJ\3\2\2\2HF\3\2\2\2HI\3\2\2\2IK\3\2\2\2"+
		"JH\3\2\2\2KL\7\7\2\2L\17\3\2\2\2MN\5\22\n\2NO\7\f\2\2O\21\3\2\2\2PQ\t"+
		"\2\2\2Q\23\3\2\2\2RS\7\f\2\2ST\7\13\2\2T\25\3\2\2\2\b\34&,;BH";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}