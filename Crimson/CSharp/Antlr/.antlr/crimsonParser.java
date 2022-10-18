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
		RULE_internalVariableDeclaration = 12, RULE_assignVariable = 13, RULE_functionCall = 14, 
		RULE_inputParameters = 15, RULE_functionReturn = 16, RULE_resolvableValue = 17, 
		RULE_parameterList = 18, RULE_parameter = 19, RULE_parameterType = 20;
	private static String[] makeRuleNames() {
		return new String[] {
			"program", "packageDefinitionList", "packageDefinition", "packageDependencyList", 
			"packageDependency", "packageBody", "topLevelStatement", "globalVariableDeclaration", 
			"functionDeclaration", "functionReturnType", "functionBody", "functionOnlyStatement", 
			"internalVariableDeclaration", "assignVariable", "functionCall", "inputParameters", 
			"functionReturn", "resolvableValue", "parameterList", "parameter", "parameterType"
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
			setState(42);
			packageDefinitionList();
			setState(43);
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
			setState(48);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==Package) {
				{
				{
				setState(45);
				packageDefinition();
				}
				}
				setState(50);
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
			setState(51);
			match(Package);
			setState(52);
			match(Identifier);
			setState(53);
			packageDependencyList();
			setState(54);
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
			setState(56);
			match(OpenBracket);
			setState(58);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==Identifier) {
				{
				setState(57);
				packageDependency();
				}
			}

			setState(64);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==Comma) {
				{
				{
				setState(60);
				match(Comma);
				setState(61);
				packageDependency();
				}
				}
				setState(66);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(67);
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
			setState(69);
			match(Identifier);
			setState(70);
			match(OpenBracket);
			setState(71);
			match(Identifier);
			setState(72);
			match(CloseBracket);
			setState(73);
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
			setState(75);
			match(OpenBrace);
			setState(79);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==Function || _la==Global) {
				{
				{
				setState(76);
				topLevelStatement();
				}
				}
				setState(81);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(82);
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
			setState(86);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case Global:
				enterOuterAlt(_localctx, 1);
				{
				setState(84);
				globalVariableDeclaration();
				}
				break;
			case Function:
				enterOuterAlt(_localctx, 2);
				{
				setState(85);
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
		public InternalVariableDeclarationContext internalVariableDeclaration() {
			return getRuleContext(InternalVariableDeclarationContext.class,0);
		}
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
			setState(88);
			match(Global);
			setState(89);
			internalVariableDeclaration();
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
			setState(91);
			match(Function);
			setState(92);
			match(Identifier);
			setState(93);
			functionReturnType();
			setState(94);
			parameterList();
			setState(95);
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
			setState(97);
			match(OpenSquare);
			setState(98);
			parameterType();
			setState(99);
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
			setState(101);
			match(OpenBrace);
			setState(105);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << Return) | (1L << Integer) | (1L << Boolean) | (1L << Identifier))) != 0)) {
				{
				{
				setState(102);
				functionOnlyStatement();
				}
				}
				setState(107);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(108);
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
		public TerminalNode SemiColon() { return getToken(crimsonParser.SemiColon, 0); }
		public FunctionOnlyStatementContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_functionOnlyStatement; }
	}

	public final FunctionOnlyStatementContext functionOnlyStatement() throws RecognitionException {
		FunctionOnlyStatementContext _localctx = new FunctionOnlyStatementContext(_ctx, getState());
		enterRule(_localctx, 22, RULE_functionOnlyStatement);
		try {
			setState(115);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case Integer:
			case Boolean:
				enterOuterAlt(_localctx, 1);
				{
				setState(110);
				internalVariableDeclaration();
				}
				break;
			case Return:
				enterOuterAlt(_localctx, 2);
				{
				setState(111);
				functionReturn();
				}
				break;
			case Identifier:
				enterOuterAlt(_localctx, 3);
				{
				setState(112);
				functionCall();
				setState(113);
				match(SemiColon);
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
		public TerminalNode SemiColon() { return getToken(crimsonParser.SemiColon, 0); }
		public TerminalNode Equals() { return getToken(crimsonParser.Equals, 0); }
		public ResolvableValueContext resolvableValue() {
			return getRuleContext(ResolvableValueContext.class,0);
		}
		public InternalVariableDeclarationContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_internalVariableDeclaration; }
	}

	public final InternalVariableDeclarationContext internalVariableDeclaration() throws RecognitionException {
		InternalVariableDeclarationContext _localctx = new InternalVariableDeclarationContext(_ctx, getState());
		enterRule(_localctx, 24, RULE_internalVariableDeclaration);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(117);
			parameterType();
			setState(118);
			match(Identifier);
			setState(121);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==Equals) {
				{
				setState(119);
				match(Equals);
				setState(120);
				resolvableValue();
				}
			}

			setState(123);
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

	public static class AssignVariableContext extends ParserRuleContext {
		public TerminalNode Identifier() { return getToken(crimsonParser.Identifier, 0); }
		public TerminalNode Equals() { return getToken(crimsonParser.Equals, 0); }
		public ResolvableValueContext resolvableValue() {
			return getRuleContext(ResolvableValueContext.class,0);
		}
		public TerminalNode SemiColon() { return getToken(crimsonParser.SemiColon, 0); }
		public AssignVariableContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_assignVariable; }
	}

	public final AssignVariableContext assignVariable() throws RecognitionException {
		AssignVariableContext _localctx = new AssignVariableContext(_ctx, getState());
		enterRule(_localctx, 26, RULE_assignVariable);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(125);
			match(Identifier);
			setState(126);
			match(Equals);
			setState(127);
			resolvableValue();
			setState(128);
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
		enterRule(_localctx, 28, RULE_functionCall);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(130);
			match(Identifier);
			setState(131);
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
		enterRule(_localctx, 30, RULE_inputParameters);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(133);
			match(OpenBracket);
			setState(135);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==Identifier) {
				{
				setState(134);
				match(Identifier);
				}
			}

			setState(141);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==Comma) {
				{
				{
				setState(137);
				match(Comma);
				setState(138);
				match(Identifier);
				}
				}
				setState(143);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(144);
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
		public ResolvableValueContext resolvableValue() {
			return getRuleContext(ResolvableValueContext.class,0);
		}
		public TerminalNode SemiColon() { return getToken(crimsonParser.SemiColon, 0); }
		public FunctionReturnContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_functionReturn; }
	}

	public final FunctionReturnContext functionReturn() throws RecognitionException {
		FunctionReturnContext _localctx = new FunctionReturnContext(_ctx, getState());
		enterRule(_localctx, 32, RULE_functionReturn);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(146);
			match(Return);
			setState(147);
			resolvableValue();
			setState(148);
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

	public static class ResolvableValueContext extends ParserRuleContext {
		public TerminalNode Value() { return getToken(crimsonParser.Value, 0); }
		public FunctionCallContext functionCall() {
			return getRuleContext(FunctionCallContext.class,0);
		}
		public ResolvableValueContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_resolvableValue; }
	}

	public final ResolvableValueContext resolvableValue() throws RecognitionException {
		ResolvableValueContext _localctx = new ResolvableValueContext(_ctx, getState());
		enterRule(_localctx, 34, RULE_resolvableValue);
		try {
			setState(152);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case Value:
				enterOuterAlt(_localctx, 1);
				{
				setState(150);
				match(Value);
				}
				break;
			case Identifier:
				enterOuterAlt(_localctx, 2);
				{
				setState(151);
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
		enterRule(_localctx, 36, RULE_parameterList);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(154);
			match(OpenBracket);
			setState(156);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==Integer || _la==Boolean) {
				{
				setState(155);
				parameter();
				}
			}

			setState(162);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==Comma) {
				{
				{
				setState(158);
				match(Comma);
				setState(159);
				parameter();
				}
				}
				setState(164);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(165);
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
		enterRule(_localctx, 38, RULE_parameter);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(167);
			parameterType();
			setState(168);
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
		enterRule(_localctx, 40, RULE_parameterType);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(170);
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
		"\3\u608b\ua72a\u8133\ub9ed\u417c\u3be7\u7786\u5964\3\24\u00af\4\2\t\2"+
		"\4\3\t\3\4\4\t\4\4\5\t\5\4\6\t\6\4\7\t\7\4\b\t\b\4\t\t\t\4\n\t\n\4\13"+
		"\t\13\4\f\t\f\4\r\t\r\4\16\t\16\4\17\t\17\4\20\t\20\4\21\t\21\4\22\t\22"+
		"\4\23\t\23\4\24\t\24\4\25\t\25\4\26\t\26\3\2\3\2\3\2\3\3\7\3\61\n\3\f"+
		"\3\16\3\64\13\3\3\4\3\4\3\4\3\4\3\4\3\5\3\5\5\5=\n\5\3\5\3\5\7\5A\n\5"+
		"\f\5\16\5D\13\5\3\5\3\5\3\6\3\6\3\6\3\6\3\6\3\6\3\7\3\7\7\7P\n\7\f\7\16"+
		"\7S\13\7\3\7\3\7\3\b\3\b\5\bY\n\b\3\t\3\t\3\t\3\n\3\n\3\n\3\n\3\n\3\n"+
		"\3\13\3\13\3\13\3\13\3\f\3\f\7\fj\n\f\f\f\16\fm\13\f\3\f\3\f\3\r\3\r\3"+
		"\r\3\r\3\r\5\rv\n\r\3\16\3\16\3\16\3\16\5\16|\n\16\3\16\3\16\3\17\3\17"+
		"\3\17\3\17\3\17\3\20\3\20\3\20\3\21\3\21\5\21\u008a\n\21\3\21\3\21\7\21"+
		"\u008e\n\21\f\21\16\21\u0091\13\21\3\21\3\21\3\22\3\22\3\22\3\22\3\23"+
		"\3\23\5\23\u009b\n\23\3\24\3\24\5\24\u009f\n\24\3\24\3\24\7\24\u00a3\n"+
		"\24\f\24\16\24\u00a6\13\24\3\24\3\24\3\25\3\25\3\25\3\26\3\26\3\26\2\2"+
		"\27\2\4\6\b\n\f\16\20\22\24\26\30\32\34\36 \"$&(*\2\3\3\2\7\b\2\u00a7"+
		"\2,\3\2\2\2\4\62\3\2\2\2\6\65\3\2\2\2\b:\3\2\2\2\nG\3\2\2\2\fM\3\2\2\2"+
		"\16X\3\2\2\2\20Z\3\2\2\2\22]\3\2\2\2\24c\3\2\2\2\26g\3\2\2\2\30u\3\2\2"+
		"\2\32w\3\2\2\2\34\177\3\2\2\2\36\u0084\3\2\2\2 \u0087\3\2\2\2\"\u0094"+
		"\3\2\2\2$\u009a\3\2\2\2&\u009c\3\2\2\2(\u00a9\3\2\2\2*\u00ac\3\2\2\2,"+
		"-\5\4\3\2-.\7\2\2\3.\3\3\2\2\2/\61\5\6\4\2\60/\3\2\2\2\61\64\3\2\2\2\62"+
		"\60\3\2\2\2\62\63\3\2\2\2\63\5\3\2\2\2\64\62\3\2\2\2\65\66\7\3\2\2\66"+
		"\67\7\23\2\2\678\5\b\5\289\5\f\7\29\7\3\2\2\2:<\7\n\2\2;=\5\n\6\2<;\3"+
		"\2\2\2<=\3\2\2\2=B\3\2\2\2>?\7\20\2\2?A\5\n\6\2@>\3\2\2\2AD\3\2\2\2B@"+
		"\3\2\2\2BC\3\2\2\2CE\3\2\2\2DB\3\2\2\2EF\7\13\2\2F\t\3\2\2\2GH\7\23\2"+
		"\2HI\7\n\2\2IJ\7\23\2\2JK\7\13\2\2KL\7\23\2\2L\13\3\2\2\2MQ\7\16\2\2N"+
		"P\5\16\b\2ON\3\2\2\2PS\3\2\2\2QO\3\2\2\2QR\3\2\2\2RT\3\2\2\2SQ\3\2\2\2"+
		"TU\7\17\2\2U\r\3\2\2\2VY\5\20\t\2WY\5\22\n\2XV\3\2\2\2XW\3\2\2\2Y\17\3"+
		"\2\2\2Z[\7\5\2\2[\\\5\32\16\2\\\21\3\2\2\2]^\7\4\2\2^_\7\23\2\2_`\5\24"+
		"\13\2`a\5&\24\2ab\5\26\f\2b\23\3\2\2\2cd\7\f\2\2de\5*\26\2ef\7\r\2\2f"+
		"\25\3\2\2\2gk\7\16\2\2hj\5\30\r\2ih\3\2\2\2jm\3\2\2\2ki\3\2\2\2kl\3\2"+
		"\2\2ln\3\2\2\2mk\3\2\2\2no\7\17\2\2o\27\3\2\2\2pv\5\32\16\2qv\5\"\22\2"+
		"rs\5\36\20\2st\7\22\2\2tv\3\2\2\2up\3\2\2\2uq\3\2\2\2ur\3\2\2\2v\31\3"+
		"\2\2\2wx\5*\26\2x{\7\23\2\2yz\7\t\2\2z|\5$\23\2{y\3\2\2\2{|\3\2\2\2|}"+
		"\3\2\2\2}~\7\22\2\2~\33\3\2\2\2\177\u0080\7\23\2\2\u0080\u0081\7\t\2\2"+
		"\u0081\u0082\5$\23\2\u0082\u0083\7\22\2\2\u0083\35\3\2\2\2\u0084\u0085"+
		"\7\23\2\2\u0085\u0086\5 \21\2\u0086\37\3\2\2\2\u0087\u0089\7\n\2\2\u0088"+
		"\u008a\7\23\2\2\u0089\u0088\3\2\2\2\u0089\u008a\3\2\2\2\u008a\u008f\3"+
		"\2\2\2\u008b\u008c\7\20\2\2\u008c\u008e\7\23\2\2\u008d\u008b\3\2\2\2\u008e"+
		"\u0091\3\2\2\2\u008f\u008d\3\2\2\2\u008f\u0090\3\2\2\2\u0090\u0092\3\2"+
		"\2\2\u0091\u008f\3\2\2\2\u0092\u0093\7\13\2\2\u0093!\3\2\2\2\u0094\u0095"+
		"\7\6\2\2\u0095\u0096\5$\23\2\u0096\u0097\7\22\2\2\u0097#\3\2\2\2\u0098"+
		"\u009b\7\24\2\2\u0099\u009b\5\36\20\2\u009a\u0098\3\2\2\2\u009a\u0099"+
		"\3\2\2\2\u009b%\3\2\2\2\u009c\u009e\7\n\2\2\u009d\u009f\5(\25\2\u009e"+
		"\u009d\3\2\2\2\u009e\u009f\3\2\2\2\u009f\u00a4\3\2\2\2\u00a0\u00a1\7\20"+
		"\2\2\u00a1\u00a3\5(\25\2\u00a2\u00a0\3\2\2\2\u00a3\u00a6\3\2\2\2\u00a4"+
		"\u00a2\3\2\2\2\u00a4\u00a5\3\2\2\2\u00a5\u00a7\3\2\2\2\u00a6\u00a4\3\2"+
		"\2\2\u00a7\u00a8\7\13\2\2\u00a8\'\3\2\2\2\u00a9\u00aa\5*\26\2\u00aa\u00ab"+
		"\7\23\2\2\u00ab)\3\2\2\2\u00ac\u00ad\t\2\2\2\u00ad+\3\2\2\2\17\62<BQX"+
		"ku{\u0089\u008f\u009a\u009e\u00a4";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}