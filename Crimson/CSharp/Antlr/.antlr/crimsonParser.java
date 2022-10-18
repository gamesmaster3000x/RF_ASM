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
		Package=1, Function=2, Global=3, Return=4, Integer=5, Boolean=6, Equals=7, 
		OpenBracket=8, CloseBracket=9, OpenSquare=10, CloseSquare=11, OpenBrace=12, 
		CloseBrace=13, Comma=14, Dot=15, SemiColon=16, Identifier=17, Value=18;
	public static final int
		RULE_program = 0, RULE_packageDefinitionList = 1, RULE_packageDefinition = 2, 
		RULE_packageDependencyList = 3, RULE_packageDependency = 4, RULE_packageBody = 5, 
		RULE_topLevelStatement = 6, RULE_globalVariableDeclaration = 7, RULE_functionDeclaration = 8, 
		RULE_functionReturnType = 9, RULE_functionBody = 10, RULE_functionOnlyStatement = 11, 
		RULE_internalVariableDeclaration = 12, RULE_functionCall = 13, RULE_inputParameters = 14, 
		RULE_functionReturn = 15, RULE_parameterList = 16, RULE_parameter = 17, 
		RULE_parameterType = 18;
	private static String[] makeRuleNames() {
		return new String[] {
			"program", "packageDefinitionList", "packageDefinition", "packageDependencyList", 
			"packageDependency", "packageBody", "topLevelStatement", "globalVariableDeclaration", 
			"functionDeclaration", "functionReturnType", "functionBody", "functionOnlyStatement", 
			"internalVariableDeclaration", "functionCall", "inputParameters", "functionReturn", 
			"parameterList", "parameter", "parameterType"
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
			setState(38);
			packageDefinitionList();
			setState(39);
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
			setState(44);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==Package) {
				{
				{
				setState(41);
				packageDefinition();
				}
				}
				setState(46);
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
			setState(47);
			match(Package);
			setState(48);
			match(Identifier);
			setState(49);
			packageDependencyList();
			setState(50);
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
			setState(52);
			match(OpenBracket);
			setState(54);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==Identifier) {
				{
				setState(53);
				packageDependency();
				}
			}

			setState(60);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==Comma) {
				{
				{
				setState(56);
				match(Comma);
				setState(57);
				packageDependency();
				}
				}
				setState(62);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(63);
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
			setState(65);
			match(Identifier);
			setState(66);
			match(OpenBracket);
			setState(67);
			match(Identifier);
			setState(68);
			match(CloseBracket);
			setState(69);
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
		public List<TopLevelStatementContext> topLevelStatement() {
			return getRuleContexts(TopLevelStatementContext.class);
		}
		public TopLevelStatementContext topLevelStatement(int i) {
			return getRuleContext(TopLevelStatementContext.class,i);
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
			setState(71);
			match(OpenBrace);
			setState(75);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==Function || _la==Global) {
				{
				{
				setState(72);
				topLevelStatement();
				}
				}
				setState(77);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(78);
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

	public static class TopLevelStatementContext extends ParserRuleContext {
		public GlobalVariableDeclarationContext globalVariableDeclaration() {
			return getRuleContext(GlobalVariableDeclarationContext.class,0);
		}
		public FunctionDeclarationContext functionDeclaration() {
			return getRuleContext(FunctionDeclarationContext.class,0);
		}
		public TopLevelStatementContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_topLevelStatement; }
	}

	public final TopLevelStatementContext topLevelStatement() throws RecognitionException {
		TopLevelStatementContext _localctx = new TopLevelStatementContext(_ctx, getState());
		enterRule(_localctx, 12, RULE_topLevelStatement);
		try {
			setState(82);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case Global:
				enterOuterAlt(_localctx, 1);
				{
				setState(80);
				globalVariableDeclaration();
				}
				break;
			case Function:
				enterOuterAlt(_localctx, 2);
				{
				setState(81);
				functionDeclaration();
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

	public static class GlobalVariableDeclarationContext extends ParserRuleContext {
		public TerminalNode Global() { return getToken(crimsonParser.Global, 0); }
		public ParameterTypeContext parameterType() {
			return getRuleContext(ParameterTypeContext.class,0);
		}
		public TerminalNode Identifier() { return getToken(crimsonParser.Identifier, 0); }
		public TerminalNode SemiColon() { return getToken(crimsonParser.SemiColon, 0); }
		public GlobalVariableDeclarationContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_globalVariableDeclaration; }
	}

	public final GlobalVariableDeclarationContext globalVariableDeclaration() throws RecognitionException {
		GlobalVariableDeclarationContext _localctx = new GlobalVariableDeclarationContext(_ctx, getState());
		enterRule(_localctx, 14, RULE_globalVariableDeclaration);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(84);
			match(Global);
			setState(85);
			parameterType();
			setState(86);
			match(Identifier);
			setState(87);
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

	public static class FunctionDeclarationContext extends ParserRuleContext {
		public TerminalNode Function() { return getToken(crimsonParser.Function, 0); }
		public TerminalNode Identifier() { return getToken(crimsonParser.Identifier, 0); }
		public FunctionReturnTypeContext functionReturnType() {
			return getRuleContext(FunctionReturnTypeContext.class,0);
		}
		public ParameterListContext parameterList() {
			return getRuleContext(ParameterListContext.class,0);
		}
		public FunctionBodyContext functionBody() {
			return getRuleContext(FunctionBodyContext.class,0);
		}
		public FunctionDeclarationContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_functionDeclaration; }
	}

	public final FunctionDeclarationContext functionDeclaration() throws RecognitionException {
		FunctionDeclarationContext _localctx = new FunctionDeclarationContext(_ctx, getState());
		enterRule(_localctx, 16, RULE_functionDeclaration);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(89);
			match(Function);
			setState(90);
			match(Identifier);
			setState(91);
			functionReturnType();
			setState(92);
			parameterList();
			setState(93);
			functionBody();
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

	public static class FunctionReturnTypeContext extends ParserRuleContext {
		public TerminalNode OpenSquare() { return getToken(crimsonParser.OpenSquare, 0); }
		public ParameterTypeContext parameterType() {
			return getRuleContext(ParameterTypeContext.class,0);
		}
		public TerminalNode CloseSquare() { return getToken(crimsonParser.CloseSquare, 0); }
		public FunctionReturnTypeContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_functionReturnType; }
	}

	public final FunctionReturnTypeContext functionReturnType() throws RecognitionException {
		FunctionReturnTypeContext _localctx = new FunctionReturnTypeContext(_ctx, getState());
		enterRule(_localctx, 18, RULE_functionReturnType);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(95);
			match(OpenSquare);
			setState(96);
			parameterType();
			setState(97);
			match(CloseSquare);
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

	public static class FunctionBodyContext extends ParserRuleContext {
		public TerminalNode OpenBrace() { return getToken(crimsonParser.OpenBrace, 0); }
		public TerminalNode CloseBrace() { return getToken(crimsonParser.CloseBrace, 0); }
		public List<FunctionOnlyStatementContext> functionOnlyStatement() {
			return getRuleContexts(FunctionOnlyStatementContext.class);
		}
		public FunctionOnlyStatementContext functionOnlyStatement(int i) {
			return getRuleContext(FunctionOnlyStatementContext.class,i);
		}
		public FunctionBodyContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_functionBody; }
	}

	public final FunctionBodyContext functionBody() throws RecognitionException {
		FunctionBodyContext _localctx = new FunctionBodyContext(_ctx, getState());
		enterRule(_localctx, 20, RULE_functionBody);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(99);
			match(OpenBrace);
			setState(103);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << Return) | (1L << Integer) | (1L << Boolean) | (1L << Identifier))) != 0)) {
				{
				{
				setState(100);
				functionOnlyStatement();
				}
				}
				setState(105);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(106);
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

	public static class FunctionOnlyStatementContext extends ParserRuleContext {
		public InternalVariableDeclarationContext internalVariableDeclaration() {
			return getRuleContext(InternalVariableDeclarationContext.class,0);
		}
		public FunctionReturnContext functionReturn() {
			return getRuleContext(FunctionReturnContext.class,0);
		}
		public FunctionCallContext functionCall() {
			return getRuleContext(FunctionCallContext.class,0);
		}
		public FunctionOnlyStatementContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_functionOnlyStatement; }
	}

	public final FunctionOnlyStatementContext functionOnlyStatement() throws RecognitionException {
		FunctionOnlyStatementContext _localctx = new FunctionOnlyStatementContext(_ctx, getState());
		enterRule(_localctx, 22, RULE_functionOnlyStatement);
		try {
			setState(111);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case Integer:
			case Boolean:
				enterOuterAlt(_localctx, 1);
				{
				setState(108);
				internalVariableDeclaration();
				}
				break;
			case Return:
				enterOuterAlt(_localctx, 2);
				{
				setState(109);
				functionReturn();
				}
				break;
			case Identifier:
				enterOuterAlt(_localctx, 3);
				{
				setState(110);
				functionCall();
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

	public static class InternalVariableDeclarationContext extends ParserRuleContext {
		public ParameterTypeContext parameterType() {
			return getRuleContext(ParameterTypeContext.class,0);
		}
		public TerminalNode Identifier() { return getToken(crimsonParser.Identifier, 0); }
		public TerminalNode Equals() { return getToken(crimsonParser.Equals, 0); }
		public TerminalNode SemiColon() { return getToken(crimsonParser.SemiColon, 0); }
		public FunctionCallContext functionCall() {
			return getRuleContext(FunctionCallContext.class,0);
		}
		public TerminalNode Value() { return getToken(crimsonParser.Value, 0); }
		public InternalVariableDeclarationContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_internalVariableDeclaration; }
	}

	public final InternalVariableDeclarationContext internalVariableDeclaration() throws RecognitionException {
		InternalVariableDeclarationContext _localctx = new InternalVariableDeclarationContext(_ctx, getState());
		enterRule(_localctx, 24, RULE_internalVariableDeclaration);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(113);
			parameterType();
			setState(114);
			match(Identifier);
			setState(115);
			match(Equals);
			setState(118);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case Identifier:
				{
				setState(116);
				functionCall();
				}
				break;
			case Value:
				{
				setState(117);
				match(Value);
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
			setState(120);
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

	public static class FunctionCallContext extends ParserRuleContext {
		public TerminalNode Identifier() { return getToken(crimsonParser.Identifier, 0); }
		public InputParametersContext inputParameters() {
			return getRuleContext(InputParametersContext.class,0);
		}
		public FunctionCallContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_functionCall; }
	}

	public final FunctionCallContext functionCall() throws RecognitionException {
		FunctionCallContext _localctx = new FunctionCallContext(_ctx, getState());
		enterRule(_localctx, 26, RULE_functionCall);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(122);
			match(Identifier);
			setState(123);
			inputParameters();
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

	public static class InputParametersContext extends ParserRuleContext {
		public TerminalNode OpenBracket() { return getToken(crimsonParser.OpenBracket, 0); }
		public TerminalNode CloseBracket() { return getToken(crimsonParser.CloseBracket, 0); }
		public List<TerminalNode> Identifier() { return getTokens(crimsonParser.Identifier); }
		public TerminalNode Identifier(int i) {
			return getToken(crimsonParser.Identifier, i);
		}
		public List<TerminalNode> Comma() { return getTokens(crimsonParser.Comma); }
		public TerminalNode Comma(int i) {
			return getToken(crimsonParser.Comma, i);
		}
		public InputParametersContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_inputParameters; }
	}

	public final InputParametersContext inputParameters() throws RecognitionException {
		InputParametersContext _localctx = new InputParametersContext(_ctx, getState());
		enterRule(_localctx, 28, RULE_inputParameters);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(125);
			match(OpenBracket);
			setState(127);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==Identifier) {
				{
				setState(126);
				match(Identifier);
				}
			}

			setState(133);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==Comma) {
				{
				{
				setState(129);
				match(Comma);
				setState(130);
				match(Identifier);
				}
				}
				setState(135);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(136);
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

	public static class FunctionReturnContext extends ParserRuleContext {
		public TerminalNode Return() { return getToken(crimsonParser.Return, 0); }
		public TerminalNode Value() { return getToken(crimsonParser.Value, 0); }
		public TerminalNode SemiColon() { return getToken(crimsonParser.SemiColon, 0); }
		public FunctionReturnContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_functionReturn; }
	}

	public final FunctionReturnContext functionReturn() throws RecognitionException {
		FunctionReturnContext _localctx = new FunctionReturnContext(_ctx, getState());
		enterRule(_localctx, 30, RULE_functionReturn);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(138);
			match(Return);
			setState(139);
			match(Value);
			setState(140);
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
		enterRule(_localctx, 32, RULE_parameterList);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(142);
			match(OpenBracket);
			setState(144);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==Integer || _la==Boolean) {
				{
				setState(143);
				parameter();
				}
			}

			setState(150);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==Comma) {
				{
				{
				setState(146);
				match(Comma);
				setState(147);
				parameter();
				}
				}
				setState(152);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(153);
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
		enterRule(_localctx, 34, RULE_parameter);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(155);
			parameterType();
			setState(156);
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
		enterRule(_localctx, 36, RULE_parameterType);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(158);
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

	public static final String _serializedATN =
		"\3\u608b\ua72a\u8133\ub9ed\u417c\u3be7\u7786\u5964\3\24\u00a3\4\2\t\2"+
		"\4\3\t\3\4\4\t\4\4\5\t\5\4\6\t\6\4\7\t\7\4\b\t\b\4\t\t\t\4\n\t\n\4\13"+
		"\t\13\4\f\t\f\4\r\t\r\4\16\t\16\4\17\t\17\4\20\t\20\4\21\t\21\4\22\t\22"+
		"\4\23\t\23\4\24\t\24\3\2\3\2\3\2\3\3\7\3-\n\3\f\3\16\3\60\13\3\3\4\3\4"+
		"\3\4\3\4\3\4\3\5\3\5\5\59\n\5\3\5\3\5\7\5=\n\5\f\5\16\5@\13\5\3\5\3\5"+
		"\3\6\3\6\3\6\3\6\3\6\3\6\3\7\3\7\7\7L\n\7\f\7\16\7O\13\7\3\7\3\7\3\b\3"+
		"\b\5\bU\n\b\3\t\3\t\3\t\3\t\3\t\3\n\3\n\3\n\3\n\3\n\3\n\3\13\3\13\3\13"+
		"\3\13\3\f\3\f\7\fh\n\f\f\f\16\fk\13\f\3\f\3\f\3\r\3\r\3\r\5\rr\n\r\3\16"+
		"\3\16\3\16\3\16\3\16\5\16y\n\16\3\16\3\16\3\17\3\17\3\17\3\20\3\20\5\20"+
		"\u0082\n\20\3\20\3\20\7\20\u0086\n\20\f\20\16\20\u0089\13\20\3\20\3\20"+
		"\3\21\3\21\3\21\3\21\3\22\3\22\5\22\u0093\n\22\3\22\3\22\7\22\u0097\n"+
		"\22\f\22\16\22\u009a\13\22\3\22\3\22\3\23\3\23\3\23\3\24\3\24\3\24\2\2"+
		"\25\2\4\6\b\n\f\16\20\22\24\26\30\32\34\36 \"$&\2\3\3\2\7\b\2\u009c\2"+
		"(\3\2\2\2\4.\3\2\2\2\6\61\3\2\2\2\b\66\3\2\2\2\nC\3\2\2\2\fI\3\2\2\2\16"+
		"T\3\2\2\2\20V\3\2\2\2\22[\3\2\2\2\24a\3\2\2\2\26e\3\2\2\2\30q\3\2\2\2"+
		"\32s\3\2\2\2\34|\3\2\2\2\36\177\3\2\2\2 \u008c\3\2\2\2\"\u0090\3\2\2\2"+
		"$\u009d\3\2\2\2&\u00a0\3\2\2\2()\5\4\3\2)*\7\2\2\3*\3\3\2\2\2+-\5\6\4"+
		"\2,+\3\2\2\2-\60\3\2\2\2.,\3\2\2\2./\3\2\2\2/\5\3\2\2\2\60.\3\2\2\2\61"+
		"\62\7\3\2\2\62\63\7\23\2\2\63\64\5\b\5\2\64\65\5\f\7\2\65\7\3\2\2\2\66"+
		"8\7\n\2\2\679\5\n\6\28\67\3\2\2\289\3\2\2\29>\3\2\2\2:;\7\20\2\2;=\5\n"+
		"\6\2<:\3\2\2\2=@\3\2\2\2><\3\2\2\2>?\3\2\2\2?A\3\2\2\2@>\3\2\2\2AB\7\13"+
		"\2\2B\t\3\2\2\2CD\7\23\2\2DE\7\n\2\2EF\7\23\2\2FG\7\13\2\2GH\7\23\2\2"+
		"H\13\3\2\2\2IM\7\16\2\2JL\5\16\b\2KJ\3\2\2\2LO\3\2\2\2MK\3\2\2\2MN\3\2"+
		"\2\2NP\3\2\2\2OM\3\2\2\2PQ\7\17\2\2Q\r\3\2\2\2RU\5\20\t\2SU\5\22\n\2T"+
		"R\3\2\2\2TS\3\2\2\2U\17\3\2\2\2VW\7\5\2\2WX\5&\24\2XY\7\23\2\2YZ\7\22"+
		"\2\2Z\21\3\2\2\2[\\\7\4\2\2\\]\7\23\2\2]^\5\24\13\2^_\5\"\22\2_`\5\26"+
		"\f\2`\23\3\2\2\2ab\7\f\2\2bc\5&\24\2cd\7\r\2\2d\25\3\2\2\2ei\7\16\2\2"+
		"fh\5\30\r\2gf\3\2\2\2hk\3\2\2\2ig\3\2\2\2ij\3\2\2\2jl\3\2\2\2ki\3\2\2"+
		"\2lm\7\17\2\2m\27\3\2\2\2nr\5\32\16\2or\5 \21\2pr\5\34\17\2qn\3\2\2\2"+
		"qo\3\2\2\2qp\3\2\2\2r\31\3\2\2\2st\5&\24\2tu\7\23\2\2ux\7\t\2\2vy\5\34"+
		"\17\2wy\7\24\2\2xv\3\2\2\2xw\3\2\2\2yz\3\2\2\2z{\7\22\2\2{\33\3\2\2\2"+
		"|}\7\23\2\2}~\5\36\20\2~\35\3\2\2\2\177\u0081\7\n\2\2\u0080\u0082\7\23"+
		"\2\2\u0081\u0080\3\2\2\2\u0081\u0082\3\2\2\2\u0082\u0087\3\2\2\2\u0083"+
		"\u0084\7\20\2\2\u0084\u0086\7\23\2\2\u0085\u0083\3\2\2\2\u0086\u0089\3"+
		"\2\2\2\u0087\u0085\3\2\2\2\u0087\u0088\3\2\2\2\u0088\u008a\3\2\2\2\u0089"+
		"\u0087\3\2\2\2\u008a\u008b\7\13\2\2\u008b\37\3\2\2\2\u008c\u008d\7\6\2"+
		"\2\u008d\u008e\7\24\2\2\u008e\u008f\7\22\2\2\u008f!\3\2\2\2\u0090\u0092"+
		"\7\n\2\2\u0091\u0093\5$\23\2\u0092\u0091\3\2\2\2\u0092\u0093\3\2\2\2\u0093"+
		"\u0098\3\2\2\2\u0094\u0095\7\20\2\2\u0095\u0097\5$\23\2\u0096\u0094\3"+
		"\2\2\2\u0097\u009a\3\2\2\2\u0098\u0096\3\2\2\2\u0098\u0099\3\2\2\2\u0099"+
		"\u009b\3\2\2\2\u009a\u0098\3\2\2\2\u009b\u009c\7\13\2\2\u009c#\3\2\2\2"+
		"\u009d\u009e\5&\24\2\u009e\u009f\7\23\2\2\u009f%\3\2\2\2\u00a0\u00a1\t"+
		"\2\2\2\u00a1\'\3\2\2\2\16.8>MTiqx\u0081\u0087\u0092\u0098";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}